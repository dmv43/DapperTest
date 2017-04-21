using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DapperProject1.Models;



namespace DapperProject1.Controllers
{
    public class HomeController : Controller
    {
        TeacherContext db;
        public HomeController(TeacherContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Teachers.ToList());
        }
        public IActionResult TeacherGrid()
        {

           //  return View(db.Teachers.ToList());
            return View();
        }
        [HttpGet]
        public IActionResult ShowTeacherPage(int ID)
        {
            ViewBag.TeacherID = ID;
            // return View(db.Teachers.ToList());
            var teacher = db.Teachers.FirstOrDefault(s=>s.TeacherID == ID);
            return View(teacher);
        
        }
        public  JsonResult GetData(int pageSize, int pageIndex)
        {
           
          //  string pageSizeString = Request.Query.FirstOrDefault(p => p.Key == "pageSize").Value;
         //   int pageSize = Int32.Parse(pageSizeString);
            
         //   string pageIndexString = Request.Query.FirstOrDefault(p => p.Key == "pageIndex").Value;
         //   int pageIndex = Int32.Parse(pageSizeString);
            

            var adata = new { data = GetContext(db, pageSize, pageIndex), itemsCount = db.Teachers.Count(), pageSize, pageIndex };
            
            return Json(adata);
           // return Json(new { data = GetContext(db, pageSize, pageIndex),itemsCount = db.Teachers.Count(),pageSize,pageIndex });
           
        }
        //-----------paging method------------
        public IEnumerable<Teacher> GetContext(TeacherContext t, int pageSize, int pageIndex)
        {
           // pageSize = 2;
          //  pageIndex = 1;
            int skipRows = (pageIndex - 1) * pageSize;
            var q = from teach in t.Teachers select teach ;
            return q.OrderBy(p=>p.TeacherID).Skip(skipRows).Take(pageSize).ToList();
        }
        //=============================================


    }
}
