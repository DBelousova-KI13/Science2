using System.IO;
//using Microsoft.Office.Interop.Excel;
using Microsoft.Vbe.Interop;

namespace Science2.Model.BuisnessLogic
{
    public static class ExcelToStringParser
    {
        //public static string GetStringFromExcel(string path)
        ////{
        //    var str = "";
            //Application excel = new Application();
            //Workbook wb = excel.Workbooks.Open(path);
            //Worksheet worksheet = (Worksheet)wb.Worksheets[1];
            //var kostil = "";
            //foreach (Range row in worksheet.Rows)
            //{
            //    kostil = "break";
            //    foreach (Range cell in row.Cells)
            //    {
            //        if (cell != null && cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
            //        {
            //            str += cell.Value + " ";
            //            kostil = "done";
            //        }
            //        else
            //        {
            //            if(kostil != "done")
            //                kostil = "break";
            //            break;
            //        }
            //    }
            //    if (kostil == "break")
            //        break;
            //    str += "\n";
        //    }
        //    return str;
        //}
    }
}