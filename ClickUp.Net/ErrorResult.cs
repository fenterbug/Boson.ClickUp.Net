namespace Boson.ClickUp.Net
{
	public class ErrorResult
	{
		public string ECODE { get; set; }
		public string err { get; set; }
		public bool IsError => err != default && ECODE != default;
	}
}