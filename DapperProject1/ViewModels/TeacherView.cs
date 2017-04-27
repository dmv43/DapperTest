using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject1.ViewModels
{
    public class TeacherView
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public string italki_url { get; set; }

        public string min_price { get; set; }
        public int student_count { get; set; }
        public int session_count { get; set; }
        public string description { get; set; }
        public string rating { get; set; }
        public string country { get; set; }
        public string url { get; set; }
        public int italki_id { get; set; }

        public double session_to_student { get; set; }
        public List<LanguageView> languages = new List<LanguageView>();
        public List<TagView> tags = new List<TagView>();

    }
    public class LanguageView
    {

        public string language { get; set; }
        public int id { get; set; }
    }
    public class TagView
    {
        public string tag { get; set; }
        public int id { get; set; }
    }
}
