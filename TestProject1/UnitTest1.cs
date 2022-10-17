using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace TestProject1
{
    public class Tests
    {
        protected IWebDriver dr;
        protected Random rnd;
        private static readonly log4net.ILog
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [SetUp]
        public void Setup()
        {
            var opt = new ChromeOptions();
            opt.AddArgument("window-position=-2000,0");
            opt.AddArgument("--disable-notifications");
            dr = new ChromeDriver(@"C:\Users\opilane\Source\Repos\TestProject1\", opt);
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
            List<string> items = new List<string>();
            List<IWebElement> buttons = new List<IWebElement>();
            List<string> comparing = new List<string>();
            dr.Manage().Window.Maximize();
            dr.Navigate().GoToUrl("https://hgdft53.frog.ee/en/1615/pc-components");
            var products = dr.FindElements(By.ClassName("ajax_block_product"));
            int count = 0;
            foreach (var item in products)
            {
                if (count == 4)
                    break;
                buttons.Add(item.FindElements(By.ClassName("add_to_compare"))[0]);
                items.Add(item.FindElement(By.ClassName("product-name")).GetAttribute("title"));
                count++;
            }
            int pressed = 0;
            for (int i = 0; i < buttons.Count; i++)
            {
                if (pressed == 4)
                    break;
                if (buttons[i].Displayed)
                {
                    WebDriverWait wait = new WebDriverWait(dr, TimeSpan.FromSeconds(10));
                    buttons[i].Click();
                    pressed++;
                    wait.Until((d) =>
                    {
                        if(dr.FindElements(By.CssSelector("a.add_to_compare.checked")).Count == pressed)
                        {
                            return dr;
                        }
                        return null;
                    });
                    
                }
                dr.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }
            new WebDriverWait(dr, TimeSpan.FromSeconds(10));
            dr.FindElement(By.ClassName("bt_compare")).Click();
            foreach (var item in dr.FindElements(By.ClassName("ajax_block_product")))
            {
                try
                {
                    string itemName = item.FindElement(By.ClassName("product-name")).GetAttribute("title");
                    if (items.Contains(itemName))
                    {
                        items.Remove(itemName);
                    }
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                }
            }
            if (items.Count == 0)
                log.Info("Successfully added to comparison");
            //deleting
            foreach (var item in dr.FindElements(By.ClassName("ajax_block_product")))
            {
                
            }
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