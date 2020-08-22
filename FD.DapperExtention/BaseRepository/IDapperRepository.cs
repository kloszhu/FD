using System;
using System.Text;
using System.Data.SqlTypes;
using System.Data;
using System.Collections;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FD.DapperExtention
{
    public interface IDapperRepository
    {
        IDbAccess DbAccess { get; set; }

    }
   
    public interface IDbAccess
    {
         IDbConnection _DbConnection { get; set; }
         object param { get; set; } 
         IDbTransaction transaction { get; set; } 
         bool buffered { get; set; } 
         int? commandTimeout { get; set; }
         CommandType? commandType { get; set; }
        IEnumerable<T> Query<T>(string sql);
        IEnumerable<object> Query(string sql);

         Task<IEnumerable<object>> QueryAsync(Type type, string sql);

         object ExecuteScalar(string sql);
         Task<object> ExecuteScalarAsync(string sql);
         int Execute(string sql);
    }

    public class DapperRepository : IDapperRepository
    {
        public IDbAccess DbAccess { get; set; }
        private IDbProvider dbProvider;
        public DapperRepository(IDbAccess _DbAccess, IDbProvider _dbProvider)
        {
            DbAccess = _DbAccess;
            dbProvider = _dbProvider;
            DbAccess._DbConnection = dbProvider.DbConnection;
        }
    }

    [Obsolete]
    public interface IDbTrans
    {
    }
}
