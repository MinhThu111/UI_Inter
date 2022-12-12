using DemoUI.Lib;
using DemoUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoUI.Controllers
{
    public class FolkController : BaseController<FolkController>
    {
        private readonly IS_Folk _s_folk;
        public FolkController(IS_Folk s_folk)
        {
            _s_folk = s_folk;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]//ok
        public async Task<JsonResult> GetList()
        {
            var res = await _s_folk.getListFolk(_accessToken);

            return Json(new M_JResult(res));
        }

    }
}
