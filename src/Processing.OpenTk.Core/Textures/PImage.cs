using System;
using System.IO;
using ImageSharp;
using ImageSharp.Processing;
using SixLabors.Primitives;
using OpenTK.Graphics.OpenGL;
using Processing.OpenTk.Core.Extensions;
using OpenTK.Graphics;

namespace Processing.OpenTk.Core.Textures
{
    public class PImage
    {
        private readonly Image<Rgba32> _source;
        public readonly uint Handle;
        public readonly int Width;
        public readonly int Height;
        public readonly int[] Data;
        
        private PImage(Image<Rgba32> source)
        {
            _source = source;
            Data = GetData(source);
            Width = source.Width;
            Height = source.Height;

            GL.Color4(Color4.White);
            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out Handle);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Nearest);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            WithDataPtr(dataptr =>
            {
                GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, Width, Height, PixelFormat.Rgba, PixelType.UnsignedByte, dataptr);
            });

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }       

        public static implicit operator uint (PImage image) => image.Handle;

        private ResizeOptions GetResizer(int width, int height) => new ResizeOptions
        {
            Size = new Size(width, height),
            Sampler = new BicubicResampler(),
            Mode = ResizeMode.Stretch
        };

        public PImage Resize(int newWidth, int newHeight) => WithImageCopy(copy => GetResizer(newWidth, newHeight).Then(copy.Resize).Then(FromImage));        

        public static PImage FromSteam(Func<Stream> streamSource)
        {
            using (var stream = streamSource())
                return FromImage(Image.Load(stream));
        }

        public static PImage FromFile(string file) => FromImage(Image.Load(file));

        public static PImage FromImage(Image<Rgba32> image) => new PImage(image);        

        private static int[] GetData(Image<Rgba32> image)
        {            
            int[] data = new int[image.Pixels.Length];

            int outIndex = 0;
            for (int row = 0; row < image.Height; row++)
            {
                for (int column = 0; column < image.Width; column++)
                {
                    int inIndex = column + (row * image.Width);
                    data[outIndex] = image.Pixels[inIndex].A << 24
                                   | image.Pixels[inIndex].B << 16
                                   | image.Pixels[inIndex].G << 8
                                   | image.Pixels[inIndex].R;
                    outIndex++;
                }
            }

            return data;
        }

        public void WithDataPtr(Action<IntPtr> action)
        {
            unsafe
            {
                fixed (int* dataptr = &(Data[0]))
                {
                    action((IntPtr)dataptr);
                }
            }
        }

        private PImage WithImageCopy(Func<Image<Rgba32>, PImage> func) => func(new Image<Rgba32>(_source));       
    }        
}
