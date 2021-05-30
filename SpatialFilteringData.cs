using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEditTools
{

    public enum SpatialFilteringType
    {
        None,
        Linear,
        Median
    }
    public class SpatialFilteringData : ICloneable
    {
        public SpatialFilteringType type { get; set; } = SpatialFilteringType.None;
        public double[,] kernel { get; set; } = new double[0, 0];
        public int medianRadius { get; set; } = 5;
        public int typeView
        {
            get => (int)type;
            set => type = (SpatialFilteringType)value;
        }

        public string kernelView { get; set; } = "";
        public object Clone()
        {
            SpatialFilteringData data = new SpatialFilteringData();
            data.type = type;
            data.kernel = kernel.Clone() as double[,];
            data.medianRadius = medianRadius;
            data.kernelView = kernelView;
            return data;
        }
    }
}
