namespace ClickUp.Net
{
	public class Response
	{
		public ErrorResult Error { get; set; }
		public bool Success => !Error.IsError;
	}

	public class Response<T> : Response
	{
		public T Value { get; set; }
	}
}