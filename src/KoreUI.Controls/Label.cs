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
        public VerticalAlignment VerticalAlignment { get => Get<VerticalAlignment>(); set => Set(value); }

        public HorizontalAlignment HorizontalAlignment { get => Get<HorizontalAlignment>(); set => Set(value); }

        public Font Font { get => Get<Font>(); set => Set(value); }        

        public Color4 TextColor { get => Get<Color4>(); set => Set(value); }

        public string Text { get => Get<string>(); set => Set(value); }

        public (int from, int to)? Selection { get => Get<(int from, int to)>(); set => Set(value); }
        
        protected override void DefaultStyle()
        {
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Left;                  
            TextColor = Color4.Black;
            Text = "";            
            Size = ((0, 1), (0, 1));
            base.DefaultStyle();
        }

        public Label() : this(l => { }){ }

        public Label(Action<Label> setup)
        {
            DefaultStyle();
            setup(this);
        }

        public override void Draw(Canvas canvas)
        {
            canvas.WithStyle(() =>
            {
                if (Font != null)
                    canvas.Font = Font;                
                canvas.Fill = TextColor;                
                canvas.Text(Text, (10, 10));
            });
        }
    }
}
