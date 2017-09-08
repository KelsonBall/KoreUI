using OpenTK;
using OpenTK.Graphics;
using Processing.OpenTk.Core;
using System.Linq;

namespace KoreUI.Controls
{    
    public class Application : Canvas
    {        

        public Application() : base(640, 400)
        {
            Draw += _ => Root.Draw(this);            
        }

        public UiControl Root = new UiControl(c => c.Name = "Root");

        public UiControl FocusedControl { get; set; }

        protected override void PostOnLoad()
        {
            Root.Coordinates.SizeOffsetX = Width;
            Root.Coordinates.SizeOffsetY = Height;

            StrokeWeight = 0;
            Stroke = Color4.Transparent;
            Fill = Color4.White;
        }

        protected override void PostOnResize()
        {
            Root.Coordinates.SizeOffsetX = Width;
            Root.Coordinates.SizeOffsetY = Height;
        }

        protected override void PostOnUpdateFrame(FrameEventArgs e)
        {
            base.PostOnUpdateFrame(e);
            Root.IsMouseOver = Root.Encloses(MousePosition);
            foreach (var control in Root.Descendents)
                control.IsMouseOver = control.Encloses(MousePosition);
        }

        protected override void OnMousePressed()
        {
            foreach (var clicked in Root.Descendents.Where(d => d.IsMouseOver))
                clicked.MousePressed?.Invoke(clicked, new InputEventArgs());
        }
    }
}
