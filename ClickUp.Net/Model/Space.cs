namespace Boson.ClickUp.Net.Model
{
	public class Space
	{
		public bool @private { get; set; }
		public bool archived { get; set; }
		public Features features { get; set; }
		public string id { get; set; }
		public bool multiple_assignees { get; set; }
		public string name { get; set; }
		public List<Status> statuses { get; set; }
	}
}