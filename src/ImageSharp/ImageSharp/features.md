
# Features

### What works so far/ What is planned?

We've achieved a lot so far and hope to do a lot more in the future. We're always looking for help so please pitch in!

- **Encoding/decoding of image formats (plugable).**
 - [x] Jpeg (Includes Subsampling. Progressive writing required)
 - [x] Bmp (Read: 32bit, 24bit, 16 bit. Write: 32bit, 24bit just now)
 - [x] Png (Read: Rgb, Rgba, Grayscale, Grayscale + alpha, Palette. Write: Rgb, Rgba, Grayscale, Grayscale + alpha, Palette) Supports interlaced decoding 
 - [x] Gif (Includes animated)
 - [ ] Tiff (Help needed)
- **Metadata**
 - [x] EXIF Read/Write (Jpeg just now)
 - [ ] ICC (In Progress)
- **Quantizers (IQuantizer with alpha channel support, dithering, and thresholding)**
 - [x] Octree
 - [x] Xiaolin Wu
 - [x] Palette
- **DIthering (Error diffusion and Ordered)**
 - [x] Atkinson
 - [x] Burks
 - [x] FloydSteinburg
 - [x] JarvisJudiceNinke
 - [x] Sieera2
 - [x] Sierra3
 - [x] SerraLite
 - [x] Bayer
 - [x] Ordered
- **Basic color structs with implicit operators.**
 - [x] Bgra32
 - [x] CIE Lab
 - [x] CIE XYZ
 - [x] CMYK
 - [x] HSV
 - [x] HSL
 - [x] YCbCr
- **IPackedPixel representations of color models. Compatible with Microsoft XNA Game Studio and MonoGame IPackedVector\<TPacked\>.**
 - [x] Alpha8 
 - [x] Argb32 
 - [x] Bgr565 
 - [x] Bgra444 
 - [x] Bgra565 
 - [x] Byte4 
 - [x] HalfSingle 
 - [x] HalfVector2 
 - [x] HalfVector4 
 - [x] NormalizedByte2 
 - [x] NormalizedByte4 
 - [x] NormalizedShort2 
 - [x] NormalizedShort4 
 - [x] Rg32 
 - [x] Rgba1010102 
 - [x] Rgba32 - 32bit color in RGBA order - Our default pixel format.
 - [x] Rgba64 
 - [x] RgbaVector
 - [x] Short2 
 - [x] Short4 
- **Basic shape primitives.**
 - [x] Rectangle
 - [x] Size
 - [x] Point
 - [x] Ellipse
- **Resampling algorithms. (Optional gamma correction, resize modes, Performance improvements?)**
 - [x] Box
 - [x] Bicubic
 - [x] Lanczos2
 - [x] Lanczos3
 - [x] Lanczos5
 - [x] Lanczos8
 - [x] MitchelNetravali
 - [x] Nearest Neighbour 
 - [x] Robidoux
 - [x] Robidoux Sharp
 - [x] Spline
 - [x] Triangle
 - [x] Welch
- **Padding**
 - [x] Pad
 - [x] ResizeMode.Pad
 - [x] ResizeMode.BoxPad
- **Cropping**
 - [x] Rectangular Crop
 - [ ] Elliptical Crop
 - [x] Entropy Crop
 - [x] ResizeMode.Crop
- **Rotation/Skew**
 - [x] Flip (90, 270, FlipType etc)
 - [x] Rotate by angle and center point (Expandable canvas).
 - [x] Skew by x/y angles and center point (Expandable canvas).
- **ColorMatrix operations (Uses Matrix4x4)**
 - [x] BlackWhite
 - [x] Grayscale BT709
 - [x] Grayscale BT601
 - [x] Hue
 - [x] Saturation
 - [x] Lomograph
 - [x] Polaroid
 - [x] Kodachrome
 - [x] Sepia
 - [x] Achromatomaly 
 - [x] Achromatopsia
 - [x] Deuteranomaly
 - [x] Deuteranopia
 - [x] Protanomaly
 - [x] Protanopia
 - [x] Tritanomaly
 - [x] Tritanopia
- **Edge Detection**
 - [x] Kayyali
 - [x] Kirsch
 - [x] Laplacian3X3
 - [x] Laplacian5X5
 - [x] LaplacianOfGaussian
 - [x] Prewitt
 - [x] RobertsCross
 - [x] Robinson
 - [x] Scharr
 - [x] Sobel
- **Blurring/Sharpening**
 - [x] Gaussian blur
 - [x] Gaussian sharpening
 - [x] Box Blur
- **Filters**
 - [x] Alpha
 - [x] Contrast
 - [x] Invert
 - [x] BackgroundColor
 - [x] Brightness
 - [x] Pixelate
 - [ ] Mask
 - [x] Oil Painting
 - [x] Vignette
 - [x] Glow
 - [x] Threshold
- **Drawing**
 - [x] Image brush
 - [x] Pattern brush
 - [x] Solid brush 
 - [X] Hatch brush (Partial copy of System.Drawing brushes)
 - [x] Pen (Solid, Dash, Custom)
 - [x] Line drawing
 - [x] Complex Polygons (Fill, draw)
 - [x] DrawImage
 - [ ] Gradient brush (Need help)
- **DrawingText**
 - [ ] DrawString (In-progress. Single variant support just now, no italic,bold)
- Other stuff I haven't thought of.