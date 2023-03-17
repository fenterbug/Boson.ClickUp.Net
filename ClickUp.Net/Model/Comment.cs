namespace Boson.ClickUp.Net.Model
{
	public class Comment
	{
		public List<Comment> comment { get; set; }
		public string comment_text { get; set; }
		public string date { get; set; }
		public string id { get; set; }
		public List<object> reactions { get; set; }
		public User user { get; set; }
	}

	public class Comment2
	{
		public Attributes attributes { get; set; }
		public string text { get; set; }
	}
}