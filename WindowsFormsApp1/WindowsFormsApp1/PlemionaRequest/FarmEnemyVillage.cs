using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1.Extension;
using WindowsFormsApp1.Main;

namespace WindowsFormsApp1.PlemionaRequest
{
    public class AttackEnemyVillage : RequestBase
    {
        public FarmVillage VillageToAttack { get; set; } = new FarmVillage("23699", "361", "550")
        {
            Units = new UnitsForAttack(
            spear: "",
            sword: "",
            axe: "",
            spy: "",
            light: "",
            catapult: "",
            knight: "",
            ram: "",
            snob: "")
        };

        public void EnemyVilageSendAttack(FarmVillage farmVillage)
        {
            // START ------------ New Implementation ----------------
            VillageToAttack = farmVillage;
            // END------------ New Implementation ----------------

            // Do wyciągania informacji o aktualnym stanie wojska w wiosce TODO: dodać regexy do wyciągania z htmlowej odpowiedzi 
            //var gg = new UnitInfo_Request();
            //gg.MakeRequests();


            string responseBody = "";
            // template_id=1409 ma 1 szarego i 30 lk, 

            List<FarmVillage> villageToPlunder = new List<FarmVillage>()
            {
                new FarmVillage("23699", "361", "550"),
            };

            foreach (var item in villageToPlunder)
            {
                VillageToAttack = new FarmVillage("23699", "361", "550"); //item;

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

                    if (Confirm_Request(out response, out responseBody))
                    {
                        response.Close();
                    }

                    string chBodyAttributRgxGroupName = "chBodyAttribute";
                    var rgx = new System.Text.RegularExpressions.Regex($@"name=\\""ch\\"" value=\\""(?<{chBodyAttributRgxGroupName}>.*?)(\\"")");
                    var hh = rgx.Match(responseBody);
                    var chBodyAttribute = rgx.Match(responseBody).Groups[chBodyAttributRgxGroupName].Value;

                    HttpWebResponse response4;

                    if (PopupCommand(out response4, chBodyAttribute))
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map&ajax=map_info&source={Settings.CurrentVillageID}&target={VillageToAttack.VillageId}&&");

                request.KeepAlive = true;
                request.Headers.Add("sec-ch-ua", @"""Chromium"";v=""94"", ""Google Chrome"";v=""94"", "";Not A Brand"";v=""99""");
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
                request.Headers.Add("sec-ch-ua-mobile", @"?0");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36";
                request.Headers.Add("TribalWars-Ajax", @"1");
                request.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                request.Headers.Add("Sec-Fetch-Site", @"same-origin");
                request.Headers.Add("Sec-Fetch-Mode", @"cors");
                request.Headers.Add("Sec-Fetch-Dest", @"empty");
                request.Referer = $"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Headers.Set(HttpRequestHeader.Cookie, RequestBase.GetCookieString());

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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=place&ajax=command&target={VillageToAttack.VillageId}");
                request.KeepAlive = true;
                request.Headers.Add("sec-ch-ua", @"""Chromium"";v=""94"", ""Google Chrome"";v=""94"", "";Not A Brand"";v=""99""");
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
                request.Headers.Add("sec-ch-ua-mobile", @"?0");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36";
                request.Headers.Add("TribalWars-Ajax", @"1");
                request.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                request.Headers.Add("Sec-Fetch-Site", @"same-origin");
                request.Headers.Add("Sec-Fetch-Mode", @"cors");
                request.Headers.Add("Sec-Fetch-Dest", @"empty");
                request.Referer = $"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Headers.Set(HttpRequestHeader.Cookie, RequestBase.GetCookieString());
                
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

        private bool Confirm_Request(out HttpWebResponse response, out string responseBody)
        {
            response = null;
            responseBody = "";
            
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=place&ajax=confirm");

                request.KeepAlive = true;
                request.Headers.Add("sec-ch-ua", @"""Chromium"";v=""94"", ""Google Chrome"";v=""94"", "";Not A Brand"";v=""99""");
                request.Headers.Add("sec-ch-ua-mobile", @"?0");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
                request.Headers.Add("TribalWars-Ajax", @"1");
                request.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                request.Headers.Add("Origin", @"https://pl169.plemiona.pl");
                request.Headers.Add("Sec-Fetch-Site", @"same-origin");
                request.Headers.Add("Sec-Fetch-Mode", @"cors");
                request.Headers.Add("Sec-Fetch-Dest", @"empty");
                request.Referer = $"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Headers.Set(HttpRequestHeader.Cookie, RequestBase.GetCookieString());

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;
                
                string body = $@"89b0297c94b5ae8ac34e42=ec85e3cc89b029&template_id=&"
+ $"source_village={Settings.CurrentVillageID}&"
+ $"spear={VillageToAttack.Units.Spear}&"
+ $"sword={VillageToAttack.Units.Sword}&"
+ $"axe={VillageToAttack.Units.Axe}&"
+ $"archer=&spy={VillageToAttack.Units.Spy}&"
+ $"light={VillageToAttack.Units.Light}&"
+ $"marcher=&heavy=&ram={VillageToAttack.Units.Ram}&"
+ $"catapult={VillageToAttack.Units.Catapult}&"
+ $"knight={VillageToAttack.Units.Knight}&"
+ $"snob={VillageToAttack.Units.Snob}&"
+ $"x={VillageToAttack.villageX}&"
+ $"y={VillageToAttack.villageY}&"
+ $"input=&attack=l&h={Settings.H}";

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

        private bool PopupCommand(out HttpWebResponse response, string ch)
        {
            response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=place&ajaxaction=popup_command");

                request.KeepAlive = true;
                request.Headers.Add("sec-ch-ua", @"""Chromium"";v=""94"", ""Google Chrome"";v=""94"", "";Not A Brand"";v=""99""");
                request.Headers.Add("sec-ch-ua-mobile", @"?0");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Accept = "application/json, text/javascript, */*; q=0.01";
                request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
                request.Headers.Add("TribalWars-Ajax", @"1");
                request.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                request.Headers.Add("Origin", $@"https://pl169.plemiona.pl");
                request.Headers.Add("Sec-Fetch-Site", @"same-origin");
                request.Headers.Add("Sec-Fetch-Mode", @"cors");
                request.Headers.Add("Sec-Fetch-Dest", @"empty");
                request.Referer = $"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Headers.Set(HttpRequestHeader.Cookie, RequestBase.GetCookieString());

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                string body = $@"attack=true&"
+ $"ch={ch}&"
+ $"cb=troop_confirm_submit&"
+ $"x={VillageToAttack.villageX}&"
+ $"y={VillageToAttack.villageY}&"
+ $"source_village={Settings.CurrentVillageID}&"
+ $"village={Settings.CurrentVillageID}&"
+ $"attack_name=&"
+ $"spear={VillageToAttack.Units.Spear.UnitAmount()}&"
+ $"sword={VillageToAttack.Units.Sword.UnitAmount()}&"
+ $"axe={VillageToAttack.Units.Axe.UnitAmount()}&"
+ $"archer=0&"
+ $"spy={VillageToAttack.Units.Spy.UnitAmount()}&"
+ $"light={VillageToAttack.Units.Light.UnitAmount()}&"
+ $"marcher=0&"
+ $"heavy=0&"
+ $"ram={VillageToAttack.Units.Ram.UnitAmount()}&"
+ $"catapult={VillageToAttack.Units.Catapult.UnitAmount()}&"
+ $"knight={VillageToAttack.Units.Knight.UnitAmount()}&"
+ $"snob={VillageToAttack.Units.Snob.UnitAmount()}&"
+ $"building=main&h={Settings.H}&"
+ $"h={Settings.H}";
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



