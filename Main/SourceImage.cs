using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoEditTools
{
    public enum Method
    {
        None,
        Normal,
        Sum,
        Subtraction,
        Multiply,
        Divide,
        Screen,
        Diff,
        Overlay,
        Exclusion,
        SoftLight,
        HardLight,
        VividLight,
        LinearLight,
        PinLight,
        HardMix,
        DarkenOnly,
        LightenOnly,
        ColorDodge,
        ColorBurn,
        LinearBurn,
        Experimental1,
        Experimental2,
        Experimental3
    }
    public class SourceImage : ICloneable
    {
        public Bitmap bitmap { get; set; }
       // public Pixel[,] pixels_matrix { get; private set; }
        public int opacity { get; set; } = 100;

        public float brightness { get; set; } = 1F;

        public int contrast { get; set; } = 0;

       
        public Pixel[,] Pixels_matrix { get; set; }

        public Pixel[,] Original_pixels_matrix { get; set; }
        public Method method { get; set; } = Method.None;

        public BinarizationData binarizationData { get; private set; } = new BinarizationData();

        private int _height;
        public int height
        {
            get => _height;
            private set
            {
                _height_view = value;
                _height = value;
            }
        }
        private int _width;
        public int width
        {
            get => _width;
            private set
            {
                _width_view = value;
                _width = value;
            }
        }

        public int method_view
        {
            get => (int)method;
            set => method = (Method)value;
        }

        public bool keepAspectRatio { get; set; } = true;

        private int _width_view;
        private int _height_view;

        public SourceImage(string path)
        {
            bitmap = new Bitmap(path);

            width = bitmap.Width;
            height = bitmap.Height;
            
            SetPixelsMatrix(bitmap);
        }
        public BitmapImage ImageSource
        {
            get => Utilities.GetImageSource(bitmap);
        }
        public SourceImage() { }

        ~SourceImage()
        {
            bitmap?.Dispose();
        }
        public object Clone()
        {
            SourceImage source = new SourceImage();
            source.opacity = opacity;
            source.brightness = brightness;
            source.contrast = contrast;
            source.method = method;
            source.width = width;
            source.height = height;
            source.Pixels_matrix = Pixels_matrix.Clone() as Pixel[,];
            source.Original_pixels_matrix = Original_pixels_matrix.Clone() as Pixel[,];
            return source;
        }

        private void SetPixelsMatrix(Bitmap image)
        {
            Pixels_matrix = new Pixel[width, height];
            
            byte[] colors = new byte[width * height * 4];

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            IntPtr Iptr = bitmapData.Scan0;
            Marshal.Copy(Iptr, colors, 0, colors.Length);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int offset = ((y * width) + x) * 4;
                    Pixels_matrix[x, y] = new Pixel(colors[offset + 3], colors[offset + 2], colors[offset + 1], colors[offset]);
                }
            }
            image.UnlockBits(bitmapData);

            Original_pixels_matrix = Pixels_matrix.Clone() as Pixel[,];

        }


    }
}
