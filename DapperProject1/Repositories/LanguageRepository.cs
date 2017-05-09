using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperProject1.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DapperProject1.Repositories
{
    public interface ILanguageRepository
    {
        int Add(Language language);
        Language Get(int id);
        void Update(Language language);

    }
    internal class LanguageRepository : RepositoryBase, ILanguageRepository

    {
        public LanguageRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
       // string connectionString = null;

        public int Add(Language language)
        {
            int idd = Connection.ExecuteScalar<int>("SELECT id FROM Language WHERE language = @lang", param:new {lang = language.language },transaction:Transaction);
            if(idd == 0)
            {
                language.id = Connection.ExecuteScalar<int>("INSERT INTO Language (language) VALUES(@language); SELECT SCOPE_IDENTITY()", param: new { language = language.language }, transaction: Transaction);
            }
            else
            {
                language.id = idd;
            }

            return language.id;


        }

        public Language Get(int id)
        {

            return Connection.Query<Language>("SELECT * FROM Language" +
                " WHERE id = @idd", new { idd= id },transaction:Transaction).FirstOrDefault();

        }

        public void Update(Language language)
        {
            throw new NotImplementedException();
        }
    }
}
