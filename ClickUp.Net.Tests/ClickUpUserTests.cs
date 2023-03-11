using Shouldly;

namespace ClickUp.Net.Tests
{
	[TestClass]
	public class ClickUpUserTests
	{
		[TestMethod]
		public async Task Can_Identify_Current_User()
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