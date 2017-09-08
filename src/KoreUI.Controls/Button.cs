using OpenTK.Graphics;
using System;
using Processing.OpenTk.Core.Extensions;

namespace KoreUI.Controls
{
    public class Button : UiControl
    {
        public Color4 BackgroundHoverColor { get; set; }

        public int OutlineThickness { get; set; } = 2;

        public Color4 OutlineColor { get; set; }

        public Color4 OutlineHoverColor { get; set; }

        public Color4 TextColor { get => _label.TextColor; set => _label.TextColor = value; }

        public string Text { get => _label.Text; set => _label.Text = value; }

        private Label _label = new Label();

        public Button()
        {
            base.Add(_label);
        }

        public Button(Action<Button> setup) : this() => setup(this);        

        public override void Draw(Application canvas)
        {
            if (canvas.Mouse[OpenTK.Input.MouseButton.Left])
            {
                canvas.WithStyle(() =>
                {
                    if (IsMouseOver)
                        canvas.Fill = OutlineHoverColor.WithBrightnessScalar(0.6);
                    else
                        canvas.Fill = OutlineColor;

                    canvas.Rectangle((0, 0), (ActualWidth, ActualHeight));

                    if (IsMouseOver)
                        canvas.Fill = BackgroundHoverColor.WithBrightnessScalar(0.6);
                    else
                        canvas.Fill = Background;

                    canvas.Rectangle((OutlineThickness, OutlineThickness), (ActualWidth - (2 * OutlineThickness), ActualHeight - (2 * OutlineThickness)));
                });
            }
            else
            {
                canvas.WithStyle(() =>
                {
                    if (IsMouseOver)
                        canvas.Fill = OutlineHoverColor;
                    else
                        canvas.Fill = OutlineColor;

                    canvas.Rectangle((0, 0), (ActualWidth, ActualHeight));

                    if (IsMouseOver)
                        canvas.Fill = BackgroundHoverColor;
                    else
                        canvas.Fill = Background;

                    canvas.Rectangle((OutlineThickness, OutlineThickness), (ActualWidth - (2 * OutlineThickness), ActualHeight - (2 * OutlineThickness)));
                });
            }
           base.DrawChildren(canvas);
        }
    }
}
