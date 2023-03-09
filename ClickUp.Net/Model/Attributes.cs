using System.Text.Json.Serialization;

namespace ClickUp.Net.Model
{
	public class Attributes
	{
		[JsonPropertyName("block-id")]
		public string blockid { get; set; }
	}
}
