using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChomikujToExcel.Utils;

namespace ChomikujToExcel
{
    static class Menu
    {
        static string[] menuItem = { "Set new url", "Run Program", "View Config", "Exit" };
        static int selectedMenuItem = 0;

        public static void StartMenu()
        {
            Console.Title = "Chomikuj Crowler";
            Console.CursorVisible = false;
            while (true)
            {
                ShowMenu();
                SelectMenuItem();
                RunMenuItem();
            }
        }

        static void ShowMenu()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Obecny url: " + Json_Data.WriteData("url"));
            Console.WriteLine();
            for (int i = 0; i < menuItem.Length; i++)
            {
                if (i == selectedMenuItem)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("{0, -20}", menuItem[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.WriteLine(menuItem[i]);
                }

            }
        }

        static void SelectMenuItem()
        {
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedMenuItem = (selectedMenuItem > 0) ? selectedMenuItem - 1 : menuItem.Length - 1;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedMenuItem = (selectedMenuItem + 1) % menuItem.Length;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    selectedMenuItem = menuItem.Length - 1;
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (true);
        }

        static void RunMenuItem()
        {
            switch (selectedMenuItem)
            {
                case 0: Console.Clear(); SetUrl(); break;
                case 1: Console.Clear(); RunProgram(); break;
                case 2: Console.Clear(); InfoOption(); break;
                case 3: Environment.Exit(0); break;
            }
        }

        static void SetUrl()
        {
            Console.Write("Present url: " + Json_Data.WriteData("url"));
            Console.SetCursorPosition(0, 2);
            Console.Write("Write new url: ");
            string newUrl = Console.ReadLine();
            if (!string.IsNullOrEmpty(newUrl))
                Json_Data.ModifyData("url", newUrl);
            Console.Write("Saved!");
            Console.ReadKey();
        }
        static void InfoOption()
        {
            Console.SetCursorPosition(0, 2);
            Console.Write(Json_Data.WriteAllData());
            Console.ReadKey();
        }
        static void RunProgram()
        {
            Console.Clear();
            Start test = new Start();
            test.SetUp();
            Console.Clear();
            Console.WriteLine("Opening website...");
            test.LoginToOwnAccount();
        }
    }
}
