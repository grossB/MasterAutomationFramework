using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Main;

namespace WindowsFormsApp1.Extension
{
    public static class HttpRequestExtension
    {
        public static HttpWebRequest SetBasicHeaders(this HttpWebRequest request)
        {
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
            request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            request.Headers.Set(HttpRequestHeader.AcceptLanguage, "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
            request.Headers.Set(HttpRequestHeader.Cookie, RequestBase.GetCookieString());
            return request;
        }
    }
}
