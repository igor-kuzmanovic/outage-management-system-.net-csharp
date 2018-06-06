using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service.Contexts;
using OutageManagementSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OutageManagementSystem.Service.Repositories
{
    class InvalidTestOutageRepository : IOutageRepository
    {
        public ICollection<Outage> GetAll()
        {
            return new List<Outage>();
        }

        public ICollection<Outage> Find(Expression<Func<Outage, bool>> predicate)
        {
            return new List<Outage>();
        }

        public Outage Get(int id)
        {
            return new Outage();
        }

        public int Insert(Outage outage)
        {
            return 0;
        }

        public bool Update(Outage outage)
        {
            return false;
        }

        public bool Remove(int id)
        {
            return false;
        }
    }
}
