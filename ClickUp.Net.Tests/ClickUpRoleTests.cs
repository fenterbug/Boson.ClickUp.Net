using Shouldly;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boson.ClickUp.Net.Tests
{
    [TestClass]
    public class ClickUpRoleTests
    {
        [TestMethod]
        public async Task Can_Get_Roles()
        {
            var api = new ClickUpApi().UsingMockServer().WithPersonalToken("NOT REQUIRED FOR MOCK SERVER");
            var roleResult = await api.GetCustomRoles(team_id: 301539);

            if (roleResult.Success)
            {
                //TODO: This is a weak test. It relies on the sample data coming from ClickUp. If ClickUp ever changes their sample data, this test will break;
                var roleList = roleResult.Value;
                roleList.Count().ShouldBe(3);
                foreach (var role in roleList)
                {
                    switch (Convert.ToInt32(role.id)) {
                        case 4547089:
                        case 6715664:
                        case 2957195:
                            break;
                        default:
                            Assert.Fail("Call to GetCustomRole failed to return the expected values.");
                            break;
                    }
                }
            }
        }
    }
}
