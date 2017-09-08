using OpenTK.Graphics;

namespace Processing.OpenTk.Core.Extensions
{
    public static class Color4Extensions
    {
        public static int ToRgbaIntegerFormat(this Color4 color)
        {
            byte ToByte(float f) => (byte)(0xFF * f);

            return ToByte(color.R) << 24 | ToByte(color.G) << 16 | ToByte(color.B) << 8 | ToByte(color.A);
        }

        public static byte Depack(this int source, int n)
        {
            return (byte)((source & (0xFF << n * 8)) >> n * 8);
        }

        public static byte Depack(this Color4 source, int n)
        {
            return (byte)((source.ToRgbaIntegerFormat() & (0xFF << n * 8)) >> n * 8);
        }
        
        public static Color4 WithRed(this Color4 color, double red) => new Color4((float)red, color.G, color.B, color.A);

        public static Color4 WithGreen(this Color4 color, double green) => new Color4(color.R, (float)green, color.B, color.A);

        public static Color4 WithBlue(this Color4 color, double blue) => new Color4(color.R, color.G, (float)blue, color.A);

        public static Color4 WithAlpha(this Color4 color, double alpha) => new Color4(color.R, color.G, color.B, (float)alpha);

        public static Color4 WithBrightnessScalar(this Color4 color, double scalar) => scalar.Then(s => (float)s).Then(s => new Color4(color.R * s, color.G * s, color.B * s, color.A));
    }
}
