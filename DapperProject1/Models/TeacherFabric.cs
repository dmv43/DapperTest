using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject1.Models
{
    public interface ITeacherFabric
    {
        Teacher Build(TeacherInfoObj t);
    }
    public class TeacherFabric : ITeacherFabric
    {
        public  Teacher Build(TeacherInfoObj t)
        {
            Teacher teach = new Teacher();
            teach.nickname = t.nickname;
            teach.italki_url = t.ItalkiUrl;
            teach.hire_date = t.HireDate;
            teach.italki_id = t.user_id;
            teach.rating = t.pro_rating;
            teach.languages = t.language_obj_s;
            teach.session_count = t.session_count;
            teach.student_count = t.student_count;
            teach.tags = t.personal_tag;
            teach.url = t.url;
            teach.description = t.textid;
            teach.country = t.origin_country_id;

            return teach;
        }
    }
}
