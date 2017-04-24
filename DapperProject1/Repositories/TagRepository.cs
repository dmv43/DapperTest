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
    public interface ITagRepository
    {
        void Add(Tag tag);
        Tag Get(int id);


    }
    internal class TagRepository : RepositoryBase, ITagRepository

    {
        public TagRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
        string connectionString = null;

        public void Add(Tag tag)
        {

            tag.id = Connection.ExecuteScalar<int>("INSERT INTO Tag (tag) VALUES(@tag); SELECT CAST(SCOPE_IDENTITY())", tag, transaction: Transaction);

        }

        public Tag Get(int id)
        {
            return Connection.Query<Tag>("SELECT * FROM Tag" +
                   " WHERE id = @id", new { id }).FirstOrDefault();


        }
    }
}
