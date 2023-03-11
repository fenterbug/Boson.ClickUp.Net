using Shouldly;

namespace ClickUp.Net.Tests
{
	[TestClass]
	public class ClickUpSpacesTests
	{
		[TestMethod]
		public async Task Can_Get_Spaces()
		{
			var api = new ClickUpApi().UsingMockServer().WithPersonalToken("NOT REQUIRED FOR MOCK SERVER");
			var userResult = await api.GetSpaces(1);

			if (userResult.Success)
			{
				//TODO: This is a weak test. It relies on the sample data coming from ClickUp. If ClickUp ever changes their sample data, this test will break;
				var spaceList = userResult.Value;
				spaceList.Count().ShouldBe(2);
				foreach (var space in spaceList)
				{
					Convert.ToInt32(space.id).ShouldBeInRange(790, 791);
				}
			}
		}
	}
}