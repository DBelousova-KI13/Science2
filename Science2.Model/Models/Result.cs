using System.Collections.Generic;

namespace Science2.Model.Models
{
    public class Result
    {
        public List<DataPoint> Points { get; set; }
        public Square UsedSquare { get; set; }
        public decimal Value { get; set; }
        //otherimportantinformation todo:
    }
}