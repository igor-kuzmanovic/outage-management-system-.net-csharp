using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service.Interfaces;
using OutageManagementSystem.Service.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OutageManagementSystem.Service.Services
{
    class OutageService : IOutageService
    {
        private readonly IOutageRepository repository;

        public OutageService(IOutageRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Outage> FindByDate(string startDate, string endDate, out string message)
        {
            DateTime.TryParse(startDate, out DateTime startDateTime);
            DateTime.TryParse(endDate, out DateTime endDateTime);

            if (DateTime.Compare(endDateTime, DateTime.MinValue) == 0)
            {
                endDateTime = DateTime.MaxValue;
            }

            IEnumerable<Outage> outages = repository.Find(o =>
                DateTime.Compare(o.CreationDate, startDateTime) >= 0
                &&
                DateTime.Compare(o.CreationDate, endDateTime) <= 0);

            if (outages.Any())
            {
                message = string.Empty;
            }
            else
            {
                message = "No outages found.";
            }

            return outages;
        }

        public Outage Get(int id, out string message)
        {
            if (id <= 0)
            {
                message = "Outage id is invalid.";
                return new Outage();
            }

            Outage outage = repository.Get(id);

            if (outage != null && outage.Id > 0)
            {
                message = string.Empty;
            }
            else
            {
                message = "An error occured while trying to get outage.";
                outage = new Outage();
            }

            return outage;
        }

        public int Insert(Outage outage, out string message)
        {
            if (outage == null)
            {
                message = "Outage is invalid.";
                return 0;
            }

            if (outage.Id > 0)
            {
                message = "Outage cannot have an id.";
                return 0;
            }

            bool IsValid = OutageValidator.Validate(outage, out message);

            if (!IsValid)
            {
                return 0;
            }

            int id = repository.Insert(outage);

            if (id > 0)
            {
                message = string.Empty;
            }
            else
            {
                message = "An error occured while trying to insert outage.";
            }

            return id;
        }

        public bool Update(Outage outage, out string message)
        {
            if (outage == null)
            {
                message = "Outage is invalid.";
                return false;
            }

            if (outage.Id <= 0)
            {
                message = "Outage id is invalid.";
                return false;
            }

            bool IsValid = OutageValidator.Validate(outage, out message);

            if (!IsValid)
            {
                return false;
            }

            bool result = repository.Update(outage);

            if (result)
            {
                message = string.Empty;
            }
            else
            {
                message = "An error occured while trying to update outage.";
            }

            return result;
        }
    }
}
