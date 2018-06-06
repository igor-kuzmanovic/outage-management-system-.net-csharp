using OutageManagementSystem.Common.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace OutageManagementSystem.Common
{
    [ServiceContract]
    public interface IOutageManagementSystem
    {
        [OperationContract]
        Response<IEnumerable<Outage>> FindByDate(string startDate, string endDate);
        [OperationContract]
        Response<Outage> Get(int id);
        [OperationContract]
        Response<int> Insert(Outage outage);
        [OperationContract]
        Response<bool> Update(Outage outage);
        [OperationContract]
        Response<PDFReport> GenerateReport(int id);
    }
}
