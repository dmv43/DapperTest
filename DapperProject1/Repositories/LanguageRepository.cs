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


    }
    internal class LanguageRepository : RepositoryBase, ILanguageRepository

    {
        public LanguageRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
        string connectionString = null;

        public int Add(Language language)
        {

           return language.id = Connection.ExecuteScalar<int>("INSERT INTO Language (language) VALUES(@language); SELECT CAST(SCOPE_IDENTITY())", language, transaction: Transaction);


        }

        public Language Get(int id)
        {

            return Connection.Query<Language>("SELECT * FROM Language" +
                " WHERE id = @id", new { id }).FirstOrDefault();

        }
    }
}
