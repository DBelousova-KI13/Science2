using System;

namespace Science2.Model.Models
{
    [Obsolete]
    public class Square
    {
        public int XStart { get; set; }
        public int YStart { get; set; }
        public int XLength { get; set; }
        public int YLength { get; set; }
        public bool IsInSquare(decimal x, decimal y)
        {
            throw new NotImplementedException();
        }
    }
}