using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ChomikujToExcel.PageObjects;
using ChomikujToExcel.Utils;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace ChomikujToExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.StartMenu();
            Start test = new Start();
            test.SetUp();
            test.LoginToOwnAccount();
        }
    }

    class Start
    {
        private IWebDriver driver;

        public void SetUp()
        {
            driver = new ChromeDriver();
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
