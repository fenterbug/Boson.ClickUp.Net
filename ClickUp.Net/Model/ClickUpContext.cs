﻿namespace ClickUp.Net.Model
{
	public class ClickUpContext
	{
		public List<Comment> comments { get; set; }
		public List<Team> teams { get; set; }
		public User user { get; set; }
	}
}