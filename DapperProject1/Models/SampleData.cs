using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DapperProject1.Models
{
    public class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            {
                var context = serviceProvider.GetService<TeacherContext>();
               //context.Database.EnsureDeleted();
                if (!context.Teachers.Any())
                {
                    context.Teachers.AddRange(
                        new Teacher
                        {
                            //TeacherID =1,
                            FirstName = "George",
                            LastName = "Michael",
                            NumberOfStudents = 130,
                            NumberOfLessons = 220,
                            Description = "bla bla description",
                            Rating = 4.9,
                            Location = "Ukraine",
                            URL = "/Home/ShowTeacherPage/1",
                            ItalkiURL = "www.italki.com/teacher/598613"
                        },
                        
                        new Teacher
                        {
                           // TeacherID = 2,
                            FirstName = "Michael",
                            LastName = "Jackson",
                            NumberOfStudents = 110,
                            NumberOfLessons = 620,
                            Description = "bla bla description2",
                            Rating = 3.9,
                            Location = "Nowhere",
                            URL = "/Home/ShowTeacherPage/2",
                            ItalkiURL = "www.italki.com/teacher/4045562"
                        },
                        new Teacher
                        {
                           // TeacherID = 3,
                            FirstName = "Emma",
                            LastName = "Stone",
                            NumberOfStudents = 430,
                            NumberOfLessons = 1220,
                            Description = "bla bla description3",
                            Rating = 5.0,
                            Location = "United States",
                            URL = "/Home/ShowTeacherPage/3",
                            ItalkiURL = "www.italki.com/teacher/1685538"
                        }

                    );
                    context.SaveChanges();
                }
            }
        }
    }
}

