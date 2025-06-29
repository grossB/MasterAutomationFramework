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
using WindowsFormsApp1.Pages;

namespace WindowsFormsApp1.PlemionaRequest
{
    public class Farm_Request : RequestBase
    {
        public static int templateID_A = 7593;

        public void FarmGivenVillage(string villageId, List<BarbarianFarmVillage> villages, bool burzyciel= false)
        {
            SetCurrentVillageId();
            villages.ForEach(x => Console.WriteLine($"sasasa {x.WallLevel}"));

            foreach (var village in villages)
            {
                //if (burzyciel && village.WallLevel != 0)
                //{
                //    switch (village.WallLevel)
                //    {
                //        case 1:
                //            new AttackEnemyVillage().EnemyVilageSendAttack(new FarmVillage(village.VillageID, village.VillageCoordination));
                //            break;
                //        case 2:
                //            break;
                //        case 3:
                //        case 4:
                //        case 5:
                //        case 6:
                //            break;
                //        default:
                //            break;
                //    }
                //}

                if (village.LastAttackStatus != LastAttackStatus.Green)
                {
                    continue;
                }

                HttpWebResponse response;

                if (Request_pl169_plemiona_pl(out response, village.VillageID, templateID: templateID_A))
                {
                    response.Close();
                }

                Thread.Sleep(259);

            }

            RestoreMainVIllageId();


            void SetCurrentVillageId()
            {
                Settings.CurrentVillageID = villageId;
            }

            void RestoreMainVIllageId()
            {
                Settings.CurrentVillageID = "21880";
            }
        }

        public void MakeRequests(int howManyVillage)
        {
            foreach (var barbID in barbdsDictionaryIds.Take(howManyVillage))
            {
                HttpWebResponse response;

                if (Request_pl169_plemiona_pl(out response, barbID.Key.ToString(), (int)barbID.Value))
                {
                    response.Close();
                }

                Thread.Sleep(251);
            }
        }

        private bool Request_pl169_plemiona_pl(out HttpWebResponse response, string barb_village_number, int templateID)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=am_farm&mode=farm&ajaxaction=farm&json=1&");
                request.Referer = $"https://pl169.plemiona.pl/game.php?village={Settings.CurrentVillageID}&screen=am_farm";
                request.SetBasicHeaders();
                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;
                request.Headers.Set(HttpRequestHeader.Cookie, GetCookieString());

                string body = $@"target={barb_village_number}&template_id={templateID}&source={Settings.CurrentVillageID}&h={Settings.H}";
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

        public enum AttackTemplate
        {
            SmallAttack = 7593,
            BigAttack = 7946
        }

        Dictionary<int, AttackTemplate> barbdsDictionaryIds = new Dictionary<int, AttackTemplate>()
        {//7{ 23521, AttackTemplate.SmallAttack },
            { 23873, AttackTemplate.SmallAttack },
            { 21887, AttackTemplate.SmallAttack },
            { 22478, AttackTemplate.SmallAttack },
            { 24108, AttackTemplate.SmallAttack },
            { 23508, AttackTemplate.SmallAttack },
            { 23108, AttackTemplate.SmallAttack },
            { 21791, AttackTemplate.SmallAttack },
            { 25557, AttackTemplate.SmallAttack },
            { 24138, AttackTemplate.SmallAttack },
            { 23337, AttackTemplate.SmallAttack },
            { 24787, AttackTemplate.SmallAttack },
            { 23737, AttackTemplate.SmallAttack },
            { 23446, AttackTemplate.SmallAttack },
            { 24608, AttackTemplate.SmallAttack },
            { 22730, AttackTemplate.SmallAttack },
            { 21444, AttackTemplate.SmallAttack },
            { 22250, AttackTemplate.SmallAttack },
            { 25352, AttackTemplate.SmallAttack },
            { 22367, AttackTemplate.SmallAttack },
            { 24695, AttackTemplate.SmallAttack },
            { 23694, AttackTemplate.SmallAttack },
            { 23536, AttackTemplate.SmallAttack },
            { 22095, AttackTemplate.SmallAttack },
            { 22483, AttackTemplate.SmallAttack },
            { 21892, AttackTemplate.SmallAttack },
            { 25548, AttackTemplate.SmallAttack },
            { 21317, AttackTemplate.SmallAttack },
            { 23486, AttackTemplate.SmallAttack },
            { 21121, AttackTemplate.SmallAttack },
            { 26091, AttackTemplate.SmallAttack },
            { 24145, AttackTemplate.SmallAttack },
            { 23712, AttackTemplate.SmallAttack },
            { 21352, AttackTemplate.SmallAttack },
            { 22808, AttackTemplate.SmallAttack },
            { 21288, AttackTemplate.SmallAttack },
            { 23229, AttackTemplate.SmallAttack },
            { 22264, AttackTemplate.SmallAttack },
            { 26361, AttackTemplate.SmallAttack },
            { 22495, AttackTemplate.SmallAttack },
            { 26454, AttackTemplate.SmallAttack },
            { 25815, AttackTemplate.SmallAttack },
            { 21450, AttackTemplate.SmallAttack },
            { 21743, AttackTemplate.SmallAttack },
            { 25100, AttackTemplate.SmallAttack },
            { 20802, AttackTemplate.SmallAttack },
            { 25133, AttackTemplate.SmallAttack },
            { 26189, AttackTemplate.SmallAttack },
            { 20640, AttackTemplate.SmallAttack },
            { 25261, AttackTemplate.SmallAttack },
            { 25382, AttackTemplate.SmallAttack },
            { 23656, AttackTemplate.SmallAttack },
            { 23167, AttackTemplate.SmallAttack },
            { 19673, AttackTemplate.SmallAttack },
            { 26446, AttackTemplate.SmallAttack },
            { 19463, AttackTemplate.SmallAttack },
            { 26045, AttackTemplate.SmallAttack },
            { 25108, AttackTemplate.SmallAttack },
            { 23030, AttackTemplate.SmallAttack },
            { 20653, AttackTemplate.SmallAttack },
            { 22724, AttackTemplate.SmallAttack },
            { 25095, AttackTemplate.SmallAttack },
            { 25137, AttackTemplate.SmallAttack },
            { 19290, AttackTemplate.SmallAttack },
            { 20150, AttackTemplate.SmallAttack },
            { 26435, AttackTemplate.SmallAttack },
            { 25283, AttackTemplate.SmallAttack },
            { 21353, AttackTemplate.SmallAttack },
            { 23219, AttackTemplate.SmallAttack },
            { 20587, AttackTemplate.SmallAttack },
            { 19230, AttackTemplate.SmallAttack },
            { 20612, AttackTemplate.SmallAttack },
            { 22105, AttackTemplate.SmallAttack },
            { 27016, AttackTemplate.SmallAttack },
            { 22516, AttackTemplate.SmallAttack },
            { 19436, AttackTemplate.SmallAttack },
            { 25651, AttackTemplate.SmallAttack },
            { 22384, AttackTemplate.SmallAttack },
            { 25209, AttackTemplate.SmallAttack },
            { 20000, AttackTemplate.SmallAttack },
        };
    }
}
