using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Drawing;

namespace NCQ
{
    public class FChrom
    {
        ////div[@aria-label='Thích']
        public IWebDriver driver;
        public static ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
        public static ChromeOptions op = new ChromeOptions();
        Random rnd = new Random();
        public void OpenChrome()
        {
            driverService.HideCommandPromptWindow = true;
            op.AddArguments("--disable-notifications");
            driver = new ChromeDriver(driverService, op);
        }
        public void SetSize(int sizeX, int sizeY)
        {
            driver.Manage().Window.Size = new Size(sizeX, sizeY);
        }
        public void SetFullSize()
        {
            this.driver.Manage().Window.Maximize();
        }
        public void Position(int positionX, int positionY)
        {
            driver.Manage().Window.Position = new Point(positionX, positionY);
        }
        public void Open_Link(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
        public void Slip()
        {
            Actions actions = new Actions(driver);
             actions
                 .KeyDown(Keys.Down)
                 .Build()
                 .Perform();
        }
        public void Wait_For_Javascript()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until((x) =>
            {
                return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
            });
        }
        public void SleepRandom(int timeMin, int timeMax)
        {
            int trd1 = rnd.Next(timeMin, timeMax);
            Thread.Sleep(trd1);
        }
        public void Clik_P(int x, int y)
        {
            Actions actions = new Actions(driver);
            actions
                .MoveByOffset(x, y)
                .Click()
                .Build()
                .Perform(); 
        }






    }
}
