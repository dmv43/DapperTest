using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject1.Models
{
    public interface ITeacherFabric
    {
        List<Teacher> Build(RootObject t);
    }
    public class TeacherFabric : ITeacherFabric
    {
        public  List<Teacher> Build(RootObject t)
        {
            List<Teacher> teach = new List<Teacher>(t.data.Count);
            for(int i=0; i<t.data.Count; i++)
            {
                Teacher teacher = new Teacher();


                teacher.nickname = t.data[i].nickname;
                teacher.italki_url = "https://italki.com/teacher/"+ t.data[i].teacher_info_obj.user_id;
                teacher.hire_date = t.data[i].last_login_time;
                teacher.italki_id = t.data[i].teacher_info_obj.user_id;
                teacher.rating = double.Parse(t.data[i].teacher_info_obj.pro_rating);
                foreach (var z in t.data[i].language_obj_s)
                {
                    Language l = new Language()
                    {
                        language = z.language

                    };
                    teacher.languages.Add(l);
                }
                foreach (var tg in t.data[i].personal_tag)
                {
                    Tag ss = new Tag()
                    {
                        tag = tg.tag_name

                    };
                    teacher.tags.Add(ss);
                }

                teacher.session_count = t.data[i].teacher_info_obj.session_count;
                teacher.student_count = t.data[i].teacher_info_obj.student_count;

                teacher.url = "/Home/ShowTeacherPage"+teach[i].id;
                teacher.description = t.data[i].textid;
                teacher.country = t.data[i].origin_country_id;
                teach.Add(teacher);
            }
            return teach;
        }
    }
}
