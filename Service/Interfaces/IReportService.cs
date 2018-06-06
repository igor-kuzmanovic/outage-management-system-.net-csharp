using OutageManagementSystem.Common.Models;

namespace OutageManagementSystem.Service.Interfaces
{
    interface IReportService
    {
        PDFReport GenerateReport(Outage outage, out string message);
    }
}
