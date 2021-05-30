using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoEditTools
{

    public enum CurveChannel
    {
        R,
        G,
        B,
        RGB
    }

    public class CurveData : ICloneable
    {
        public CurveChannel channel { get; set; } = CurveChannel.RGB;

        public List<Point> points { get; set; } = new List<Point>();

        public int[] interpolatedPoints { get; set; } = new int[256];

        public int[] histogramPoints { get; set; } = new int[256];
        public int channelView
        {
            get => (int)channel;
            set => channel = (CurveChannel)value;
        }

        public CurveData()
        {
            points.Add(new Point(0, 0));
            points.Add(new Point(255, 255));
            SetInterpolation();
        }

        // Lagrange interpolation
        public void SetInterpolation()
        {
            double n = points.Count;

            for (int c = 0; c < 256; c++)
            {
                double result = 0;

                //if (points[0] == new Point(0, 0) && points[1] == new Point(255, 255))
                //{
                //    interpolated_points[c] = c;
                //    continue;
                //}

                for (int i = 0; i < n; i++)
                {
                    double term = points[i].Y;
                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                            term = term * (c - points[j].X) /
                                      (points[i].X - points[j].X);
                    }

                    result += term;
                }

                int y = (int)Math.Round(result);
                interpolatedPoints[c] = Utilities.Clamp(y, 0, 255);
            }
        }

        public void SetHistoPoints(Pixel[,] pixels_matrix)
        {
            int[] pix_count = new int[256];

            Parallel.For<int[]>(0, pixels_matrix.GetLength(0), () => (new int[256]), (x, loop, subtotal) =>
            {
                for (int y = 0; y < pixels_matrix.GetLength(1); y++)
                {

                    
                    int a = 0;

                    switch (channel)
                    {
                        case CurveChannel.RGB:
                            a = (pixels_matrix[x, y].R + pixels_matrix[x, y].G + pixels_matrix[x, y].B) / 3;
                            break;
                        case CurveChannel.R:
                            a = pixels_matrix[x, y].R;
                            break;
                        case CurveChannel.G:
                            a = pixels_matrix[x, y].G;
                            break;
                        case CurveChannel.B:
                            a = pixels_matrix[x, y].B;
                            break;
                    }

                    subtotal[a]++;
                }
                return subtotal;
            },
                (arr) =>
                {
                    for (int i = 0; i < 256; i++) Interlocked.Add(ref pix_count[i], arr[i]);
                }
            );

            histogramPoints = pix_count;
        }

        public object Clone()
        {
            CurveData data = new CurveData();
            data.channel = channel;
            data.histogramPoints = histogramPoints;
            data.interpolatedPoints = interpolatedPoints;
            data.points = points;
            return data;
        }
    }
}
