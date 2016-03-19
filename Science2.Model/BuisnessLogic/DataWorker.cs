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
            var xCurrentPoint = 0;
            var yCurrentPoint = 0;
            //math logic comes here
            while (xCurrentPoint < DataStorage.DataMatrix.GetLength(0))
            {
                var result = GetResultFromSingleSquare(xCurrentPoint, yCurrentPoint); 
                resultMatrix.Add(result);
                xCurrentPoint += Step;
                yCurrentPoint += Step;
            }

            return resultMatrix;
        }

        private Point3D GetResultFromSingleSquare(int x, int y) //question: eqch square = single point (X, Y, Z = result)
        {
            var matrix = DataStorage.DataMatrix;
            var xLength = x + SquareXLength;
            var yLength = y + SquareYLength;
            var amountOfPoints = SquareXLength * SquareYLength;

            var xMatrixLen = xLength - x;
            var yMatrixLen = yLength - y;

            DataPoint[,] pointsInSquare = new DataPoint[xMatrixLen, yMatrixLen];
            for (int i = x; i < xLength; i++)
            {
                for (int j = y; j < yLength; j++)
                {
                    pointsInSquare[i - xMatrixLen, j - yMatrixLen] = (matrix[x,y]);
                }
            }
            
            var R = CalculateRZ1Z2(pointsInSquare);
            //todo:todo: calculate logic
            return new Point3D() { X = 1, Y = 1, Z = 3 }; //todo: coords for this result?
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
            result = (decimal)(Math.Pow((double)sum1,2)*Math.Pow((double)sum2, 2));
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
                list.Add(dataPoint);
            }
            return list;
        }
    }
}