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
            var matrix = DataStorage.DataMatrix;
            var xCurrentPoint = 0;
            var yCurrentPoint = 0;
            //math logic comes here
            while (xCurrentPoint < DataStorage.DataMatrix.GetLength(0))
            {
                var result = GetResultFromSingleSquare(xCurrentPoint, yCurrentPoint);
                xCurrentPoint += Step;
                yCurrentPoint += Step;
            }
            
            throw new NotImplementedException();
        }

        private decimal GetResultFromSingleSquare(int x, int y)
        {
            throw new NotImplementedException();
            var square = new Square(){XStart = x, YStart = y, XLength = SquareXLength, YLength = SquareYLength};
            //logic to find shit
        }
    }
}