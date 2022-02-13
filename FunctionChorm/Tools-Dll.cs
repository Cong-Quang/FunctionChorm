using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Drawing;
namespace FunctionChorm
{
    public class FunctionChorm
    {
        IWebDriver driver;
        public void OpenChrome()
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            ChromeOptions op = new ChromeOptions();
            op.AddArguments("--disable-notifications");
            driver = new ChromeDriver(driverService, op);
        }
        public void OpenChrome(int sizeX,int sizeY)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            ChromeOptions op = new ChromeOptions();
            op.AddArguments("--disable-notifications");
            driver = new ChromeDriver(driverService, op);
            driver.Manage().Window.Size = new Size(sizeX, sizeY);
        }
        public void OpenChrome(int sizeX, int sizeY,int positionX,int positionY)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            ChromeOptions op = new ChromeOptions();
            op.AddArguments("--disable-notifications");
            driver = new ChromeDriver(driverService, op);
            driver.Manage().Window.Size = new Size(sizeX, sizeY);
            driver.Manage().Window.Position = new Point(positionX, positionY);
        }
        public void login(string usename, string password)
        {
            driver.Navigate().GoToUrl("https://www.facebook.com/");
            driver.FindElement(By.Id("email")).SendKeys(usename);
            driver.FindElement(By.Id("pass")).SendKeys(password + Keys.Return);
        }
        ///     Function    ///
        public void auto_slip(int stop)
        {
            Actions actions = new Actions(driver);
            if (stop ==1)
            {
                actions
                .KeyDown(Keys.Down)
                .Build()
                .Perform();
            }
            else
            {
                return;
            }
        }
        public void auto_slip()
        {
            Actions actions = new Actions(driver);
            actions
                .KeyDown(Keys.Down)
                .Build()
                .Perform();
        }
        public void Like(int Stop)
        {
            var button = driver.FindElement(By.XPath("//div[@aria-label='Thích']"));
            if (button != null)
            {
                if (Stop == 1)
                {
                    Actions actions = new Actions(driver);
                    try
                    {
                        actions.MoveToElement(button).Perform();
                        button.Click();
                    }
                    catch (Exception)
                    {
                        Like();
                    }
                }
            }    
        }
        public void Like()
        {
            Actions actions = new Actions(driver);
            try
            {
                IWebElement button = driver.FindElement(By.XPath("//div[@aria-label='Thích']"));
                actions.MoveToElement(button).Perform();
                button.Click();
            }
            catch (Exception)
            {
                Like();
            }
        }
        public void Love()
        {
            Actions actions = new Actions(driver);
            try
            {
                IWebElement buttonLike = driver.FindElement(By.XPath("//div[@aria-label='Thích']"));
                actions
                     .MoveToElement(buttonLike)
                     .Perform();
                Thread.Sleep(100);
                IWebElement buttonLove = driver.FindElement(By.XPath("//div[@aria-label='Yêu thích']"));
                buttonLove.Click();
                Love();
            }
            catch (Exception)
            {
                Love();
            }
        }
        public void openLink(string link)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.Navigate().GoToUrl(link);
        }
        public void SleepRandom()
        {
            Random rnd = new Random();
            int trd1 = rnd.Next(1000, 10000);
            Thread.Sleep(trd1);
        }
        public void SleepRandom(int timeMin, int timeMax)
        {
            Random rnd = new Random();
            int trd1 = rnd.Next(timeMin, timeMax);
            Thread.Sleep(trd1);
        }
        public void SleepRandom(int timeMinX, int timeMaxX, int timeMinY, int timeMaxY)
        {
            Random rnd = new Random();
            int trd1 = rnd.Next(timeMinX, timeMaxX);
            int trd2 = rnd.Next(timeMinY, timeMaxY);
            int rd = (trd1 + trd2) / 2;
            if (rd < timeMinX)
            {
                rd += timeMinX;
            }
            Thread.Sleep(trd1);
        }
        public void CloseChrom(bool QuitCH)
        {
            if (QuitCH == false)
            {
                driver.Close();
            }
            else
            {
                return;
            }
        }
        public void CloseChrom()
        {
            driver.Close();
        }


        private void goToPoin(IWebElement button)
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)driver;
            je.ExecuteScript("arguments[0].scrollIntoView(true);", button);
        }
    }
}