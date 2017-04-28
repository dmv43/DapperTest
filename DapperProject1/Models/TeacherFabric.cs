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
               
                if (t.data[i].nickname.ToCharArray().First() == ' ')
                {
                   
                    teacher.nickname = t.data[i].nickname.TrimStart(' '); ;
                    
                }
                else
                {
                    teacher.nickname = t.data[i].nickname;
                }
                teacher.italki_url = "www.italki.com/teacher/"+ t.data[i].teacher_info_obj.user_id;
                teacher.min_price = t.data[i].teacher_info_obj.min_price_usd;
                teacher.italki_id = t.data[i].teacher_info_obj.user_id;
                
                teacher.rating = t.data[i].teacher_info_obj.tutor_rating;
                foreach (var z in t.data[i].language_obj_s)
                {
                    Language l = new Language();
                    l.language = z.language;
                    l.id = z.id;
                    teacher.languages.Add(l);
                }
                foreach (var tg in t.data[i].personal_tag)
                {
                    Tag ss = new Tag();
                    ss.tag = tg.tag_name;
                    ss.id = tg.tag_id;
                    teacher.tags.Add(ss);
                }

                teacher.session_count = t.data[i].teacher_info_obj.session_count;
                teacher.student_count = t.data[i].teacher_info_obj.student_count;

                teacher.url = "/Home/ShowTeacherPage?id="+ t.data[i].teacher_info_obj.user_id;
                teacher.description = t.data[i].teacher_info_obj.intro;
                teacher.country = t.data[i].origin_country_id;
               // teacher.session_to_student = (double)t.data[i].teacher_info_obj.session_count / t.data[i].teacher_info_obj.student_count;
                teach.Add(teacher);
            }
            return teach;
        }
    }
}
