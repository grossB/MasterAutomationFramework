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
    public class Farm_Request : RequestBase
    {
        public void MakeRequests(string h, int howManyVillage)
        {
            foreach (var barbID in barbdsDictionaryIds.Take(howManyVillage))
            {
                HttpWebResponse response;

                if (Request_pl169_plemiona_pl(out response, barbID.Key, (int)barbID.Value, h))
                {
                    response.Close();
                }

                Thread.Sleep(251);
            }
        }

        private bool Request_pl169_plemiona_pl(out HttpWebResponse response, int barb_village_number, int templateID, string h)
        {
            response = null;

            try
            {
                //SeleniumDriver.WebDriver.SwitchTo().Window(SeleniumDriver.WebDriver.WindowHandles[0]);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=am_farm&mode=farm&ajaxaction=farm&json=1&");

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
                request.Referer = "https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=am_farm";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                //request.Headers.Set(HttpRequestHeader.Cookie, $@"cid=742255562; pl_auth={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("pl_auth")}; websocket_available=true; global_village_id={Settings.CurrentVillageID}; popup_pos_unit_popup_spy=384.6000061035156x357.4375; popup_pos_unit_popup_light=384.6000061035156x400.0375061035156; popup_pos_unit_popup_axe=384.6000061035156x445; popup_pos_own_villages=377.8000183105469x401.75; sid={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("sid").Value}; pomo=1; io={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("io").Value}");
                request.Headers.Set(HttpRequestHeader.Cookie, GetCookieString());

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                string body = $@"target={barb_village_number}&template_id={templateID}&source={Settings.CurrentVillageID}&h={h}";
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

                //Debug.WriteLine(dddupa);
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

        public enum AttackTemplate
        {
            SmallAttack = 7593,
            BigAttack = 7946
        }

        Dictionary<int, AttackTemplate> barbdsDictionaryIds = new Dictionary<int, AttackTemplate>()
        {//7946
            { 21288, AttackTemplate.SmallAttack },
            { 21731, AttackTemplate.BigAttack },
            { 25261, AttackTemplate.SmallAttack },
            { 23699, AttackTemplate.SmallAttack },
            { 23393, AttackTemplate.SmallAttack },
            { 22478, AttackTemplate.SmallAttack },
            { 21791, AttackTemplate.SmallAttack },
            { 24787, AttackTemplate.SmallAttack },
            { 23446, AttackTemplate.SmallAttack },
            { 23737, AttackTemplate.SmallAttack },
            { 24608, AttackTemplate.SmallAttack },
            { 22367, AttackTemplate.SmallAttack },
            { 24695, AttackTemplate.SmallAttack },
            { 22250, AttackTemplate.SmallAttack },
            { 22095, AttackTemplate.SmallAttack },
            { 21892, AttackTemplate.SmallAttack },
            { 19960, AttackTemplate.SmallAttack },
            { 21450, AttackTemplate.SmallAttack },
            { 20643, AttackTemplate.SmallAttack },
            { 23656, AttackTemplate.SmallAttack },
            { 19673, AttackTemplate.SmallAttack },
            { 19463, AttackTemplate.SmallAttack },
            { 23030, AttackTemplate.SmallAttack },
            { 22724, AttackTemplate.SmallAttack },
            { 20150, AttackTemplate.SmallAttack },
            { 20587, AttackTemplate.SmallAttack },
            { 19230, AttackTemplate.SmallAttack },
            { 22105, AttackTemplate.SmallAttack },
            { 22516, AttackTemplate.SmallAttack },
            { 20000, AttackTemplate.SmallAttack },
            { 22939, AttackTemplate.SmallAttack },
            { 24516, AttackTemplate.SmallAttack },
            { 21294, AttackTemplate.SmallAttack },
            { 19811, AttackTemplate.SmallAttack },
            { 20844, AttackTemplate.SmallAttack },
            { 23193, AttackTemplate.SmallAttack },
            { 19033, AttackTemplate.SmallAttack },
            { 22336, AttackTemplate.SmallAttack },
            { 21218, AttackTemplate.SmallAttack },
            { 23019, AttackTemplate.SmallAttack },
            { 22652, AttackTemplate.SmallAttack },
        };
    }
}
