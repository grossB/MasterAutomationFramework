using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WindowsFormsApp1.Main;

namespace WindowsFormsApp1.PlemionaRequest
{
    public class ExchangeAjaxData_Request : RequestBase
    {
        public ExchangeData MakeRequests()
        {
            SeleniumDriver.WebDriver.Navigate().Refresh();
            HttpWebResponse response;
            ExchangeData myObj;

            if (Request_pl169_plemiona_pl(out response, out myObj))
            {
                response.Close();
            }

            return myObj;
        }

        private bool Request_pl169_plemiona_pl(out HttpWebResponse responseHttp, out ExchangeData myObj)
        {
            responseHttp = null;
            myObj = null;

            try
            {
                HttpWebRequest requestHttp = (HttpWebRequest)WebRequest.Create("https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=market&ajax=exchange_data");

                requestHttp.KeepAlive = true;
                requestHttp.Headers.Add("sec-ch-ua", @"""Chromium"";v=""94"", ""Google Chrome"";v=""94"", "";Not A Brand"";v=""99""");
                requestHttp.Accept = "application/json, text/javascript, */*; q=0.01";
                requestHttp.Headers.Add("X-Requested-With", @"XMLHttpRequest");
                requestHttp.Headers.Add("sec-ch-ua-mobile", @"?0");
                requestHttp.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36";
                requestHttp.Headers.Add("TribalWars-Ajax", @"1");
                requestHttp.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                requestHttp.Headers.Add("Sec-Fetch-Site", @"same-origin");
                requestHttp.Headers.Add("Sec-Fetch-Mode", @"cors");
                requestHttp.Headers.Add("Sec-Fetch-Dest", @"empty");
                requestHttp.Headers.Add("Keep-Alive", @"1");
                requestHttp.Referer = "https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=market&mode=exchange";
                requestHttp.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                requestHttp.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                //requestHttp.Headers.Set(HttpRequestHeader.Cookie, $@"cid=742255562; pl_auth=26cc5f5d4fa2:617f99bbd37c960a470915402ef0994216715fdb41f77f23f521897423aad0a9; websocket_available=true; global_village_id={Settings.CurrentVillageID}; popup_pos_unit_popup_spy=384.6000061035156x357.4375; popup_pos_unit_popup_light=384.6000061035156x400.0375061035156; popup_pos_unit_popup_axe=384.6000061035156x445; popup_pos_own_villages=377.8000183105469x401.75; sid={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("sid").Value}; mobile_mapsize=9x9; pomo=1; io={io}");
                //requestHttp.Headers.Set(HttpRequestHeader.Cookie, $@"cid=742255562; pl_auth=26cc5f5d4fa2:617f99bbd37c960a470915402ef0994216715fdb41f77f23f521897423aad0a9; websocket_available=true; global_village_id={Settings.CurrentVillageID}; popup_pos_unit_popup_spy=384.6000061035156x357.4375; popup_pos_unit_popup_light=384.6000061035156x400.0375061035156; popup_pos_unit_popup_axe=384.6000061035156x445; popup_pos_own_villages=377.8000183105469x401.75; sid={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("sid").Value}; pomo=1; io={SeleniumDriver.WebDriver.Manage().Cookies.GetCookieNamed("io").Value}");
                requestHttp.Headers.Set(HttpRequestHeader.Cookie, GetCookieString());

                responseHttp = (HttpWebResponse)requestHttp.GetResponse();

                using (var twitpicResponse = (HttpWebResponse)requestHttp.GetResponse())
                {
                    using (var reader = new StreamReader(twitpicResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var objText = reader.ReadToEnd();
                        myObj = (ExchangeData)js.Deserialize(objText, typeof(ExchangeData));
                    }
                }


                //var encoding = Encoding.GetEncoding(response.CharacterSet);
                //string dddupa;
                //using (var responseStream = response.GetResponseStream())
                //using (var reader = new StreamReader(responseStream, encoding))
                //{
                //    dddupa = reader.ReadToEnd();
                //}

                //Debug.WriteLine(dddupa);

            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError) responseHttp = (HttpWebResponse)e.Response;
                else return false;
            }
            catch (Exception e)
            {
                if (responseHttp != null) responseHttp.Close();
                return false;
            }

            return true;
        }
    }
}
