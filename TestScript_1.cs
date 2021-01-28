using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using GameTVAutomation.Entitiy;
using ConsoleTables;

namespace GameTVAutomation
{
    [TestClass]
    public class TestScript
    {
        [TestMethod]
        public void TestMethod1()
        {
            IWebDriver webDriver = new ChromeDriver();

            webDriver.Navigate().GoToUrl("https://game.tv");
            webDriver.Manage().Window.Maximize();

            List<IWebElement> allElements = webDriver.FindElements(By.XPath("//ul[@class= 'games-list swiper-wrapper']//li/a")).ToList();            
            List<GameDetails> gameList = new List<GameDetails>();

            foreach (var element in allElements)
            {              
                var gamedetail = new GameDetails();
                gamedetail.GameUrl = element.GetAttribute("href");
                gameList.Add(gamedetail);
            }
            
            foreach (var data in gameList)
            {
                var count = 1;
                webDriver.Navigate().GoToUrl(data.GameUrl);
                data.Id = count;
                var name = webDriver.FindElement(By.TagName("h1")).Text;
                string[] arr = name.Trim().Split(null);
                arr = arr.Where((o, i) => i != arr.Length - 1).ToArray();
                data.GameName = string.Join(" ", arr);
                
                data.TournamentsCount = webDriver.FindElement(By.XPath("//span[@class='count-tournaments']")).Text;                
                //Status Code
                var re = (HttpWebRequest)WebRequest.Create(webDriver.Url);
                var response = (HttpWebResponse)re.GetResponse();
                if (response.StatusCode.ToString() == "OK")
                    data.PageStatus = "200";
                else
                    data.PageStatus = "Error";
                count++;
            }
            ConsoleTable.From<GameDetails>(gameList).Write();
            webDriver.Quit();

        }
    }
}
