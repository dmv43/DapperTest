using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using DapperProject1.Models;



namespace DapperProject1.Repositories
{
    public interface ITeacherRepository
    {
        void Create(Teacher teacher);
        Teacher Get(int id);
        List<Teacher> GetTeachers();
        void Update(Teacher teacher);

    }
    internal class TeacherRepository : RepositoryBase, ITeacherRepository
    {
        public TeacherRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public void Create(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException("teacher");
            LanguageRepository langRep = new LanguageRepository(transaction:Transaction);
            TagRepository tagRep = new TagRepository(transaction: Transaction);


            teacher.id = Connection.ExecuteScalar<int>("INSERT INTO Teacher (nickname, italki_url, min_price, student_count ," +
                            "session_count , description, rating, country, url,italki_id) VALUES(@nickname, @italki_url, @min_price, @student_count, @session_count" +
                            ", @description, @rating, @country, @url, @italki_id); SELECT SCOPE_IDENTITY()", param: new { nickname = teacher.nickname, italki_url = teacher.italki_url, min_price = teacher.min_price, student_count = teacher.student_count,
                                teacher.session_count, description = teacher.description, rating = teacher.rating,
                            country = teacher.country, url = teacher.url, italki_id = teacher.italki_id}, transaction: Transaction);
            int langid;
            int tagid;
            foreach (var lang in teacher.languages)
            {
                langid = langRep.Add(lang);
                Connection.Execute("INSERT INTO TeacherLanguage (teacher_id, language_id) VALUES(@id, @langvid); SELECT SCOPE_IDENTITY()", param : new {id = teacher.id ,langvid = langid  }, transaction: Transaction);
            }
            foreach (var tg in teacher.tags)
            {
               tagid =  tagRep.Add(tg);
                Connection.Execute("INSERT INTO TeacherTag (teacher_id, tag_id) VALUES(@teacher_id, @tag_id); SELECT SCOPE_IDENTITY()", param: new { teacher_id = teacher.id, tag_id = tagid }, transaction: Transaction);
            }
        }

        public Teacher Get(int italki_id)
        {
            LanguageRepository langRep = new LanguageRepository(transaction: Transaction);
            TagRepository tagRep = new TagRepository(transaction: Transaction);
            Teacher teach = new Teacher();
              teach = Connection.Query<Teacher>("SELECT * FROM Teacher" +
              " WHERE italki_id = @lki_id", new { lki_id = italki_id }, transaction: Transaction).FirstOrDefault();
            int numberoflang = 0;
            if (teach != null)
            {
                numberoflang = Connection.ExecuteScalar<int>("SELECT  COUNT (teacher_id) From TeacherLanguage WHERE teacher_id = @id"
                , param: new { id = teach.id }, transaction: Transaction);
               var langs = Connection.Query<Language>("SELECT  Language.id , Language.language From TeacherLanguage LEFT JOIN Language ON Language.id = TeacherLanguage.language_id WHERE TeacherLanguage.teacher_id = @id", param: new { id = teach.id }, transaction: Transaction);
               
                if (numberoflang != 0)
                {
           
                    teach.languages.AddRange(langs);
                }

                int? numberoftag = Connection.ExecuteScalar<int>("SELECT  COUNT (teacher_id) From TeacherTag WHERE teacher_id = @id"
                    , param: new { id = teach.id }, transaction: Transaction);
                var tags = Connection.Query<Tag>("SELECT  Tag.id , Tag.tag From TeacherTag LEFT JOIN Tag ON Tag.id = TeacherTag.tag_id WHERE TeacherTag.teacher_id = @id", param: new { id = teach.id }, transaction: Transaction);
                if (numberoftag != 0)
                {
                    
                        teach.tags.AddRange(tags);
                    
                }
            }

            return teach;
        }

        public List<Teacher> GetTeachers()
        {
            return Connection.Query<Teacher>("SELECT * FROM Teacher", transaction: Transaction).ToList();
        }

        public void Update(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException("teacher");
        

            teacher.id = Connection.Execute("Update Teacher SET nickname = @nickname, italki_url = @italki_url, min_price = @min_price," +
                " student_count = @student_count, session_count = @session_count" +
                            ", description = @description, rating = @rating, country = @country, url = @url" +
                            " WHERE italki_id = @italki_id", param: new
                            {
                                nickname = teacher.nickname,
                                italki_url = teacher.italki_url,
                                min_price = teacher.min_price,
                                student_count = teacher.student_count,
                                teacher.session_count,
                                description = teacher.description,
                                rating = teacher.rating,
                                country = teacher.country,
                                url = teacher.url,
                                italki_id = teacher.italki_id
                            }, transaction: Transaction);
       
        }
    }
}





