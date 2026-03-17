using BTGK_NHOM7.Models.TokenAuth;
using BTGK_NHOM7.Web.Controllers;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace BTGK_NHOM7.Web.Tests.Controllers;

public class HomeController_Tests : BTGK_NHOM7WebTestBase
{
    [Fact]
    public async Task Index_Test()
    {
        await AuthenticateAsync(null, new AuthenticateModel
        {
            UserNameOrEmailAddress = "admin",
            Password = "123qwe"
        });

        //Act
        var response = await GetResponseAsStringAsync(
            GetUrl<HomeController>(nameof(HomeController.Index))
        );

        //Assert
        response.ShouldNotBeNullOrEmpty();
    }
}