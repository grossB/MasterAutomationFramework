namespace WindowsFormsApp1.PlemionaRequest
{
    public class UnitsForAttack
    {
        public UnitsForAttack(
            string spear, 
            string sword, 
            string axe, 
            string spy, string light, string ram, string catapult, string knight, string snob)
        {
            this.Spear = spear;
            this.Sword = sword;
            this.Axe = axe;
            this.Spy = spy;
            this.Light = light;
            this.Ram = ram;
            this.Catapult = catapult;
            this.Knight = knight;
            this.Snob = snob;
        }

        public string Spear { get; set; }
        public string Sword { get; set; }
        public string Axe { get; set; }
        public string Spy { get; set; }
        public string Light { get; set; }
        public string Ram { get; set; }
        public string Catapult { get; set; }
        public string Knight { get; set; }
        public string Snob { get; set; }
    }
}
