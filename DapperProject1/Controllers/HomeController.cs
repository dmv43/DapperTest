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
using DapperProject1.ViewModels;
namespace DapperProject1.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork repo ;
        ITeacherViewFabric fab;
        ITeacherFabric fabric;
        public HomeController(IUnitOfWork r,ITeacherViewFabric f,ITeacherFabric fa)
        {
            repo = r;
            fab = f;
            fabric = fa;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ItalkiManipulation()
        {
            return View();
        }
        //main grid page
        public IActionResult TeacherGrid()
        {

            // return View(repo.TeacherRepository.GetTeachers());
            return View();
         
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
        public JsonResult GetData(int pageSize, int pageIndex, string sortField ="id", string sortOrder ="asc",string nickname =null )
        {
            var q = from teach in repo.TeacherRepository.GetTeachers() select teach;

            IEnumerable<Teacher> final = GetContext(repo, pageSize, pageIndex, sortField, sortOrder, nickname);
            var myFinalData = fab.Build(final);

            var adata = new {data = myFinalData, itemsCount = q.Count(), pageSize, pageIndex};
            
            return Json(adata);
        }
        //-----------paging method------------
        public IEnumerable<Teacher> GetContext(IUnitOfWork t, int pageSize, int pageIndex, string sortField,string sortOrder,string nickname)
        {
            
            int skipRows = (pageIndex - 1) * pageSize;
            var q = from teach in t.TeacherRepository.GetTeachers() select teach ;
            var param = Expression.Parameter(typeof(Teacher), "x");
            Expression conversion = Expression.Convert(Expression.Property
            (param, sortField), typeof(object));   

            //--------------------------
            foreach(var tea in q)
            {
                if(tea.student_count==0)
                {
                    tea.student_count = 1;
                }
            }
            //--------------------------

            var zz = Expression.Lambda<Func<Teacher, object>>(conversion, param);
            List<Teacher> y;
            if (nickname != null)
            {

                y = q.AsQueryable().OrderBy(s => s.nickname).SkipWhile(s => !s.nickname.Contains(nickname)).Take(pageSize).ToList();
                foreach (var x in q)
                {
                    x.session_to_student = Math.Round((double)x.session_count / x.student_count, 2, MidpointRounding.AwayFromZero);
                }
                return y;
            }
            else
            {
                if (sortOrder == "asc")
                {
                    if (sortField == "session_to_student")
                    {

                        y = q.AsQueryable().OrderBy(s => s.session_count / s.student_count).Skip(skipRows).Take(pageSize).ToList();

                    }
                    else
                    {
                        y = q.AsQueryable().OrderBy(zz).Skip(skipRows).Take(pageSize).ToList();
                    }
                    foreach (var x in q)
                    {
                        x.session_to_student = Math.Round((double)x.session_count / x.student_count, 2, MidpointRounding.AwayFromZero);
                    }
                    return y;


                }
                else
                {
                    if (sortField == "session_to_student")
                    {
                        y = q.AsQueryable().OrderByDescending(s => s.session_count / s.student_count).Skip(skipRows).Take(pageSize).ToList();
                    }
                    else
                    {
                        y = q.AsQueryable().OrderByDescending(zz).Skip(skipRows).Take(pageSize).ToList();
                    }
                    foreach (var x in y)
                    {
                        x.session_to_student = Math.Round((double)x.session_count / x.student_count, 2, MidpointRounding.AwayFromZero);
                    }
                    return y;

                }
            }
            
        }
        //=============================================

        public IActionResult DatabaseManipulation()
        {
            
            List<Teacher> teachers;

          //  teachers = fabric.Build(WorkWithFile.deserialize());
            teachers = fabric.Build(Query.startQuery());
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
