using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service.Interfaces;
using System.Linq;

namespace OutageManagementSystem.Service.Services
{
    class ReportService : IReportService
    {
        private readonly IReportGenerator generator;

        public ReportService(IReportGenerator generator)
        {
            this.generator = generator;
        }

        public PDFReport GenerateReport(Outage outage, out string message)
        {
            if (outage == null || outage.Id <= 0)
            {
                message = "Outage is invalid.";
                return new PDFReport();
            }

            PDFReport report = generator.GenerateReport(outage);

            if (!string.IsNullOrWhiteSpace(report.FileName) && report.BinaryData.Any())
            {
                message = string.Empty;
            }
            else
            {
                message = "An unexpected error occured while trying to generate a report.";
            }

            return report;
        }
    }
}
