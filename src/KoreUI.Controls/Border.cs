using OpenTK.Graphics;
using Processing.OpenTk.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KoreUI.Controls
{
    public class Border : UiControl
    {
        public Color4 BorderColor { get; set; }

        private UiControl _container = new UiControl(c =>
        {
            c.Coordinates.SizeScaleX = 1;
            c.Coordinates.SizeScaleY = 1;
        });

        public int BorderTop { get => _container.Coordinates.PositionOffsetY; set => _container.Coordinates.PositionOffsetY = value; }

        public int BorderBottom { get => -_container.Coordinates.SizeOffsetY; set => _container.Coordinates.SizeOffsetY = -BorderTop - value; }

        public int BorderLeft { get => _container.Coordinates.PositionOffsetX; set => _container.Coordinates.PositionOffsetX = value; }

        public int BorderRight { get => -_container.Coordinates.SizeOffsetX; set => _container.Coordinates.SizeOffsetX = -BorderLeft - value; }

        public new Color4 Background { get => _container.Background; set => _container.Background = value; }

        public string BorderThickness
        {
            get
            {
                if (BorderTop == BorderBottom)
                {
                    if (BorderLeft == BorderRight)
                    {
                        if (BorderTop == BorderLeft)
                        {
                            return $"{BorderTop}";
                        }
                        return $"{BorderLeft},{BorderTop}";
                    }
                }
                return $"{BorderLeft},{BorderTop},{BorderRight},{BorderBottom}";
            }

            set
            {
                var borders = value.Split(",").Select(s => s.Trim()).Select(s => Int32.Parse(s)).ToArray();
                switch (borders.Length)
                {
                    case 1:
                        BorderLeft = borders[0];
                        BorderRight = borders[0];
                        BorderTop = borders[0];
                        BorderBottom = borders[0];
                        break;
                    case 2:
                        BorderLeft = borders[0];
                        BorderRight = borders[0];
                        BorderTop = borders[1];
                        BorderBottom = borders[1];
                        break;
                    case 4:
                        BorderLeft = borders[0];
                        BorderTop = borders[1];
                        BorderRight = borders[2];
                        BorderBottom = borders[3];
                        break;
                    default:
                        throw new ArgumentException("Invalid number of border parameters");
                }
            }
        }

        public Border()
        {
            base.Add(_container);
        }
        
        public Border(Action<Border> setup) : this() => setup(this);        

        public override void Draw(Canvas canvas)
        {
            canvas.WithStyle(() =>
            {
                canvas.Fill = BorderColor;
                canvas.Rectangle((0, 0), (ActualWidth, ActualHeight));
            });
            base.DrawChildren(canvas);
        }

        public override UiControl Add(IEnumerable<UiControl> controls)
        {
            return _container.Add(controls);
        }

        public override UiControl Add(UiControl control)
        {
            return _container.Add(control);
        }

        public override UiControl Remove(IEnumerable<UiControl> controls)
        {
            return _container.Remove(controls);
        }

        public override UiControl Remove(UiControl control)
        {
            return _container.Remove(control);
        }

    }
}

