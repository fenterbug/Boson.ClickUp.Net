namespace ClickUp.Net.Model
{
	public class Comment
	{
		public string id { get; set; }
		public List<Comment> comment { get; set; }
		public string comment_text { get; set; }
		public User user { get; set; }
		public List<object> reactions { get; set; }
		public string date { get; set; }
	}

	public class Comment2
	{
		public string text { get; set; }
		public Attributes attributes { get; set; }
	}
}
