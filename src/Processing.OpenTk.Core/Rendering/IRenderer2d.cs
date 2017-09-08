using OpenTK.Graphics;
using Processing.OpenTk.Core.Math;
using Processing.OpenTk.Core.Textures;

namespace Processing.OpenTk.Core.Rendering
{
    public interface IRenderer2d 
    {        
        void Background(Color4 color);
        void Triangle(PVector a, PVector b, PVector c);
        void Rectangle(PVector position, PVector size);
        void Quad(PVector a, PVector b, PVector c, PVector d);
        void Ellipse(PVector position, PVector size);
        void Line(PVector a, PVector b);
        void Arc(PVector position, PVector size, float startAngle, float sweepAngle);
        void Image(PImage image, PVector position);
        TextRenderResult Text(string text, PVector position);
        void Shape(PVector position, params PVector[] points);

    }
}
