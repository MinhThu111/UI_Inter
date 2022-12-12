using Microsoft.AspNetCore.Mvc;
using DemoUI.Models;
using DemoUI.Lib;
using System.Net.Http.Headers;
using System;

namespace DemoUI.Controllers
{
    public class PeopleController : Controller
    {
        //call api old
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetList(string status)
        {
            var res = new ResponseData<List<M_Person>>();
            List<int?> lstStatus = new List<int?>();
            foreach (string s in status.Split(","))
                lstStatus.Add(Convert.ToInt32(s.Replace(".", "").Replace(" ", "")));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://api-interns.h2aits.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(@"Person/getListPerson");
                if (response.IsSuccessStatusCode)
                {
                    //var _testO = await response.Content.ReadAsStringAsync();
                    res = await response.Content.ReadAsAsync<ResponseData<List<M_Person>>>();
                }
            };
            return Json(new M_JResult(res));
        }
        public async Task<IActionResult> P_Add()
        {
            return PartialView();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> P_Add(M_Person person)
        {
            var res = new ResponseData<M_Person>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://api-interns.h2aits.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                MultipartFormDataContent formData = new MultipartFormDataContent();
                formData.Add(new StringContent(String.IsNullOrEmpty(person.firstName) ? "" : person.firstName), "firstName");
                formData.Add(new StringContent(String.IsNullOrEmpty(person.lastName) ? "" : person.lastName), "lastName");
                formData.Add(new StringContent(person.personTypeId.ToString()), "personTypeId");
                formData.Add(new StringContent(person.birthDay.ToString()), "birthDay");
                formData.Add(new StringContent(person.gender.ToString()), "gender");
                formData.Add(new StringContent(person.nationalityId.ToString()), "nationalityId");
                formData.Add(new StringContent(person.religionId.ToString()), "religionId");
                formData.Add(new StringContent(person.folkId.ToString()), "folkId");
                formData.Add(new StringContent(person.addressId.ToString()), "addressId");
                formData.Add(new StringContent(String.IsNullOrEmpty(person.phoneNumber) ? "" : person.phoneNumber), "phoneNumber");
                formData.Add(new StringContent(String.IsNullOrEmpty(person.email) ? "" : person.email), "email");

                var response = await client.PutAsync(@"Person/Create", formData);
                if (response.IsSuccessStatusCode)
                {
                    res = await response.Content.ReadAsAsync<ResponseData<M_Person>>();
                }
            }
            return Json(new M_JResult(res));
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var res = new ResponseData<M_Person>();
            res.result = 1;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://api-interns.h2aits.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync(@"Person/Delete?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    res = await response.Content.ReadAsAsync<ResponseData<M_Person>>();
                }
            }

            return Json(new M_JResult(res));
        }
        public async Task<IActionResult> P_Update(int id)
        {
            var res = new ResponseData<M_Person>();
            res.result = 1;

            var model = new EM_Person();
            return PartialView(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> P_Update(EM_Person person)
        {
            var res = new ResponseData<M_Person>();
            M_JResult jResult = new M_JResult();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://api-interns.h2aits.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                MultipartFormDataContent formData = new MultipartFormDataContent();
                formData.Add(new StringContent(person.id.ToString()), "id");
                formData.Add(new StringContent(String.IsNullOrEmpty(person.firstName) ? "" : person.firstName), "firstName");
                formData.Add(new StringContent(String.IsNullOrEmpty(person.lastName) ? "" : person.lastName), "lastName");
                formData.Add(new StringContent(person.personTypeId.ToString()), "personTypeId");
                formData.Add(new StringContent(person.gender.ToString()), "gender");
                formData.Add(new StringContent(person.addressId.ToString()), "addressId");
                formData.Add(new StringContent(person.phoneNumber), "phonNumber");

                formData.Add(new StringContent(person.timer.ToString()), "timer");
                formData.Add(new StringContent(person.status.ToString()), "status");

                var response = await client.PutAsync(@"Person/Update", formData);
                if (response.IsSuccessStatusCode)
                {
                    res = await response.Content.ReadAsAsync<ResponseData<M_Person>>();
                }
            }

            return Json(jResult.MapData(res));
        }
        [HttpPost]
        public async Task<JsonResult> ChangeStatus(int id, int status, DateTime timer)
        {
            M_JResult jResult = new M_JResult();
            var res = new ResponseData<M_Student>();
            
            res.result = 1;
            return Json(jResult.MapData(res));
        }
    }
}
