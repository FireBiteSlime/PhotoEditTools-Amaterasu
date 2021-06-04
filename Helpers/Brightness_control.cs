using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEditTools
{
    class Brightness_control
    {
/*        SourceImage Brightness_sontrol( SourceImage image)
        {
            ImageAttributes imageAttributes = new ImageAttributes();
            int width = image.width;
            int height = image.height;
            int brightness = image.brightness;

            float[][] colorMatrixElements = {
                                                new float[] {brightness, 0, 0, 0, 0},
                                                new float[] {0, brightness, 0, 0, 0},
                                                new float[] {0, 0, brightness, 0, 0},
                                                new float[] {0, 0, 0, 1, 0},
                                                new float[] {0, 0, 0, 0, 1}
                                            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(
                colorMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);
            Graphics graphics = Graphics.FromImage(image.bitmap);
            graphics.DrawImage(image.bitmap, new Rectangle(0, 0, width, height), 0, 0, width,
                                   height,
                                   GraphicsUnit.Pixel,
                                   imageAttributes);
            
            return image;
        }*/
        
    }
}
