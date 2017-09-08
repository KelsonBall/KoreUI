using OpenTK.Graphics;

namespace Processing.OpenTk.Core.Rendering
{
    public class Style
    {
        private static Font defaultFont = null;

        public Font Font { get; set; }        
        public Color4 Fill { get; set; } = Color4.White;
        public Color4 Stroke { get; set; } = Color4.Black;
        public float StrokeWeight { get; set; } = 1;

        public Style()
        {
            if (defaultFont == null)
                defaultFont = new Font("arial.ttf", 14);
            Font = defaultFont;
        }

        public Style Copy()
        {
            return new Style
            {
                Font = Font,                
                Fill = Fill,
                Stroke = Stroke,
                StrokeWeight = StrokeWeight
            };
        }
    }
}
