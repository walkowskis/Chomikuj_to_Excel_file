using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChomikujToExcel.Utils;

namespace ChomikujToExcel
{
    class Menu
    {
        public static void StartMenu()
        {
            Console.Title = "Chomikuj Crowler";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Obecny url: " + Json_Data.WriteData("url") + "\n\nWprowadz nowy Url lub pomin: ");
            string newUrl = Console.ReadLine();
            if (!string.IsNullOrEmpty(newUrl))
                Json_Data.ModifyData("url", newUrl);
        }
        
    }
}
