using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEditTools
{

    public enum BinarizationType
    {
        None,
        Gavrilov,
        Otsu,
        Niblack,
        Sauvola,
        Wolf,
        Bradley
    }
    public class BinarizationData : ICloneable
    {
        public BinarizationType type { get; set; } = BinarizationType.None;
        public int windowsSize { get; set; } = 15;
        public double parametrs { get; set; } = 0.0d;
        public Dictionary<BinarizationType, double> defaultParametrs { get; private set; } = new Dictionary<BinarizationType, double>();
        public int typeView
        {
            get => (int)type;
            set => type = (BinarizationType)value;
        }

        public BinarizationData()
        {
            defaultParametrs[BinarizationType.Niblack] = -0.2d;
            defaultParametrs[BinarizationType.Sauvola] = 0.25d;
            defaultParametrs[BinarizationType.Wolf] = 0.5d;
            defaultParametrs[BinarizationType.Bradley] = 0.15d;
        }

        public object Clone()
        {
            BinarizationData data = new BinarizationData();
            data.type = type;
            data.parametrs = parametrs;
            data.windowsSize = windowsSize;
            return data;
        }
    }
}
