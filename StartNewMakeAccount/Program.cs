﻿using OpenQA.Selenium.Chrome;
using StartNewMakeAccount.Models.Email;
using System;
using System.IO;
using System.Linq;


namespace StartNewMakeAccount
{
    class Program
    {
        static bool show = false;
        static bool google = false;

        static string currentProxy;
        

 
        static void Main(string[] args)
        {


            //var randome = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(@"C:\test\my.json"));
            //File.WriteAllLines("gmailAcc.txt", randome);
            
            RandomProxy();


            //  WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(70));
            int i = 0;
            Console.WriteLine("Show ?");
            if (Console.ReadLine() == "y")
                show = true;
            if (show)
            {
                Console.WriteLine("Good ?");
                if (Console.ReadLine() == "y")
                    google = true;
            }
           
            while (true)
            {





                if (i > 5)
                {
                    RandomProxy();
                    i = 0;
                }

                ChromeOptions option = new ChromeOptions();
                option.AddArgument($"--proxy-server={currentProxy}");  //
                option.AddArgument("no-sandbox");
                var user_agent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36";

                option.AddArgument($"--user-agent={user_agent}"); // disabling infobars
                Console.Title = currentProxy;
                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;

                if (!show)
                {
                    option.AddArguments("headless");
                }
                   

                ChromeDriver driver = new ChromeDriver(driverService, option);
               

                // driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 0);
                driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 30);

                var emailProvider = new GenerateEmail();
                Steps ac = new Steps(driver, emailProvider);

                int actions = 0;
                if (ac.MakeLogin())
                {
                    while (ac.Settings() != true && actions < 7)
                    {

                        if (google && actions == 1)
                        {
                            MakeGoogle g = new MakeGoogle();
                            g.Driver = driver;
                            g.MakePin(currentProxy);
                        }

                        try
                        {
                            ac.CheckPage();
                        }
                        catch { }
                        finally
                        {
                            actions++;

                        }
                        Console.WriteLine(actions + "action number ");

                    }
                    driver.Quit();
                    i++;
                }
                else
                {

                    RandomProxy();
                    driver.Quit();
                }
            }







        }

        private static void RandomProxy()
        {
            GetProxy.ProxyReader proxyReader = new GetProxy.ProxyReader();
            var   proxyList = proxyReader.GetList();
            currentProxy = proxyList[new Random().Next(0, proxyList.Count())];

            try
            {
               
                proxyList.Remove(currentProxy);
                File.WriteAllLines(proxyReader.Path, proxyList.ToArray());
              
            }
            catch { }
        }


      

    }
}
