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
                app.Root.Add(new UiControl(c =>
                {
                    c.Position = ((30, 0), (30, 0));
                    c.Size = ((400, 0), (0, 1));                    
                    c.Background = Color4.White;
                    c.Name = "Box";
                })                
                {
                    new UiControl(c =>
                    {
                        c.Size = ((0, 1), (0, 0.2));                        
                        c.Background = Color4.Green;
                        c.Name = "Bar";       
                    }),
                    new Button(c =>
                    {
                        c.Position = ((10, 0), (0, 0.3));
                        c.Size = ((0, 0.5), (32, 0));                        
                        c.Background = Color4.LightGray;
                        c.HoverColor = Color4.LightSkyBlue;
                        c.TextColor = Color4.Black;
                        c.Text = "Click Me";
                        c.Name = "Button";
                        c.MousePressed += Button1Click;
                    })
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
