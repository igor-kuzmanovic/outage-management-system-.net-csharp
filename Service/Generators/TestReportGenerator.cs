using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service.Interfaces;

namespace OutageManagementSystem.Service.Generators
{
    class TestReportGenerator : IReportGenerator
    {
        public PDFReport GenerateReport(Outage outage)
        {
            return new PDFReport("Test", new byte[4] { 1, 0, 1, 0 });
        }
    }
}
