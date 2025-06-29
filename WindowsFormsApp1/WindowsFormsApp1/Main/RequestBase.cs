using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Main
{
    public class RequestBase
    {
        public static string GetCookieString()
        {
            var gg = SeleniumDriver.WebDriver.Manage().Cookies.AllCookies;

            string cookies = "";

            foreach (var cookie in gg)
            {
                cookies += cookie.Name + "=" + cookie.Value + "; ";
            }

            return cookies;

            return $@"cid=742255562; websocket_available=true; global_village_id={Settings.CurrentVillageID};"+
                $@"popup_pos_unit_popup_snob={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("popup_pos_unit_popup_snob").Value};"+
                $@" popup_pos_village_targets ={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("popup_pos_village_targets").Value};"+ 
                $@"call_resources_checkboxes={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("call_resources_checkboxes").Value};"+ 
                $@" popup_pos_unit_popup_spy=384.6000061035156x357.4375; popup_pos_unit_popup_light=384.6000061035156x400.0375061035156; popup_pos_unit_popup_axe=384.6000061035156x445;"
                + $@" popup_pos_own_villages=377.8000183105469x401.75;"
                + $@" mobile_mapsize=9x9;"
                + $@" pomo=1;"
                + $@" popup_pos_unit_popup_sword={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("popup_pos_unit_popup_sword").Value};"
                + $@" popup_pos_unit_popup_spear={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("popup_pos_unit_popup_spear").Value};"
                + $@" popup_pos_emoji_picker={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("popup_pos_emoji_picker").Value};"
                + $@" popup_pos_group_popup={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("popup_pos_group_popup").Value};"
                + $@" popup_pos_unit_popup_ram={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("popup_pos_unit_popup_ram").Value};"
                + $@" popup_pos_unit_popup_heavy={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("popup_pos_unit_popup_heavy").Value};"
                + $@" popup_pos_unit_popup_marcher={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("popup_pos_unit_popup_marcher").Value};"
                + $@" ref=start; pl_auth={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("pl_auth").Value};"
                + $@" popup_pos_edit_color_popup={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("popup_pos_edit_color_popup").Value};"
                + $@" popup_pos_unit_popup_catapult={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("popup_pos_unit_popup_catapult").Value};"
                + $@" sid={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("sid").Value};"
                + $@" PHPSESSID={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("PHPSESSID").Value};"
                + $@" io={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("io").Value}";
        }
    }
}
