using OpenTK.Graphics;
using System;

namespace KoreUI.Controls
{
    public class Button : UiControl
    {
        public Color4 HoverColor { get; set; }

        public Color4 TextColor { get; set; }

        public string Text { get; set; }

        public Button() { }

        public Button(Action<Button> setup)
        {
            setup(this);
        }

        public override void Draw(Application canvas)
        {
            canvas.WithStyle(() =>
            {
                if (IsMouseOver)
                {
                    canvas.Fill = HoverColor;
                }
                else
                {
                    canvas.Fill = Background;
                }

                canvas.Rectangle((0, 0), (ActualWidth, ActualHeight));
                canvas.Fill = TextColor;
                canvas.Text(Text, (10, 10));
            });
        }
    }
}
