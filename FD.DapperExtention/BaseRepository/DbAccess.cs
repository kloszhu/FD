using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
namespace FD.DapperExtention
{
    public class DbAccess : IDbAccess
    {
        public IDbConnection _DbConnection { get; set; }
        public object param { get; set; } = null;
        public IDbTransaction transaction { get; set; } = null;
        public bool buffered { get; set; } = true;
        public int? commandTimeout { get; set; } = null;
        public CommandType? commandType { get; set; } = null;

        public virtual IEnumerable<object> Query(string sql){
           return _DbConnection.Query( sql,  param ,  transaction,  buffered , commandTimeout, commandType);
        }

        public virtual Task<IEnumerable<object>> QueryAsync(Type type, string sql) {
            return _DbConnection.QueryAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        public virtual object ExecuteScalar( string sql) {
            return _DbConnection.  ExecuteScalar(  sql,  param ,  transaction , commandTimeout ,  commandType );
        }
        public virtual Task<object> ExecuteScalarAsync(string sql) {
            return _DbConnection.ExecuteScalarAsync(sql, param, transaction, commandTimeout, commandType);
        }
        public virtual int Execute( string sql) {
            return _DbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
