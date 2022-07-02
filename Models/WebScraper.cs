using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using PuppeteerSharp;

namespace CEL_MAI_SITE.Models
{
    public class WebScraper
    {
        static string url = "https://www.binance.com/en/markets";

        /*
        static void GetCryptoCurrency(dynamic names, dynamic prices)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(url);
            //var details = Tuple.Create(htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'css-74g683')]/div[contains(@class, 'css-1ap5wc6')]").ToList(), htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'css-ydcgk2')]/div[contains(@class, 'css-li1e6c')]").ToList());
            names = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'css-74g683')]/div[contains(@class, 'css-1ap5wc6')]").ToList();
            prices = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'css-ydcgk2')]/div[contains(@class, 'css-li1e6c')]").ToList();
            for (int i = 0; i < names.Count - 1; i++)
            {
                Console.WriteLine(names[i].InnerText + " - " + prices[i].InnerText);
            }
        }

        static void GetCryptoCurrencyPrice(string cryptocurrencyname)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(url);
            var names = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'css-74g683')]/div[contains(@class, 'css-1ap5wc6')]").ToList();
            var prices = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'css-ydcgk2')]/div[contains(@class, 'css-li1e6c')]").ToList();
            for (int i = 0; i < names.Count - 1; i++)
            {
                if (cryptocurrencyname.ToLower() == names[i].InnerText.ToLower())
                {
                    Console.WriteLine(names[i].InnerText + " - " + prices[i].InnerText);
                }

            }
        }

        XmlWriterSettings settings = new XmlWriterSettings();
        
        
        XmlWriter coaie = XmlWriter.Create("coins.xml");
            
        */

        

        
        
        
        


    }

    
}
