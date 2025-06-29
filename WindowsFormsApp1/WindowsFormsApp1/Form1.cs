using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Main;
using WindowsFormsApp1.Pages;
using WindowsFormsApp1.PlemionaRequest;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var gameInfo = new ExchangeAjaxData_Request().MakeRequests();
                var kucyki = gameInfo.game_data.units;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
            }
        }

        public static TimeSpan ZbieractwoTimer = new TimeSpan();
        public static Stopwatch ZbieractwoStopWatch = new Stopwatch();
        public static Stopwatch SendMailStopWatch = new Stopwatch();
        Stopwatch FarmaStopWatch = new Stopwatch();

        private void StartMainLoop_Click(object sender, EventArgs e)
        {
            Task thread1 = new Task(() => 
            {
                //new Dupppa().MethodName();

                var asystentFarmeraPage = new AsystentFarmeraPage();
                var villagesToFarm = new System.Collections.Generic.List<string>
                {
                    "21880",
                    "23332",
                };
                var mainOptions = new MainOptions();

                while (true)
                {
                    try
                    {
                        mainOptions.SpamFarmVillage(FarmaStopWatch, asystentFarmeraPage);

                        if (mainOptions.CheckZbieractwoTimeCondition() && ZbieractwoLv2Checkbox.Checked)
                        {
                            mainOptions.Zbieractwo(ZbieractwoStopWatch, SeleniumDriver.WebDriver);
                        }

                        if (checkBoxFarmByRequest.Checked && (FarmaStopWatch.Elapsed.TotalSeconds == 0 || FarmaStopWatch.Elapsed.Minutes >= 15))
                        //&& /*(Convert.ToInt32(unitStats.MakeRequests().light.count) > 40))*/ asystentFarmeraPage.VerifyCavalryPublic(50))
                        {
                            mainOptions.FarmBarbarianVillages(FarmaStopWatch, asystentFarmeraPage);
                        }
                        Thread.Sleep(TimeSpan.FromSeconds(60));
                    }
                    catch (Exception ee)
                    {
                        mainOptions.SendNotyficationMail();
                        Console.WriteLine(ee);
                    }
                }
            });
            thread1.Start();
        }

        #region MyRegion

        private void button3_Click(object sender, EventArgs e)
        {
            var hh = SeleniumDriver.WebDriver.WindowHandles;
            SeleniumDriver.WebDriver.SwitchTo().Window(hh.First());
            Console.WriteLine(SeleniumDriver.WebDriver.Title);
            StartMainLoopButton.Enabled = true;
            StartDriverButton.Enabled = false;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            var Class1 = new Class1();
            Class1.StartChrome();
        }
    }

    public class MainOptions
    {
        int Counter = 0;
        Stopwatch BurzenieMurówStopWatch = new Stopwatch();

        public void SpamFarmVillage(Stopwatch FarmaStopWatch, AsystentFarmeraPage asystentFarmeraPage)
        {
            if (FarmaStopWatch.Elapsed.Minutes >= 5)
            {
                // village 006
                var villages = asystentFarmeraPage.GetFarmVillages("23332");
                var unita = asystentFarmeraPage.GetUnitsStatisics();
                for (int i = 0; i < (Convert.ToInt32(unita.Light)) - 1; i++)
                {
                    ((IJavaScriptExecutor)SeleniumDriver.WebDriver).ExecuteScript("Accountmanager.farm.sendUnits(this, 23029, 7946)");
                    Thread.Sleep(253);
                }

                // village 003
                villages = asystentFarmeraPage.GetFarmVillages("22492");
                unita = asystentFarmeraPage.GetUnitsStatisics();
                for (int i = 0; i < (Convert.ToInt32(unita.Light)) - 1; i++)
                {
                    ((IJavaScriptExecutor)SeleniumDriver.WebDriver).ExecuteScript("Accountmanager.farm.sendUnits(this, 23486, 7946)");
                    Thread.Sleep(253);
                }

                // village 009
                villages = asystentFarmeraPage.GetFarmVillages("21646");
                unita = asystentFarmeraPage.GetUnitsStatisics();
                for (int i = 0; i < (Convert.ToInt32(unita.Light)) - 1; i++)
                {
                    ((IJavaScriptExecutor)SeleniumDriver.WebDriver).ExecuteScript("Accountmanager.farm.sendUnits(this, 23029, 7946)");
                    Thread.Sleep(253);
                }
            }
        }

        public void FarmBarbarianVillages(Stopwatch FarmaStopWatch, AsystentFarmeraPage asystentFarmeraPage)
        {
            bool burzyciel = false;

            if (BurzenieMurówStopWatch.Elapsed.TotalHours == 4)
            {
                // TODO
                burzyciel = true;
                BurzenieMurówStopWatch.Reset();
                BurzenieMurówStopWatch.Start();
            }

            FarmaStopWatch.Reset();

            // village 003
            var villages = asystentFarmeraPage.GetFarmVillages("22492", take: 60);
            //villages.ForEach(x => Console.Write($"xfsdfsdf {x.WallLevel}"));
            new Farm_Request().FarmGivenVillage("22492", villages, burzyciel);

            // village 005
            villages = asystentFarmeraPage.GetFarmVillages("23880", 60);
            new Farm_Request().FarmGivenVillage("23880", villages, burzyciel);

            // village 006
            villages = asystentFarmeraPage.GetFarmVillages("23332", 60);
            new Farm_Request().FarmGivenVillage("23332", villages, burzyciel);

            // village 007
            villages = asystentFarmeraPage.GetFarmVillages("25125", 60);
            new Farm_Request().FarmGivenVillage("25125", villages, burzyciel);

            // village 009
            villages = asystentFarmeraPage.GetFarmVillages("21646", 60);
            new Farm_Request().FarmGivenVillage("21646", villages, burzyciel);

            // village 011
            villages = asystentFarmeraPage.GetFarmVillages("27049", 60);
            new Farm_Request().FarmGivenVillage("27049", villages, burzyciel);

            // village 012
            villages = asystentFarmeraPage.GetFarmVillages("21606", 60);
            new Farm_Request().FarmGivenVillage("21606", villages, burzyciel);

            // village 013
            villages = asystentFarmeraPage.GetFarmVillages("26832", 60);
            new Farm_Request().FarmGivenVillage("26832", villages, burzyciel);

            // village 014
            villages = asystentFarmeraPage.GetFarmVillages("26457", 60);
            new Farm_Request().FarmGivenVillage("26457", villages, burzyciel);

            // village Capital
            var villages2 = asystentFarmeraPage.GetFarmVillages("21880", 80);
            new Farm_Request().FarmGivenVillage("21880", villages2);
            SeleniumDriver.WebDriver.Url = ($"https://pl169.plemiona.pl/game.php?village=21880&screen=overview");

            Console.WriteLine($"Farm Counter: {Counter++}");
            FarmaStopWatch.Start();

            Form1.SendMailStopWatch.Stop();
            Form1.SendMailStopWatch.Reset();
        }

        public void Zbieractwo(Stopwatch ZbieractwoStopWatch, IWebDriver webDriver)
        {
            while (true)
            {
                BotProtectionChecker.CheckForAntiBotCaptcha();
                webDriver.Navigate().GoToUrl($"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=place&mode=scavenge");
                var timer = ElementsHelper.FindElementSafe(By.XPath("//*[@id='scavenge_screen']//*[text()[contains(.,'Zawodowi')]]/..//*[@class='return-countdown']"));

                if (timer != null || Form1.ZbieractwoTimer.TotalSeconds == 0)
                {
                    try
                    {
                        var hours = Convert.ToInt32(timer.Text.Substring(0, 1), 10);
                        var minutes = Convert.ToInt32(timer.Text.Substring(2, 2)) + 1;
                        var seconds = Convert.ToInt32(timer.Text.Substring(5, 2));
                        Form1.ZbieractwoTimer = new TimeSpan(hours, minutes, seconds);
                        ZbieractwoStopWatch.Start();
                        return;
                    }
                    catch (Exception e)
                    {
                    }
                }

                if (timer == null)
                {
                    this.Zbieractwo_SendUnits();
                }

                Thread.Sleep(3000);
            }
        }

        public bool CheckZbieractwoTimeCondition()
        {
            if (Form1.ZbieractwoStopWatch.Elapsed.TotalSeconds < Form1.ZbieractwoTimer.TotalSeconds)
            {
                Console.WriteLine("Still waiting");
                return false;
            }
            else
            {
                Console.WriteLine("Ready to start new Zbieractwo");
                Form1.ZbieractwoStopWatch.Stop();
                Form1.ZbieractwoStopWatch.Reset();
                return true;
            }
        }

        public void SendNotyficationMail()
        {
            Form1.SendMailStopWatch.Start();

            if (Form1.SendMailStopWatch.Elapsed.TotalMinutes > 10 || Form1.SendMailStopWatch.Elapsed.TotalSeconds == 0)
            {

                try
                {
                    var fromAddress = new System.Net.Mail.MailAddress("januszandrzej332@gmail.com", "From Name");
                    var toAddress = new System.Net.Mail.MailAddress("grossbartosz@gmail.com", "To Name");
                    const string fromPassword = "dupa123@";
                    const string subject = "Subject";
                    const string body = "Body";

                    var smtp = new System.Net.Mail.SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new System.Net.Mail.MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }

                    Form1.SendMailStopWatch.Stop();
                    Form1.SendMailStopWatch.Reset();
                }
                catch (Exception e)
                {
                }
            }
        }

        private void Zbieractwo_SendUnits()
        {
            //var spearCount = 2200;
            //var swordCount = 2000;
            //var ciezki = 160;

            //var FarmingLevel3 = 8;

            //var x = spearCount;
            //int level3_Spear = Convert.ToInt32(x / FarmingLevel3);
            //int level2_Spear = Convert.ToInt32(level3_Spear * 2);
            //int level1_Spear = Convert.ToInt32(level3_Spear * 5);

            //x = swordCount;
            //int level3_Sword = Convert.ToInt32(x / FarmingLevel3);
            //int level2_Sword = Convert.ToInt32(level3_Sword * 2);
            //int level1_Sword = Convert.ToInt32(level3_Sword * 5);

            //x = ciezki;
            //int level3_Axe = Convert.ToInt32(x / FarmingLevel3);
            //int level2_Axe = Convert.ToInt32(level3_Axe * 2);
            //int level1_Axe = Convert.ToInt32(level3_Axe * 5);

            //Console.WriteLine("level3 " + level3_Spear + " " + level3_Sword + " " + level3_Axe);
            //Console.WriteLine("level2 " + level2_Spear + " " + level2_Sword + " " + level2_Axe);
            //Console.WriteLine("lvel1 " + level1_Spear + " " + level1_Sword + " " + level1_Axe);

            //var villageInfo = new GeneralInformation_Request().MakeRequests();

            var spearCount = 370;// Convert.ToInt32(villageInfo.spear.count) - 152;
            var swordCount = 246;// Convert.ToInt32(villageInfo.sword.count) - 76;
            var axeCount = 3100;//Convert.ToInt32(villageInfo.axe.count);
            var lekka = 100;
            //var level1Units;// = new[] { new { level1 = 1, level2 = 1, level3 = 1 } };
            var FarmingLevel3 = 8;
            var FarmingLevel4 = 13;

            var x = spearCount;
            int level4_Spear = Convert.ToInt32(x / 13);
            int level3_Spear = Convert.ToInt32(level4_Spear * 1.5);
            int level2_Spear = Convert.ToInt32(level4_Spear * 3);
            int level1_Spear = Convert.ToInt32(level4_Spear * 7.5);

            x = swordCount;
            int level4_Sword = Convert.ToInt32(x / 13);
            int level3_Sword = Convert.ToInt32(level4_Sword * 1.5);
            int level2_Sword = Convert.ToInt32(level4_Sword * 3);
            int level1_Sword = Convert.ToInt32(level4_Sword * 7.5);

            x = axeCount;
            int level4_Axe = Convert.ToInt32(x / 13);
            int level3_Axe = Convert.ToInt32(level4_Axe * 1.5);
            int level2_Axe = Convert.ToInt32(level4_Axe * 3);
            int level1_Axe = Convert.ToInt32(level4_Axe * 7.5);

            x = lekka;
            int level4_lekka = Convert.ToInt32(x / 13);
            int level3_lekka = Convert.ToInt32(level4_lekka * 1.5);
            int level2_lekka = Convert.ToInt32(level4_lekka * 3);
            int level1_lekka = Convert.ToInt32(level4_lekka * 7.5);

            new Zbieractwo__Request(4).MakeRequests(level4_Spear, level4_Sword, level4_Axe, 0);
            Thread.Sleep(2000);
            new Zbieractwo__Request(3).MakeRequests(level3_Spear, level3_Sword, level3_Axe, 0);
            Thread.Sleep(2000);
            new Zbieractwo__Request(2).MakeRequests(level2_Spear, level2_Sword, level2_Axe, 0);
            Thread.Sleep(2000);
            new Zbieractwo__Request(1).MakeRequests(level1_Spear, level1_Sword, level1_Axe, 0);
        }
    }

    public class Dupppa
    {
        public class Oferta
        {
            public double Salary { get; set; }
            public string Link { get; set; }
        }

        public void MethodName()
        {
            try
            {
                var regex = new Regex(".*:\\s.*-(?<widelki>.*)K:\\s(?<link>https.*)");
                var matches = regex.Matches(File.ReadAllText(@"C:\Users\Bartony\Desktop\Oferty.txt"));

                var oferety = new System.Collections.Generic.List<Oferta>();
                
                foreach (Match match in matches)
                {
                    //Console.WriteLine($"{String.Format("{0:0.##}", match.Groups["widelki"].Value )} {match.Groups["link"].Value}");
                    //Console.WriteLine($"{match.Groups["widelki"].Value} {match.Groups["link"].Value}");

                    oferety.Add(new Oferta
                    {
                        Link = match.Groups["link"].Value,
                        Salary = Convert.ToDouble(Decimal.Parse(match.Groups["widelki"].Value.Substring(0,2))) // Convert.ToDouble(match.Groups["widelki"].Value)
                    });
                }

                oferety = oferety.OrderByDescending(x => x.Salary).ToList();

                foreach (var ofertta in oferety)
                {
                    Console.WriteLine($"{ofertta.Salary} {ofertta.Link}");
                }
            }
            catch (Exception eee)
            {
            }
        }
    }
}
