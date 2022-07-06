using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Net;
using System.Text;
using CEL_MAI_SITE.Models;

namespace CEL_MAI_SITE.Controllers
{
    public class HomeController : Controller
    {
        List<IWebElement> names;
        List<IWebElement> prices;
        List<IWebElement> tags;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            string fullUrl = "https://www.binance.com/en/markets";
            List<string>? coinLinks = new List<string>();
            
            var options = new ChromeOptions()
            {
                BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
            };

            options.AddArguments(new List<string>() { "headless", "disable-gpu" });

            var browser = new ChromeDriver(options);
            browser.Navigate().GoToUrl(fullUrl);
            names = browser.FindElements(By.ClassName("css-1ap5wc6")).ToList();
            prices = browser.FindElements(By.ClassName("css-ydcgk2")).ToList();
            tags = browser.FindElements(By.ClassName("css-1x8dg53")).ToList();

            foreach (var name in names)
            {
                coinLinks.Add(name.Text);
            }
            foreach (var price in prices)
            {
                coinLinks.Add(price.Text);
            }
            foreach (var tag in tags)
            {
                coinLinks.Add(tag.Text);           ;
            }
            
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

        private void WriteToCsv(List<string> links)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var link in links)
            {
                sb.AppendLine(link);
            }

            System.IO.File.WriteAllText("links.csv", sb.ToString());
        }

    }
}
