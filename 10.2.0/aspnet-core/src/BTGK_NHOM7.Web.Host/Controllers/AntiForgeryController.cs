using Abp.Web.Security.AntiForgery;
using BTGK_NHOM7.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace BTGK_NHOM7.Web.Host.Controllers
{
    public class AntiForgeryController : BTGK_NHOM7ControllerBase
    {
        private readonly IAntiforgery _antiforgery;
        private readonly IAbpAntiForgeryManager _antiForgeryManager;

        public AntiForgeryController(IAntiforgery antiforgery, IAbpAntiForgeryManager antiForgeryManager)
        {
            _antiforgery = antiforgery;
            _antiForgeryManager = antiForgeryManager;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }

        public void SetCookie()
        {
            _antiForgeryManager.SetCookie(HttpContext);
        }
    }
}
