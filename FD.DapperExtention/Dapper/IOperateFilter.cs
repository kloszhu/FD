using System;

namespace FD.DapperExtention
{
    public interface IQueryFilter<T>
    {

    }
    public interface IQueryFilter
    {
        
    }
    public interface IInsertFilter
    {
    }
    public interface IUpdateFilter
    {
    }
    public interface IDeleteFilter
    {
    }

    public interface Select: IQueryFilter
    {
    }
    public interface From: IQueryFilter
    { 
    
    }
    public interface Where: IQueryFilter
    { 
        
    }
    public interface InnerJoin : IQueryFilter
    {

    }


}