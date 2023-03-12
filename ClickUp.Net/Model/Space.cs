namespace Boson.ClickUp.Net.Model
{
	public class Space
	{
		public string id { get; set; }
		public string name { get; set; }
		public bool @private { get; set; }
		public List<Status> statuses { get; set; }
		public bool multiple_assignees { get; set; }
		public Features features { get; set; }
	}

}
