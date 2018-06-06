using OutageManagementSystem.Common.Models;
using System.Collections.Generic;

namespace OutageManagementSystem.Service.Interfaces
{
    interface IOutageService
    {
        IEnumerable<Outage> FindByDate(string startDate, string endDate, out string message);
        Outage Get(int id, out string message);
        int Insert(Outage outage, out string message);
        bool Update(Outage outage, out string message);
    }
}
