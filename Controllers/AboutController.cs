using DemoUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoUI.Controllers
{

    public class AboutController : Controller
    {
        [HttpGet("introduce")]
        //[HttpGet("gioi-thieu")]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet("gioi-thieu/{id:int}")]
        ////[HttpGet("introduce/{id:int}")]
        ////[HttpGet("gioi-thieu/{name}-{id:int}")]
        ////[HttpGet("gioi-thieu")]
        //[HttpGet]
        public IActionResult Detail(int id, string name)
        {
            ViewBag.id = id;
            ViewBag.name=name;
            return View();
        }

    }
}
