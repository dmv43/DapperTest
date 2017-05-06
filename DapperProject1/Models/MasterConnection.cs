using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.IO;


namespace DapperProject1.Models
{
    public interface IMasterConnection
    {
        void Execute();
        void WriteDB();
        
    }
    public class MasterConnection :IMasterConnection
    {
        private SqlConnection _connection;
      //  private SqlTransaction _transaction;
       // protected IDbConnection Connection { get { return _transaction.Connection; } }

        public MasterConnection(String connectionMaster)
        {
            _connection = new SqlConnection(connectionMaster);
            _connection.Open();

       //     _transaction = _connection.BeginTransaction();

        }
       
        public static string GetDatabaseName()
        {
             string databaseName = "Italkii";
            return databaseName;
        }
        public void Execute()
        {

            
        
            SqlCommand command1 = new SqlCommand();
            
            command1.CommandText = "USE [master]; IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '"+GetDatabaseName()+ "') CREATE DATABASE " + GetDatabaseName() + ";";
        
            command1.CommandType = CommandType.Text;
            command1.Connection = _connection;
          
            command1.ExecuteNonQuery();

            

            // Connection.Execute(file,transaction:_transaction);

        }
        public void WriteDB()
        {
            
             var a = File.OpenText(Directory.GetCurrentDirectory() + @""+Path.DirectorySeparatorChar+ "Queries" + Path.DirectorySeparatorChar + "SQLQuery3.sql");
            SqlCommand command = new SqlCommand();
            string file = a.ReadToEndAsync().Result;
            command.CommandText = "USE " + GetDatabaseName() + ";" + file;

            command.CommandType = CommandType.Text;
            command.Connection = _connection;

            command.ExecuteNonQuery();

        }
        

    }
}
