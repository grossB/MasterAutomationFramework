using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1.Main;

namespace WindowsFormsApp1.PlemionaRequest
{
    public class FarmEnemyVillage : RequestBase
    {
        public FarmVillage VillageToFarm { get; set; } = new FarmVillage(21731, 363, 549);

        public void EnemyVilageSendAttack()
        {
            string responseBody = "";
            //int x = 360; int y = 549; 21731
            //int x = 363; int y = 549; 21474
            // template_id=1409 ma 1 szarego i 30 lk, 
            // sword =0& axe=0& archer=0& spy=1& light=30& marcher=0& heavy=0& ram=0& catapult=0& knight=0& snob=0&

            List<FarmVillage> villageToPlunder = new List<FarmVillage>()
            {
                new FarmVillage(21731, 363, 549),
                new FarmVillage(21474, 360, 549),
                //new FarmVillage(23316, 366, 555)
            };

            foreach (var item in villageToPlunder)
            {
                VillageToFarm = item;

                try
                {
                    HttpWebResponse response3;
                    if (Target_21474_Request(out response3))
                    {
                        response3.Close();
                    }

                    HttpWebResponse response2;

                    if (Command_And_Target(out response2))
                    {
                        response2.Close();
                    }

                    HttpWebResponse response;

                    if (Confirm_Request(out response, out responseBody, VillageToFarm.villageX, VillageToFarm.villageY))
                    {
                        response.Close();
                    }

                    string chBodyAttributRgxGroupName = "chBodyAttribute";
                    var rgx = new System.Text.RegularExpressions.Regex($@"name=\\""ch\\"" value=\\""(?<{chBodyAttributRgxGroupName}>.*?)(\\"")");
                    var hh = rgx.Match(responseBody);
                    var chBodyAttribute = rgx.Match(responseBody).Groups[chBodyAttributRgxGroupName].Value;

                    HttpWebResponse response4;

                    if (PopupCommand(out response4, chBodyAttribute, VillageToFarm.villageX, VillageToFarm.villageY, lk: 30))
                    {
                        response4.Close();
                    }
                }
                catch (Exception e)
                {
                } 
            }
        }

        private bool Target_21474_Request(out HttpWebResponse response)
        {
            response = null;

            try
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{"https://"}pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map&ajax=map_info&source={Settings.CurrentVillageID}&target={VillageId}&&");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map&ajax=map_info&source={Settings.CurrentVillageID}&target={VillageToFarm.VillageId}&&");
                request.ProtocolVersion = HttpVersion.Version11;
                request.KeepAlive = true;
                //request.Connection = "keep-alive";
                //request.Headers.Add("Connection", "keep-alive");
                //request.Headers["Keep-Alive"] = "timeout=15, max=100";

                request.Headers.Add("sec-ch-ua", @"""Chromium"";v=""94"", ""Google Chrome"";v=""94"", "";Not A Brand"";v=""99""");
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
                request.Headers.Add("sec-ch-ua-mobile", @"?0");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36";
                request.Headers.Add("TribalWars-Ajax", @"1");
                request.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                request.Headers.Add("Sec-Fetch-Site", @"same-origin");
                request.Headers.Add("Sec-Fetch-Mode", @"cors");
                request.Headers.Add("Sec-Fetch-Dest", @"empty");
                request.Referer = "https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Headers.Set(HttpRequestHeader.Cookie, GetCookieString());

                response = (HttpWebResponse)request.GetResponse();

                string responseBody = "";

                var encoding = Encoding.GetEncoding(response.CharacterSet);
                using (var responseStream = response.GetResponseStream())
                using (var reader = new StreamReader(responseStream, encoding))
                {
                    responseBody = reader.ReadToEnd();
                }

                Console.WriteLine(responseBody);
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
                else return false;
            }
            catch (Exception ee)
            {
                if (response != null) response.Close();
                return false;
            }

            return true;
        }

        private bool Command_And_Target(out HttpWebResponse response)
        {
            response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=place&ajax=command&target={VillageToFarm.VillageId}");
                request.ProtocolVersion = HttpVersion.Version10;
                request.KeepAlive = true;

                request.Headers.Add("sec-ch-ua", @"""Chromium"";v=""94"", ""Google Chrome"";v=""94"", "";Not A Brand"";v=""99""");
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
                request.Headers.Add("sec-ch-ua-mobile", @"?0");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36";
                request.Headers.Add("TribalWars-Ajax", @"1");
                request.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                request.Headers.Add("Sec-Fetch-Site", @"same-origin");
                request.Headers.Add("Sec-Fetch-Mode", @"cors");
                request.Headers.Add("Sec-Fetch-Dest", @"empty");
                request.Referer = "https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                //request.Headers.Set(HttpRequestHeader.Cookie, @"cid=742255562; websocket_available=true; global_village_id={Settings.CurrentVillageID}; popup_pos_unit_popup_spy=384.6000061035156x357.4375; popup_pos_unit_popup_light=384.6000061035156x400.0375061035156; popup_pos_unit_popup_axe=384.6000061035156x445; popup_pos_own_villages=377.8000183105469x401.75; mobile_mapsize=9x9; pomo=1; popup_pos_unit_popup_sword=470.8000183105469x373.6000061035156; popup_pos_unit_popup_spear=399.20001220703125x373.6000061035156; popup_pos_emoji_picker=866.9750366210938x258.08750915527344; popup_pos_group_popup=597.8375244140625x119.70000457763672; popup_pos_unit_popup_ram=384x164; popup_pos_unit_popup_heavy=384.6000061035156x500.5625; popup_pos_unit_popup_marcher=384.6000061035156x462.25; ref=start; pl_auth=8d635ee62d42:3de3e1e27f236e3b697c0e8c8b5d5622c06d426595133921e7c38999418f4619; popup_pos_edit_color_popup=467.4750183224678x1176.1499938964844; popup_pos_unit_popup_catapult=427x156; sid=0%3A13a65685e62abc156d9d4c2872eae7cfb2ea08e253ec5f7138f48167291f2ea04598b49858939b9570c9db9449a7f742add338d2745d164a9eb9f6ed5131ae26; PHPSESSID=asv1um5hm51om56nkr1r1ui87f20lmtcvjqir6c6es8dn1ao; io=6b07hY62zbzKlWoAEYRY");
                request.Headers.Set(HttpRequestHeader.Cookie, GetCookieString());
                //request.KeepAlive = true;

                //var sp = request.ServicePoint;
                //var prop = sp.GetType().GetProperty("HttpBehaviour", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //prop.SetValue(sp, (byte)0, null);

                response = (HttpWebResponse)request.GetResponse();
                string responseBody = "";

                var encoding = Encoding.GetEncoding(response.CharacterSet);
                using (var responseStream = response.GetResponseStream())
                using (var reader = new StreamReader(responseStream, encoding))
                {
                    responseBody = reader.ReadToEnd();
                }
                Console.WriteLine(responseBody);
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

        private bool Confirm_Request(out HttpWebResponse response, out string responseBody, int Villagex, int villageY)
        {
            response = null;
            responseBody = "";
            //SeleniumDriver.WebDriver.Navigate().Refresh();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=place&ajax=confirm");
                request.ProtocolVersion = HttpVersion.Version10;
                request.KeepAlive = true;

                request.Headers.Add("sec-ch-ua", @"""Chromium"";v=""94"", ""Google Chrome"";v=""94"", "";Not A Brand"";v=""99""");
                request.Headers.Add("sec-ch-ua-mobile", @"?0");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
                request.Headers.Add("TribalWars-Ajax", @"1");
                request.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                request.Headers.Add("Origin", @"https://pl169.plemiona.pl");
                request.Headers.Add("Sec-Fetch-Site", @"same-origin");
                request.Headers.Add("Sec-Fetch-Mode", @"cors");
                request.Headers.Add("Sec-Fetch-Dest", @"empty");
                request.Referer = "https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Headers.Set(HttpRequestHeader.Cookie, GetCookieString());

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                string body = $@"8548d03bfd026884ea619d=55fc31258548d0&template_id={VillageToFarm.templateId}&source_village={Settings.CurrentVillageID}&spear=0&sword=0&axe=0&archer=0&spy=1&light={VillageToFarm.lekkaKawaleria}&marcher=0&heavy=0&ram=0&catapult=0&knight=0&snob=0&x={VillageToFarm.villageX}&y={villageY}&input=&attack=l&h=1da00794";

                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();

                response = (HttpWebResponse)request.GetResponse();

                var encoding = Encoding.GetEncoding(response.CharacterSet);
                using (var responseStream = response.GetResponseStream())
                using (var reader = new StreamReader(responseStream, encoding))
                {
                    responseBody = reader.ReadToEnd();
                }

                Console.WriteLine(responseBody);
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

        private bool PopupCommand(out HttpWebResponse response, string ch, int x, int y, int lk = 30)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=place&ajaxaction=popup_command");
                request.ProtocolVersion = HttpVersion.Version10;
                request.KeepAlive = true;

                request.Headers.Add("sec-ch-ua", @"""Chromium"";v=""94"", ""Google Chrome"";v=""94"", "";Not A Brand"";v=""99""");
                request.Headers.Add("sec-ch-ua-mobile", @"?0");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
                request.Headers.Add("TribalWars-Ajax", @"1");
                request.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                request.Headers.Add("Origin", @"https://pl169.plemiona.pl");
                request.Headers.Add("Sec-Fetch-Site", @"same-origin");
                request.Headers.Add("Sec-Fetch-Mode", @"cors");
                request.Headers.Add("Sec-Fetch-Dest", @"empty");
                request.Referer = "https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Headers.Set(HttpRequestHeader.Cookie, GetCookieString());

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                string body = $@"attack=true&ch={ch}&cb=troop_confirm_submit&x={x}&y={y}&source_village={Settings.CurrentVillageID}&village={Settings.CurrentVillageID}&attack_name=&spear=0&sword=0&axe=0&archer=0&spy=1&light={VillageToFarm.lekkaKawaleria}&marcher=0&heavy=0&ram=0&catapult=0&knight=0&snob=0&building=main&h=107fe650&h=107fe650";
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();

                response = (HttpWebResponse)request.GetResponse();

                var encoding = Encoding.GetEncoding(response.CharacterSet);
                string dddupa;
                using (var responseStream = response.GetResponseStream())
                using (var reader = new StreamReader(responseStream, encoding))
                {
                    dddupa = reader.ReadToEnd();
                }

                Console.WriteLine(dddupa);
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
