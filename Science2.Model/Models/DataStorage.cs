using System;
using System.Collections.Generic;
using System.Linq;

namespace Science2.Model.Models
{
    public class DataStorage
    {
        public List<Point3D> Points1 { get; private set; }
        public List<Point3D> Points2 { get; private set; }
        public List<DataPoint> DataPoints { get; private set; }
        public DataPoint[,] DataMatrix { get; private set; }

        public DataStorage(List<DataPoint> points)
        {
            DataPoints = points;
            FillDataMatrix();
        }

        public DataStorage(List<Point3D> points1, List<Point3D> points2)
        {
            Points1 = points1;
            Points2 = points2;
            DataPoints = GenerateDataPoints(points1, points2);
            FillDataMatrix();
        }

        /// <summary>
        /// Fills DataMatrix to represent values as 2-D array with X and Y equals on lines and tabs
        /// </summary>
        private void FillDataMatrix()
        {
            List<MatrixSortHelper> uniqX = new List<MatrixSortHelper>(); //todo: better implementation
            List<MatrixSortHelper> uniqY = new List<MatrixSortHelper>();
            var xCounter = 0;
            var yCounter = 0;
            foreach (var point in DataPoints)
            {
                var x = point.X;
                var y = point.Y;
                if (uniqX.All(i => i.Value != x))
                {
                    uniqX.Add(new MatrixSortHelper(x, xCounter));
                    xCounter++;
                }
                if (uniqY.All(i => i.Value != y))
                {
                    uniqY.Add(new MatrixSortHelper(y, yCounter));
                    yCounter++;
                }
            }
            var xCount = uniqX.Count;
            var yCount = uniqY.Count;
            uniqX = uniqX.OrderBy(arg => arg).ToList(); //todo: possible mistakes
            uniqY = uniqY.OrderBy(arg => arg).ToList();
            DataMatrix = new DataPoint[uniqX.Count, uniqY.Count];
            for(int i = 0; i < xCount; i++)
                for (int j = 0; j < yCount; j++)
                {
                    DataMatrix[i, j] =
                        DataPoints.Single(
                            p =>
                                p.X == uniqX.Single(u => u.ColumnNamber == i).Value &&
                                p.Y == uniqY.Single(u => u.ColumnNamber == j).Value).Clone();
                }

        }

        private List<DataPoint> GenerateDataPoints(List<Point3D> points1, List<Point3D> points2)
        {
            //question: validation required? for example list1 has 10 elems while list2 has 9, we can map 99 but one would be thrown away
            DataPoints = new List<DataPoint>();
            foreach (var point3D in points1)
            {
                var pair = points2.FirstOrDefault(i => i.X == point3D.X && i.Y == point3D.Y);
                if(pair != null)
                    DataPoints.Add(new DataPoint(){X = pair.X, Y = pair.Y, Z1 = point3D.Z, Z2 = pair.Z});
                //else throw new Exception("no pair were found for");
            }
            throw new NotImplementedException();
        }

        public class MatrixSortHelper : IComparable
        {
            public MatrixSortHelper(decimal value, int columnNamber)
            {
                Value = value;
                ColumnNamber = columnNamber;
            }
            public decimal Value { get; set; }
            public int ColumnNamber { get; set; }
            public int CompareTo(object obj)
            {
                var val = obj as MatrixSortHelper;
                if (val.Value == Value)
                    return 0;
                if (val.Value > Value)
                    return -1;
                else return 1;
            }
        }
    }
}