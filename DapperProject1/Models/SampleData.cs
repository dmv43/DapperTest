using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DapperProject1.Repositories;

namespace DapperProject1.Models
{
    public class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            {
                var context = serviceProvider.GetService<UnitOfWork>();
                Language english = new Language();
                english.language = "English";
                
                Language french = new Language();
                french.language = "French";
                Tag nice = new Tag();
                nice.tag = "Nice";



                context.TeacherRepository.Create(new Teacher
                {
                    nickname = "John Wick",
                    italki_url = "",
                    hire_date = "14.11.2011",
                    student_count = 125,
                    session_count = 555,
                    description = "blablablatext1",
                    rating = 5.3,
                    url = "",
                    italki_id = 12223,
                    country = "Brazil",
                    languages = { english,french},
                    tags = { nice }



                });
                //context.SaveChanges();
            }

        }
    }
}

