using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DapperProject1.Models;
using DapperProject1.Repositories;
namespace DapperProject1.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork repo ;
        public HomeController(IUnitOfWork r)
        {
            repo = r;
        }
        public IActionResult Index()
        {
            return View(repo.TeacherRepository.GetTeachers());
        }
        public IActionResult TeacherGrid()
        {

             return View(repo.TeacherRepository.GetTeachers());
          //  return View();
        }
        [HttpGet]
        public IActionResult ShowTeacherPage(int ID)
        {
            ViewBag.TeacherID = ID;
            // return View(db.Teachers.ToList());
            var teacher = repo.TeacherRepository.Get(ID);
            return View(teacher);
        
        }
        public JsonResult GetData()
        {
           int pageSize = 2;
            int pageCount = 1;
            //  return  Json(db.Teachers.ToList());
            return Json(GetContext(repo, pageSize, pageCount));
        }
        //-----------paging method------------
        public IEnumerable<TeacherInfoObj> GetContext(IUnitOfWork t, int pageSize, int pageCount)
        {
            int skipRows = (pageCount - 1) * pageSize;
            var q = from teach in t.TeacherRepository.GetTeachers() select teach ;
            return q.Skip(skipRows).Take(pageSize).ToList();
        }
        //=============================================


    }
}
