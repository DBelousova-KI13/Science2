using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Science2.Model.BuisnessLogic;
using Science2.Model.Models;

namespace Science2.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Science2_Model_DataWorker()
        {
            throw new NotImplementedException();
            //Arrange
            var step = 2;
            var sqLen = 2; //both sides
            var points = GetDataPoints();
            //var worker = new DataWorker();

            //Act

            //Assert
        }

        private List<DataPoint> GetDataPoints()
        {
            return new List<DataPoint>()
            {
                new DataPoint(1.0, 7.0, 13.0, 48.0),
                new DataPoint(2.0, 7.0, 14.0, 47.0),
                new DataPoint(3.0, 7.0, 15.0, 46.0),
                new DataPoint(4.0, 7.0, 16.0, 45.0),
                new DataPoint(5.0, 7.0, 17.0, 44.0),
                new DataPoint(6.0, 7.0, 18.0, 43.0),
                new DataPoint(1.0, 8.0, 19.0, 42.0),
                new DataPoint(2.0, 8.0, 20.0, 41.0),
                new DataPoint(3.0, 8.0, 21.0, 40.0),
                new DataPoint(4.0, 8.0, 22.0, 39.0),
                new DataPoint(5.0, 8.0, 23.0, 38.0),
                new DataPoint(6.0, 8.0, 24.0, 37.0),
                new DataPoint(1.0, 9.0, 25.0, 36.0),
                new DataPoint(2.0, 9.0, 26.0, 35.0),
                new DataPoint(3.0, 9.0, 27.0, 34.0),
                new DataPoint(4.0, 9.0, 28.0, 33.0),
                new DataPoint(5.0, 9.0, 29.0, 32.0),
                new DataPoint(6.0, 9.0, 30.0, 31.0),
                new DataPoint(1.0, 10.0, 31.0, 30.0),
                new DataPoint(2.0, 10.0, 32.0, 29.0),
                new DataPoint(3.0, 10.0, 33.0, 28.0),
                new DataPoint(4.0, 10.0, 34.0, 27.0),
                new DataPoint(5.0, 10.0, 35.0, 26.0),
                new DataPoint(6.0, 10.0, 36.0, 25.0),
                new DataPoint(1.0, 11.0, 37.0, 24.0),
                new DataPoint(2.0, 11.0, 38.0, 23.0),
                new DataPoint(3.0, 11.0, 39.0, 22.0),
                new DataPoint(4.0, 11.0, 40.0, 21.0),
                new DataPoint(5.0, 11.0, 41.0, 20.0),
                new DataPoint(6.0, 11.0, 42.0, 19.0),
                new DataPoint(1.0, 12.0, 43.0, 18.0),
                new DataPoint(2.0, 12.0, 44.0, 17.0),
                new DataPoint(3.0, 12.0, 45.0, 16.0),
                new DataPoint(4.0, 12.0, 46.0, 15.0),
                new DataPoint(5.0, 12.0, 47.0, 14.0),
                new DataPoint(6.0, 12.0, 48.0, 13.0)
            };
        }

        private List<Point3D> GetDataPointsResult()
        {
            return new List<Point3D>()
            {
                new Point3D(1.5, 7.5, -1.0),
                new Point3D(3.5, 7.5, -1.0),
                new Point3D(5.5, 7.5, -0.96),
                new Point3D(1.5, 9.5, -0.66),
                new Point3D(3.5, 9.5, 1.0),
                new Point3D(5.5, 9.5, -0.66),
                new Point3D(1.5, 17.0, -0.96),
                new Point3D(3.5, 17.0, -1.0),
                new Point3D(5.5, 17.0, -1.0)
            };
        }
        [TestMethod]
        public void Sicence2_Model_DataParser2_Excel()
        {
            //Arrange
            var path = @"F:\visual studio 2013\Projects\University\Science2\Science2.Tests\FileExamples\D.xls";
            var parser = new DefaultDataParser();
            decimal aaaaa = (decimal) 9.31;
            //Act
            var str = ExcelToStringParser.GetStringFromExcel(path);
            var parsedStr = parser.ParseString(str);

            //Assert
            Assert.AreEqual(parsedStr.Count, 12143);
            Assert.IsTrue(new Point3D()
            {
                X = 16505018,
                Y = 6682434,
                Z = (decimal)8.6
            }.Equals(parsedStr[0]));
            

        }
    }
}
