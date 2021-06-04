using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEditTools
{
    public class Pixel
    {
       // public byte Cf { get; }
       // public byte Bf { get; }
        public byte A { get; }
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }

        public Pixel(byte a, byte r, byte g, byte b) => (A, R, G, B) = (a, r, g, b);
       // public Pixel(byte bf, byte a, byte r, byte g, byte b) => (Bf, A, R, G, B) = (bf, a, r, g, b);
        public Pixel(int a, int r, int g, int b) => ( A, R, G, B) = ((byte)a, (byte)r, (byte)g, (byte)b);
       // public Pixel(int bf, int a, int r, int g, int b) => (Bf, A, R, G, B) = ((byte)bf, (byte)a, (byte)r, (byte)g, (byte)b);
    }
}
