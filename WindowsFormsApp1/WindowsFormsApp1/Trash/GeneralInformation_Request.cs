using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using WindowsFormsApp1.Extension;
using WindowsFormsApp1.Main;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.PlemionaRequest
{
    public class GeneralInformation_Request : RequestBase
    {
        public UnitsInfo MakeRequests()
        {
            HttpWebResponse response;
            var myObj = new UnitsInfo();

            if (Request_pl169_plemiona_pl(out response))
            {
                var encoding = Encoding.GetEncoding(response.CharacterSet);
                var villageInfoRgxGroupName = "VillageInfo";
                var responseBody = "";

                using (var responseStream = response.GetResponseStream())
                using (var reader = new StreamReader(responseStream, encoding))
                {
                    responseBody = reader.ReadToEnd();
                }

                var rgx = new Regex($@"VillageOverview\.units\[0\]\s=\s(?<VillageInfo>.*);\s*.*VillageOverview\.units\[1\]");
                var villageInfoText = rgx
                    .Match(responseBody)
                    .Groups[villageInfoRgxGroupName]
                    .Value;
                myObj = (UnitsInfo)new JavaScriptSerializer().Deserialize(villageInfoText, typeof(UnitsInfo));

                response.Close();
            }

            return myObj;
        }

        private bool Request_pl169_plemiona_pl(out HttpWebResponse response)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pl169.plemiona.pl/game.php?village=21880&screen=overview");

                request.KeepAlive = true;
                request.Headers.Add("sec-ch-ua", @"""Chromium"";v=""94"", ""Google Chrome"";v=""94"", "";Not A Brand"";v=""99""");
                request.Headers.Add("sec-ch-ua-mobile", @"?0");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36";
                request.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                request.Accept = "*/*";
                request.Headers.Add("Sec-Fetch-Site", @"same-origin");
                request.Headers.Add("Sec-Fetch-Mode", @"no-cors");
                request.Headers.Add("Sec-Fetch-Dest", @"empty");
                request.Referer = "https://pl169.plemiona.pl/game.php?village=21880&screen=overview";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Headers.Set(HttpRequestHeader.Cookie, @"cid=742255562; websocket_available=true; popup_pos_unit_popup_spy=384.6000061035156x357.4375; popup_pos_unit_popup_light=384.6000061035156x400.0375061035156; popup_pos_unit_popup_axe=384.6000061035156x445; popup_pos_own_villages=377.8000183105469x401.75; pomo=1; popup_pos_unit_popup_sword=470.8000183105469x373.6000061035156; popup_pos_unit_popup_spear=399.20001220703125x373.6000061035156; popup_pos_emoji_picker=866.9750366210938x258.08750915527344; popup_pos_group_popup=597.8375244140625x119.70000457763672; popup_pos_unit_popup_ram=384x164; popup_pos_unit_popup_heavy=384.6000061035156x500.5625; popup_pos_unit_popup_marcher=384.6000061035156x462.25; ref=start; pl_auth=8d635ee62d42:3de3e1e27f236e3b697c0e8c8b5d5622c06d426595133921e7c38999418f4619; popup_pos_edit_color_popup=467.4750183224678x1176.1499938964844; popup_pos_unit_popup_catapult=427x156; PHPSESSID=asv1um5hm51om56nkr1r1ui87f20lmtcvjqir6c6es8dn1ao; popup_pos_unit_popup_snob=817x276.6000061035156; popup_pos_village_targets=542.2000122070312x570.6000198364258; call_resources_checkboxes=wood%7Cstone%7Ciron; sid=0%3A840c10d632cab9dcfb93f974d46e87659f4049713f359056a33e3ad41ee3db1ffcf6f83dd56cf51c5babb04f0167d5e85a762ba40477efedde2943a28c78050b; global_village_id=21880; io=CdDi4NrjS9e8MqB1ArIe");

                response = (HttpWebResponse)request.GetResponse();
                //request.Headers.Set(HttpRequestHeader.Cookie, @"cid=742255562; websocket_available=true; popup_pos_unit_popup_spy=384.6000061035156x357.4375; popup_pos_unit_popup_light=384.6000061035156x400.0375061035156; popup_pos_unit_popup_axe=384.6000061035156x445; popup_pos_own_villages=377.8000183105469x401.75; pomo=1; popup_pos_unit_popup_sword=470.8000183105469x373.6000061035156; popup_pos_unit_popup_spear=399.20001220703125x373.6000061035156; popup_pos_emoji_picker=866.9750366210938x258.08750915527344; popup_pos_group_popup=597.8375244140625x119.70000457763672; popup_pos_unit_popup_ram=384x164; popup_pos_unit_popup_heavy=384.6000061035156x500.5625; popup_pos_unit_popup_marcher=384.6000061035156x462.25; ref=start; pl_auth=8d635ee62d42:3de3e1e27f236e3b697c0e8c8b5d5622c06d426595133921e7c38999418f4619; popup_pos_edit_color_popup=467.4750183224678x1176.1499938964844; popup_pos_unit_popup_catapult=427x156; popup_pos_unit_popup_snob=817x276.6000061035156; popup_pos_village_targets=542.2000122070312x570.6000198364258; call_resources_checkboxes=wood%7Cstone%7Ciron; global_village_id=21880;" 
                //+ $@" sid ={ SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("sid").Value}; "
                //   + $@" PHPSESSID={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("PHPSESSID").Value};"
                //   + $@" io={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("io").Value}");
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
                else return false;
            }
            catch (Exception)
            {
                if (response != null) response.Close();
                return false;
            }

            return true;
        }
    }
}
