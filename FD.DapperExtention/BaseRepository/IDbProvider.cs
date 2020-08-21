using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using FD.Tool;
namespace FD.DapperExtention
{
    public interface IDbProvider
    {
        DataBaseType _dataBaseType { get; set; }
        string _ConnectionString { get; set; }
        IDbConnection DbConnection { get; set; }
        void  Build(DataBaseType dataBaseType, string ConnectionString);
    }

    public enum DataBaseType
    {
        MSSQL,
        MYSQL,
        ORACLE,
        SQLITE
    }

    public class DBProvider : IDbProvider
    {
//{db:
//    {
//DbType:"MSSQL",//MSSQL,MYSQL,ORACLE,SQLITE
//ConnectionString:""
//    }
//}
        public IDbConnection DbConnection { get; set; }
        public DataBaseType _dataBaseType { get; set; } = SectionConf.Config["db:DbType"] == null ? DataBaseType.MSSQL : (DataBaseType)Enum.Parse(typeof(DataBaseType), SectionConf.Config["db:DbType"]);
        public string _ConnectionString { get; set; } = SectionConf.Config["db:ConnectionString"] ;

        public DBProvider() {
            switch (_dataBaseType)
            {
                case DataBaseType.MSSQL:
                    DbConnection = new SqlConnection(_ConnectionString);
                    break;
                case DataBaseType.MYSQL:
                    DbConnection = new MySqlConnection(_ConnectionString);
                    break;
                case DataBaseType.ORACLE:
                    DbConnection = new OracleConnection(_ConnectionString);
                    break;
                case DataBaseType.SQLITE:
                    DbConnection = new SqliteConnection(_ConnectionString);
                    break;
                default:
                    break;
            }
        }

        public void Build(DataBaseType dataBaseType,string ConnectionString) {
            _dataBaseType = dataBaseType;
            _ConnectionString = ConnectionString;
            switch (dataBaseType)
            {
                case DataBaseType.MSSQL:
                    DbConnection = new SqlConnection(ConnectionString);
                    break;
                case DataBaseType.MYSQL:
                    DbConnection = new MySqlConnection(ConnectionString);
                    break;
                case DataBaseType.ORACLE:
                    DbConnection = new OracleConnection(ConnectionString);
                    break;
                case DataBaseType.SQLITE:
                    DbConnection = new SqliteConnection(ConnectionString);
                    break;
                default:
                    break;
            }
        }

    }

}
