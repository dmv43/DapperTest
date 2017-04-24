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


            teacher.id = Connection.ExecuteScalar<int>("INSERT INTO Teacher (nickname, italki_url, hire_date, student_count ," +
                            "session_count , description, rating, country, url,italki_id) VALUES(@nickname, @italki_url, @hire_date, @student_count, @session_count" +
                            ", @ description, @rating, @country, @url, @italki_id); SELECT CAST(SCOPE_IDENTITY())", teacher, transaction: Transaction);
            int langid;
            int tagid;
            foreach (var lang in teacher.languages)
            {
                langid = langRep.Add(lang);
                Connection.Execute("INSERT INTO TeacherLanguage (teacher_id, language_id) VALUES(@id, @langid); SELECT CAST(SCOPE_IDENTITY())", param : new {teacher_id = teacher.id ,language_id = langid  }, transaction: Transaction);
            }
            foreach (var tg in teacher.tags)
            {
               tagid =  tagRep.Add(tg);
                Connection.Execute("INSERT INTO TeacherTag (teacher_id, tag_id) VALUES(@id, @tagid); SELECT CAST(SCOPE_IDENTITY())", param: new { teacher_id = teacher.id, tag_id = tagid }, transaction: Transaction);
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





