using OpenTK.Graphics;
using System;
using Processing.OpenTk.Core.Extensions;
using Processing.OpenTk.Core;

namespace KoreUI.Controls
{
    public class Button : UiControl
    {
        public Color4 BackgroundHoverColor { get => Get<Color4>(); set => Set(value); }

        public int OutlineThickness { get => Get<int>(); set => Set(value); }

        public Color4 OutlineColor { get => Get<Color4>(); set => Set(value); }

        public Color4 OutlineHoverColor { get => Get<Color4>(); set => Set(value); }

        public Color4 TextColor { get => _label.TextColor; set => _label.TextColor = value; }

        public string Text { get => Get<string>(); set => Set(value); }

        private Label _label = new Label();

        public Button() : this(b => { })
        {
            base.Add(_label);            
        }

        protected override void DefaultStyle()
        {
            OutlineThickness = 2;
            Background = Color4.LightGray;
            BackgroundHoverColor = Color4.LightSkyBlue;
            OutlineColor = Color4.DarkGray;
            OutlineHoverColor = Color4.Gray;
            TextColor = Color4.Black;
            base.DefaultStyle();
        }

        public Button(Action<Button> setup)
        {
            DefaultStyle();
            setup(this);
        }

        public override void Draw(Canvas canvas)
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
