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
        IUnitOfWork _unitOfWork ;
        ITeacherViewFabric _teacherViewFabric;
        ITeacherFabric _teacherFabric;
        IMasterConnection _masterConnection;
        public HomeController(IUnitOfWork r,ITeacherViewFabric f,ITeacherFabric fa, IMasterConnection conn)
        {
            _masterConnection = conn;
            _unitOfWork = r;
            _teacherViewFabric = f;
            _teacherFabric = fa;
        }
        public IActionResult Index()
        {
           // _masterConnection.Execute();
            return View();
        }
       
        public IActionResult ItalkiManipulation()
        {
            
            return View();
        }
        //main grid page
        public IActionResult TeacherGrid()
        {

            // return View(_unitOfWork.TeacherRepository.GetTeachers());
            return View();
         
        }
        //concrete teacher page
        [HttpGet]
        public IActionResult ShowTeacherPage(int id)
        {
            ViewBag.id = id;
            
            var teacher = _unitOfWork.TeacherRepository.Get(id);
            
            _unitOfWork.Commit();
            return View(teacher);
        
        }
        //creating JSON from database data
        public JsonResult GetData(int pageSize, int pageIndex, string sortField ="id", string sortOrder ="asc",string nickname =null )
        {
            var allTeachers = from teach in _unitOfWork.TeacherRepository.GetTeachers() select teach;

            IEnumerable<Teacher> final = GetContext(_unitOfWork, pageSize, pageIndex, sortField, sortOrder, nickname);
            var myFinalData = _teacherViewFabric.Build(final);

            var adata = new {data = myFinalData, itemsCount = allTeachers.Count(), pageSize, pageIndex};
            
            return Json(adata);
        }
        //-----------paging method------------
        public IEnumerable<Teacher> GetContext(IUnitOfWork unitOfWork, int pageSize, int pageIndex, string sortField,string sortOrder,string nickname)
        {
            
            int skipRows = (pageIndex - 1) * pageSize;
            var all_teachers = from teach in unitOfWork.TeacherRepository.GetTeachers() select teach ;
            var param = Expression.Parameter(typeof(Teacher), "x");
            Expression conversion = Expression.Convert(Expression.Property
            (param, sortField), typeof(object));   

            //--------------------------
            foreach(var tea in all_teachers)
            {
                if(tea.student_count==0)
                {
                    tea.student_count = 1;
                }
            }
            //--------------------------

            var zz = Expression.Lambda<Func<Teacher, object>>(conversion, param);
            List<Teacher> paginatedTeachers;
            if (nickname != null)
            {

                paginatedTeachers = all_teachers.AsQueryable().OrderBy(s => s.nickname).SkipWhile(s => !s.nickname.Contains(nickname)).Take(pageSize).ToList();
                foreach (var x in all_teachers)
                {
                    x.session_to_student = Math.Round((double)x.session_count / x.student_count, 2, MidpointRounding.AwayFromZero);
                }
                return paginatedTeachers;
            }
            else
            {
                if (sortOrder == "asc")
                {
                    if (sortField == "session_to_student")
                    {

                        paginatedTeachers = all_teachers.AsQueryable().OrderBy(s => s.session_count / s.student_count).Skip(skipRows).Take(pageSize).ToList();

                    }
                    else
                    {
                        paginatedTeachers = all_teachers.AsQueryable().OrderBy(zz).Skip(skipRows).Take(pageSize).ToList();
                    }
                    foreach (var x in all_teachers)
                    {
                        x.session_to_student = Math.Round((double)x.session_count / x.student_count, 2, MidpointRounding.AwayFromZero);
                    }
                    return paginatedTeachers;


                }
                else
                {
                    if (sortField == "session_to_student")
                    {
                        paginatedTeachers = all_teachers.AsQueryable().OrderByDescending(s => s.session_count / s.student_count).Skip(skipRows).Take(pageSize).ToList();
                    }
                    else
                    {
                        paginatedTeachers = all_teachers.AsQueryable().OrderByDescending(zz).Skip(skipRows).Take(pageSize).ToList();
                    }
                    foreach (var x in paginatedTeachers)
                    {
                        x.session_to_student = Math.Round((double)x.session_count / x.student_count, 2, MidpointRounding.AwayFromZero);
                    }
                    return paginatedTeachers;

                }
            }
            
        }
        //=============================================

        public IActionResult DatabaseManipulation()
        {
            
            List<Teacher> teachers;

          teachers = _teacherFabric.Build(WorkWithFile.deserialize());
        //    teachers = _teacherFabric.Build(Deserialize.deserializeBulk((Query.startQuery())));
            foreach (var teacher in teachers) {
                if (_unitOfWork.TeacherRepository.Get(teacher.italki_id) == null)
                {
                    _unitOfWork.TeacherRepository.Create(teacher);
                }
                else
                {
                    _unitOfWork.TeacherRepository.Update(teacher);
                }
                    }
            _unitOfWork.Commit();
            return View();
        }  


    }
}
