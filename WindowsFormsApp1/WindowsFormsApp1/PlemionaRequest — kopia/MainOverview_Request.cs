using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WindowsFormsApp1.Main;

namespace WindowsFormsApp1.PlemionaRequest
{
    public class MainOverview_Request : RequestBase
    {
        public void MakeRequests()
        {
            HttpWebResponse response;

            if (Request_pl169_plemiona_pl(out response))
            {
                response.Close();
            }
        }

        private bool Request_pl169_plemiona_pl(out HttpWebResponse response)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=overview");

                request.KeepAlive = true;
                request.Headers.Set(HttpRequestHeader.CacheControl, "max-age=0");
                request.Headers.Add("sec-ch-ua", @"""Chromium"";v=""94"", ""Google Chrome"";v=""94"", "";Not A Brand"";v=""99""");
                request.Headers.Add("sec-ch-ua-mobile", @"?0");
                request.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                request.Headers.Add("Upgrade-Insecure-Requests", @"1");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.Headers.Add("Sec-Fetch-Site", @"same-origin");
                request.Headers.Add("Sec-Fetch-Mode", @"navigate");
                request.Headers.Add("Sec-Fetch-User", @"?1");
                request.Headers.Add("Sec-Fetch-Dest", @"document");
                request.Referer = "https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=market&mode=exchange";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                //request.Headers.Set(HttpRequestHeader.Cookie, @"cid=742255562; pl_auth=26cc5f5d4fa2:617f99bbd37c960a470915402ef0994216715fdb41f77f23f521897423aad0a9; websocket_available=true; global_village_id={Settings.CurrentVillageID}; popup_pos_unit_popup_spy=384.6000061035156x357.4375; popup_pos_unit_popup_light=384.6000061035156x400.0375061035156; popup_pos_unit_popup_axe=384.6000061035156x445; popup_pos_own_villages=377.8000183105469x401.75; mobile_mapsize=9x9; pomo=1; popup_pos_unit_popup_sword=470.8000183105469x373.6000061035156; popup_pos_unit_popup_spear=399.20001220703125x373.6000061035156; popup_pos_emoji_picker=866.9750366210938x258.08750915527344; popup_pos_group_popup=597.8375244140625x119.70000457763672; popup_pos_unit_popup_ram=384x164; popup_pos_unit_popup_heavy=384.6000061035156x500.5625; popup_pos_unit_popup_marcher=384.6000061035156x462.25; sid=0%3A77828e314fa067c01a875d1bcb6b6bda57adf527bfb6a854d1a554f4f15793815b7ca9c1e6e1bf00acd6a3e212ed32cfcaf44a922d854772c8315298ab8f0c45; ref=start; PHPSESSID=5tsb7dvp9ihqnv0ppavhenmfs0vh0e6frsqagga6bgelgdtn; io=-yvnBJeTKFaqz_SnAT4T");
                //request.Headers.Set(HttpRequestHeader.Cookie, $@"cid=742255562; pl_auth=26cc5f5d4fa2:617f99bbd37c960a470915402ef0994216715fdb41f77f23f521897423aad0a9; websocket_available=true; global_village_id={Settings.CurrentVillageID}; popup_pos_unit_popup_spy=384.6000061035156x357.4375; popup_pos_unit_popup_light=384.6000061035156x400.0375061035156; popup_pos_unit_popup_axe=384.6000061035156x445; popup_pos_own_villages=377.8000183105469x401.75; sid={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("sid").Value}; pomo=1; io={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("io").Value}");
                request.Headers.Set(HttpRequestHeader.Cookie, GetCookieString());

                response = (HttpWebResponse)request.GetResponse();

                string responseBody;

                var encoding = Encoding.GetEncoding(response.CharacterSet);
                using (var responseStream = response.GetResponseStream())
                using (var reader = new StreamReader(responseStream, encoding))
                {
                    responseBody = reader.ReadToEnd();
                }

                string cavRegexGroupName = "CavaleryAmount";
                Regex rgx = new Regex($"<strong data-count=\"light\">(?<{cavRegexGroupName}>.*)<");
                var anavaliableCavlary = rgx.Match(responseBody).Groups[cavRegexGroupName].Value;
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
