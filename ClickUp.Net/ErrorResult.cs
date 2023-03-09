namespace ClickUp.Net
{
	public class ErrorResult
	{
		public string err { get; set; }
		public string ECODE { get; set; }

		public bool IsError => err != default && ECODE != default;
	}
}
