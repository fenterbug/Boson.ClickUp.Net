using Shouldly;

namespace Boson.ClickUp.Net.Tests
{
    [TestClass]
    public class ClickUpListTests
    {
        [TestMethod]
        public async Task Can_Get_Lists_For_Folder()
        {
            var api = new ClickUpApi().UsingMockServer().WithPersonalToken("NOT REQUIRED FOR MOCK SERVER");
            var userResult = await api.GetLists(folderId: 1);

            if (userResult.Success)
            {
                //TODO: This is a weak test. It relies on the sample data coming from ClickUp. If ClickUp ever changes their sample data, this test will break;
                var listList = userResult.Value;
                listList.Count().ShouldBe(1);
                var list = listList.First();
                list.id.ShouldBe("124");
                list.name.ShouldBe("Updated List Name");
            }
        }
    }
}