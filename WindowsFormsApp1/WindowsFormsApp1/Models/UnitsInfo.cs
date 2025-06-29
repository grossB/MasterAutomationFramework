using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Spear
    {
        public string id { get; set; }
        public string image { get; set; }
        public string type { get; set; }
        public string prod_building { get; set; }
        public int build_time { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
        public int iron { get; set; }
        public int pop { get; set; }
        public double speed { get; set; }
        public int attack { get; set; }
        public object building_attack_multiplier { get; set; }
        public object additional_max_wall_negation { get; set; }
        public int defense { get; set; }
        public int defense_cavalry { get; set; }
        public int defense_archer { get; set; }
        public int carry { get; set; }
        public int stealth { get; set; }
        public int perception { get; set; }
        public bool can_attack { get; set; }
        public bool can_support { get; set; }
        public int attackpoints { get; set; }
        public int defpoints { get; set; }
        public int cost_modifier { get; set; }
        public string count { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public string desc { get; set; }
        public List<object> desc_abilities { get; set; }
    }

    public class Sword
    {
        public string id { get; set; }
        public string image { get; set; }
        public string type { get; set; }
        public string prod_building { get; set; }
        public int build_time { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
        public int iron { get; set; }
        public int pop { get; set; }
        public double speed { get; set; }
        public int attack { get; set; }
        public object building_attack_multiplier { get; set; }
        public object additional_max_wall_negation { get; set; }
        public int defense { get; set; }
        public int defense_cavalry { get; set; }
        public int defense_archer { get; set; }
        public int carry { get; set; }
        public int stealth { get; set; }
        public int perception { get; set; }
        public bool can_attack { get; set; }
        public bool can_support { get; set; }
        public int attackpoints { get; set; }
        public int defpoints { get; set; }
        public int cost_modifier { get; set; }
        public string count { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public string desc { get; set; }
        public List<object> desc_abilities { get; set; }
    }

    public class Axe
    {
        public string id { get; set; }
        public string image { get; set; }
        public string type { get; set; }
        public string prod_building { get; set; }
        public int build_time { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
        public int iron { get; set; }
        public int pop { get; set; }
        public double speed { get; set; }
        public int attack { get; set; }
        public object building_attack_multiplier { get; set; }
        public object additional_max_wall_negation { get; set; }
        public int defense { get; set; }
        public int defense_cavalry { get; set; }
        public int defense_archer { get; set; }
        public int carry { get; set; }
        public int stealth { get; set; }
        public int perception { get; set; }
        public bool can_attack { get; set; }
        public bool can_support { get; set; }
        public int attackpoints { get; set; }
        public int defpoints { get; set; }
        public int cost_modifier { get; set; }
        public string count { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public string desc { get; set; }
        public List<object> desc_abilities { get; set; }
    }

    public class Spy
    {
        public string id { get; set; }
        public string image { get; set; }
        public string type { get; set; }
        public string prod_building { get; set; }
        public int build_time { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
        public int iron { get; set; }
        public int pop { get; set; }
        public double speed { get; set; }
        public int attack { get; set; }
        public object building_attack_multiplier { get; set; }
        public object additional_max_wall_negation { get; set; }
        public int defense { get; set; }
        public int defense_cavalry { get; set; }
        public int defense_archer { get; set; }
        public int carry { get; set; }
        public int stealth { get; set; }
        public int perception { get; set; }
        public bool can_attack { get; set; }
        public bool can_support { get; set; }
        public int attackpoints { get; set; }
        public int defpoints { get; set; }
        public int cost_modifier { get; set; }
        public string count { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public string desc { get; set; }
        public List<string> desc_abilities { get; set; }
    }

    public class Light
    {
        public string id { get; set; }
        public string image { get; set; }
        public string type { get; set; }
        public string prod_building { get; set; }
        public int build_time { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
        public int iron { get; set; }
        public int pop { get; set; }
        public double speed { get; set; }
        public int attack { get; set; }
        public object building_attack_multiplier { get; set; }
        public object additional_max_wall_negation { get; set; }
        public int defense { get; set; }
        public int defense_cavalry { get; set; }
        public int defense_archer { get; set; }
        public int carry { get; set; }
        public int stealth { get; set; }
        public int perception { get; set; }
        public bool can_attack { get; set; }
        public bool can_support { get; set; }
        public int attackpoints { get; set; }
        public int defpoints { get; set; }
        public int cost_modifier { get; set; }
        public string count { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public string desc { get; set; }
        public List<object> desc_abilities { get; set; }
    }

    public class Ram
    {
        public string id { get; set; }
        public string image { get; set; }
        public string type { get; set; }
        public string prod_building { get; set; }
        public int build_time { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
        public int iron { get; set; }
        public int pop { get; set; }
        public double speed { get; set; }
        public int attack { get; set; }
        public int building_attack_multiplier { get; set; }
        public int additional_max_wall_negation { get; set; }
        public int defense { get; set; }
        public int defense_cavalry { get; set; }
        public int defense_archer { get; set; }
        public int carry { get; set; }
        public int stealth { get; set; }
        public int perception { get; set; }
        public bool can_attack { get; set; }
        public bool can_support { get; set; }
        public int attackpoints { get; set; }
        public int defpoints { get; set; }
        public int cost_modifier { get; set; }
        public string count { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public string desc { get; set; }
        public List<string> desc_abilities { get; set; }
    }

    public class Catapult
    {
        public string id { get; set; }
        public string image { get; set; }
        public string type { get; set; }
        public string prod_building { get; set; }
        public int build_time { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
        public int iron { get; set; }
        public int pop { get; set; }
        public double speed { get; set; }
        public int attack { get; set; }
        public int building_attack_multiplier { get; set; }
        public object additional_max_wall_negation { get; set; }
        public int defense { get; set; }
        public int defense_cavalry { get; set; }
        public int defense_archer { get; set; }
        public int carry { get; set; }
        public int stealth { get; set; }
        public int perception { get; set; }
        public bool can_attack { get; set; }
        public bool can_support { get; set; }
        public int attackpoints { get; set; }
        public int defpoints { get; set; }
        public int cost_modifier { get; set; }
        public string count { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public string desc { get; set; }
        public List<string> desc_abilities { get; set; }
    }

    public class Knight
    {
        public string id { get; set; }
        public string image { get; set; }
        public string type { get; set; }
        public string prod_building { get; set; }
        public int build_time { get; set; }
        public int wood { get; set; }
        public int stone { get; set; }
        public int iron { get; set; }
        public int pop { get; set; }
        public double speed { get; set; }
        public int attack { get; set; }
        public object building_attack_multiplier { get; set; }
        public object additional_max_wall_negation { get; set; }
        public int defense { get; set; }
        public int defense_cavalry { get; set; }
        public int defense_archer { get; set; }
        public int carry { get; set; }
        public int stealth { get; set; }
        public int perception { get; set; }
        public bool can_attack { get; set; }
        public bool can_support { get; set; }
        public int attackpoints { get; set; }
        public int defpoints { get; set; }
        public int cost_modifier { get; set; }
        public int count { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public string desc { get; set; }
        public List<string> desc_abilities { get; set; }
    }

    public class UnitsInfo
    {
        public Spear spear { get; set; }
        public Sword sword { get; set; }
        public Axe axe { get; set; }
        public Spy spy { get; set; }
        public Light light { get; set; }
        public Ram ram { get; set; }
        public Catapult catapult { get; set; }
        public Knight knight { get; set; }
    }
}
