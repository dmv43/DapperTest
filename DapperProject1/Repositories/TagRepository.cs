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
        int Add(Tag tag);
        Tag Get(int id);
        void Update(Tag tag);


    }
    internal class TagRepository : RepositoryBase, ITagRepository

    {
        public TagRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
        string connectionString = null;

        public int Add(Tag tag)
        {

           return tag.id = Connection.ExecuteScalar<int>("INSERT INTO Tag (tag) VALUES(@tag); SELECT SCOPE_IDENTITY()", param: new {tag = tag.tag }, transaction: Transaction);

        }

        public Tag Get(int id)
        {
            return Connection.Query<Tag>("SELECT * FROM Tag" +
                   " WHERE id = @id", new { id }).FirstOrDefault();


        }

        public void Update(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
