using Shouldly;

namespace ClickUp.Net.Tests
{
	[TestClass]
	public class ClickUpSpacesTests
	{
		[TestMethod]
		public async Task TestMethod1()
		{
			var api = new ClickUpApi().UsingMockServer().WithPersonalToken("NOT REQUIRED FOR MOCK SERVER");
			var userResult = await api.GetAuthorizedUser();

			if (userResult.Success)
			{
				var user = userResult.Value;
				user.username.ShouldBe("John Doe");
			}
		}
	}
}