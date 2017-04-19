using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DapperProject1.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NumberOfStudents { get; set; }
        public int NumberOfLessons { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public string Location { get; set; }
        public string URL { get; set; }
        public string ItalkiURL { get; set; }

        //      public static IEnumerable<Teacher> GetTeachers(TeacherContext context)
        //      {

        //connection can also be injected
        //           return context.Teachers
        //                    .Select(entity => new Teacher())
        //                   .ToList();



        //      }
    }
}
