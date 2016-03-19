using System;
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

            //var worker = new DataWorker();

            //Act

            //Assert
        }

        [TestMethod]
        public void Sicence2_Model_DataParser2()
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
