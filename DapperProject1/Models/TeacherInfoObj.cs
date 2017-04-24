using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DapperProject1.Models
{
    public class TeacherInfoObj
    {
        public int TeacherInfoObjID { get; set; }
        public string nickname { get; set; }
        public string ItalkiUrl { get; set; }

        public string HireDate { get; set; }
        public int student_count { get; set; }
        public int session_count { get; set; }
        public string textid { get; set; }
        public double pro_rating { get; set; }
        public string origin_country_id { get; set; }
        public string url { get; set; }
        public int user_id { get; set; }

        public List<LanguageObj> language_obj_s { get; set; }
        public List<TagObj> personal_tag { get; set; }

    }
    public class LanguageObj
    {
        public LanguageObj(string l, int id)
        {
            language = l;
            LanguageObjID = id;
        }
        public string language { get; set; }
        public int LanguageObjID { get; set; }
    }
    public class TagObj
    {
        public TagObj(string l, int id)
        {
            tag = l;
            TagID = id;
        }
        public string tag { get; set; }
        public int TagID { get; set; }
    }
}
