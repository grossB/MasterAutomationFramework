using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Extension;
using WindowsFormsApp1.Main;

namespace WindowsFormsApp1.PlemionaRequest
{
    public class AttackEnemy_Reqest : RequestBase
    {
        private void MakeRequests()
        {
            HttpWebResponse response1;

            if (Request_pl169_plemiona_pl_First(out response1))
            {
                response1.Close();
            }

            HttpWebResponse response2;

            if (Request_pl169_plemiona_plSecond(out response2))
            {
                response2.Close();
            }
        }

        private bool Request_pl169_plemiona_pl_First(out HttpWebResponse response)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=place&ajax=confirm");

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
                //request.Headers.Set(HttpRequestHeader.Cookie, @"cid=742255562; websocket_available=true; global_village_id={Settings.CurrentVillageID}; popup_pos_unit_popup_spy=384.6000061035156x357.4375; popup_pos_unit_popup_light=384.6000061035156x400.0375061035156; popup_pos_unit_popup_axe=384.6000061035156x445; popup_pos_own_villages=377.8000183105469x401.75; mobile_mapsize=9x9; pomo=1; popup_pos_unit_popup_sword=470.8000183105469x373.6000061035156; popup_pos_unit_popup_spear=399.20001220703125x373.6000061035156; popup_pos_emoji_picker=866.9750366210938x258.08750915527344; popup_pos_group_popup=597.8375244140625x119.70000457763672; popup_pos_unit_popup_ram=384x164; popup_pos_unit_popup_heavy=384.6000061035156x500.5625; popup_pos_unit_popup_marcher=384.6000061035156x462.25; ref=start; pl_auth=8d635ee62d42:3de3e1e27f236e3b697c0e8c8b5d5622c06d426595133921e7c38999418f4619; popup_pos_edit_color_popup=467.4750183224678x1176.1499938964844; popup_pos_unit_popup_catapult=427x156; sid=0%3A13a65685e62abc156d9d4c2872eae7cfb2ea08e253ec5f7138f48167291f2ea04598b49858939b9570c9db9449a7f742add338d2745d164a9eb9f6ed5131ae26; PHPSESSID=9fkmpm3ancj5jgkjvrnsbui2gaa6vo2im1l2ancoo20chgfj; io=SKv_Ykfu_ZqrYYkzDfq2");
                request.Headers.Set(HttpRequestHeader.Cookie, GetCookieString());

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                string body = @"8548d03bfd026884ea619d=55fc31258548d0&template_id=1409&source_village={Settings.CurrentVillageID}&spear=0&sword=0&axe=0&archer=0&spy=1&light=30&marcher=0&heavy=0&ram=0&catapult=0&knight=0&snob=0&x=360&y=549&input=&attack=l&h=1da00794";
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();

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



        #region Second

        private bool Request_pl169_plemiona_plSecond(out HttpWebResponse response)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=place&ajaxaction=popup_command");
                request.SetBasicHeaders();
                request.Referer = "https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=map";
                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                string body = $@"attack=true&ch=83a403d1567a6d4856c0ec28f2a4078e%3A38e2278bf4384416ca9b2937ecd9be3b52476c7bdb60a2ca0197d3a5c2166af9&cb=troop_confirm_submit&x=360&y=549&source_village={Settings.CurrentVillageID}&village={Settings.CurrentVillageID}&attack_name=&spear=0&sword=0&axe=0&archer=0&spy=1&light=30&marcher=0&heavy=0&ram=0&catapult=0&knight=0&snob=0&building=main&h=1da00794&h=1da00794";
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();

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

        #endregion



    }
}
