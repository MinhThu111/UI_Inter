using DemoUI.ExtensionMethods;
using DemoUI.Lib;
using DemoUI.Models;
using DemoUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoUI.Controllers
{
    public class PersonTypeController : BaseController<PersonTypeController>
    {
        private static List<M_Person> _persons = new List<M_Person>();
        private readonly IS_PersonType _s_person;
        public PersonTypeController(IS_PersonType person)
        {
            _s_person = person;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]//ok
        public async Task<JsonResult> GetList()
        {
            var res = await _s_person.getListPersonType(_accessToken);
            return Json(new M_JResult(res));
        }

        [HttpGet]
        public async Task<JsonResult> P_View(int id)
        {
            var res = await _s_person.getPersonTypeById(_accessToken, id);
            return Json(new M_JResult(res));
        }


        //[HttpGet]//ok
        //public IActionResult P_Add()
        //{
        //    return PartialView();
        //}

        //[HttpPost, ValidateAntiForgeryToken]
        //public async Task<JsonResult> P_Add(EM_Person model)
        //{
        //    M_JResult jResult = new M_JResult();
        //    if (!ModelState.IsValid)
        //    {
        //        jResult.error = new error(0, DataAnnotationExtensionMethod.GetErrorMessage(ModelState));
        //        return Json(jResult);
        //    }
        //    var res = await _s_person.Create(_accessToken, model, _userId);

        //    return Json(jResult.MapData(res));
        //}


        //[HttpGet]//ok
        //public async Task<IActionResult> P_Edit(int id)
        //{
        //    var res = await _s_person.getPersonById(_accessToken, id);
        //    if (res.result != 1 || res.data == null)
        //        return Json(new M_JResult(res));
        //    var model = new EM_Person
        //    {
        //        id = res.data.id,
        //        firstName = res.data.firstName,
        //        lastName = res.data.lastName,
        //        personTypeId = res.data.personTypeId,
        //        gender = res.data.gender,
        //        addressId = res.data.addressId,
        //        phoneNumber = res.data.phoneNumber,
        //        status=res.data.status

        //        //.....
        //    };
        //    return PartialView(model);
        //}

        //[HttpPost, ValidateAntiForgeryToken]
        //public async Task<JsonResult> P_Edit(EM_Person model)
        //{
        //    M_JResult jResult = new M_JResult();
        //    if (!ModelState.IsValid)
        //    {
        //        jResult.error = new error(0, DataAnnotationExtensionMethod.GetErrorMessage(ModelState));
        //        return Json(jResult);
        //    }
        //    var res = await _s_person.Update(_accessToken, model, _userId);

        //    return Json(jResult.MapData(res));
        //}


        //[HttpPost]//ok
        //public async Task<JsonResult> Delete(int id)
        //{
        //    var res = await _s_person.Delete(_accessToken, id);
        //    return Json(new M_JResult(res));
        //}

        //[HttpPost]//ok
        //public async Task<JsonResult> ChangeStatus(int id, int status, DateTime? timer)
        //{
        //    var res = await _s_person.UpdateStatus(_accessToken, id, status, timer, _userId);
        //    return Json(new M_JResult(res));
        //}

    }
}
