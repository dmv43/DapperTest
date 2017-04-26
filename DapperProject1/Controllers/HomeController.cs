﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DapperProject1.Models;
using DapperProject1.Repositories;
using System.IO;
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
        public IActionResult TeacherGrid()
        {

             return View(repo.TeacherRepository.GetTeachers());
          //  return View();
        }
        [HttpGet]
        public IActionResult ShowTeacherPage(int id)
        {
            ViewBag.id = id;
            
            var teacher = repo.TeacherRepository.Get(id);
            repo.Commit();
            return View(teacher);
        
        }
        public JsonResult GetData(int pageSize, int pageIndex)
        {
            var q = from teach in repo.TeacherRepository.GetTeachers() select teach;

            var adata = new {data = GetContext(repo, pageSize, pageIndex), itemsCount = q.Count(), pageSize, pageIndex};
            return Json(adata);
        }
        //-----------paging method------------
        public IEnumerable<Teacher> GetContext(IUnitOfWork t, int pageSize, int pageIndex)
        {
            int skipRows = (pageIndex - 1) * pageSize;
            var q = from teach in t.TeacherRepository.GetTeachers() select teach ;
            
            return q.OrderBy(p => p.id).Skip(skipRows).Take(pageSize).ToList();
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
