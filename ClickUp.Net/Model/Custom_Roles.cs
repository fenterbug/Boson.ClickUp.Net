namespace Boson.ClickUp.Net.Model
{
    public class Custom_Roles
    {
        public string date_created { get; set; }
        public int id { get; set; }
        public int inherited_role { get; set; }
        public int[] members { get; set; }
        public string name { get; set; }
        public string team_id { get; set; }
    }
}