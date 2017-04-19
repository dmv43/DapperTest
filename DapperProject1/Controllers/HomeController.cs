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

            

            
            return  Json(
                db.Teachers.ToList());
           
        }
        //---------------------------------------------

        //=============================================


    }
}
