using System;
using System.IO;
using System.Net;
using System.Text;
using WindowsFormsApp1.Extension;
using WindowsFormsApp1.Main;

namespace WindowsFormsApp1.PlemionaRequest
{
    public class Zbieractwo__Request : RequestBase
    {
        private int ZbieractwoLevel { get; set; }

        public Zbieractwo__Request(int level)
        {
            ZbieractwoLevel = level;
        }

        public void MakeRequests(int spear, int sword, int axe, int lk)
        {
            HttpWebResponse response;

            if (Request_pl169_plemiona_pl(out response, spear, sword, axe, lk))
            {
                response.Close();
            }
        }

        private bool Request_pl169_plemiona_pl(out HttpWebResponse response, int spear, int sword, int axe, int lk)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=scavenge_api&ajaxaction=send_squads");
                request.SetBasicHeaders();
                request.Referer = $"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=place&mode=scavenge";
                
                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                var weight = spear * 25 + axe * 10 + sword * 15 + lk * 80;
                string body = $@"squad_requests%5B0%5D%5Bvillage_id%5D={Settings.CurrentVillageID}&squad_requests%5B0%5D%5Bcandidate_squad%5D%5Bunit_counts%5D%5Bspear%5D={spear}&squad_requests%5B0%5D%5Bcandidate_squad%5D%5Bunit_counts%5D%5Bsword%5D={sword}&squad_requests%5B0%5D%5Bcandidate_squad%5D%5Bunit_counts%5D%5Baxe%5D={axe}&squad_requests%5B0%5D%5Bcandidate_squad%5D%5Bunit_counts%5D%5Barcher%5D=0&squad_requests%5B0%5D%5Bcandidate_squad%5D%5Bunit_counts%5D%5Blight%5D={lk}&squad_requests%5B0%5D%5Bcandidate_squad%5D%5Bunit_counts%5D%5Bmarcher%5D=0&squad_requests%5B0%5D%5Bcandidate_squad%5D%5Bunit_counts%5D%5Bheavy%5D=0&squad_requests%5B0%5D%5Bcandidate_squad%5D%5Bunit_counts%5D%5Bknight%5D=0&squad_requests%5B0%5D%5Bcandidate_squad%5D%5Bcarry_max%5D={weight}&squad_requests%5B0%5D%5Boption_id%5D={ZbieractwoLevel}&squad_requests%5B0%5D%5Buse_premium%5D=false&h={Settings.H}";
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
