using OutageManagementSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OutageManagementSystem.Service.Interfaces
{
    interface IOutageRepository
    {
        ICollection<Outage> Find(Expression<Func<Outage, bool>> predicate);
        Outage Get(int id);
        ICollection<Outage> GetAll();
        int Insert(Outage outage);
        bool Remove(int id);
        bool Update(Outage outage);
    }
}
