using Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aggregates
{
    public class BaseAggregate<T>  where T : IEntity
    {
        public T Entity;
        public List<string> ResultMessages { get;  }
        public BaseAggregate(T entity)
        {
            this.Entity = entity;
            ResultMessages = new List<string>();
        }

        public void AddMessage(string msg)
        {
            this.ResultMessages.Add(msg);
        }

    }
}
