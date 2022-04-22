using System;
using System.Data;

namespace ConsoleApp5.DapperUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbConnection ConnectionProd { get; }
        //  IDbTransaction Transaction { get; }
        //  void Commit();
        // void Dispose();
    }
}