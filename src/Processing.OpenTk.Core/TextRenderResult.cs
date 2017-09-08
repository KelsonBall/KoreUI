using System.Collections.Generic;
using System.Linq;

namespace Processing.OpenTk.Core
{
    public class CharacterRenderResult
    {
        public string Text { get; set; }
        public int HorizontalOffset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class TextRenderResult
    {

        private readonly List<CharacterRenderResult> _results = new List<CharacterRenderResult>();
        private readonly int _width;
        private readonly int _height;

        public TextRenderResult(IEnumerable<CharacterRenderResult> results)
        {
            int widthSum = 0;
            int heightMax = 0;
            foreach (var character in results)
            {
                character.HorizontalOffset = widthSum;
                _results.Add(character);
                if (character.Height > heightMax)
                    heightMax = character.Height;
                widthSum += (character.Width);
            }
            _width = widthSum;
            _height = heightMax;
        }

        public CharacterRenderResult this[int x]
        {
            get => _results.Where(c => c.HorizontalOffset <= x && c.HorizontalOffset + c.Width > x).FirstOrDefault();
        }

        public int Width { get => _width; }
        public int Height { get => _height; }
    }
}