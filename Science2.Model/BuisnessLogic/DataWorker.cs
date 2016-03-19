using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Science2.Model.Models;

namespace Science2.Model.BuisnessLogic
{
    public class DataWorker
    {
        public int Step {get;set;}
        public int SquareXLength {get; set; }
        public int SquareYLength {get;set;}
        public DataStorage DataStorage {get;set;}

        public DataWorker(int step, int squareXLength, int squareYLength, DataStorage dataStorage)
        {
            Step = step;
            SquareXLength = squareXLength;
            SquareYLength = squareYLength;
            DataStorage = dataStorage;
        }

        public List<Point3D> GetResult()
        {
            List<Point3D> resultMatrix = new List<Point3D>();
            //math logic comes here

            for (int xCurrentPoint = 0; xCurrentPoint < DataStorage.DataMatrix.GetLength(0); )
            {
                for (int yCurrentPoint = 0; yCurrentPoint < DataStorage.DataMatrix.GetLength(1); )
                {
                    var result = GetResultFromSingleSquare(xCurrentPoint, yCurrentPoint);
                    resultMatrix.Add(result);
                    yCurrentPoint += Step;
                }
                xCurrentPoint += Step;
            }
            return resultMatrix;
        }

        private Point3D GetResultFromSingleSquare(int x, int y) //question: eqch square = single point (X, Y, Z = result)
        {
            var matrix = DataStorage.DataMatrix;
            var xLength = x + SquareXLength;
            var yLength = y + SquareYLength;
            var amountOfPoints = SquareXLength * SquareYLength;

            DataPoint[,] pointsInSquare = new DataPoint[SquareXLength, SquareYLength];
            for (int i = x; i < xLength; i++)
            {
                for (int j = y; j < yLength; j++)
                {
                    pointsInSquare[i - x, j - y] = (matrix[i,j]);//.Clone(); //+++++
                }
            }
            
            var R = CalculateRZ1Z2(pointsInSquare);
            return R;
        }

        private Point3D CalculateRZ1Z2(DataPoint[,] pointsInSquare)
        {
            var list = GetListFromMatrix(pointsInSquare);
            var xAvg = list.Average(i => i.X);
            var yAvg = list.Average(i => i.Y);
            var z1Avg = list.Average(i => i.Z1);
            var z2Avg = list.Average(i => i.Z2);
            var top = CalculateTop(pointsInSquare, z1Avg, z2Avg);
            var bot = CalculateBot(pointsInSquare, z1Avg, z2Avg);
            return new Point3D(xAvg, yAvg, top / bot);
        }

        private decimal CalculateBot(DataPoint[,] pointsInSquare, decimal z1Avg, decimal z2Avg)
        {
            decimal result = 0;
            decimal sum1 = 0;
            decimal sum2 = 0;
            foreach (var point in pointsInSquare)
            {
                sum1 += (decimal)(Math.Pow((double)(point.Z1 - z1Avg), 2));
                sum2 += (decimal)(Math.Pow((double)(point.Z2 - z2Avg), 2));
            }
            result = sum1*sum2;
            return (decimal)Math.Sqrt((double)result);
        }

        private decimal CalculateTop(DataPoint[,] pointsInSquare, decimal z1Avg, decimal z2Avg)
        {
            decimal sum = 0;
            for (int i = 0; i < pointsInSquare.GetLength(0); i++)
            {
                decimal laneResult = 0;
                for (int j = 0; j < pointsInSquare.GetLength(1); j++)
                {
                    var a = (pointsInSquare[i, j].Z1 - z1Avg);
                    var b = (pointsInSquare[i, j].Z2 - z2Avg);
                    var c = a*b;
                    laneResult += (pointsInSquare[i, j].Z1 - z1Avg)*(pointsInSquare[i,j].Z2 - z2Avg); //(xi-xavg)*(yi-yavg)
                }
                sum += laneResult;
            }
            return sum;
        }

        private List<DataPoint> GetListFromMatrix(DataPoint[,] points)
        {
            var list = new List<DataPoint>();
            foreach (var dataPoint in points)
            {
                list.Add(dataPoint.Clone());
            }
            return list;
        }
    }
}