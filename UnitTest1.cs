using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class Autorize01
    {
        [TestMethod]
        public void OpenUrl()
        {
            RemoteWebDriver driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl(@"https://www.booking.com/");
        }

        [TestMethod]
        public void RedirectToLoginPage()
        {
            RemoteWebDriver driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl(@"https://www.booking.com/");

            var btnLogin = driver.FindElementById("current_account");
            btnLogin.Click();

            var expected = "https://account.booking.com/sign-in"; //указываем ожид адрес куда попастть до знака вопроса, как переменную 
            var actual = driver.Url.Substring(0, driver.Url.IndexOf("?")); //получаем факт адрес (от нуля до знака вопрос)
            Assert.AreEqual(expected, actual); //тут можно так а можно ссылками (если без переменных )
            driver.Quit();
            
        }

        [TestMethod]
        public void ConfirmEmail()
        {
            RemoteWebDriver driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl(@"https://www.booking.com/");

            var btnLogin = driver.FindElementById("current_account");
            btnLogin.Click();

            var email = driver.FindElementById("username");
            email.Clear();
            email.SendKeys("aapopova94@gmail.com");

            var btnNext = driver.FindElementByCssSelector("form button[type='submit']");
            btnNext.Click();


        }

        [TestMethod]
        public void ConfirmPassword()
        {
            RemoteWebDriver driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl(@"https://www.booking.com/");

            var btnLogin = driver.FindElementById("current_account");
            btnLogin.Click();

            //пауза-ожидание пока не загрузится 
            //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //wait.Until(ExpectedConditions.ElementExists(By.Id("username")));

            var email = driver.FindElementById("username");
            email.Clear();
            email.SendKeys("aapopova94@gmail.com");

            var btnNext = driver.FindElementByCssSelector("form button[type='submit']");
            btnNext.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.Id("password")));

            var password = driver.FindElementById("password");
            password.Clear();
            password.SendKeys("aapopova94");

            btnNext = driver.FindElementByCssSelector("form button[type='submit']");
            btnNext.Click();

        }

        [TestMethod]
        public void ConfirmIncorrectPassword()
        {
            RemoteWebDriver driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl(@"https://www.booking.com/");

            var btnLogin = driver.FindElementById("current_account");
            btnLogin.Click();

            //пауза-ожидание пока не загрузится 
            //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //wait.Until(ExpectedConditions.ElementExists(By.Id("username")));

            var email = driver.FindElementById("username");
            email.Clear();
            email.SendKeys("aapopova94@gmail.com");

            var btnNext = driver.FindElementByCssSelector("form button[type='submit']");
            btnNext.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.Id("password")));

            var password = driver.FindElementById("password");
            password.Clear();
            password.SendKeys("Test123");

            btnNext = driver.FindElementByCssSelector("form button[type='submit']");
            btnNext.Click();

        }
    }
}
