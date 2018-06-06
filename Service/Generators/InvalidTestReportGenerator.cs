using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service.Interfaces;

namespace OutageManagementSystem.Service.Generators
{
    class InvalidTestReportGenerator : IReportGenerator
    {
        public PDFReport GenerateReport(Outage outage)
        {
            return new PDFReport();
        }
    }
}
