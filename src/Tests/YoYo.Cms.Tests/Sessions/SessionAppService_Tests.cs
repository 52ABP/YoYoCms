using System.Threading.Tasks;
using YoYo.Cms.Sessions;
using Shouldly;
using Xunit;

namespace YoYo.Cms.Tests.Sessions
{
    public class SessionAppService_Tests : CmsTestBase
    {
        private readonly ISessionAppService _sessionAppService;

        public SessionAppService_Tests()
        {
            _sessionAppService = Resolve<ISessionAppService>();
        }

        [MultiTenantFact]
        public async Task Should_Get_Current_User_When_Logged_In_As_Host()
        {
            //Arrange
            LoginAsHostAdmin();

            //Act
            var output = await _sessionAppService.GetCurrentLoginInformations();

            //Assert
            var currentUser = await GetCurrentUserAsync();
            output.User.ShouldNotBe(null);
            output.User.Name.ShouldBe(currentUser.Name);
            output.User.Surname.ShouldBe(currentUser.Surname);

            output.Tenant.ShouldBe(null);
        }

        [Fact]
        public async Task Should_Get_Current_User_And_Tenant_When_Logged_In_As_Tenant()
        {
            //Act
            var output = await _sessionAppService.GetCurrentLoginInformations();

            //Assert
            var currentUser = await GetCurrentUserAsync();
            var currentTenant = await GetCurrentTenantAsync();

            output.User.ShouldNotBe(null);
            output.User.Name.ShouldBe(currentUser.Name);

            output.Tenant.ShouldNotBe(null);
            output.Tenant.Name.ShouldBe(currentTenant.Name);
        }

        [Fact]
        public async Task Should_Not_Get_User_When_Not_Logged_In_As_Host()
        {
            //Arrange
            LogoutAsHost();

            //Act
            var output = await _sessionAppService.GetCurrentLoginInformations();

            //Assert
            output.User.ShouldBeNull();
            output.Tenant.ShouldBeNull();
        }

        [Fact]
        public async Task Should_Not_Get_User_When_Not_Logged_In_As_Tenant()
        {
            //Arrange
            LogoutAsDefaultTenant();

            //Act
            var output = await _sessionAppService.GetCurrentLoginInformations();

            //Assert
            var currentTenant = await GetCurrentTenantAsync();

            output.User.ShouldBeNull();

            output.Tenant.ShouldNotBeNull();
            output.Tenant.Name.ShouldBe(currentTenant.Name);
        }
    }
}
