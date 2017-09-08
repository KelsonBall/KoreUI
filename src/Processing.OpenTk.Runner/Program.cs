using System;
using OpenTK.Graphics;
using Processing.OpenTk.Core;
using Processing.OpenTk.Core.Textures;
using Processing.OpenTk.Core.Math;

namespace Processing.OpenTk.Runner
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (Canvas canvas = new Canvas(800, 640))
            {
                PImage squid = default(PImage);

                canvas.Setup += _ =>
                {
                    squid = PImage.FromFile("squid.jpg").Resize(256, 256);
                };

                canvas.Draw += _ =>
                {
                    canvas.WithStyle(() =>
                    {
                        canvas.Stroke = Color4.Red;
                        canvas.Fill = Color4.Green;

                        canvas.StrokeWeight = 2;

                        PVector a = canvas.MousePosition;
                        var c = (200, 200);

                        canvas.Rectangle(a, c);

                        canvas.Fill = Color4.DeepSkyBlue;

                        canvas.WithBoundry(a, c, () =>
                        {                            
                            canvas.Image(squid, (0, 0));
                        });
                    });

                    //canvas.Image(squid, (canvas.Width - squid.Width, canvas.Height - squid.Height));

                    canvas.Fill = Color4.Firebrick;
                    //canvas.Text("Hello, world!", (10, 10));
                };

                canvas.Run(60f);
            }
        }

        
    }
}