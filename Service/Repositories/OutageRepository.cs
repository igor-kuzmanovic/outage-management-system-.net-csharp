using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service.Contexts;
using OutageManagementSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace OutageManagementSystem.Service.Repositories
{
    class OutageRepository : IOutageRepository
    {
        protected readonly DatabaseContext context;

        public OutageRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public ICollection<Outage> GetAll()
        {
            try
            {
                return context.Outages.ToList();
            }
            catch (Exception)
            {
                return new List<Outage>();
            }
        }

        public ICollection<Outage> Find(Expression<Func<Outage, bool>> predicate)
        {
            try
            {
                return context.Outages.Where(predicate).ToList();
            }
            catch (Exception)
            {
                return new List<Outage>();
            }
        }

        public Outage Get(int id)
        {
            try
            {
                return context.Outages.Find(id);
            }
            catch (Exception)
            {
                return new Outage();
            }
        }

        public int Insert(Outage outage)
        {
            try
            {
                context.Outages.Add(outage);
                context.SaveChanges();
                return outage.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool Update(Outage outage)
        {
            try
            {
                Outage oldOutage = context.Outages.First(o => o.Id == outage.Id);
                oldOutage.Description = outage.Description;
                oldOutage.CreationDate = outage.CreationDate;
                oldOutage.VoltageLevel = outage.VoltageLevel;
                oldOutage.State = outage.State;
                oldOutage.ElementName = outage.ElementName;
                oldOutage.Longitude = outage.Longitude;
                oldOutage.Latitude = outage.Latitude;
                oldOutage.Actions = outage.Actions;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                Outage outage = context.Outages.Find(id);
                context.Outages.Remove(outage);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
