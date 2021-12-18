using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    class Program
    {

        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.youtube.com/");
            var acceptbutton = driver.FindElement(By.XPath("/html/body/ytd-app/ytd-consent-bump-v2-lightbox/tp-yt-paper-dialog/div[4]/div[2]/div[5]/div[2]/ytd-button-renderer[2]/a/tp-yt-paper-button"));
            acceptbutton.Click();
            var element = driver.FindElement(By.XPath("/html/body/ytd-app/div/div/ytd-masthead/div[3]/div[2]/ytd-searchbox/form/div[1]/div[1]/input"));

            Thread.Sleep(2000);

            element.SendKeys("webshop");
            element.Submit();
            var filterbutton = driver.FindElement(By.XPath("/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/div/ytd-toggle-button-renderer/a/tp-yt-paper-button"));
            filterbutton.Click();
            var uploadbutton = driver.FindElement(By.XPath("/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/iron-collapse/div/ytd-search-filter-group-renderer[5]/ytd-search-filter-renderer[2]/a/div/yt-formatted-string"));
            uploadbutton.Click();

            Thread.Sleep(2000);


            By elem_video_link = By.CssSelector("ytd-video-renderer.style-scope.ytd-item-section-renderer");
            ReadOnlyCollection<IWebElement> videos = driver.FindElements(elem_video_link);

            /* Go through the Videos List and scrap the same to get the attributes of the videos in the channel */
            foreach (IWebElement video in videos)
            {
                string str_title, str_views, str_uploader, str_url;
                IWebElement elem_video_title = video.FindElement(By.CssSelector("#video-title"));
                str_title = elem_video_title.Text;

                IWebElement elem_video_views = video.FindElement(By.XPath(".//*[@id='metadata-line']/span[1]"));
                str_views = elem_video_views.Text;

                IWebElement elem_video_uploader = video.FindElement(By.Id("channel-info"));
                str_uploader = elem_video_uploader.Text;

                IWebElement elem_video_url = video.FindElement(By.CssSelector("#video-title"));
                str_url = elem_video_url.GetAttribute("href");

                Console.WriteLine("Video Title: " + str_title);
                Console.WriteLine("Video Views: " + str_views);
                Console.WriteLine("Video Uploader: " + str_uploader);
                Console.WriteLine("Video URL: " + str_url);
                Console.WriteLine("\n");


                StringBuilder csvcontent = new StringBuilder();
                csvcontent.AppendLine("Youtube title: " + str_title + "\n " + "Amount of views: " + str_views + "\n " + "Uploader video: "+ str_uploader + "\n " + "Video URL: " + str_url + "\n");
                string csvpath = "D:\\School\\AI\\youtube\\Webscraper\\youtube.csv";
                File.AppendAllText(csvpath, csvcontent.ToString());


            }

        }
    }
}
