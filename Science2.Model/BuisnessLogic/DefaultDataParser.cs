using System;
using System.Collections.Generic;
using System.Linq;
using Science2.Model.Interfaces;
using Science2.Model.Models;

namespace Science2.Model.BuisnessLogic
{
    public class DefaultDataParser : IDataParser
    {
        public List<Point3D> ParseString(string str)
        {
            var strLines = str.Split(new []{'\n'}, StringSplitOptions.RemoveEmptyEntries).ToList();

            //remove headers entry
            strLines.Remove(strLines[0]);

            var list = strLines.Select(line => line.Split(' ')).Select(parsedLine => new Point3D()
            {
                X = decimal.Parse(parsedLine[0]), Y = decimal.Parse(parsedLine[1]), Z = decimal.Parse(parsedLine[2])
            }).ToList();
            return list;
        }
    }
}