using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Main;
using WindowsFormsApp1.PlemionaRequest;

namespace WindowsFormsApp1.Pages
{
    public enum LastAttackStatus
    {
        Green = 1,
        Yellow = 2,
        Red = 3
    }

    public class Resources
    {
        public int Wood { get; set; }
        public int Stone { get; set; }
        public int Iron { get; set; }
    }

    public class BarbarianFarmVillage
    {
        public LastAttackStatus LastAttackStatus { get; set; }
        public bool MaxLoot { get; set; }
        public string VillageID { get; set; }
        public string VillageCoordination { get; set; }
        public Resources ResourcesExpected { get; set; }
        public int WallLevel { get; set; }
        public double Distance { get; set; }
    }

    public class AsystentFarmeraPage
    {
        #region MyRegion

        IWebElement Plunder_list => ElementsHelper.FindElementSafe(By.Id("plunder_list"));
        IWebElement Units_home => ElementsHelper.FindElementSafe(By.Id("units_home"));

        public UnitsForAttack GetUnitsStatisics()
        {
            var spear = Units_home.FindElement(By.Id("spear")).Text;
            var sword = Units_home.FindElement(By.Id("sword")).Text;
            var axe = Units_home.FindElement(By.Id("axe")).Text;
            var spy = Units_home.FindElement(By.Id("spy")).Text;
            var light = SeleniumDriver.WebDriver.FindElement(By.XPath("*//td[@id='light']")).Text;
            var units = new UnitsForAttack(spear, sword, axe, sword, light, "0", "0", "0", "0");
            return units;
        }

        public List<BarbarianFarmVillage> GetFarmVillages(string village, int take = 60)
        {
            OpenUrl();

            var listOfFarmVillages = Plunder_list.FindElements(By.XPath($"//*[@id='plunder_list']//tr[contains(@id,'village')]"))
                .Take(take);
            var villages = new List<BarbarianFarmVillage>();

            for (int i = 1; i < listOfFarmVillages.Count() + 1; i++)
            {
                var barbVillage = ElementsHelper.FindElementSafe(By.XPath($"//*[@id='plunder_list']//tr[contains(@id,'village')][{i}]"));
                while (TryAdd(barbVillage))
                {
                    barbVillage = ElementsHelper.FindElementSafe(By.XPath($"//*[@id='plunder_list']//tr[contains(@id,'village')][{i}]"));
                }

            }

            return villages;

            Resources ParseResources(IWebElement element)
            {
                try
                {
                    //var wood = TryConvert(element.FindElement(By.XPath("./span[@class='nowrap'][1]/span[@class='res']")).Text.Replace(".", string.Empty));
                    var wood = Convert.ToInt32(element.FindElement(By.XPath("./span[@class='nowrap'][1]/span[@class='res']")).Text.Replace(".", string.Empty));
                    var stone = Convert.ToInt32(element.FindElement(By.XPath("./span[@class='nowrap'][2]/span[@class='res']")).Text.Replace(".", string.Empty));
                    var iron = Convert.ToInt32(element.FindElement(By.XPath("./span[@class='nowrap'][3]/span[@class='res']")).Text.Replace(".", string.Empty));

                    return new Resources() { Wood = wood, Stone = stone, Iron = iron };
                }
                catch (Exception ee)
                {
                    return new Resources() { Iron = 0, Stone =0, Wood =0};
                }
            }

            int TryConvert(string value)
            {
                try
                {
                    return Convert.ToInt32(value.Replace(".", string.Empty));
                }
                catch (Exception eeee)
                {
                    return 0;
                }
            }

            LastAttackStatus GetLastAttackStatus(string imgUrl)
            {
                if (imgUrl.Contains("green"))
                    return LastAttackStatus.Green;
                if (imgUrl.Contains("yellow"))
                    return LastAttackStatus.Yellow;
                return LastAttackStatus.Red;
            }

            void OpenUrl()
            {
                SeleniumDriver.WebDriver.Url = ($"https://pl169.plemiona.pl/game.php?village={village}&screen=am_farm");
                Settings.H = ElementsHelper.FindElementSafe(By.XPath("//*[@id='content_value']/div[2]/div/form[1]/input")).GetAttribute("value");
            }

            bool TryAdd(IWebElement barbVillage)
            {
                try
                {
                  //  var LastAttackStatuss = GetLastAttackStatus(barbVillage.FindElement(By.XPath("./td[2]/img")).GetAttribute("src"));
                  //  //MaxLoot = barbVillage.FindElement(By.XPath("./td[3]/img")).GetAttribute("src").Contains("1.png"),
                  //  var VillageIDD = new Regex("this, (?<VillageId>.*),")
                  //.Match(barbVillage.FindElement(By.XPath("./td[9]/a")).GetAttribute("onclick"))
                  //.Groups["VillageId"]
                  //.Value;
                  //  var VillageCoordinationn = barbVillage.FindElement(By.XPath("./td[4]/a")).GetAttribute("href");
                  //      //ResourcesExpected = ParseResources(barbVillage.FindElement(By.XPath("./td[6]"))),
                  //      var WallLevell = TryConvert(barbVillage.FindElement(By.XPath("./td[7]")).GetAttribute("innerText"));

                    var barbInfo = new BarbarianFarmVillage()
                    {
                        LastAttackStatus = GetLastAttackStatus(barbVillage.FindElement(By.XPath("./td[2]/img")).GetAttribute("src")),
                        //MaxLoot = barbVillage.FindElement(By.XPath("./td[3]/img")).GetAttribute("src").Contains("1.png"),
                        VillageID = new Regex("this, (?<VillageId>.*),")
                    .Match(barbVillage.FindElement(By.XPath("./td[9]/a")).GetAttribute("onclick"))
                    .Groups["VillageId"]
                    .Value,
                        VillageCoordination = barbVillage.FindElement(By.XPath("./td[4]/a")).GetAttribute("innerText"),
                        ResourcesExpected = ParseResources(barbVillage.FindElement(By.XPath("./td[6]"))),
                        WallLevel = TryConvert(barbVillage.FindElement(By.XPath("./td[7]")).GetAttribute("innerText")),
                        //Distance = Convert.ToInt32(barbVillage.FindElement(By.XPath("./td[8]")).GetAttribute("innerText")),
                };

                    villages.Add(barbInfo);
                }
                catch (Exception ee)
                {
                    return true;
                }

                return false;
            }
        }

        #endregion


        private IWebElement FarmManager => ElementsHelper.FindElementSafe(By.Id("manager_icon_farm"));
        private IWebElement LightCavalry => ElementsHelper.FindElementSafe(By.Id("light"));
        private IWebElement Axeman => ElementsHelper.FindElementSafe(By.Id("axe"));

        public void SendFarmAttack()
        {
            try
            {
                Open();
                SendAttacks();
            }
            catch (Exception e)
            { }
        }

        private void SendAttacks()
        {
            BotProtectionChecker.CheckForAntiBotCaptcha();
            var villages = GetVillagesList().ToList();
            By attackTemplate = By.XPath(".//a[contains(@onclick, 'return Accountmanager.farm.sendUnits')]");

            foreach (var village in villages)
            {
                BotProtectionChecker.CheckForAntiBotCaptcha();
                if (!VerifyCavalry(5)) return;
                ElementsHelper.ClickElement(village.FindElements(attackTemplate).First());
            }
        }

        #region Private Method Section

        private void Open()
        {
            BotProtectionChecker.CheckForAntiBotCaptcha();
            SeleniumDriver.WebDriver.Navigate().GoToUrl("https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=am_farm");
            BotProtectionChecker.CheckForAntiBotCaptcha();
            ElementsHelper.FindElementSafe(By.XPath("//*[@id='content_value']//a[contains(text(),'Pomoc')]"), 3000);
        }

        private IEnumerable<IWebElement> GetVillagesList()
        {
            By selector = By.XPath("//table[@id='plunder_list']/tbody/tr");
            IEnumerable<IWebElement> villages = SeleniumDriver.WebDriver.FindElements(selector).Skip(2);
            return villages;
        }

        public bool VerifyCavalryPublic(int numberOfAttacks)
        {
            Open();
            var lk = Convert.ToInt32(LightCavalry.Text, 10);
            return lk > numberOfAttacks*2;
        }

        private bool VerifyCavalry(int amount = 35)
        {
            var lk = Convert.ToInt32(LightCavalry.Text, 10);
            return lk > amount;
        }

        private bool VerifyAxeman()
        {
            var axeman = Convert.ToInt32(Axeman.Text, 10);
            return axeman > 4 * 35;
        }

        #endregion
    }
}
