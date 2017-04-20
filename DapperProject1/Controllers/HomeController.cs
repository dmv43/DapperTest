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

             return View(db.Teachers.ToList());
          //  return View();
        }
        [HttpGet]
        public IActionResult ShowTeacherPage(int ID)
        {
            ViewBag.TeacherID = ID;
            // return View(db.Teachers.ToList());
            var teacher = db.Teachers.FirstOrDefault(s=>s.TeacherID == ID);
            return View(teacher);
        
        }
        public JsonResult GetData()
        {
           int pageSize = 2;
            int pageCount = 1;
            //  return  Json(db.Teachers.ToList());
            return Json(GetContext(db, pageSize, pageCount));
        }
        //-----------paging method------------
        public IEnumerable<Teacher> GetContext(TeacherContext t, int pageSize, int pageCount)
        {
            int skipRows = (pageCount - 1) * pageSize;
            var q = from teach in t.Teachers select teach ;
            return q.Skip(skipRows).Take(pageSize).ToList();
        }
        //=============================================


    }
}
