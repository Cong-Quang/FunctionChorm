using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Interactions;

namespace Funcion_Chrome.Browser
{
    /// <summary>
    /// The Chrome class implements IDisposable to enable the use of 'using' for automatic resource cleanup.
    /// </summary>
    public class Chrome : IDisposable
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        /// <summary>
        /// Constructor for the Chrome class.
        /// </summary>
        /// <param name="headless">Indicates whether to run Chrome in headless mode.</param>
        /// <param name="disableNotifications">Indicates whether to disable notifications in Chrome.</param>
        public Chrome(bool headless = false, bool disableNotifications = false)
        {
            // Tạo dịch vụ trình điều khiển Chrome
            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            // Tạo tùy chọn cho trình duyệt Chrome
            ChromeOptions options = new ChromeOptions();
            if (headless)
            {
                options.AddArgument("headless");
            }
            if (disableNotifications)
            {
                options.AddArguments("--disable-notifications");
            }

            // Khởi tạo trình duyệt Chrome và WebDriverWait
            driver = new ChromeDriver(driverService, options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Phương thức điều hướng đến một URL cụ thể.
        /// Method to navigate to a specific URL.
        /// </summary>
        public void NavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Phương thức nhấp chuột vào một phần tử.
        /// Method to click on an element.
        /// </summary>
        public void Click(By by)
        {
            FindElement(by).Click();
        }

        /// <summary>
        /// Phương thức chờ cho đến khi một phần tử xuất hiện.
        /// Method to wait for an element to appear.
        /// </summary>
        public void WaitForElement(By by, int timeoutInSeconds = 10)
        {
            wait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(driver => IsElementPresent(by));
        }

        /// <summary>
        /// Phương thức lấy văn bản từ một phần tử.
        /// Method to get text from an element.
        /// </summary>
        public string GetText(By by)
        {
            return FindElement(by).Text.Trim();
        }

        /// <summary>
        /// Phương thức quay lại trang trước đó.
        /// Method to go back to the previous page.
        /// </summary>
        public void GoBack()
        {
            driver.Navigate().Back();
        }

        /// <summary>
        /// Phương thức điền dữ liệu vào trường input.
        /// Method to fill input field with data.
        /// </summary>
        public void FillInput(By by, string text)
        {
            FindElement(by).SendKeys(text);
        }

        /// <summary>
        /// Phương thức chọn một giá trị từ dropdown.
        /// Method to select a value from a dropdown.
        /// </summary>
        public void SelectDropdown(By by, string value)
        {
            IWebElement dropdown = FindElement(by);
            SelectElement select = new SelectElement(dropdown);
            select.SelectByValue(value);
        }

        /// <summary>
        /// Phương thức kiểm tra xem một phần tử có hiển thị trên trang hay không.
        /// Method to check if an element is present on the page.
        /// </summary>
        public bool IsElementPresent(By by)
        {
            try
            {
                return FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Phương thức lấy danh sách tất cả các lựa chọn từ dropdown.
        /// Method to get a list of all options from a dropdown.
        /// </summary>
        public List<string> GetDropdownOptions(By by)
        {
            IWebElement dropdown = FindElement(by);
            SelectElement select = new SelectElement(dropdown);

            List<string> options = new List<string>();
            foreach (IWebElement option in select.Options)
            {
                options.Add(option.Text.Trim());
            }

            return options;
        }

        /// <summary>
        /// Phương thức lấy một phần tử dựa trên biểu thức By.
        /// Method to get an element based on the By expression.
        /// </summary>
        private IWebElement FindElement(By by)
        {
            return driver.FindElement(by);
        }

        /// <summary>
        /// Phương thức chọn giá trị trong một thẻ <select> dựa trên tên thẻ và giá trị.
        /// Method to select a value in a <select> tag based on tag name and value.
        /// </summary>
        public void SelectOptionInDropdownByNameAndValue(string selectName, string optionValue)
        {
            By selectLocator = By.Name(selectName);
            SelectElement select = new SelectElement(FindElement(selectLocator));
            select.SelectByValue(optionValue);
        }

        /// <summary>
        /// Phương thức GetHtml để lấy đoạn mã html.
        /// Method GetHtml to retrieve HTML source code.
        /// </summary>
        public string GetHtml()
        {
            return driver.PageSource;
        }

        /// <summary>
        /// Phương thức chụp ảnh màn hình và lưu vào một tệp.
        /// Method to capture a screenshot and save it to a file.
        /// </summary>
        [Obsolete]
        public void CaptureScreenshot(string filePath)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
        }

        /// <summary>
        /// Phương thức kiểm tra xem tiêu đề của trang có khớp với giá trị cho trước hay không.
        /// Method to check if the page title matches the given value.
        /// </summary>
        public bool IsPageTitleMatching(string expectedTitle)
        {
            return driver.Title.Equals(expectedTitle, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Phương thức thực hiện scroll trang xuống dưới.
        /// Method to scroll the page down.
        /// </summary>
        public void ScrollDown(int pixelsToScroll)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript($"window.scrollBy(0, {pixelsToScroll});");
        }

        /// <summary>
        /// Phương thức mở một cửa sổ hoặc tab mới và chuyển đến nó.
        /// Method to open a new window or tab and switch to it.
        /// </summary>
        public void OpenNewWindowAndSwitch()
        {
            string currentWindowHandle = driver.CurrentWindowHandle;
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        /// <summary>
        /// Phương thức lấy đường dẫn URL hiện tại.
        /// Method to get the current URL.
        /// </summary>
        public string GetCurrentUrl()
        {
            return driver.Url;
        }

        /// <summary>
        /// Phương thức kiểm tra xem một phần tử có hiển thị trên trang và có sẵn hay không.
        /// Method to check if an element is present and visible on the page.
        /// </summary>
        public bool IsElementVisible(By by)
        {
            try
            {
                IWebElement element = FindElement(by);
                return element.Displayed && element.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Phương thức thực hiện hover (di chuột qua) trên một phần tử.
        /// Method to perform a hover action on an element.
        /// </summary>
        public void HoverOverElement(By by)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(FindElement(by)).Perform();
        }

        /// <summary>
        /// Phương thức chờ cho đến khi trang đã hoàn thành tải.
        /// Method to wait until the page has finished loading.
        /// </summary>
        public void WaitForPageToLoad()
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            wait.Until(driver => jsExecutor.ExecuteScript("return document.readyState").Equals("complete"));
        }

        /// <summary>
        /// Phương thức thực hiện click sử dụng JavaScript.
        /// Method to perform a click using JavaScript.
        /// </summary>
        public void ClickUsingJavaScript(By by)
        {
            IWebElement element = FindElement(by);
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// Phương thức điền dữ liệu vào một form tự động.
        /// Method to automatically fill data into a form.
        /// </summary>
        public void AutoFillForm(Dictionary<By, string> formData)
        {
            foreach (var entry in formData)
            {
                FillInput(entry.Key, entry.Value);
            }
        }

        /// <summary>
        /// Phương thức lấy giá trị của một thuộc tính của một phần tử.
        /// Method to get the value of an attribute of an element.
        /// </summary>
        public string GetAttributeValue(By by, string attributeName)
        {
            return FindElement(by).GetAttribute(attributeName);
        }

        /// <summary>
        /// Phương thức kiểm tra hoặc bỏ chọn một checkbox.
        /// Method to check or uncheck a checkbox.
        /// </summary>
        public void CheckCheckbox(By by, bool isChecked = true)
        {
            IWebElement checkbox = FindElement(by);

            if (checkbox.Selected != isChecked)
            {
                checkbox.Click();
            }
        }

        // <summary>
        /// Phương thức thực hiện drag and drop từ nguồn đến đích.
        /// Method to perform drag and drop from source to target.
        /// </summary>
        public void DragAndDrop(By source, By target)
        {
            IWebElement sourceElement = FindElement(source);
            IWebElement targetElement = FindElement(target);

            Actions actions = new Actions(driver);
            actions.DragAndDrop(sourceElement, targetElement).Perform();
        }

        /// <summary>
        /// Phương thức tìm và chọn một phần tử trong một bảng dựa trên giá trị trong cột và hàng.
        /// Method to find and select an element in a table based on values in a column and row.
        /// </summary>
        public void SelectTableCell(string columnName, string columnValue, string rowValue)
        {
            By cellLocator = By.XPath($"//td[@data-column='{columnName}' and text()='{columnValue}']/ancestor::tr/td[text()='{rowValue}']");
            Click(cellLocator);
        }

        /// <summary>
        /// Phương thức thực hiện click và chờ cho đến khi một phần tử xuất hiện sau sự kiện click.
        /// Method to click and wait for an element to appear after the click event.
        /// </summary>
        public void ClickAndWaitForElement(By by, int timeoutInSeconds = 10)
        {
            Click(by);
            WaitForElement(by, timeoutInSeconds);
        }

        /// <summary>
        /// Phương thức kiểm tra xem một checkbox đã được chọn hay chưa.
        /// Method to check if a checkbox is selected.
        /// </summary>
        public bool IsCheckboxSelected(By by)
        {
            return FindElement(by).Selected;
        }

        /// <summary>
        /// Phương thức lấy thông tin chi tiết của một phần tử.
        /// Method to get detailed information about an element.
        /// </summary>
        public Dictionary<string, string> GetElementDetails(By by)
        {
            IWebElement element = FindElement(by);
            Dictionary<string, string> details = new Dictionary<string, string>
            {
                { "TagName", element.TagName },
                { "Text", element.Text },
                { "Displayed", element.Displayed.ToString() },
                { "Enabled", element.Enabled.ToString() },
                { "Selected", element.Selected.ToString() },
                { "Location", element.Location.ToString() },
                { "Size", element.Size.ToString() },
                { "Attribute_Class", element.GetAttribute("class") },
                // Add more attributes as needed
            };
            return details;
        }

        /// <summary>
        /// Phương thức kiểm tra xem một phần tử có thể chỉnh sửa (editable) hay không.
        /// Method to check if an element is editable.
        /// </summary>
        public bool IsElementEditable(By by)
        {
            return FindElement(by).Enabled && FindElement(by).Displayed;
        }

        /// <summary>
        /// Phương thức trả về danh sách các cửa sổ đang mở.
        /// Method to return a list of currently open windows.
        /// </summary>
        public List<string> GetOpenWindows()
        {
            return new List<string>(driver.WindowHandles);
        }

        /// <summary>
        /// Phương thức mở trang web trong cửa sổ mới và chuyển đến nó.
        /// Method to open a new window with a specific URL and switch to it.
        /// </summary>
        public void OpenUrlInNewWindow(string url)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript($"window.open('{url}', '_blank');");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        /// <summary>
        /// Phương thức kiểm tra xem một phần tử có bị ẩn hay không.
        /// Method to check if an element is hidden.
        /// </summary>
        public bool IsElementHidden(By by)
        {
            IWebElement element = FindElement(by);
            return element.Displayed && element.Size.Height == 0 && element.Size.Width == 0;
        }

        /// <summary>
        /// Phương thức thực hiện right click (chuột phải) trên một phần tử.
        /// Method to perform a right click on an element.
        /// </summary>
        public void RightClick(By by)
        {
            Actions actions = new Actions(driver);
            actions.ContextClick(FindElement(by)).Perform();
        }

        /// <summary>
        /// Phương thức lấy tổng số lựa chọn trong một dropdown.
        /// Method to get the total number of options in a dropdown.
        /// </summary>
        public int GetDropdownOptionsCount(By by)
        {
            IWebElement dropdown = FindElement(by);
            SelectElement select = new SelectElement(dropdown);
            return select.Options.Count;
        }


        /// <summary>
        /// Phương thức Dispose để đóng trình duyệt khi kết thúc.
        /// Dispose method to close the browser when finished.
        /// </summary>
        public void Dispose()
        {
            driver.Quit();
        }
    }
}
