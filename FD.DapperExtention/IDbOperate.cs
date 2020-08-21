using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using System.Data;
using System.Collections;
using System;

namespace FD.DapperExtention
{
    //public interface IDbOperate<T>
    //{
    //    IEnumerable<T> Query(IQueryFilter<T> query);
    //    bool Insert(T entity);
    //    bool Insert(IEnumerable<T> entity);
    //    bool Update();
    //}

    public interface IDbOperate
    {
        IEnumerable Query(IQueryFilter  query);
        bool Insert(IInsertFilter entity);
        bool Update(IUpdateFilter entity);
        bool Delete(IDeleteFilter entity);
        IEnumerable GetFirst(IQueryFilter query);
    }


    public class DbOperate : IDbOperate
    {
        private IDbAccess dbAccess;
        private IDeleteFilter deleteFilter;
        private IInsertFilter  InsertFilter;
        private IQueryFilter queryFilter;
        IEnumerable<Action> GetActions = new List<Action>();
        public DbOperate(IDbAccess _dbAccess, IDeleteFilter _deleteFilter, IInsertFilter _InsertFilter, IQueryFilter _queryFilter) {
            this.dbAccess = _dbAccess;
            this.deleteFilter = _deleteFilter;
            this.InsertFilter = _InsertFilter;
            this.queryFilter = _queryFilter;
            
        }
        public IEnumerable Query(IQueryFilter query)
        {
            throw new System.NotImplementedException();
        }
        public bool Delete(IDeleteFilter entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(IInsertFilter entity)
        {
            throw new System.NotImplementedException();
        }

    

        public bool Update(IUpdateFilter entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable GetFirst(IQueryFilter query)
        {
            throw new NotImplementedException();
        }
    }
}