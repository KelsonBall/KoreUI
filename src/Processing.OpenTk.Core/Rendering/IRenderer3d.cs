using OpenTK.Graphics;
using Processing.OpenTk.Core.Math;
using Processing.OpenTk.Core.Textures;

namespace Processing.OpenTk.Core.Rendering
{
    public interface IRenderer3d
    {
        IRenderer3d BeginDraw();
        IRenderer3d EndDraw();        

        IRenderer3d Background(Color4 color);
        IRenderer3d Triangle(PVector3 a, PVector3 b, PVector3 c);
        IRenderer3d Box(PVector3 position, PVector3 size);
        IRenderer3d Quad(PVector3 a, PVector3 b, PVector3 c, PVector3 d);
        IRenderer3d Ellipsoid(PVector3 position, PVector3 size);
        IRenderer3d Line(PVector3 a, PVector3 b);        
        IRenderer3d Image(PImage image, PVector3 position, PVector3 normal);
        IRenderer3d Text(string text, PVector3 position, PVector3 normal);
        IRenderer3d Shape(PVector3 position, params PVector3[] points);

        IRenderer3d Model(PVector3 position, Model model, TextureMap map);
        IRenderer3d Model(PVector3 position, Model model, Color4 color);
        IRenderer3d Model(PVector3 position, Model model, Color4[] vertexColors);

    }
}
