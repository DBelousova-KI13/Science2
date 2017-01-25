using System.Collections.Generic;
using Science2.Model.Models;

namespace Science2.Model.Interfaces
{
    public interface IDataParser
    {
        List<Point3D> ParseString(string str);
    }
}