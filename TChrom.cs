using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;

namespace NCQ
{
    public class TChrom
    {
        IWebDriver driver;

        public void OpenChrome()
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            ChromeOptions op = new ChromeOptions();
            op.AddArguments("--disable-notifications");
            driver = new ChromeDriver(driverService, op);
            driver.Navigate().GoToUrl("https://www.tiktok.com/en/");
        }
        public void OpenChrome(int sizeX, int sizeY)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            ChromeOptions op = new ChromeOptions();
            op.AddArguments("--disable-notifications");
            driver = new ChromeDriver(driverService, op);
            driver.Manage().Window.Size = new Size(sizeX, sizeY);
        }
        public void OpenChrome(int sizeX, int sizeY, int positionX, int positionY)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            ChromeOptions op = new ChromeOptions();
            op.AddArguments("--disable-notifications");
            driver = new ChromeDriver(driverService, op);
            driver.Manage().Window.Size = new Size(sizeX, sizeY);
            driver.Manage().Window.Position = new Point(positionX, positionY);
        }
        public void CloseChrom()
        {
            driver.Close();
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
        /*=========================================================================================*/

    }
}
