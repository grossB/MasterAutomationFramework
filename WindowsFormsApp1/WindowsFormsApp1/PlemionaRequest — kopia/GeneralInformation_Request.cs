using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
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
                request.Referer = "https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=place&mode=scavenge";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Headers.Set(HttpRequestHeader.Cookie, GetCookieString());

                response = (HttpWebResponse)request.GetResponse();
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
