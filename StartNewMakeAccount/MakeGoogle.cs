using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StartNewMakeAccount
{
    public class MakeGoogle
    {
        private const string Path = "piniedProxy.txt";

        public MakeGoogle()
        {
            if (File.Exists(Path))
            {
                this.Disabled = File.ReadAllLines(Path).ToList();
            }
            else
            {
                this.Disabled = new List<string>();
            }
        }

        private List<string> Disabled;

        public RemoteWebDriver Driver { get; set; }


        public void MakePin(string proxy)
        {
            if (this.Disabled.Contains(proxy))
                return;
            try
            {


                int rand = new Random().Next(0, 9);

                ((IJavaScriptExecutor)Driver).ExecuteScript("window.open();");
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());

                Driver.Url = "http://diy.gear.host/";
                File.AppendAllText(Path, proxy + Environment.NewLine);
                Thread.Sleep(new TimeSpan(0, 0, new Random().Next(4, 19)));

                var links = Driver.FindElementsByCssSelector("a");
                foreach (var item in links)
                {
                    Console.WriteLine(item.GetAttribute("href"));
                    try
                    {
                        if (item.GetAttribute("href") !=  null && item.GetAttribute("href").Contains("double"))
                        {
                            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(Driver);
                            action.MoveToElement(item);
                            action.DoubleClick(item);
                        }
                    }

                    catch { }
                     
                }


        
                File.AppendAllText(Path, proxy + Environment.NewLine);
                Driver.FindElementByTagName("body").Click();
                Thread.Sleep(new TimeSpan(0, 0, new Random().Next(4, 19)));
                Driver.FindElementById("google").Click();

            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Driver.SwitchTo().Window(Driver.WindowHandles.First());
            }




        }

    }
}
