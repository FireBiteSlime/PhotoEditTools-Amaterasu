using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoEditTools
{
    public class Instruments
    {

        
        public Instruments() { }

        public Window Find_This_Window(Type totwin)
        {
            /*var windows = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive && x.GetType() == totwin);
            return windows;*/
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.GetType() == totwin);
            /*foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == totwin)
                {
                    return window;
                }
            }*/
            return window;
        }


        public void DoActionDraw(ImagesProcessing result)
        {
            //for (int i = 0; i < images.Count; i++)
            Pixel[,] changed_pixels_matrix = new Pixel[result.width, result.height];
            changed_pixels_matrix = result.pixels_matrix.Clone() as Pixel[,];


            Parallel.For(0, result.width, x =>
            {

                for (int y = 0; y < result.height; y++)
                {
                    int R = result.pixels_matrix[x, y].R;
                    int G = result.pixels_matrix[x, y].G;
                    int B = result.pixels_matrix[x, y].B;

                    switch (result.curveData.channel)
                    {
                        case CurveChannel.RGB:
                            R = result.curveData.interpolatedPoints[R];
                            G = result.curveData.interpolatedPoints[G];
                            B = result.curveData.interpolatedPoints[B];
                            break;
                        case CurveChannel.R:
                            R = result.curveData.interpolatedPoints[R];
                            break;
                        case CurveChannel.G:
                            G = result.curveData.interpolatedPoints[G];
                            break;
                        case CurveChannel.B:
                            B = result.curveData.interpolatedPoints[B];
                            break;

                    }
                    int a = result.pixels_matrix[x, y].A;
                    changed_pixels_matrix[x, y] = new Pixel(a, R, G, B);
                }
            });




            result.curveData.SetHistoPoints(changed_pixels_matrix);


            
            DrawResult(result, changed_pixels_matrix);

        }

        public void DrawResult(ImagesProcessing result, Pixel[,] pix_mat)
        {

            byte[] colors = new byte[result.width * result.height * 4];
            //pixels_matrix = new Pixel[w, h];


            int w = result.width;
            int h = result.height;

            Rectangle rect = new Rectangle(0, 0, w, h);


            Bitmap ResultImage = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            //width = ResultImage.Width;
            //height = ResultImage.Height;

            BitmapData bitmapData = ResultImage.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            IntPtr Iptr = bitmapData.Scan0;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Pixel pix = pix_mat[x, y];
                    //byte A = (byte)((masks[0].opacity / 100.0f) * (pix.A / 255.0f) * 255);
                    //byte Bf = (byte)((masks[0].brightness / 100.0f) * (pix.Bf / 255.0f) * 255);
                    //byte Bf = (byte)((masks[0].brightness / 100.0f) * (pix.Bf / 255.0f) * 255);
                    int offset = ((y * w) + x) * 4;
                    colors[offset] = pix.B;
                    colors[offset + 1] = pix.G;
                    colors[offset + 2] = pix.R;
                    colors[offset + 3] = pix.A;

                    //pixels_matrix[x, y] = new Pixel(colors[offset + 3], colors[offset + 2], colors[offset + 1], colors[offset]);

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

            result.ResultImage = ResultImage.Clone() as Bitmap;
        }

  

        public void DoActionBinarization(SourceImage sourceIMG)
        {

            sourceIMG.Pixels_matrix = sourceIMG.Original_pixels_matrix.Clone() as Pixel[,];
            switch (sourceIMG.binarizationData.type)
            {
                case  BinarizationType.None:
                   
                    break;
                case BinarizationType.Gavrilov:
                    Gavrilov(sourceIMG);
                    break;
                case BinarizationType.Otsu:
                    Otsu(sourceIMG);
                    break;
                case BinarizationType.Niblack:
                    Niblack(sourceIMG);
                    break;
                case BinarizationType.Sauvola:
                    Sauvola(sourceIMG);
                    break;
                case BinarizationType.Wolf:
                    Wolf(sourceIMG);
                    break;
                case BinarizationType.Bradley:
                    Bradley(sourceIMG);
                    break;
            }
        }


        private int toGreyscale(Pixel pix)
        {
            return (byte)(0.2125 * pix.R + 0.7154 * pix.G + 0.0721 * pix.B);
        }

        private double toGreyscaleDouble(Pixel pix)
        {
            return 0.2125d * pix.R + 0.7154d * pix.G + 0.0721d * pix.B;
        }

        private void GlobalMethod(SourceImage editing_image, int t)
        {
            Parallel.For(0, editing_image.width, x =>
            {
                for (int y = 0; y < editing_image.height; y++)
                {
                    int res = toGreyscale(editing_image.Pixels_matrix[x, y]) <= t ? 0 : 255;
                    editing_image.Pixels_matrix[x, y] = new Pixel(editing_image.Pixels_matrix[x, y].A, res, res, res);
                }
            });
        }

        #region Global Methods
        private void Gavrilov(SourceImage editing_image)
        {
            int[,] integrated_image = getIntegrated(editing_image.Pixels_matrix);
            int t = integrated_image[editing_image.width - 1, editing_image.height - 1] / (editing_image.width * editing_image.height);

            GlobalMethod(editing_image, t);
        }

        private void Otsu(SourceImage editing_image)
        {
            double[] norm_gisto = buildNormGisto(editing_image.Pixels_matrix);

            int t = 0;
            double sigma_max = 0;

            double l_max = 256;
            for (int i = 255; i >= 0; i--)
            {
                if (norm_gisto[i] == 0.0d)
                    l_max = i;
                else
                    break;
            }

            double u_T = 0;
            for (int i = 0; i < l_max; i++)
            {
                u_T += i * norm_gisto[i];
            }

            double N_sum = 0.0d;
            double Ni_sum = 0.0d;
            for (int i = 0; i < l_max; i++)
            {
                double omega1 = N_sum;
                double omega2 = 1.0d - omega1;

                double u1 = Ni_sum / omega1;
                double u2 = (u_T - u1 * omega1) / omega2;

                double sigma = omega1 * omega2 * Math.Pow(u1 - u2, 2);
                if (sigma > sigma_max)
                {
                    sigma_max = sigma;
                    t = i;
                }

                N_sum += norm_gisto[i];
                Ni_sum += i * norm_gisto[i];
            }

            GlobalMethod(editing_image, t);
        }

        private double[] buildNormGisto(Pixel[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            int[] pix_count = new int[256];
            double[] norm_gisto = new double[256];

            Parallel.For<int[]>(0, w, () => (new int[256]), (x, loop, subtotal) =>
            {
                for (int y = 0; y < h; y++)
                {
                    int a = toGreyscale(matrix[x, y]);

                    subtotal[a]++;
                }
                return subtotal;
            },
                (arr) =>
                {
                    for (int i = 0; i < 256; i++) Interlocked.Add(ref pix_count[i], arr[i]);
                }
            );

            for (int i = 0; i < 256; i++)
            {
                norm_gisto[i] = pix_count[i] * 1.0d / (w * h * 1.0d);
            }

            return norm_gisto;
        }
        #endregion

        private int takerWindow(int[,] integrated, int x, int y, int a, int w, int h)
        {
            int x1 = x - a / 2 - 1;
            int y1 = y - a / 2 - 1;
            int x2 = x + a / 2;
            int y2 = y + a / 2;
            if (x2 >= w) x2 = w - 1;
            if (y2 >= h) y2 = h - 1;

            int s_left_bottom = x1 < 0 || y1 < 0 ? 0 : integrated[x1, y1];
            int s_left_top = x1 < 0 ? 0 : integrated[x1, y2];
            int s_right_bottom = y1 < 0 ? 0 : integrated[x2, y1];
            int sum = (integrated[x2, y2] + s_left_bottom - s_left_top - s_right_bottom);
            int w_a = x2 - (x1 < 0 ? -1 : x1);
            int h_a = y2 - (y1 < 0 ? -1 : y1);
            return sum / (w_a * h_a);
        }

        private Func<int, int, int> buildTakerWindow(int[,] integrated, int w, int h, int a) => (int x, int y) => takerWindow(integrated, x, y, a, w, h);

        private void Niblack(SourceImage editing_image)
        {
            (int[,] integrated_image, int[,] integrated_pow2) = getIntegratedWithSquare(editing_image.Pixels_matrix);

            double k = editing_image.binarizationData.parametrs;
            int a = editing_image.binarizationData.windowsSize;
            int w = editing_image.width;
            int h = editing_image.height;

            var take = buildTakerWindow(integrated_image, w, h, a);
            var takeSquared = buildTakerWindow(integrated_pow2, w, h, a);

            Parallel.For(0, w, x =>
            {
                for (int y = 0; y < h; y++)
                {
                    int M = take(x, y);
                    int M_of_squared = takeSquared(x, y);

                    int D = M_of_squared - (M * M);
                    double sigma = Math.Sqrt(D);
                    double t = M + k * sigma;

                    int res = toGreyscale(editing_image.Pixels_matrix[x, y]) <= (int)t ? 0 : 255;
                    editing_image.Pixels_matrix[x, y] = new Pixel(editing_image.Pixels_matrix[x, y].A, res, res, res);
                }
            });
        }

        private void Sauvola(SourceImage editing_image)
        {
            (int[,] integrated_image, int[,] integrated_pow2) = getIntegratedWithSquare(editing_image.Pixels_matrix);

            double k = editing_image.binarizationData.parametrs;
            int a = editing_image.binarizationData.windowsSize;
            int w = editing_image.width;
            int h = editing_image.height;

            var take = buildTakerWindow(integrated_image, w, h, a);
            var takeSquared = buildTakerWindow(integrated_pow2, w, h, a);

            Parallel.For(0, w, x =>
            {
                for (int y = 0; y < h; y++)
                {
                    int M = take(x, y);
                    int M_of_squared = takeSquared(x, y);

                    int D = M_of_squared - (M * M);
                    double sigma = Math.Sqrt(D);
                    double t = M * (1 + k * (sigma / 128 - 1));

                    int res = toGreyscale(editing_image.Pixels_matrix[x, y]) <= (int)t ? 0 : 255;
                    editing_image.Pixels_matrix[x, y] = new Pixel(editing_image.Pixels_matrix[x, y].A, res, res, res);
                }
            });
        }

        private void Wolf(SourceImage editing_image)
        {
            (int[,] integrated_image, int[,] integrated_pow2, int min) = getIntegratedWithSquareAndMin(editing_image.Pixels_matrix);

            double param = editing_image.binarizationData.parametrs;
            int a = editing_image.binarizationData.windowsSize;
            int w = editing_image.width;
            int h = editing_image.height;

            var take = buildTakerWindow(integrated_image, w, h, a);
            var takeSquared = buildTakerWindow(integrated_pow2, w, h, a);

            double max_sigma = 0.0d;

            Parallel.For<double>(0, w, () => (0.0d), (x, loop, subtotal) =>
            {
                for (int y = 0; y < h; y++)
                {
                    int M = take(x, y);
                    int M_of_squared = takeSquared(x, y);
                    int D = M_of_squared - (M * M);
                    double sigma = (double)Math.Sqrt(D);

                    if (sigma > subtotal) subtotal = sigma;
                }
                return subtotal;
            },
                (t_max) =>
                {
                    lock (max_sigma as Object)
                    {
                        if (t_max > max_sigma) max_sigma = t_max;
                    }
                }
            );

            Parallel.For(0, w, x =>
            {
                for (int y = 0; y < h; y++)
                {
                    int M = take(x, y);
                    int M_of_squared = takeSquared(x, y);

                    int D = M_of_squared - (M * M);
                    double sigma = Math.Sqrt(D);
                    double t = (1.0d - param) * M + param * min + param * (sigma / max_sigma) * (M - min);

                    int res = toGreyscale(editing_image.Pixels_matrix[x, y]) <= (int)t ? 0 : 255;
                    editing_image.Pixels_matrix[x, y] = new Pixel(editing_image.Pixels_matrix[x, y].A, res, res, res);
                }
            });
        }

        private void Bradley(SourceImage editing_image)
        {
            int[,] integrated_image = getIntegrated(editing_image.Pixels_matrix);

            double k = editing_image.binarizationData.parametrs;
            int a = editing_image.binarizationData.windowsSize;
            int w = editing_image.width;
            int h = editing_image.height;

            var take = buildTakerWindow(integrated_image, w, h, a);

            Parallel.For(0, w, x =>
            {
                for (int y = 0; y < h; y++)
                {
                    int M = take(x, y);
                    double t = M * (1.0d - k);

                    int res = toGreyscale(editing_image.Pixels_matrix[x, y]) < (int)t ? 0 : 255;
                    editing_image.Pixels_matrix[x, y] = new Pixel(editing_image.Pixels_matrix[x, y].A, res, res, res);
                }
            });
        }

        private int[,] getIntegrated(Pixel[,] matrix)
        {
            (int[,] res, _, _) = buildIntegratedImages(matrix);
            return res;
        }

        private (int[,], int[,]) getIntegratedWithSquare(Pixel[,] matrix)
        {
            (int[,] res, int[,] resSquared, _) = buildIntegratedImages(matrix, true);
            return (res, resSquared);
        }

        private (int[,], int[,], int) getIntegratedWithSquareAndMin(Pixel[,] matrix)
        {
            return buildIntegratedImages(matrix, true, true);
        }

        private (int[,], int[,], int) buildIntegratedImages(Pixel[,] matrix, bool includePow2 = false, bool includeMin = false)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);
            int[,] integrated_image = new int[w, h];
            int[,] integrated_pow2 = null;
            int min = 255;

            if (includePow2) integrated_pow2 = new int[w, h];

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int prev_left = x == 0 ? 0 : integrated_image[x - 1, y];
                    int prev_top = y == 0 ? 0 : integrated_image[x, y - 1];
                    int prev_left_top = y == 0 || x == 0 ? 0 : integrated_image[x - 1, y - 1];
                    int g = toGreyscale(matrix[x, y]);

                    integrated_image[x, y] = g + prev_left + prev_top - prev_left_top;

                    if (includePow2)
                    {
                        prev_left = x == 0 ? 0 : integrated_pow2[x - 1, y];
                        prev_top = y == 0 ? 0 : integrated_pow2[x, y - 1];
                        prev_left_top = y == 0 || x == 0 ? 0 : integrated_pow2[x - 1, y - 1];

                        integrated_pow2[x, y] = g * g + prev_left + prev_top - prev_left_top;
                    }

                    if (includeMin)
                    {
                        if (min > g) min = g;
                    }
                }
            }
            return (integrated_image, integrated_pow2, min);
        }


        public void DoActionSpatialFiltering(ImagesProcessing resultIMG)
        {

            resultIMG.pixels_matrix = resultIMG.originals_pixels_matrix.Clone() as Pixel[,];
            switch (resultIMG.spatialFilteringData.type)
            {
                case SpatialFilteringType.None:

                    break;
                case SpatialFilteringType.Linear:
                    Linear(resultIMG);
                    break;
                case SpatialFilteringType.Median:
                    Median(resultIMG);
                    break;
               
            }
            
            DrawResult(resultIMG, resultIMG.pixels_matrix);
        }

        private void Median(ImagesProcessing image)
        {
            int r = image.spatialFilteringData.medianRadius;
            if (r <= 0) return;
            int side = r * 2 + 1;
            int mid = side * side / 2 - 1;
            Pixel[,] pixels = image.pixels_matrix.Clone() as Pixel[,];
            Parallel.For(0, image.width, x =>
            {
                for (int y = 0; y < image.height; y++)
                {
                    int[] Rvalues = new int[side * side];
                    int[] Gvalues = new int[side * side];
                    int[] Bvalues = new int[side * side];

                    for (int i = -r; i <= r; i++)
                    {
                        for (int j = -r; j <= r; j++)
                        {
                            int x_match = x + j;
                            int y_match = y + i;

                            if (x_match < 0)
                            {
                                x_match = -j;
                            }
                            else if (x_match >= image.width)
                            {
                                x_match = x - j;
                            }

                            if (y_match < 0)
                            {
                                y_match = -i;
                            }
                            else if (y_match >= image.height)
                            {
                                y_match = y - i;
                            }

                            Pixel pix = pixels[x_match, y_match];
                            int d = (i + r) * side + (j + r);

                            Rvalues[d] = pix.R;
                            Gvalues[d] = pix.G;
                            Bvalues[d] = pix.B;
                        }
                    }

                    image.pixels_matrix[x, y] = new Pixel(
                        image.pixels_matrix[x, y].A,
                        Utilities.QuickSelect(Rvalues, mid),
                        Utilities.QuickSelect(Gvalues, mid),
                        Utilities.QuickSelect(Bvalues, mid));
                }
            });
        }

        private void Linear(ImagesProcessing image)
        {
            int w_kernel = image.spatialFilteringData.kernel.GetLength(0);
            int h_kernel = image.spatialFilteringData.kernel.GetLength(1);
            if (w_kernel == 0 || h_kernel == 0) return;
            if (w_kernel % 2 == 0) w_kernel--;
            if (h_kernel % 2 == 0) h_kernel--;

            int radius_h = h_kernel / 2;
            int radius_w = w_kernel / 2;

            Pixel[,] pixels = image.pixels_matrix.Clone() as Pixel[,];
            Parallel.For(0, image.width, x =>
            {
                //int y = d / image.width;
                //int x = d % image.width;
                for (int y = 0; y < image.height; y++)
                {
                    double conv_R = 0.0d;
                    double conv_G = 0.0d;
                    double conv_B = 0.0d;

                    for (int i = -radius_h; i <= radius_h; i++)
                    {
                        for (int j = -radius_w; j <= radius_w; j++)
                        {
                            int x_match = x + j;
                            int y_match = y + i;

                            if (x_match < 0)
                            {
                                x_match = -j;
                            }
                            else if (x_match >= image.width)
                            {
                                x_match = x - j;
                            }

                            if (y_match < 0)
                            {
                                y_match = -i;
                            }
                            else if (y_match >= image.height)
                            {
                                y_match = y - i;
                            }

                            Pixel pix = pixels[x_match, y_match];
                            double k = image.spatialFilteringData.kernel[i + radius_h, j + radius_w];
                            conv_R += k * pix.R;
                            conv_G += k * pix.G;
                            conv_B += k * pix.B;
                        }
                    }

                    int R = Utilities.Clamp((int)conv_R, 0, 255);
                    int G = Utilities.Clamp((int)conv_G, 0, 255);
                    int B = Utilities.Clamp((int)conv_B, 0, 255);
                    image.pixels_matrix[x, y] = new Pixel(image.pixels_matrix[x, y].A, R, G, B);
                }
            });
        }
    }
}
