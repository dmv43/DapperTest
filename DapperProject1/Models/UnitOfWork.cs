using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DapperProject1.Repositories;
namespace DapperProject1.Models
{
    public interface IUnitOfWork : IDisposable
    {
        ITeacherRepository TeacherRepository { get; }
        ITagRepository TagRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        void Commit();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private ITeacherRepository _teacherRepository;
        private ILanguageRepository _languageRepository;
        private ITagRepository _tagRepository;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public ITeacherRepository TeacherRepository
        {
            get { return _teacherRepository ?? (_teacherRepository = new TeacherRepository(_transaction)); }
        }

        public ILanguageRepository LanguageRepository
        {
            get { return _languageRepository ?? (_languageRepository = new LanguageRepository(_transaction)); }
        }
        public ITagRepository TagRepository
        {
            get { return _tagRepository ?? (_tagRepository = new TagRepository(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _teacherRepository = null;
            _languageRepository = null;
            _tagRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
