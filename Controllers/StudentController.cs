using Microsoft.AspNetCore.Mvc;
using DemoUI.Services;
using DemoUI.Models;
using DemoUI.Lib;
using DemoUI.ExtensionMethods;

namespace DemoUI.Controllers
{
 
    public class StudentController : Controller
    {
        private readonly IS_Student _student;
        private static List<M_Student> _lststudents = new List<M_Student>();
        
        //Start and get data
        public StudentController(IS_Student student)
        {
            _student = student;
            _lststudents = _student.getAll();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetList(string status, string gender)
        {
            List<int?> lstStatus = new List<int?>();
            foreach (string s in status.Split(","))
                lstStatus.Add(Convert.ToInt32(s.Replace(".", "").Replace(" ", "")));
            List<int?> lstGender = new List<int?>();
            foreach (string s in gender.Split(","))
                lstGender.Add(Convert.ToInt32(s.Replace(".", "").Replace(" ", "")));
            
            List<M_Student> data = _lststudents.Where(w => lstStatus.Contains(w.status)).ToList();
            data = data.Where(w => lstGender.Contains(w.gender)).ToList();

            //List<M_Student> data = _lststudents;
            var res = new ResponseData<List<M_Student>>();
            res.result = 1;
            
            res.data = data;
            return Json(new M_JResult(res));
        }

        //Add
        [HttpGet]
        public async Task<IActionResult>P_Add()
        {
            return PartialView();
        }
        public async Task<JsonResult> P_Add(M_Student model)
        {
            M_JResult jResult = new M_JResult();
            if (!ModelState.IsValid)
            {
                jResult.error = new error(0, DataAnnotationExtensionMethod.GetErrorMessage(ModelState));
                return Json(jResult);
            }
            var res = new ResponseData<M_Student>();
            
            
            M_Student student = new M_Student
            {
                id = model.id,
                firstName = model.firstName,
                lastName = model.lastName,
                gender = model.gender,
                email = model.email,
                avatar = model.avatar,
                schoolName = model.schoolName,
                className = model.className,
                status = 0

            };
            _lststudents.Add(student);
            res.result = 1;
            res.data = student;
            return Json(jResult.MapData(res));
        }

        //Edit
        [HttpGet]
        public async Task<IActionResult> P_Edit(int id)
        {
            var res = new ResponseData<M_Student>();
            res.result = 1;
           
            var model = new EM_Student();
            return PartialView(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> P_Edit(EM_Student model)
        {
            M_JResult jResult = new M_JResult();
            if (!ModelState.IsValid)
            {
                jResult.error = new error(0, DataAnnotationExtensionMethod.GetErrorMessage(ModelState));
                return Json(jResult);
            }
            var res = new ResponseData<M_Student>();
            foreach (M_Student item in _lststudents)
            {
                if (item.id == model.id)
                {
                    item.firstName = model.firstName;
                    item.lastName = model.lastName;
                    item.gender = model.gender;
                    item.email = model.email;
                    item.avatar = model.avatar;
                    item.className = model.className;
                    item.schoolName = model.schoolName;

                    res.data = item;
                }
            }
            res.result = 1;
            return Json(jResult.MapData(res));
        }

        //Delete
        [HttpPost]
        public async Task<JsonResult>Delete(int id)
        {
            var res = new ResponseData<M_Student>();
            res.result = 1;
            return Json(new M_JResult(res));
        }

        //Change status
        [HttpPost]
        public async Task<JsonResult>ChangeStatus(int id, int status,DateTime timer)
        {
            M_JResult jResult = new M_JResult();
            var res = new ResponseData<M_Student>();
            M_Student student = _lststudents.Where(s => s.id == id).FirstOrDefault();
            student.status = status;
            _lststudents.Add(student);
            res.result = 1;
            res.data = student;
            return Json(jResult.MapData(res));
        }

        

        

    }
}
