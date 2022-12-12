using DemoUI.Lib;
using DemoUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoUI.Controllers
{
    public class ReligionController : BaseController<ReligionController>
    {
        private readonly IS_Religion _s_religion;
        public ReligionController(IS_Religion s_religion)
        {
            _s_religion = s_religion;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]//ok
        public async Task<JsonResult> GetList()
        {
            var res = await _s_religion.getListReligion(_accessToken);

            return Json(new M_JResult(res));
        }

    }
}
