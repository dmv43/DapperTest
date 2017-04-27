using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperProject1.Models;
namespace DapperProject1.ViewModels
{
    public interface ITeacherViewFabric
    {
        IEnumerable<TeacherView> Build(IEnumerable<Teacher> t);
    }
    public class TeacherViewFabric : ITeacherViewFabric
    {
        public IEnumerable<TeacherView> Build(IEnumerable<Teacher> t)
        {
            List<TeacherView> teach = new List<TeacherView>(t.Count());
            int i = 1;
            foreach(var te in t)
            {

                TeacherView teacher = new TeacherView();
                teacher.id = i;
                i++;
                teacher.nickname =    te.nickname;
                teacher.italki_url = te.italki_url;
                teacher.min_price = te.min_price;
                teacher.italki_id = te.italki_id;

                teacher.rating = te.rating;
                foreach (var z in te.languages)
                {
                    LanguageView l = new LanguageView();
                    l.language = z.language;
                    l.id = z.id;
                    teacher.languages.Add(l);
                }
                foreach (var tg in te.tags)
                {
                    TagView ss = new TagView();
                    ss.tag = tg.tag;
                    ss.id = tg.id;
                    teacher.tags.Add(ss);
                }

                teacher.session_count = te.session_count;
                teacher.student_count = te.student_count;

                teacher.url = te.url;
                teacher.description = te.description;
                teacher.country = te.country;
                teacher.session_to_student = te.session_to_student;
                teach.Add(teacher);
            }
            return teach;
        }
    }
}
