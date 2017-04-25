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

        public Teacher Get(int id)
        {
            Teacher teach = new Teacher();
              teach = Connection.Query<Teacher>("SELECT * FROM Teacher" +
              " WHERE id = @id", new { id }, transaction: Transaction).FirstOrDefault();
            return teach;
        }

        public List<Teacher> GetTeachers()
        {
            return Connection.Query<Teacher>("SELECT * FROM Teacher", transaction: Transaction).ToList();
        }

    }
}





