using OpenTK.Graphics;
using Processing.OpenTk.Core.Extensions;
using Processing.OpenTk.Core.Textures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrueTypeSharp;


namespace Processing.OpenTk.Core
{
    public class Font
    {
        private readonly FontGenerator _generator;

        private Dictionary<char, Dictionary<Color4, (PImage texture, int kern, int yshift)>> _cache = new Dictionary<char, Dictionary<Color4, (PImage texture, int kern, int yshift)>>();

        private readonly float _size;

        public readonly int MaxHeight;
        public readonly int MaxOffset;

        public Font(string filename, float size) : this (() => File.OpenRead(filename), size)
        {

        }

        public Font(Func<FileStream> source, float size)
        {
            Color4 color = Color4.Black;
            using (var stream = source())
                _generator = new FontGenerator(new TrueTypeFont(stream));
            _size = size;

            int heightMax = 0;
            int offsetMax = 0;
            foreach (var letter in AlphabetProvider.Enumerate())
            {
                var current = this[letter, color];
                if (current.texture.Height > heightMax)
                    heightMax = current.texture.Height;
                if (current.yshift > offsetMax)
                    offsetMax = current.yshift;
                current = this[letter.ToString().ToUpper().Single(), color];
                if (current.texture.Height > heightMax)
                    heightMax = current.texture.Height;
                if (current.yshift > offsetMax)
                    offsetMax = current.yshift;
            }
            MaxHeight = heightMax;
            MaxOffset = offsetMax;
        }

        public (PImage texture, int kern, int yshift) this[char c, Color4 color]
        {
            get
            {
                if (!_cache.ContainsKey(c))
                    _cache[c] = new Dictionary<Color4, (PImage texture, int kern, int yshift)>();
                if (!_cache[c].ContainsKey(color))
                    _cache[c][color] = _generator[c, _size, color];
                return _cache[c][color];
            }
        }
    }
}
