using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp5.DapperUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbConnection _connectionProd;
        //private IDbTransaction _transaction;
        //private _repository;
        private bool _disposed;

        public UnitOfWork()
        {
            _connectionProd = new SqlConnection("Data Source=;Initial Catalog=;User ID=;Password=;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;");

            _connection = new SqlConnection("Data Source=;Initial Catalog=;User ID=;Password=;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;");

            
            // _connection.Open();
            //_transaction = _connection.BeginTransaction();
        }

      

        IDbConnection IUnitOfWork.Connection
        {
            get { return _connection; }
        }

        IDbConnection IUnitOfWork.ConnectionProd
        {
            get { return _connectionProd; }
        }
        //IDbTransaction IUnitOfWork.Transaction
        //{
        //    get { return _transaction; }
        //}

        public void Commit()
        {
            //try
            //{
            //    _transaction = _connection.BeginTransaction();
            //    _transaction.Commit();
            //}
            //catch
            //{
            //    _transaction.Rollback();
            //    throw;
            //}
            //finally
            //{
            //    _transaction.Dispose();
            //   // _transaction = _connection.BeginTransaction();

            //}
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
                    //if (_transaction != null)
                    //{
                    //    _transaction.Dispose();
                    //    _transaction = null;
                    //}
                    //if (_connection != null)
                    //{
                    //    _connection.Dispose();
                    //    _connection = null;
                    //}
                }
                _disposed = true;
            }
        }

        //~UnitOfWork()
        //{
        //    dispose(false);
        //}
    }
}

