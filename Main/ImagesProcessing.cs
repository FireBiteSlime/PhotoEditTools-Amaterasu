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
    public class ImagesProcessing : ICloneable
    {
        public Bitmap ResultImage { get; set; }

        private delegate int BlendMethod(int a, int b);

        private Dictionary<Method, BlendMethod> BlendMethods = new Dictionary<Method, BlendMethod>();

        public Pixel[,] pixels_matrix { get; set; }

        public Pixel[,] originals_pixels_matrix { get; set; }

        public int width;
        public int height;

        public CurveData curveData { get; private set; } = new CurveData();

        public SpatialFilteringData spatialFilteringData { get; private set; } = new SpatialFilteringData();
        public BitmapImage ImageSource
        {
            get => Utilities.GetImageSource(ResultImage);
        }

        public ImagesProcessing()
        {
            BlendMethods[Method.Diff] = Diff;
            BlendMethods[Method.Multiply] = Multiply;
            BlendMethods[Method.Divide] = Divide;
            BlendMethods[Method.Overlay] = Overlay;
            BlendMethods[Method.Exclusion] = Exclusion;
            BlendMethods[Method.Screen] = Screen;
            BlendMethods[Method.Normal] = Normal;
            BlendMethods[Method.Subtraction] = Subtraction;
            BlendMethods[Method.Sum] = Sum;
            BlendMethods[Method.SoftLight] = SoftLight;
            BlendMethods[Method.HardLight] = HardLight;
            BlendMethods[Method.VividLight] = VividLight;
            BlendMethods[Method.LinearLight] = LinearLight;
            BlendMethods[Method.PinLight] = PinLight;
            BlendMethods[Method.HardMix] = HardMix;
            BlendMethods[Method.DarkenOnly] = DarkenOnly;
            BlendMethods[Method.LightenOnly] = LightenOnly;
            BlendMethods[Method.ColorDodge] = ColorDodge;
            BlendMethods[Method.ColorBurn] = ColorBurn;
            BlendMethods[Method.LinearBurn] = LinearBurn;
            BlendMethods[Method.Experimental1] = Experimental1;
            BlendMethods[Method.Experimental2] = Experimental2;
            BlendMethods[Method.Experimental3] = Experimental3;

            //SetPixelsMatrix();
        }

        ~ImagesProcessing()
        {
            ClearResult();
        }

        private void ClearResult()
        {
            ResultImage?.Dispose();
            ResultImage = null;
        }


      
        private int AlphaBlend(int a, int b, int alpha)
        {
            //float offset = (1.0f - alpha / 255.0f) * (a - b);
            //return Util.Clamp(b + (int)Math.Round(offset), 0, 255); // mb clamp isnt necessary
           int offset = (255 - alpha) * (a - b) / 255;
           // int offset = (a + alpha > 255) ? 255 : b + alpha;
            return  b + offset;
        }

   

        

        // for LighterColor, DarkerColor, Luminosity and Color I have to rewrite these function to support Pixel argument
        private int Overlay(int a, int b) => (a <= 127) ? (2 * a * b / 255) : (255 - 2 * (255 - a) * (255 - b) / 255);
        private int Diff(int a, int b) => Math.Abs(a - b);
        private int Exclusion(int a, int b) => Utilities.Clamp(a + b - 2 * a * b / 255, 0, 255);
        private int Screen(int a, int b) => 255 - (255 - a) * (255 - b) / 255;
        private int Subtraction(int a, int b) => Utilities.Clamp(a - b, 0, 255);
        private int Sum(int a, int b) => Utilities.Clamp(a + b, 0, 255); // same as linear dodge
        private int Multiply(int a, int b) => a * b / 255;
        private int Divide(int a, int b) => b == 0 ? 255 : Utilities.Clamp((int)(a / 255.0f / (b / 255.0f) * 255), 0, 255);
        private int Normal(int a, int b) => b;
        private int SoftLight(int a, int b) => (255 - 2 * b) * a * a / 65025 + 2 * b * a / 255;
        private int HardLight(int a, int b) => Overlay(b, a);
        private int VividLight(int a, int b) => b <= 127 ? ColorBurn(a, 2 * b) : Divide(a, 2 * (255 - b));
        private int LinearLight(int a, int b) => LinearBurn(a, 2 * b);
        private int PinLight(int a, int b) => b <= 127 ? DarkenOnly(a, 2 * b) : Utilities.Clamp(LightenOnly(a, 2 * (b - 127)), 0, 255);
        private int HardMix(int a, int b) => a + b >= 255 ? 255 : 0;
        private int DarkenOnly(int a, int b) => a > b ? b : a;
        private int LightenOnly(int a, int b) => a > b ? a : b;
        private int ColorDodge(int a, int b) => Divide(a, 255 - b);
        private int LinearBurn(int a, int b) => Utilities.Clamp(a + b - 255, 0, 255);
        private int ColorBurn(int a, int b) => 255 - Divide(255 - a, b);

        private int Experimental1(int a, int b) => (a <= 127) ? (2 * (int)Math.Cos(a * b) * b / 255) : (255 - 2 * (255 - (int)Math.Sin(a)) * (255 - b) / 255);
        private int Experimental2(int a, int b) => (a <= 127) ? (2 * (int)Math.Cos(a * b) * b / 255) : (255 - 2 * (255 - (int)Math.Sin(a)) * (255 -(int)Math.Atan( b)) / 255);
        private int Experimental3(int a, int b) => (a <= 127) ? (2 * (int)Math.Cos(a * b) * (int)Math.Sin(b) / 255) : (255 - 2 * (255 - (int)Math.Sin(a)) * (255 - b) / 255);
       
        
        public void Blend(IList<SourceImage> images)
        {


            List<SourceImage> masks = images.Clone() as List<SourceImage>;
            masks.RemoveAll(m => m.method == Method.None);

            ClearResult();
            if (masks.Count() == 0) return;
           

            BlendMethod currentMethod;
            for (var i = masks.Count() - 1; i >= 1; i--)
            {
                SourceImage mask_top = masks[i];
                SourceImage mask_bottom = masks[i - 1];

                int min_h = mask_top.height > mask_bottom.height ? mask_bottom.height : mask_top.height;
                int min_w = mask_top.width > mask_bottom.width ? mask_bottom.width : mask_top.width;

                // align to center
                int h_offset = Math.Abs(mask_top.height - mask_bottom.height) / 2;
                int w_offset = Math.Abs(mask_top.width - mask_bottom.width) / 2;

                int x_top_offset, x_bottom_offset, y_top_offset, y_bottom_offset;
                if (mask_top.width > mask_bottom.width)
                {
                    x_top_offset = w_offset;
                    x_bottom_offset = 0;
                }
                else
                {
                    x_top_offset = 0;
                    x_bottom_offset = w_offset;
                }

                if (mask_top.height > mask_bottom.height)
                {
                    y_top_offset = h_offset;
                    y_bottom_offset = 0;
                }
                else
                {
                    y_top_offset = 0;
                    y_bottom_offset = h_offset;
                }

                currentMethod = BlendMethods[mask_top.method];

                for (int x = 0; x < min_w; x++)
                {
                    for (int y = 0; y < min_h; y++)
                    {
                        int x_top = x + x_top_offset;
                        int x_bottom = x + x_bottom_offset;
                        int y_top = y + y_top_offset;
                        int y_bottom = y + y_bottom_offset;

                        Pixel pix_top = mask_top.Pixels_matrix[x_top, y_top];
                        Pixel pix_bottom = mask_bottom.Pixels_matrix[x_bottom, y_bottom];

                        int R_top = currentMethod(pix_bottom.R, pix_top.R);
                        int G_top = currentMethod(pix_bottom.G, pix_top.G);
                        int B_top = currentMethod(pix_bottom.B, pix_top.B);

                        int alpha = (int)((mask_top.opacity / 100.0f) * (pix_top.A / 255.0f) * 255);
                       // int bf = (int)((mask_top.brightness / 100.0f) * (pix_top.Bf / 255.0f) * 255);

                        int R_bottom = AlphaBlend(pix_bottom.R, R_top, alpha);
                        int G_bottom = AlphaBlend(pix_bottom.G, G_top, alpha);
                        int B_bottom = AlphaBlend(pix_bottom.B, B_top, alpha);

                        int new_alpha = pix_bottom.A == 0 ? alpha : pix_bottom.A;

                        /*int bf = (int)((mask_top.brightness / 100.0f));
                        R_bottom = (int)(((pix_bottom.R & 0x00FF0000) >> 16) + bf * 128 / 100);
                        G_bottom = (int)(((pix_bottom.G & 0x0000FF00) >> 8) + bf * 128 / 100);
                        B_bottom = (int)((pix_bottom.B & 0x000000FF) + bf * 128 / 100);*/
                        /*int brightnessf = (int)((mask_top.brightness / 100.0f) * (pix_top.Bf / 255.0f) * 255);

                        R_bottom = BrightnessBlend(pix_bottom.R, R_top, brightnessf);
                        G_bottom = BrightnessBlend(pix_bottom.G, G_top, brightnessf);
                        B_bottom = BrightnessBlend(pix_bottom.B, B_top, brightnessf);

                        int new_brightnessf = pix_bottom.Bf == 0 ? alpha : pix_bottom.Bf;*/

                        masks[i - 1].Pixels_matrix[x_bottom, y_bottom] = new Pixel(new_alpha, R_bottom, G_bottom, B_bottom);
                    }
                }
            }

            // write result
            int w = masks[0].width;
            int h = masks[0].height;
            width = w;
            height = h;
            byte[] colors = new byte[w * h * 4];
            pixels_matrix = new Pixel[w, h];
            Rectangle rect = new Rectangle(0, 0, w, h);
            ResultImage = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            width = ResultImage.Width;
            height = ResultImage.Height;
            
            BitmapData bitmapData = ResultImage.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            IntPtr Iptr = bitmapData.Scan0;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Pixel pix = masks[0].Pixels_matrix[x, y];
                    byte A = (byte)((masks[0].opacity / 100.0f) * (pix.A / 255.0f) * 255);
                    //byte Bf = (byte)((masks[0].brightness / 100.0f) * (pix.Bf / 255.0f) * 255);
                    //byte Bf = (byte)((masks[0].brightness / 100.0f) * (pix.Bf / 255.0f) * 255);
                    int offset = ((y * w) + x) * 4;
                    colors[offset] = pix.B;
                    colors[offset + 1] = pix.G;
                    colors[offset + 2] = pix.R;
                    colors[offset + 3] = A;

                    pixels_matrix[x, y] = new Pixel(colors[offset + 3], colors[offset + 2], colors[offset + 1], colors[offset]);

                }
            }
            //SetPixelsMatrix();
            Marshal.Copy(colors, 0, Iptr, colors.Length);

           /* for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    int offset = ((y * w) + x) * 4;
                    pixels_matrix[x, y] = new Pixel(colors[offset + 3], colors[offset + 2], colors[offset + 1], colors[offset]);
                }
            }*/
            ResultImage.UnlockBits(bitmapData);
            originals_pixels_matrix = pixels_matrix.Clone() as Pixel[,];
            

        }
        public object Clone()
        {
            ImagesProcessing process = new ImagesProcessing();
            process.width = width;
            process.height = height;
            process.curveData = curveData.Clone() as CurveData;
            process.ResultImage = ResultImage;
            process.pixels_matrix = pixels_matrix.Clone() as Pixel[,];
            process.originals_pixels_matrix = originals_pixels_matrix.Clone() as Pixel[,];
            return process;
        }
        public void Save(string path)
        {
            if (ResultImage == null) throw new Exception("Result image is null. You need to call BlendMasks(...)");
            ResultImage.Save(path);
        }
    }
}
