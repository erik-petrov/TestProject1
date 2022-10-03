using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject1
{
    public class Tests
    {
        protected IWebDriver dr;
        protected Random rnd;
        [SetUp]
        public void Setup()
        {
            var opt = new ChromeOptions();
            opt.AddArgument("window-position=-2000,0");
            opt.AddArgument("--disable-notifications");
            dr = new ChromeDriver(@"C:\Users\kotem\source\repos\TestProject1\", opt);
            dr.Manage().Window.Maximize();
        }
        [TearDown]
        public void Clean()
        {
            dr.Quit();
        }
        [Test]
        public void Comparison()
        {
            dr.Manage().Window.Maximize();
            dr.Navigate().GoToUrl("https://hgdft53.frog.ee/en/1615/pc-components");
            var buttons = dr.FindElements(By.ClassName("add_to_compare"));
            int pressed = 0;
            for (int i = 0; i < buttons.Count; i++)
            {
                if (pressed == 4)
                    break;
                if (buttons[i].Displayed)
                {
                    buttons[i].Click();
                    pressed++;
                }
                dr.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }
            new WebDriverWait(dr, TimeSpan.FromSeconds(10));
            dr.FindElement(By.ClassName("bt_compare")).Click();
        }
        [Test]
        public void Reg()
        {
            dr.Navigate().GoToUrl("https://hgdft53.frog.ee/");
            dr.FindElement(By.XPath("/html/body/div[2]/div[1]/header/div[2]/div/div[8]/div")).Click();
            dr.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            new WebDriverWait(dr, TimeSpan.FromSeconds(10));

            dr.FindElement(By.XPath("/html/body/div[2]/div[1]/header/div[2]/div/div[8]/ul/li[1]/form/div[2]/p[2]/a")).Click();
            dr.FindElement(By.Id("customer-firstname")).SendKeys("psddas");
            dr.FindElement(By.Id("customer-lastname")).SendKeys("rdsaasd");
            dr.FindElement(By.Id("email-create")).SendKeys("iregistrationTest" + rnd.Next(1, 1000000) + "@vgmail.com");
            dr.FindElement(By.Id("passwd-create")).SendKeys("e123123123t");
            dr.FindElement(By.Id("newsletter-tmha")).Click();

            new SelectElement(dr.FindElement(By.XPath("/html/body/div[2]/div[1]/header/div[2]/div/div[8]/ul/li[2]/form/div[1]/div[6]/div/div[1]/select"))).SelectByValue("6");
            new SelectElement(dr.FindElement(By.XPath("/html/body/div[2]/div[1]/header/div[2]/div/div[8]/ul/li[2]/form/div[1]/div[6]/div/div[2]/select"))).SelectByValue("2");
            new SelectElement(dr.FindElement(By.XPath("/html/body/div[2]/div[1]/header/div[2]/div/div[8]/ul/li[2]/form/div[1]/div[6]/div/div[3]/select"))).SelectByValue("2002");
            dr.FindElement(By.XPath("/html/body/div[2]/div[1]/header/div[2]/div/div[8]/ul/li[2]/form/div[2]/p[1]/button")).Click();
            dr.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }
    }
}