using KoreUI.Controls;
using OpenTK.Graphics;
using System;

namespace KoreUI.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Application app = new Application())
            {
                app.Root.Background = Color4.CornflowerBlue;
                app.Root.Add(new UiControl(control =>
                {
                    control.Position = ((10, 0), (10, 0));
                    control.Size = ((-20, 1), (-20, 1));
                    control.Background = Color4.White;
                    control.Name = "Box";
                })
                {
                    new Button(button =>
                    {
                        button.Position = ((10, 0), (0, 0.3));
                        button.Size = ((-10, 0.5), (32, 0));
                        button.Background = Color4.LightGray;
                        button.BackgroundHoverColor = Color4.LightSkyBlue;
                        button.OutlineColor = Color4.DarkGray;
                        button.OutlineHoverColor = Color4.Gray;
                        button.TextColor = Color4.Black;
                        button.Text = "Click Me";
                        button.Name = "Button";
                        button.MousePressed += Button1Click;
                    }),
                    new Border(border =>
                    {
                        border.Position = ((10, 0.5), (10, 0));
                        border.Size = ((-20, 0.5), (-20, 1));
                        border.BorderThickness = "2,4,8,16";
                        border.BorderColor = Color4.Black;
                        border.Background = Color4.Yellow;
                    })
                    {
                        new Label(label =>
                        {
                            label.Position = ((20, 0), (-15, 0.5));
                            label.Size = ((0, 1), (0, 1));
                            label.TextColor = Color4.DarkBlue;
                            label.Text = "Content";
                        })
                    }
                });

                app.Run(60f);
            }
        }

        private static void Button1Click(UiControl arg1, InputEventArgs arg2)
        {
            Console.WriteLine("Click!");
        }
    }
}
