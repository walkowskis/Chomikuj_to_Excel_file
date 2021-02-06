using ChomikujToExcel.PageObjects;
using ChomikujToExcel.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;

namespace ChomikujToExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.StartMenu();
        }
    }

    class Start
    {
        private IWebDriver driver;

        public void SetUp()
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--silent");
            option.AddArgument("--log-level=3");
            option.AddArgument("headless");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;
            try
            {
                driver = new ChromeDriver(service, option);
            }
            catch (System.Exception ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($@"Chrome Webdriver exception - {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Yellow;            
                Environment.Exit(-1);
            }

            driver.Manage().Window.Maximize();
        }

        public void LoginToOwnAccount()
        {
            MainPage home = new MainPage(driver);
            home.goToPage();
            Task.Delay(2000).Wait();
            home.RodoPopUpClose();
            Task.Delay(2000).Wait();
            home.LoginOnPage(Json_Data.WriteData("login"), Json_Data.WriteData("password"));
            Task.Delay(2000).Wait();
            home.EnterUserPassword(Json_Data.WriteData("userPassword"));
            Task.Delay(2000).Wait();
            home.BookList();
            driver.Quit();
            PushToExcel.ListToExcel(home.bookList, home.bookLinkList);
        }
    }
}
