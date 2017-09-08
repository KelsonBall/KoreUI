using OpenTK.Graphics;
using Processing.OpenTk.Core.Textures;
using System.Collections.Generic;
using System.IO;
using CacheMap = System.Collections.Generic.Dictionary<Processing.OpenTk.Core.Fonts.FontAwesomeIcons, System.Collections.Generic.Dictionary<float, System.Collections.Generic.Dictionary<OpenTK.Graphics.Color4, Processing.OpenTk.Core.Textures.PImage>>>;

namespace Processing.OpenTk.Core.Fonts
{
    public class IconCache
    {
        private static readonly CacheMap _cache = new CacheMap();
        private static FontGenerator _generator;

        public IconCache()
        {
            using (var stream = File.OpenRead("FontAwesome.ttf"))
                _generator = new FontGenerator(new TrueTypeSharp.TrueTypeFont(stream));
        }

        public PImage this[FontAwesomeIcons icon, float size, Color4 color]
        {
            get
            {
                if (!_cache.ContainsKey(icon))                
                    _cache[icon] = new Dictionary<float, Dictionary<Color4, PImage>>();                
                if (!_cache[icon].ContainsKey(size))                
                    _cache[icon][size] = new Dictionary<Color4, PImage>();                
                if (!_cache[icon][size].ContainsKey(color))                
                    _cache[icon][size][color] = _generator[(char)icon, size, color].texture;                                 
                return _cache[icon][size][color];
            }
        }
    }
}
