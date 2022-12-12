using Microsoft.AspNetCore.Mvc;
using DemoUI.Services;
using DemoUI.Models;
using DemoUI.Lib;
using DemoUI.ExtensionMethods;


namespace DemoUI.Controllers
{
    public class UploadFileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadFileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        //Upload file
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetList()
        {

            List<M_FileName> lstfname = new List<M_FileName>();
            string[] fileLoc = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files"));
            int id = 0;
            Dictionary<int, string> dict = new Dictionary<int, string>();
            foreach (string file in fileLoc)
            {
                M_FileName fname = new M_FileName()
                {
                    id=id++,
                    fName=Path.GetFileName(file)
                };
                lstfname.Add(fname);
            }
           


            var res = new ResponseData<List<M_FileName>>();
            res.result = 1;
            res.data = lstfname;
            return Json(new M_JResult(res));
        }


        //Single file
        [HttpGet]
        public async Task<IActionResult> P_UploadSingleFile()
        {
            return PartialView();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> P_UploadSingleFile(M_UploadSingleFile model)
        {
            M_JResult jResult = new M_JResult();
            var res = new ResponseData<M_Student>();
            res.result = 1;
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(model.singlefile.FileName);
                string fileName = model.id + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.singlefile.CopyTo(stream);
                }

            }
            else
            {
                jResult.error = new error(0, DataAnnotationExtensionMethod.GetErrorMessage(ModelState));
                return Json(jResult);
            }

            return Json(jResult.MapData(res));
        }

        //multiple files
        [HttpGet]
        public async Task<IActionResult> P_UploadMutipleFile()
        {
            return PartialView();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> P_UploadMutipleFile(M_UploadMultipleFile model)
        {
            M_JResult jResult = new M_JResult();
            var res = new ResponseData<M_Student>();
            if (ModelState.IsValid)
            {
                if (model.multiplefiles.Count > 0)
                {
                    int counts = 1;
                    foreach (var file in model.multiplefiles)
                    {

                        string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files");
                        FileInfo fileInfo = new FileInfo(file.FileName);
                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        string filename = model.id.ToString() + '.' + counts.ToString() + fileInfo.Extension;

                        string fileNameWithPath = Path.Combine(path, filename);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(stream);

                        }
                        counts++;
                    }
                    res.result = 1;
                }
                else
                {
                    res.result = 0;
                }
            }
            else
            {
                jResult.error = new error(0, DataAnnotationExtensionMethod.GetErrorMessage(ModelState));
                return Json(jResult);
            }
            return Json(jResult.MapData(res));
        }

        //delete file
        [HttpDelete]
        public async Task<JsonResult> Delete(string fName)
        {
            //string fname = "2.png";
            //List<M_FileName> lstfname = new List<M_FileName>();

            //string[] fileLoc = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files"));
            //int ids = 0;
            //Dictionary<int, string> dict = new Dictionary<int, string>();
            //foreach (string file in fileLoc)
            //{
            //    M_FileName fname = new M_FileName()
            //    {
            //        id = ids++,
            //        fName = Path.GetFileName(file)
            //    };
            //    lstfname.Add(fname);
            //}
            //string fName;
            //fName = (lstfname.Where(s => s.id == id).FirstOrDefault()).fName;
            var res = new ResponseData<M_FileName>();
            var path = Path.Combine(Directory.GetCurrentDirectory(), _webHostEnvironment.WebRootPath ,"File", fName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                res.result = 1;
            }
            return Json(new M_JResult(res));
        }


    }
}
