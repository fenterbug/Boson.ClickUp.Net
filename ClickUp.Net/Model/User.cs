namespace ClickUp.Net.Model
{
	public class User
	{
		public int id { get; set; }
		public string username { get; set; }
		public string email { get; set; }
		public string color { get; set; }
		public string profilePicture { get; set; }
		public int week_start_day { get; set; }
		public bool global_font_support { get; set; }
		public string initials { get; set; }
		public int role { get; set; }
		public object custom_role { get; set; }
		public string last_active { get; set; }
		public string date_joined { get; set; }
		public string date_invited { get; set; }
		public string timezone { get; set; }
	}
}