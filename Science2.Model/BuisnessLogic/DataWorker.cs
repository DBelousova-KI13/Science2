using System;
using System.Collections.Generic;
using System.IO;
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
        public Point3D[][] ComputedMatrix { get; private set; }
        public DataPoint[][] InputedMatrix { get; private set; }
        public bool IsATail { get; set; }
        public List<Point3D> Res { get; set; }
        public List<DataPoint> TestMistakes { get; set; } 
        public List<DataPoint> WhatsWrong = new List<DataPoint>(); 
    

        
        public DataWorker(int step, int squareXLength, int squareYLength, DataStorage dataStorage)
        {
            Step = step;
            SquareXLength = squareXLength;//это стороны
            SquareYLength = squareYLength;
            DataStorage = dataStorage;
            Res = GetResult();
            TestMistakes = new List<DataPoint>();
            TestMistakes = WhatsWrong;
            //int i = 0;
        }


        public void ComputingResult()
        {
            InputedMatrix = DataStorage.TempMatrix;
            int len = InputedMatrix.Length;
            ComputedMatrix = new Point3D[len][];
            for (int i = 0; i < len; i = i + Step)
            {
                for (int j = 0; j < InputedMatrix[i].Length; j ++)
                {
                    //выбор данных квадрата
                    var tempX = 0;
                    var tempY = 0;

                    List<DataPoint> DataForComputation = new List<DataPoint>();
                    List<DataPoint> DataForTailComputation = new List<DataPoint>();

                    while (tempY < SquareYLength)
                    {
                        while (tempX < SquareXLength)
                        {
                            //обход по х
                            DataForComputation.Add(InputedMatrix[i + tempX][j + tempY]);
                            tempX++;
                        }
                        tempY++;
                    }


                    //if (j + Step >= InputedMatrix[i].Length)
                    //{
                    //    //обработка хвостов и выход 
                    //}
                }

                if (i + Step >= len)
                {
                   
                    //обработка хвостов и выход 
                    break;
                    
                }
            }

        }
        
       

        public List<Point3D> GetResult()
        {
            List<Point3D> resultMatrix = new List<Point3D>();
            //math logic comes here

            for (int xCurrentPoint = 0; xCurrentPoint < DataStorage.TempMatrix.GetLength(0); xCurrentPoint += Step)
            {
                for (int yCurrentPoint = 0; yCurrentPoint < DataStorage.TempMatrix.GetLength(0); yCurrentPoint += Step)
                {
                  
                        
                    

                   
                        //1650780 6675102
                        //1651052 6673866

                    var result = GetResultFromSingleSquare(xCurrentPoint, yCurrentPoint);
                    if (result != null)
                    {
                        resultMatrix.Add(result);
                    }
                   
                    //if (yCurrentPoint > DataStorage.TempMatrix.GetLength(0))
                    //{
                    //    IsATail = true;//убрать
                    //}
                }
               
                //if (xCurrentPoint > DataStorage.TempMatrix.GetLength(0))
                //{
                //    IsATail = true;
                //}
            }
            return resultMatrix;
        }

        private Point3D GetResultFromSingleSquare(int x, int y)
        {
            var matrix = DataStorage.TempMatrix;
            var xLength = x + SquareXLength - 1; //длина квадрата складывается с номером текущей точки в основном массиве
            var yLength = y + SquareYLength - 1;
            DataPoint temp = new DataPoint();

          
           // DataPoint[,] pointsInSquare = new DataPoint[SquareXLength, SquareYLength];

            List<DataPoint> list = new List<DataPoint>();

            for (int i = x; i < xLength; i++)
            {
                if (i >= matrix.Length)
                {
                    break;
                }
                for (int j = y; j < yLength; j++)
                {
                    if (j >= matrix[i].Length)
                    {
                        break;
                    }
                    else
                    {
                        //pointsInSquare[i - x, j - y] = matrix[i][j];//out of range  - как сделать обработку хвостов?
                        list.Add(matrix[i][j]);
                       
                            //if ((matrix[i][j].X.CompareTo(16507800) > 0) && (matrix[i][j].X.CompareTo(16510500) < 0))
                            //{
                            //    if ((matrix[i][j].Y.CompareTo(6673866) > 0) && (matrix[i][j].Y.CompareTo(6675102) < 0))
                            //    {
                            //        var s = matrix[i][j].X;
                            //    }
                            //}
                       
                    } 
                }
            }


            //  var list = GetListFromMatrix(pointsInSquare);



            if ((list.Count != 0) && (list[0].Y.CompareTo(6674000) > 0) && (list[0].Y.CompareTo(6676000) < 0) && (list[0].X.CompareTo(16509000) < 0) && (list[0].X.CompareTo(16507000) > 0))
            {
                var Text = new DirectoryInfo(@"c:\inputs\findallpoints").GetFiles().Length;

                StreamWriter writePoints = new StreamWriter(@"c:\inputs\findallpoints\" + Text.ToString() + ".csv");

                foreach (var point in list)
                {
                    if (point != null)
                    {
                        writePoints.WriteLine(point.X.ToString() + " " + point.Y.ToString() + " " + point.Z1.ToString() + " " + point.Z2.ToString());
                    }

                }
                writePoints.Close();


            }


          

            if (list.Count != 0)
            {
                var R = CalculateRZ1Z2(list);

                //decimal min = new decimal(0);
                //decimal max = (decimal) 1;


                //if ((R.Z >= min) && (R.Z <= max))
                //{
                //    foreach (var point in pointsInSquare)
                //    {
                //        if (point != null)
                //        {
                //            if ((point.Z1 != 0) || (point.Z2 != 0) && (!WhatsWrong.Contains(point)))
                //            {
                //                point.R = R.Z;
                //            }
                //            WhatsWrong.Add(point);
                //        }

                //    }
                //}

               
                return R;
            }
            else
            {
                return null;
            }

            


        }

        private Point3D CalculateRZ1Z2(List<DataPoint> list)
        {
            //List<DataPoint> list = GetListFromMatrix(pointsInSquare);//до сих пор вроде норм Оо
            var xAvg = list.Average(i => i.X);//null reference
            var yAvg = list.Average(i => i.Y);
            var z1Avg = list.Average(i => i.Z1);
            var z2Avg = list.Average(i => i.Z2);
            var topList = CalculateTopList(list, z1Avg, z2Avg);
            var botList = CalculateBotList(list, z1Avg, z2Avg);
            //var top = CalculateTop(pointsInSquare, z1Avg, z2Avg);
            //var bot = CalculateBot(pointsInSquare, z1Avg, z2Avg);
            var ratio = CalculateCoefficient(topList, botList);
            return new Point3D(xAvg, yAvg, ratio); //divide by zero
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
            if (result != 0)
            {
                return (decimal)Math.Sqrt((double)result);//не должен возвращать ноль
            }
            else
            {
                return 1;
            }
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

        private decimal CalculateTopList(List<DataPoint> pointsInSquare, decimal z1Avg, decimal z2Avg)
        {
            decimal sum = 0;

            for (int index = 0; index < pointsInSquare.Count; index++)
            {
                var point = pointsInSquare[index];
                sum = sum + (point.Z1 - z1Avg)*(point.Z2 - z2Avg);
            }
            
            return sum;
        }

        private decimal CalculateBotList(List<DataPoint> pointsInSquare, decimal z1Avg, decimal z2Avg)
        {
            decimal result = 0;
            decimal sum1 = 0;
            decimal sum2 = 0;
            for (int index = 0; index < pointsInSquare.Count; index++)
            {
                var point = pointsInSquare[index];
                sum1 += (decimal)(Math.Pow((double)(point.Z1 - z1Avg), 2));
                sum2 += (decimal)(Math.Pow((double)(point.Z2 - z2Avg), 2));
               
            }
            


            result = sum1 * sum2;

          
            return (decimal)Math.Sqrt((double)result);//не должен возвращать ноль
           
        }

        private decimal CalculateCoefficient(decimal top, decimal bot)
        {
            decimal ratio = 0;
            if (bot != 0)
            {
                ratio = top/bot;
            }
            else
            {
                //if (top >= 0)
                //{
                //    ratio = 0;
                //}
                //if (top < 0)
                //{
                //    ratio = -1;
                //}
                ratio = 0;
            }
            return ratio;
        }

        private List<DataPoint> GetListFromMatrix(DataPoint[,] points)
        {
            //var tempList = points.Cast<DataPoint>().ToList();
            List<DataPoint> tempList = new List<DataPoint>();
            for (int i = 0; i < points.GetLength(0); i++)
            {
                for (int j = 0; j < points.GetLength(1); j++)
                {
                    if (points[i, j] != null)
                    {
                        tempList.Add(points[i,j]);
                    }
                    else
                    {
                        int k = 0;
                    }
                    
                }
            }
            var a = tempList;
            return tempList;
        }
    }
}