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
        [DataRow("https://www.booking.com/")]
        public void OpenUrl(string url)
        {
            RemoteWebDriver driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl(url);

            var expected = url + "index.ru.html"; //��������� ���� ����� ���� �������� �� ����� �������, ��� ���������� 
            var actual = driver.Url.Substring(0, driver.Url.IndexOf("?")); //�������� ���� ����� (�� ���� �� ����� ������)
            Assert.AreEqual(expected, actual);

            driver.Quit();
        }

        [TestMethod]
        public void RedirectToLoginPage()
        {
            RemoteWebDriver driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl(@"https://www.booking.com/");

            var btnLogin = driver.FindElementById("current_account");
            btnLogin.Click();

            var expected = "https://account.booking.com/sign-in"; //��������� ���� ����� ���� �������� �� ����� �������, ��� ���������� 
            var actual = driver.Url.Substring(0, driver.Url.IndexOf("?")); //�������� ���� ����� (�� ���� �� ����� ������)
            Assert.AreEqual(expected, actual); //��� ����� ��� � ����� �������� (���� ��� ���������� )
            driver.Quit();

        }

        [TestMethod]
        [DataRow("aapopova94@gmail.com", true)]
        [DataRow("aaapopova94@gmail.com", false)]
        public void ConfirmEmail(string myEmail, bool exp)
        {
            RemoteWebDriver driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl(@"https://www.booking.com/");

            var btnLogin = driver.FindElementById("current_account");
            btnLogin.Click();

            var email = driver.FindElementById("username");
            email.Clear();
            email.SendKeys(myEmail);

            var btnNext = driver.FindElementByCssSelector("form button[type='submit']");
            btnNext.Click();

            try
            {

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.ElementExists(By.Id("password")));

            }
            catch (Exception)
            {
                Assert.IsFalse(exp);
            }

            var expected = "https://account.booking.com/sign-in/password"; //��������� ���� ����� ���� �������� �� ����� �������, ��� ���������� 
            var actual = driver.Url.Substring(0, driver.Url.IndexOf("?")); //�������� ���� ����� (�� ���� �� ����� ������)
            Assert.AreEqual(exp, expected == actual); //��� ����� ��� � ����� �������� (���� ��� ���������� )

            driver.Quit();


        }

        [TestMethod]
        [DataRow("aapopova94@gmail.com", "aapopova94", true)]
        [DataRow("aapopova94@gmail.com", "aapopova95", false)]
        public void ConfirmPassword(string myEmail, string myPassword, bool exp)
        {
            RemoteWebDriver driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl(@"https://www.booking.com/");

            var btnLogin = driver.FindElementById("current_account");
            btnLogin.Click();

            //�����-�������� ���� �� ���������� 
            //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //wait.Until(ExpectedConditions.ElementExists(By.Id("username")));

            var email = driver.FindElementById("username");
            email.Clear();
            email.SendKeys(myEmail);

            var btnNext = driver.FindElementByCssSelector("form button[type='submit']");
            btnNext.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.Id("password")));

            var password = driver.FindElementById("password");
            password.Clear();
            password.SendKeys(myPassword);

            btnNext = driver.FindElementByCssSelector("form button[type='submit']");
            btnNext.Click();

            var expected = "https://www.booking.com/index.ru.html"; //��������� ���� ����� ���� �������� �� ����� �������, ��� ���������� 

            try //����� ������� ����� ����������. � ���� ��� ��� - ���� ������
            {
                wait.Until(ExpectedConditions.UrlContains(expected));
            }
            catch (Exception) //� ���� �� ��� - ������������ ��� ����� � �� ��� ��������� ��� ������ � ��� �� ������ ����������
            {
                Assert.IsFalse(exp);
            }


            var actual = driver.Url.Substring(0, driver.Url.IndexOf("?")); //�������� ���� ����� (�� ���� �� ����� ������)
            Assert.AreEqual(exp, expected == actual); //��� ����� ��� � ����� �������� (���� ��� ���������� )
            driver.Quit();

        }
    }
}
