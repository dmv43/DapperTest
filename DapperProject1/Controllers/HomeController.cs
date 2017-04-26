using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DapperProject1.Models;
using DapperProject1.Repositories;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

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
            return View();
        }
        //main grid page
        public IActionResult TeacherGrid()
        {

             return View(repo.TeacherRepository.GetTeachers());
         
        }
        //concrete teacher page
        [HttpGet]
        public IActionResult ShowTeacherPage(int id)
        {
            ViewBag.id = id;
            
            var teacher = repo.TeacherRepository.Get(id);
            repo.Commit();
            return View(teacher);
        
        }
        //creating JSON from database data
        public JsonResult GetData(int pageSize, int pageIndex, string sortField ="id", string sortOrder ="asc" )
        {
            var q = from teach in repo.TeacherRepository.GetTeachers() select teach;

            var adata = new {data = GetContext(repo, pageSize, pageIndex,sortField,sortOrder), itemsCount = q.Count(), pageSize, pageIndex};
            return Json(adata);
        }
        //-----------paging method------------
        public IEnumerable<Teacher> GetContext(IUnitOfWork t, int pageSize, int pageIndex, string sortField,string sortOrder)
        {
            
            int skipRows = (pageIndex - 1) * pageSize;
            var q = from teach in t.TeacherRepository.GetTeachers() select teach ;
            //  var type = Type.GetType("Teacher");
         //   var xx = typeof(Teacher).GetProperty(sortField);
            var param = Expression.Parameter(typeof(Teacher), "x");
            Expression conversion = Expression.Convert(Expression.Property
            (param, sortField), typeof(object));   

            var zz = Expression.Lambda<Func<Teacher, object>>(conversion, param);

            if (sortOrder == "asc")
            {
               
                return q.AsQueryable().OrderBy(zz).Skip(skipRows).Take(pageSize).ToList();
            }
            else
            {
                return q.AsQueryable().OrderByDescending(zz).Skip(skipRows).Take(pageSize).ToList();
            }
            }
        //=============================================

        public IActionResult DatabaseManipulation()
        {
            TeacherFabric fabric = new TeacherFabric();
            List<Teacher> teachers;

            teachers = fabric.Build(WorkWithFile.deserialize());
           // teachers = fabric.Build(Query.startQuery());
            foreach (var teacher in teachers) {
                if (repo.TeacherRepository.Get(teacher.italki_id) == null)
                {
                    repo.TeacherRepository.Create(teacher);
                }
                else
                {
                    repo.TeacherRepository.Update(teacher);
                }
                    }
            repo.Commit();
            return View();
        }  


    }
}
