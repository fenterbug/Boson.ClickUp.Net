﻿using System.Text.Json.Serialization;

namespace Boson.ClickUp.Net.Model
{
	public class Team
	{
		public string avatar { get; set; }
		public string color { get; set; }
		public string id { get; set; }
		public List<Member> members { get; set; }

		[JsonIgnore]
		public IEnumerable<User> Members
		{
			get
			{
				foreach (var member in members)
				{
					yield return member.user;
				}
			}
		}

		public string name { get; set; }
	}
}