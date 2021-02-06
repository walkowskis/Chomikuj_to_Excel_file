using ChomikujToExcel.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChomikujToExcel.PageObjects
{
    public class MainPage
    {
        private IWebDriver driver;

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "topBarLogin")]
        private IWebElement LoginField;
        [FindsBy(How = How.Id, Using = "topBarPassword")]
        private IWebElement PasswordField;
        [FindsBy(How = How.Id, Using = "Password")]
        private IWebElement PasswordUserField;
        [FindsBy(How = How.CssSelector, Using = ".greenButtonCSS")]
        private IWebElement RodoButton;
        [FindsBy(How = How.XPath, Using = "//a[@title='następna strona »']")]
        private IWebElement NextPageLink;
        [FindsBy(How = How.XPath, Using = ".//div[contains(@class,'filename txt')]/h3/a")]
        public IList<IWebElement> Lists { get; set; }

        public List<string> bookList = new List<string>();
        public List<string> bookLinkList = new List<string>();

        public void RodoPopUpClose()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(d => RodoButton.Displayed);
                string title = (string)js.ExecuteScript("arguments[0].click();", RodoButton);
                Console.WriteLine("PopUp RODO closed.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void LoginOnPage(string login, string password)
        {
            LoginField.SendKeys(login);
            PasswordField.SendKeys(password);
            Task.Delay(500).Wait();
            PasswordField.SendKeys(Keys.Enter);
            Console.WriteLine("Login.");
        }

        public void EnterUserPassword(string password)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(d => PasswordUserField.Displayed);
            PasswordUserField.SendKeys(password);
            Task.Delay(2000).Wait();
            PasswordUserField.SendKeys(Keys.Enter);
            Console.WriteLine("User account login.");
            Console.Clear();
        }

        public void goToPage()
        {
            driver.Navigate().GoToUrl(Json_Data.WriteData("url"));
            Console.WriteLine($@"Open {Json_Data.WriteData("url")} website.");
        }

        public List<string> BookList()
        {
            do
            {
                foreach (IWebElement List in Lists)
                {
                    try
                    {
                        string content = List.Text;
                        bookList.Add(content);
                        bookLinkList.Add(List.GetAttribute("href"));
                        Console.WriteLine($@"Adding {content} item.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                NextPage();
                Task.Delay(2000).Wait();

            }
            while (IsElementPresent(NextPageLink));
            return bookList;
        }

        public void NextPage()
        {
            try
            {
                if(NextPageLink.Displayed)
                {
                    NextPageLink.Click();
                    Console.WriteLine("Go to the next page.");
                }
                else
                {
                    Console.WriteLine("End of pages.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("End of folder: {0}", e.Message);
            }
        }

        bool IsElementPresent(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
