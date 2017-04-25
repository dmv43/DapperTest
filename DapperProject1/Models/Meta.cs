using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject1.Models
{
    public class Meta
    {
        public bool has_next { get; set; }
        public int current_page { get; set; }
        public int page_size { get; set; }
    }

    public class ExamResultShownObj
    {
        public int status { get; set; }
        public long url_id { get; set; }
        public int id { get; set; }
    }

    public class TeacherInfoObj
    {
        public string trial_price { get; set; }
        public int student_count { get; set; }
        public string video_url { get; set; }
        public int user_id { get; set; }
        public string tutor_rating { get; set; }
        public string max_price_usd { get; set; }
        public string min_price_usd { get; set; }
        public int session_count { get; set; }
        public string teacher_tags { get; set; }
        public int instant_tutoring_status { get; set; }
        public string trial_price_usd { get; set; }
        public string instant_tutoring_price { get; set; }
        public string intro { get; set; }
        public string teacher_child_tags { get; set; }
        public string min_price { get; set; }
        public int has_trial { get; set; }
        public string pro_rating { get; set; }
        public string instant_tutoring_price_usd { get; set; }
        public int is_available_instant_tutoring { get; set; }
        public string max_price { get; set; }
    }

    public class LanguageObj
    {
        public string language { get; set; }
        public int level { get; set; }
        public int priority { get; set; }
        public int is_teaching { get; set; }
        public int is_learning { get; set; }
        public int id { get; set; }
    }

    public class Datum
    {
        public int is_premium { get; set; }
        public ExamResultShownObj exam_result_shown_obj { get; set; }
        public int course_count { get; set; }
        public int favourite_flag { get; set; }
        public List<PersonalTag> personal_tag { get; set; }
        public string textid { get; set; }
        public string nickname { get; set; }
        public int has_contacted { get; set; }
        public string avatar_file_name { get; set; }
        public TeacherInfoObj teacher_info_obj { get; set; }
        public int is_favourite { get; set; }
        public string origin_country_id { get; set; }
        public int is_online { get; set; }
        public int id { get; set; }
        public List<LanguageObj> language_obj_s { get; set; }
        public int is_tutor { get; set; }
        public int is_pro { get; set; }
        public string last_login_time { get; set; }
    }
    public class PersonalTag
    {
        public string tag_name { get; set; }
        public int vote_count { get; set; }
        public int tag_id { get; set; }
    }
    public class RootObject
    {
        public string performance { get; set; }
        public Meta meta { get; set; }
        public long server_time { get; set; }
        public List<Datum> data { get; set; }
    }
}
