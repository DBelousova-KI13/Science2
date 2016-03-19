using System;
using System.Collections.Generic;
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
            List<DataPoint> pointsInSquare = new List<DataPoint>();
            for (int i = x; i < xLength; i++)
            {
                for (int j = y; j < yLength; j++)
                {
                    pointsInSquare.Add(matrix[x,y]);
                }
            }
            //todo:todo: calculate logic
            return new Point3D() { X = 1, Y = 1, Z = 3 }; //todo: coords for this result?
        }
    }
}