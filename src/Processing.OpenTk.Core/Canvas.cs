using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Processing.OpenTk.Core.Rendering;
using Processing.OpenTk.Core.Math;
using Processing.OpenTk.Core.Textures;
using System.Linq;
using Processing.OpenTk.Core.Fonts;
using System.Collections.Generic;
using Processing.OpenTk.Core.Extensions;

namespace Processing.OpenTk.Core
{
    public class Canvas : GameWindow, ICanvas
    {
        #region ICanvas

        private Stack<Style> _styleStack = new Stack<Style>();
        private (PVector offset, PVector size) _baseViewport = (PVector.O, PVector.O);
        private Stack<(PVector offset, PVector size)> _boundryStack = new Stack<(PVector, PVector)>();

        public Font Font { get => _styleStack.Peek().Font; set => _styleStack.Peek().Font = value; }        
        public Color4 Fill { get => _styleStack.Peek().Fill; set => _styleStack.Peek().Fill = value; }
        public Color4 Stroke { get => _styleStack.Peek().Stroke; set => _styleStack.Peek().Stroke = value; }
        public float StrokeWeight { get => _styleStack.Peek().StrokeWeight; set => _styleStack.Peek().StrokeWeight = value; }


        public ulong FrameCount { get; set; } = 0;

        public event Action<Canvas> Setup;

        public event Action<Canvas> Draw;

        public PVector MousePosition { get; private set; }

        public void PushStyle()
        {
            _styleStack.Push(_styleStack.Peek().Copy());
        }

        public void PushStyle(Style style)
        {
            _styleStack.Push(style);
        }

        public Style PopStyle()
        {
            return _styleStack.Pop();
        }

        public void WithStyle(Style style, Action action)
        {
            PushStyle(style);
            action();
            PopStyle();
        }
        
        public void WithStyle(Action action)
        {
            PushStyle();
            action();
            PopStyle();
        }

        public void PushBoundry(PVector offset, PVector size)
        {
            _boundryStack.Push((offset, size));
            ApplyBoundry();
        }

        public (PVector offset, PVector size) PopBoundry()
        {
            var result = _boundryStack.Pop();
            ApplyBoundry();            
            return result;
        }

        public void WithBoundry(PVector offset, PVector size, Action action)
        {
            PushBoundry(offset, size);
            action();
            PopBoundry();
        }

        private void ApplyBoundry()
        {
            if (_boundryStack.Count == 0)
            {
                GL.Disable(EnableCap.ScissorTest);
            }
            else
            {
                GL.Enable(EnableCap.ScissorTest);
                var offset = _boundryStack.Select(b => b.offset).Aggregate(PVector.O, (acc, off) => acc + off);
                var size = _boundryStack.Peek().size;
                foreach (var boundry in _boundryStack.Reverse())
                    GL.Scissor((int)offset.X, Height - (int)offset.Y - (int)size.Y, (int)size.X, (int)size.Y);
            }
            
        }

        #endregion

        #region InputEvents

        public PVector PMousePosition { get; private set; }
        protected virtual void OnMouseMoved() { }

        protected virtual void OnMousePressed() { }

        protected virtual void OnMouseReleased() { }

        protected virtual void OnMouseScroll() { }

        protected virtual void OnKeyPressed() { }

        protected virtual void OnKeyReleased() { }
        #endregion

        public Canvas(int sizex, int sizey) : base(sizex, sizey, GraphicsMode.Default, "")
        {
            VSync = VSyncMode.On;
        }

        public Canvas(int sizex, int sizey, Action<Canvas> setup) : this(sizex, sizey) => setup(this);        

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);            

            GL.ClearColor(Color.CornflowerBlue);            
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.MatrixMode(MatrixMode.Projection);

            _styleStack.Push(new Style());

            icons = new IconCache();

            Mouse.ButtonDown += (sender, args) => OnMousePressed();
            Mouse.ButtonUp += (sender, args) => OnMouseReleased();
            Mouse.WheelChanged += (sender, args) => OnMouseScroll();
            Mouse.Move += (sender, args) => OnMouseMoved();

            Keyboard.KeyDown += (sender, args) => OnKeyPressed();
            Keyboard.KeyUp += (sender, args) => OnKeyReleased();
            
            Setup?.Invoke(this);

            PostOnLoad?.Invoke();
        }

        public Action PostOnLoad { get; set; }        

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            PostOnResize?.Invoke();
        }

        public Action PostOnResize { get; set; }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            MousePosition = (Mouse.X, Mouse.Y);

            if (Keyboard[Key.Escape])
                Exit();

            PostOnUpdateFrame?.Invoke(e);
        }

        public Action<FrameEventArgs> PostOnUpdateFrame { get; set; }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            FrameCount++;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Draw?.Invoke(this);

            SwapBuffers();

            PMousePosition = MousePosition;
        }

        public Action<FrameEventArgs> PostOnRenderFrame { get; set; }

        #region Renderer 2d        

        public void Background(Color4 color)
        {
            GL.ClearColor(color);
        }

        public void Triangle(PVector a, PVector b, PVector c)
        {
            Shape((0, 0), a, b, c);
        }

        public void Rectangle(PVector position, PVector size)
        {
            Quad(position,
                 position + (size.X, 0),
                 position + size,
                 position + (0, size.Y));
        }

        public void Quad(PVector a, PVector b, PVector c, PVector d)
        {
            Shape((0, 0), a, b, c, d);            
        }

        public void Ellipse(PVector position, PVector size)
        {
            throw new NotImplementedException();
        }

        public void Line(PVector a, PVector b)
        {
            if (StrokeWeight <= 0)
                return;

            WithOrtho(() =>
            {
                GL.LineWidth(StrokeWeight);
                GL.Color4(Stroke);
                WithPrimitive(PrimitiveType.Lines, () =>
                {
                    GL.Vertex2(a);
                    GL.Vertex2(b);
                });
            });
        }

        public void Arc(PVector position, PVector size, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void Image(PImage image, PVector position)
        {
            WithOrtho(() =>
            {
                GL.Color4(Color4.White);
                GL.Translate(position.ToVector3());
                GL.Disable(EnableCap.Lighting);
                GL.Enable(EnableCap.Texture2D);                

                WithTexture(image, () =>
                {
                    WithPrimitive(PrimitiveType.Quads, () =>
                    {
                        GL.TexCoord2(1f, 1f);
                        GL.Vertex2(image.Width, image.Height);
                        GL.TexCoord2(0f, 1f);
                        GL.Vertex2(0, image.Height);
                        GL.TexCoord2(0f, 0f);
                        GL.Vertex2(0, 0);
                        GL.TexCoord2(1.0f, 0.0f);
                        GL.Vertex2(image.Width, 0);
                    });
                });
            });
        }

        public TextRenderResult Text(string text, PVector position) => new TextRenderResult(RenderText(text, position));

        private IEnumerable<CharacterRenderResult> RenderText(string text, PVector position)
        {
            var characters = text.Select(c => (c, Font[c, Fill]))
                                 .Select(t => (letter: t.Item1, texture: t.Item2.texture, yshift: t.Item2.yshift));

            foreach (var character in characters)
            {
                Image(character.texture, position + (0, -character.texture.Height + Font.MaxHeight + character.yshift));
                position += (character.texture.Width, 0);
                yield return new CharacterRenderResult
                {
                    Height = character.texture.Height + character.yshift,
                    Width = character.texture.Width,
                    Text = character.letter.ToString()
                };
            }
        }

        private static IconCache icons;

        public void Icon(FontAwesomeIcons icon, float size, PVector position)
        {
            Image(icons[icon, size, Fill], position);
        }

        public void Shape(PVector position, params PVector[] points)
        {            
            var scaled = points.Indecies(i => {
                    var vectors = points[i].GetOuterFaceVector(points.Previous(i), points.Next(i));
                    return (points[i] + vectors.pv * StrokeWeight, points[i] + vectors.ov * StrokeWeight, points[i] + vectors.nv * StrokeWeight);
                });
                
            WithOrtho(() =>
            {
                GL.Translate(position.ToVector3());
                GL.Color4(Stroke);
                WithPrimitive(PrimitiveType.Polygon, () =>
                {
                    foreach (var vertexSet in scaled)
                    {
                        GL.Vertex2(vertexSet.Item3);
                        GL.Vertex2(vertexSet.Item2);
                        GL.Vertex2(vertexSet.Item1);
                    }
                });

                GL.Color4(Fill);
                WithPrimitive(PrimitiveType.Polygon, () =>
                {
                    foreach (var vertex in points)
                        GL.Vertex2(vertex);
                });
            });
        }

        #endregion

        private void WithPrimitive(PrimitiveType type, Action action)
        {
            GL.Begin(type);
            action();
            GL.End();
        }

        private void WithOrtho(Action action)
        {
            GL.PushMatrix();
            {
                GL.LoadIdentity();
                GL.Ortho(0, Width, Height, 0, -1, 1);

                if (_boundryStack.Count > 0)
                {
                    var offset = _boundryStack.Select(b => b.offset).Aggregate(PVector.O, (acc, off) => acc + off);
                    GL.Translate(offset.ToVector3());
                }
                
                action();
            }
            GL.PopMatrix();
        }

        private void WithTexture(PImage image, Action action)
        {
            GL.BindTexture(TextureTarget.Texture2D, image);
            {
                action();
            }
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }        
    }
}
