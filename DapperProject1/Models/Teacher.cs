using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject1.Models
{
    public class Teacher
    {
        public int id{ get; set; }
        public string nickname { get; set; }
        public string italki_url { get; set; }

        public string hire_date { get; set; }
        public int student_count { get; set; }
        public int session_count { get; set; }
        public string description { get; set; }
        public double rating { get; set; }
        public string country { get; set; }
        public string url { get; set; }
        public int italki_id { get; set; }

        public List<Language> languages { get; set; }
        public List<Tag> tags { get; set; }

    }
    public class Language
    {
        
        public string language { get; set; }
        public int id { get; set; }
    }
    public class Tag
    {
        public string tag { get; set; }
        public int id { get; set; }
    }
}
