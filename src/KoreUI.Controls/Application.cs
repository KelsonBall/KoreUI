using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using Processing.OpenTk.Core;
using System;
using System.Linq;

namespace KoreUI.Controls
{    
    public class Application : UiControl, IDisposable
    {
        public Canvas Canvas { get; }

        public Application()
        {
            Canvas = new Canvas(800, 640, canvas =>
            {
                canvas.PostOnLoad += this.PostOnLoad;
                canvas.PostOnResize += this.PostOnResize;
                canvas.PostOnUpdateFrame += this.PostOnUpdateFrame;
                canvas.MouseDown += this.OnMousePressed;
            });

            Canvas.Draw += _ => Draw(Canvas);
        }

        public Application(Action<Application> setup) : this() => setup(this);

        public UiControl FocusedControl { get; set; }

        protected void PostOnLoad()
        {
            Coordinates.SizeOffsetX = Canvas.Width;
            Coordinates.SizeOffsetY = Canvas.Height;

            Canvas.StrokeWeight = 0;
            Canvas.Stroke = Color4.Transparent;
            Canvas.Fill = Color4.White;
        }

        protected void PostOnResize()
        {
            Coordinates.SizeOffsetX = Canvas.Width;
            Coordinates.SizeOffsetY = Canvas.Height;
        }

        protected void PostOnUpdateFrame(FrameEventArgs e)
        {
            IsMouseOver = Encloses(Canvas.MousePosition);
            foreach (var control in Descendents)
                control.IsMouseOver = control.Encloses(Canvas.MousePosition);
        }

        private void OnMousePressed(object sender, MouseButtonEventArgs mouseEvent)
        {
            foreach (var clicked in Descendents.Where(d => d.IsMouseOver))
                clicked.MousePressed?.Invoke(clicked, new InputEventArgs());
        }

        public void Dispose()
        {
            this.Canvas.Dispose();
        }

        public void Show()
        {
            Canvas.Run(60f);
        }
    }
}
