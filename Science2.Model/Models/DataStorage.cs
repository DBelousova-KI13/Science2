using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using Science2.Model.BuisnessLogic;

namespace Science2.Model.Models
{
    public class DataStorage
    {
        public List<Point3D> Points1 { get; private set; }
        public List<Point3D> Points2 { get; private set; }
        public List<DataPoint> DataPoints { get; private set; }
        public DataPoint[][] TempMatrix { get; private set; } 
        public DataPoint[,] DataMatrix { get; private set; }

       

       // public DataPoint[][] DinamicPointsArray = new DataPoint[3][];
      
        public DataStorage(List<DataPoint> points)
        {
            DataPoints = points;
            //FillDataMatrix();
            CreateMatrix();
        }

        public DataStorage(List<Point3D> points1, List<Point3D> points2)
        {
            Points1 = points1;
            Points2 = points2;
            DataPoints = GenerateDataPoints(points1, points2);
            CreateMatrix();
            // FillDataMatrix();
        }

        

        void CreateMatrix()
        {
            int difference = 0;

            var tempPoint = new DataPoint();
            //сколько разных Y
            foreach (var point in DataPoints)
            {
                if (point.Y != tempPoint.Y)
                {
                    difference ++;
                    tempPoint = point;
                }
            }

            DataPoint[][] DinamicPointsArray = new DataPoint[difference][];

           // var PreMatrix = new List<List<DataPoint>>();

            var firstPointY = DataPoints.FirstOrDefault().Y;
            var tempPoints = new List<DataPoint>();

            var poinsToArray = DataPoints.ToArray();


            int whichString = 0;


            for (int index = 0; index < poinsToArray.Length; index++)
            {

                if (poinsToArray[index].Y == firstPointY)
                {
                       tempPoints.Add(poinsToArray[index]);

                }
                else
                {
                    var length = tempPoints.Count;
                    if (whichString > 0)//если это не первая строка
                    {
                        var lastArr = DinamicPointsArray[whichString - 1];
                        for(int n = 0; n <lastArr.Length - 1; n++)
                        {
                           try
                            {
                                if (ComparePoints(lastArr[n].X, lastArr[n + 1].X, tempPoints[n].X) != 0) //здесь нужно все перебирать, пока не найдется первое сходство
                                {
                                    length++; //сдвиг следующей строки
                                }
                            }
                            catch(Exception e)
                            {
                                var exc = e;
                            }
                                                            
                        }
                        DinamicPointsArray[whichString] = new DataPoint[length];
                        for (int i = length - tempPoints.Count; i < DinamicPointsArray[whichString].Length; i++)
                        {
                            DinamicPointsArray[whichString][i] = tempPoints[i];
                            //i++;
                        }
                    }
                    else
                    {
                        DinamicPointsArray[whichString] = new DataPoint[length];
                        for (int i = 0; i < DinamicPointsArray[whichString].Length; i++)
                        {
                            DinamicPointsArray[whichString][i] = tempPoints[i];
                            //i++;
                        }
                    }
                    
                    DinamicPointsArray[whichString] = new DataPoint[length];
                    for (int i = 0; i < DinamicPointsArray[whichString].Length; i++)
                    {
                        DinamicPointsArray[whichString][i] = tempPoints[i];
                        //i++;
                    }
                    whichString++;
                    tempPoints.Clear();
                    firstPointY = poinsToArray[index].Y;
                    tempPoints.Add(poinsToArray[index]);
                  }
                index++;
            }
            var l = tempPoints.Count;
            DinamicPointsArray[whichString] = new DataPoint[l];
            for (int i = 0; i < DinamicPointsArray[whichString].Length; i++)
            {
                DinamicPointsArray[whichString][i] = tempPoints[i];
                //i++;
            }
            //var CountWidth = new List<int>();

            //foreach (var str in DinamicPointsArray)
            //{
            //    CountWidth.Add(str.Length);
            //}

            //var maxLen = CountWidth.Max();

            //DataMatrix = new DataPoint[DinamicPointsArray.Length, maxLen];

        //    DataMatrix = DinamicPointsArray;

            TempMatrix = DinamicPointsArray; //TODO: До сих пор все работает, это матрица, которую впоследствии будем обрабатывать!

         

            //var CountHeight = PreMatrix.Count;

            //foreach (var list in PreMatrix)
            //{
            //    CountWidth.Add(list.Count);
            //}
            //CountWidth.Sort();

            //DataMatrix = new DataPoint[CountWidth.FirstOrDefault(), CountHeight];

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
                if (uniqX.All(i => i.Value != x))//тут все время тормозит как с этим жить
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
                    
                    
                        DataMatrix[j, i] =
                      DataPoints.Single(
                          p =>
                              p.X == uniqX.Single(u => u.ColumnNamber == i).Value &&
                              p.Y == uniqY.Single(u => u.ColumnNamber == j).Value);
                    
                  
                    
                  
                   
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


        public int ComparePoints(decimal a, decimal b, decimal curr)
        {
            if ((a - curr) > (b - curr))
            {
                return 1;
            }
            else { return 0; }

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