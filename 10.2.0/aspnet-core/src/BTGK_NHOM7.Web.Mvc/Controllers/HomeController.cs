using Abp.AspNetCore.Mvc.Authorization;
using BTGK_NHOM7.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BTGK_NHOM7.Web.Controllers;

[AbpMvcAuthorize]
public class HomeController : BTGK_NHOM7ControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}
