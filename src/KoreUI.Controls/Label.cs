using OpenTK.Graphics;
using Processing.OpenTk.Core;
using System;

namespace KoreUI.Controls
{
    public enum VerticalAlignment
    {
        Top,
        Center,
        Bottom
    }

    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public class Label : UiControl
    {
        public VerticalAlignment VerticalAlignment { get; set; }

        public HorizontalAlignment HorizontalAlignment { get; set; }

        public Font Font { get; set; }

        public int FontSize { get; set; }

        public Color4 TextColor { get; set; }

        public string Text { get; set; }

        public (int from, int to)? Selection { get; set; }

        public Label() { }

        public Label(Action<Label> setup)
        {
            setup(this);
        }

        public override void Draw(Application canvas)
        {
            canvas.WithStyle(() =>
            {

            });
        }
    }
}
