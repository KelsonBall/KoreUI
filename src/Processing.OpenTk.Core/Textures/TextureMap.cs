using Processing.OpenTk.Core.Math;

namespace Processing.OpenTk.Core.Textures
{
    public class TextureMap
    {
        public readonly PImage Texture;
        public readonly PVector3[] UvVerticies;

        public TextureMap(PImage texture, PVector3[] uvVerticies)
        {
            Texture = texture;
            UvVerticies = uvVerticies;
        }
    }
}
