using System;
using System.Collections.Generic;
using ChomikujToExcel.Utils;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChomikujToExcel
{
    class PushToExcel
    {
        public static string[] RemoveExtension(string book)
        {
            string RemoveExtension = book.Remove(book.Length - 5);
            string[] TitleAndAuthor = System.Text.RegularExpressions.Regex.Split(RemoveExtension, " - (?!.* - )");
            return TitleAndAuthor;
        }

        public static void ListToExcel(List<string> List1, List<string> linkList)
        {
            Microsoft.Office.Interop.Excel.Application Excel;
            Microsoft.Office.Interop.Excel._Workbook Excel_Workbook;
            Microsoft.Office.Interop.Excel._Worksheet Excel_Sheet;
            Microsoft.Office.Interop.Excel.Range oRng;
            object misvalue = System.Reflection.Missing.Value;

            //Start Excel and get Application object.
            Excel = new Microsoft.Office.Interop.Excel.Application
            {
                Visible = CommonHelpers.ExcelVisible
            };

            //Get an active workbook.
            Excel_Workbook = (Microsoft.Office.Interop.Excel._Workbook)(Excel.Workbooks.Open(Json_Data.PersonalData("filePath")));
            Excel_Sheet = (Microsoft.Office.Interop.Excel._Worksheet)Excel_Workbook.ActiveSheet;
            Excel_Sheet.AutoFilter.ShowAllData();

            oRng = Excel_Sheet.UsedRange;
            int rowNo = oRng.Rows.Count + 1;

            for (int row = 0; row < List1.Count; ++row)
            {
                string[] TitleAndAuthor = PushToExcel.RemoveExtension(List1[row]);
                Excel_Sheet.Cells[row + rowNo, "A"] = TitleAndAuthor[1];
                Excel_Sheet.Cells[row + rowNo, "B"] = TitleAndAuthor[0];
                Excel_Sheet.Cells[row + rowNo, "C"] = CommonHelpers.Folder;
            }

            for (int row = 0; row < linkList.Count; ++row)
            {
                Excel_Sheet.Hyperlinks.Add(Excel_Sheet.get_Range($"B{row + rowNo}"), linkList[row], "", $"Pobierz plik {List1[row]}");
            }

            Excel.Visible = false;
            Excel.UserControl = false;
            Excel_Workbook.Save();
            Excel_Workbook.Close();
            Excel.Quit();
        }
    }
}
