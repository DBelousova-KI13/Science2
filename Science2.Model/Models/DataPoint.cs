using System.Collections.Generic;

namespace Science2.Model.Models
{
    public class DataPoint
    {
        public DataPoint()
        {
        }

        public DataPoint(decimal x, decimal y, decimal z1, decimal z2)
        {
            X = x;
            Y = y;
            Z1 = z1;
            Z2 = z2;
        }

        public DataPoint(double x, double y, double z1, double z2)
        {
            X = (decimal)x;
            Y = (decimal)y;
            Z1 = (decimal)z1;
            Z2 = (decimal)z2;
        }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z1 { get; set; }
        public decimal Z2 { get; set; }
        public decimal R { get; set; }
      
    }
}