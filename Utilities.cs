using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoEditTools
{
    static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
    public static class Utilities
    {
        public static BitmapImage GetImageSource(Bitmap bitmap)
        {
            if (bitmap == null) return null;

            using MemoryStream memory = new MemoryStream();

            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
            memory.Position = 0;
            BitmapImage bitmapimage = new BitmapImage();
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = memory;
            bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapimage.EndInit();

            return bitmapimage;

        }

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static bool isNatural(string str)
        {
            int i;
            return Int32.TryParse(str, out i) && !str.Contains("-");
        }

        private static int partition<T>(T[] arr, int left, int right, int pivot_index) where T : IComparable<T>
        {
            T pivot_value = arr[pivot_index];
            (arr[pivot_index], arr[right]) = (arr[right], arr[pivot_index]);
            int store_index = left;
            for (int i = left; i < right; i++)
            {
                if (arr[i].CompareTo(pivot_value) < 0)
                {
                    (arr[store_index], arr[i]) = (arr[i], arr[store_index]);
                    store_index++;
                }
            }
            (arr[right], arr[store_index]) = (arr[store_index], arr[right]);
            return store_index;
        }

        private static T quick_select<T>(T[] arr, int left, int right, int k) where T : IComparable<T>
        {
            int pivot_index;
            Random rand = new Random();
            while (true)
            {
                if (left == right) return arr[left];
                pivot_index = rand.Next(left, right + 1); // rand between left and right
                pivot_index = partition(arr, left, right, pivot_index);
                if (k == pivot_index)
                {
                    return arr[k];
                }
                else if (k < pivot_index)
                {
                    right = pivot_index - 1;
                }
                else
                {
                    left = pivot_index + 1;
                }
            }
        }

        public static T QuickSelect<T>(T[] arr, int k) where T : IComparable<T>
        {
            return quick_select(arr, 0, arr.Length - 1, k);
        }
    }
}
