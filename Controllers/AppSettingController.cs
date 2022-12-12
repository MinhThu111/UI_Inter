using DemoUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DemoUI.Controllers
{
    [Route("appSettings")]
    public class AppSettingController : Controller
    {
        //connect giữa model và json
        private readonly IOptions<M_Contact> appSettings;
        public AppSettingController(IOptions<M_Contact> _appSettings)
        {
            appSettings = _appSettings;
        }
        //[Route("a",Name ="ABC")]
        [HttpGet]
        public IActionResult FirstWay()
        {
            var db = appSettings.Value;
            ViewData["contact"] = db;
            return View();
        }
        [Route("jsontest/{id?}")]
        //[HttpGet("int/{id:int}")]
        public JsonResult ReturnJson(int id)
        {
            return Json(new { a="abc", b="bbc", ids= id });
        }
        [Route("partial")]
        [HttpGet]
        public PartialViewResult ReturnPartial()
        {
            ViewBag.data = "nguyen thi minh thu";
            return PartialView();
        }
    }
}
