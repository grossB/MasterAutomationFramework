namespace WindowsFormsApp1.PlemionaRequest
{
    public class FarmVillage
    {
        public FarmVillage(string villageId, string cord)
        {
            villageX = cord.Substring(2);
            villageY = cord.Substring(2);
            VillageId = villageId;
        }

        public FarmVillage(string villageId, string x, string y)
        {
            villageX = x;
            villageY = y;
            VillageId = villageId;
        }

        public UnitsForAttack Units { get; set; }

        public string VillageId { get; set; } = "";

        public string villageX { get; set; } = "";
        public string villageY { get; set; } = "";


        public int templateId { get; set; } = 1409;
    }
}
