using Abp.AspNetCore.Mvc.Authorization;
using BTGK_NHOM7.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BTGK_NHOM7.Web.Controllers;

[AbpMvcAuthorize]
public class HomeController : BTGK_NHOM7ControllerBase
{
    public ActionResult Index()
    {
        if (User.IsInRole("Admin") || User.IsInRole("GiangVien"))
        {
            return View();
        }

        if (User.IsInRole("HocVien"))
        {
            return RedirectToAction("List", "ToeicExams");
        }

        return View();
    }
}