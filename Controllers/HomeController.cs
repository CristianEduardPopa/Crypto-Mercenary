using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using HtmlAgilityPack;
//using PuppeteerSharp;
using System.Net;
using System.Text;
using CEL_MAI_SITE.Models;

namespace ScrapingBeeScraper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        //toata functia asta poate fi facuta cu Selenium/PuppeteerSharp/HtmlAgility (primele 2 pentru site-uri dynamice)
        public async Task<IActionResult> Index()
        {
            string fullUrl = "https://www.binance.com/en/markets";
            List<string> coinLinks = new List<string>();

            var options = new ChromeOptions()
            {
                BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
            };

            options.AddArguments(new List<string>() { "headless", "disable-gpu" });

            var browser = new ChromeDriver(options);
            browser.Navigate().GoToUrl(fullUrl);
            //obtinerea de info se poate face prin xpath sau css
            var links = browser.FindElement(By.XPath("/html/body/div[1]/div/div/main/div/div[2]/div/div/div[2]/div[2]/div/div[2]/div[1]/div/a/div[2]/div"));
            
            //ar trebui un for/foreach/while aici
            coinLinks.Add(links.GetAttribute("href"));

            WriteToCsv(coinLinks);

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        private static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = client.GetStringAsync(fullUrl);
            return await response;
        }

        //HTML Parsing is the action of analyzing and converting a program into an internal format that a runtime environment can run (HTML -> DOM tree)
        private List<string> ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var coinNames = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'css-74g683')]/div[contains(@class, 'css-1ap5wc6')]");
            var coinPrices = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'css-ydcgk2')]/div[contains(@class, 'css-li1e6c')]");

            List<string> cryptoLink = new List<string>();

            foreach (var link in coinNames)
            {
                if (link.FirstChild.Attributes.Count > 0)
                    cryptoLink.Add("https://www.binance.com/en/markets" + link.FirstChild.Attributes[0].Value);
            }
            foreach (var link in coinPrices)
            {
                if (link.FirstChild.Attributes.Count > 0)
                    cryptoLink.Add("https://www.binance.com/en/markets" + link.FirstChild.Attributes[0].Value);
            }
            
            return cryptoLink;

        }

        private void WriteToCsv(List<string> links)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var link in links)
            {
                sb.AppendLine(link);
            }

            System.IO.File.WriteAllText("links.csv", sb.ToString());
        }

        //functie de afisat informatii din csv
        void ReadCsvToTable() => System.IO.File.ReadAllText("links.csv");

    }
}