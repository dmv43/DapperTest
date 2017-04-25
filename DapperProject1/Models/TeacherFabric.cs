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
            List<Teacher> teach = new List<Teacher>();
            for(int i=0; i<t.data.Count; i++)
            {

                teach[i].nickname = t.data[i].nickname;
                teach[i].italki_url = "https://italki.com/teacher/"+ t.data[i].teacher_info_obj.user_id;
                teach[i].hire_date = t.data[i].last_login_time;
                teach[i].italki_id = t.data[i].teacher_info_obj.user_id;
                teach[i].rating = double.Parse(t.data[i].teacher_info_obj.pro_rating);
                foreach (var z in t.data[i].language_obj_s)
                {
                    Language l = new Language()
                    {
                        language = z.language

                    };
                    teach[i].languages.Add(l);
                }
                foreach (var tg in t.data[i].personal_tag)
                {
                    Tag ss = new Tag()
                    {
                        tag = tg

                    };
                    teach[i].tags.Add(ss);
                }

                teach[i].session_count = t.data[i].teacher_info_obj.session_count;
                teach[i].student_count = t.data[i].teacher_info_obj.student_count;

                teach[i].url = "/Home/ShowTeacherPage"+teach[i].id;
                teach[i].description = t.data[i].textid;
                teach[i].country = t.data[i].origin_country_id;
            }
            return teach;
        }
    }
}
