namespace Science2.Model.Models
{
    public class Point3D
    {
        public Point3D()
        {
            
        }

        public Point3D(decimal x, decimal y, decimal z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(double d, double d1, double d2)
        {
            X = (decimal) d;
            Y = (decimal) d1;
            Z = (decimal) d2;
        }

        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }

        public override bool Equals(object obj)
        {
            var value = obj as Point3D;
            if (value == null)
                return false;
            return Equals(obj);
        }

        public bool Equals(Point3D obj)
        {
            return X == obj.X && Y == obj.Y && Z == obj.Z;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return "X: " + X + " Y: " + Y + " Z: " + Z;
        }
    }
}