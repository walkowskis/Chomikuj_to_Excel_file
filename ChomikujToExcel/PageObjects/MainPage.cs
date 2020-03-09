using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using ChomikujToExcel.Utils;
using OpenQA.Selenium.Support.UI;

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
        [FindsBy(How = How.CssSelector, Using = "[title^='następna strona »']")]
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
        }

        public void EnterUserPassword(string password)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(d => PasswordUserField.Displayed);
            PasswordUserField.SendKeys(password);
            Task.Delay(2000).Wait();
            PasswordUserField.SendKeys(Keys.Enter);
        }

        public void goToPage()
        {
            driver.Navigate().GoToUrl(Json_Data.WriteData("url"));
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
                NextPageLink.Click();
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
