using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service.Contexts;
using OutageManagementSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OutageManagementSystem.Service.Repositories
{
    class TestOutageRepository : IOutageRepository
    {
        private readonly TestContext context;

        public TestOutageRepository(TestContext context)
        {
            this.context = context;
        }

        public ICollection<Outage> GetAll()
        {
            if (context.Outages.Any())
            {
                return context.Outages.ToList();
            }
            else
            {
                return new List<Outage>();
            }
        }

        public ICollection<Outage> Find(Expression<Func<Outage, bool>> predicate)
        {
            if (predicate != null && context.Outages.Any(predicate.Compile()))
            {
                return context.Outages.Where(predicate.Compile()).ToList();
            }
            else
            {
                return new List<Outage>();
            }
        }

        public Outage Get(int id)
        {
            if (id > 0 && context.Outages.Any(p => p.Id == id))
            {
                return context.Outages.First(p => p.Id == id);
            }
            else
            {
                return new Outage();
            }
        }

        public int Insert(Outage outage)
        {
            if (outage.Id <= 0)
            {
                outage.Id = context.GenerateID();
                context.Outages.Add(outage);
                return outage.Id;
            }
            else
            {
                return 0;
            }
        }

        public bool Update(Outage outage)
        {
            if (outage.Id > 0 && context.Outages.Any(p => p.Id == outage.Id))
            {
                Outage oldOutage = context.Outages.First(p => p.Id == outage.Id);
                context.Outages.Remove(oldOutage);
                context.Outages.Add(outage);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Remove(int id)
        {
            if (id > 0 && context.Outages.Any(p => p.Id == id))
            {
                Outage outage = context.Outages.First(p => p.Id == id);
                context.Outages.Remove(outage);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
