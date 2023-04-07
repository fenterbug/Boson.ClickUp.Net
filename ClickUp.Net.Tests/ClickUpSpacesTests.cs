using Boson.ClickUp.Net.Model;

using Shouldly;

namespace Boson.ClickUp.Net.Tests
{
	[TestClass]
	public class ClickUpSpacesTests
	{
		[TestMethod]
		public async Task Can_Get_Spaces()
		{
			var api = new ClickUpApi().UsingMockServer().WithPersonalToken("NOT REQUIRED FOR MOCK SERVER");
			var userResult = await api.GetSpaces(team_id: 1);

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

		[TestMethod]
		public async Task Can_Create_A_Space()
		{
			var newSpace = new Space("New Space Name", false);
			double mockTeam = 1;

			var api = new ClickUpApi().UsingMockServer().WithPersonalToken("NOT REQUIRED FOR MOCK SERVER");
			var apiResult = await api.CreateSpace(mockTeam, newSpace);
			apiResult.Success.ShouldBeTrue();

			if (apiResult.Success)
			{
				var createdSpace = apiResult.Value;
				createdSpace.name.ShouldBe(newSpace.name);
			}
		}

		[TestMethod]
		public async Task Can_Update_A_Space()
		{
			var oldSpace = new Space("Updated Space Name", false)
			{
				id = "790"
			};

			var api = new ClickUpApi().UsingMockServer().WithPersonalToken("NOT REQUIRED FOR MOCK SERVER");
			var apiResult = await api.UpdateSpace(Convert.ToDouble(oldSpace.id), oldSpace);
			apiResult.Success.ShouldBeTrue();

			if (apiResult.Success)
			{
				var updatedSpace = apiResult.Value;
				updatedSpace.name.ShouldBe(oldSpace.name);
			}
		}

		[TestMethod]
		public async Task Can_Get_A_Space()
		{
			var api = new ClickUpApi().UsingMockServer().WithPersonalToken("NOT REQUIRED FOR MOCK SERVER");
			var result = await api.GetSpace(space_id: 1);
			result.Success.ShouldBeTrue();

			if (result.Success)
			{
				var space = result.Value;
				//TODO: This is a weak test. It relies on the sample data coming from ClickUp. If ClickUp ever changes their sample data, this test will break;
				space.id.ShouldBe("790");
				space.name.ShouldBe("Updated Space Name");
			}
		}

		[TestMethod]
		public async Task Can_Delete_A_Space()
		{
			var api = new ClickUpApi().UsingMockServer().WithPersonalToken("NOT REQUIRED FOR MOCK SERVER");
			var result = await api.DeleteSpace(space_id: 1);
			result.Success.ShouldBeTrue();
		}
	}
}