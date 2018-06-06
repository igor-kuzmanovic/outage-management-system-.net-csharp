using OutageManagementSystem.Common.Models;

namespace OutageManagementSystem.Service.Interfaces
{
    interface IReportGenerator
    {
        PDFReport GenerateReport(Outage outage);
    }
}
