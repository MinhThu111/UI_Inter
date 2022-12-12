using DemoUI.Lib;
using DemoUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoUI.Controllers
{
    public class NationalityController : BaseController<NationalityController>
    {
        private readonly IS_Nationality _s_nationality;
        public NationalityController(IS_Nationality s_nationality)
        {
            _s_nationality = s_nationality;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]//ok
        public async Task<JsonResult> GetList()
        {
            var res = await _s_nationality.getListNationality(_accessToken);

            return Json(new M_JResult(res));
        }

    }
}
