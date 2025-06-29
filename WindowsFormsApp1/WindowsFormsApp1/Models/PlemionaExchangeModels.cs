using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Main
{
    public class PlemionaExchangeModels
    {
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Stock
    {
        public int wood { get; set; }
        public int stone { get; set; }
        public int iron { get; set; }
    }

    public class Capacity
    {
        public int wood { get; set; }
        public int stone { get; set; }
        public int iron { get; set; }
    }

    public class Rates
    {
        public double wood { get; set; }
        public double stone { get; set; }
        public double iron { get; set; }
    }

    public class Tax
    {
        public double buy { get; set; }
        public int sell { get; set; }
    }

    public class Constants
    {
        public double resource_base_price { get; set; }
        public double resource_price_elasticity { get; set; }
        public int stock_size_modifier { get; set; }
    }

    public class Response
    {
        public Stock stock { get; set; }
        public Capacity capacity { get; set; }
        public Rates rates { get; set; }
        public Tax tax { get; set; }
        public Constants constants { get; set; }
        public int duration { get; set; }
        public int merchants { get; set; }
        public string status_bar { get; set; }
    }

    public class Player
    {
        public string id { get; set; }
        public string name { get; set; }
        public string ally { get; set; }
        public string ally_level { get; set; }
        public string ally_member_count { get; set; }
        public string sitter { get; set; }
        public string sleep_start { get; set; }
        public string sitter_type { get; set; }
        public string sleep_end { get; set; }
        public string sleep_last { get; set; }
        public string email_valid { get; set; }
        public string villages { get; set; }
        public string incomings { get; set; }
        public string supports { get; set; }
        public string knight_location { get; set; }
        public string knight_unit { get; set; }
        public int rank { get; set; }
        public string points { get; set; }
        public string date_started { get; set; }
        public string is_guest { get; set; }
        public string birthdate { get; set; }
        public string confirmation_skipping_hash { get; set; }
        public string quest_progress { get; set; }
        public string points_formatted { get; set; }
        public string rank_formatted { get; set; }
        public string pp { get; set; }
        public string new_ally_application { get; set; }
        public string new_ally_invite { get; set; }
        public string new_buddy_request { get; set; }
        public string new_daily_bonus { get; set; }
        public string new_forum_post { get; set; }
        public string new_igm { get; set; }
        public string new_items { get; set; }
        public string new_report { get; set; }
        public string new_quest { get; set; }
    }

    public class Quest
    {
        public bool use_questlines { get; set; }
    }

    public class Premium
    {
        public bool possible { get; set; }
        public bool active { get; set; }
    }

    public class AccountManager
    {
        public bool possible { get; set; }
        public bool active { get; set; }
    }

    public class FarmAssistent
    {
        public bool possible { get; set; }
        public bool active { get; set; }
    }

    public class Features
    {
        public Premium Premium { get; set; }
        public AccountManager AccountManager { get; set; }
        public FarmAssistent FarmAssistent { get; set; }
    }

    public class Bonus
    {
        public double wood { get; set; }
        public double stone { get; set; }
        public double iron { get; set; }
    }

    public class Buildings
    {
        public string main { get; set; }
        public string barracks { get; set; }
        public string stable { get; set; }
        public string garage { get; set; }
        public string church { get; set; }
        public string church_f { get; set; }
        public string snob { get; set; }
        public string smith { get; set; }
        public string place { get; set; }
        public string statue { get; set; }
        public string market { get; set; }
        public string wood { get; set; }
        public string stone { get; set; }
        public string iron { get; set; }
        public string farm { get; set; }
        public string storage { get; set; }
        public string hide { get; set; }
        public string wall { get; set; }
    }

    public class Village
    {
        public int id { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public int wood { get; set; }
        public double wood_prod { get; set; }
        public double wood_float { get; set; }
        public int stone { get; set; }
        public double stone_prod { get; set; }
        public double stone_float { get; set; }
        public int iron { get; set; }
        public double iron_prod { get; set; }
        public double iron_float { get; set; }
        public int pop { get; set; }
        public int pop_max { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int trader_away { get; set; }
        public int storage_max { get; set; }
        public object bonus_id { get; set; }
        public Bonus bonus { get; set; }
        public Buildings buildings { get; set; }
        public int player_id { get; set; }
        public int modifications { get; set; }
        public int points { get; set; }
        public long last_res_tick { get; set; }
        public string coord { get; set; }
        public bool is_farm_upgradable { get; set; }
    }

    public class Nav
    {
        public int parent { get; set; }
    }

    public class GameData
    {
        public Player player { get; set; }
        public Quest quest { get; set; }
        public Features features { get; set; }
        public Village village { get; set; }
        public Nav nav { get; set; }
        public string link_base { get; set; }
        public string link_base_pure { get; set; }
        public string csrf { get; set; }
        public string world { get; set; }
        public string market { get; set; }
        public bool RTL { get; set; }
        public string version { get; set; }
        public string majorVersion { get; set; }
        public string screen { get; set; }
        public object mode { get; set; }
        public string device { get; set; }
        public bool pregame { get; set; }
        public List<string> units { get; set; }
        public string locale { get; set; }
        public string group_id { get; set; }
        public long time_generated { get; set; }
    }

    public class ExchangeData
    {
        public Response response { get; set; }

        //[JsonProperty("game_data")]
        //public GameData Stock_Information { get; set; }

        //[JsonProperty("Stock_Information")]
        public GameData game_data { get; set; }
    }


}
