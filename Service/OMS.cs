using OutageManagementSystem.Common;
using OutageManagementSystem.Common.Enums;
using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace OutageManagementSystem.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class OMS : IOutageManagementSystem
    {
        private readonly IOutageService outageService;
        private readonly IReportService reportService;

        public OMS(IOutageService outageService, IReportService reportService)
        {
            this.outageService = outageService;
            this.reportService = reportService;
        }

        public Response<IEnumerable<Outage>> FindByDate(string startDate, string endDate)
        {
            Console.WriteLine($"Request: FindByDate.");
            IEnumerable<Outage> outages = outageService.FindByDate(startDate, endDate, out string message);

            if (outages.Any())
            {
                return new Response<IEnumerable<Outage>>(ResponseStatus.OK, message, outages);
            }
            else
            {
                return new Response<IEnumerable<Outage>>(ResponseStatus.NoContent, message, outages);
            }
        }

        public Response<Outage> Get(int id)
        {
            Console.WriteLine($"Request: Get.");
            Outage outage = outageService.Get(id, out string message);

            if (outage != null && outage.Id > 0)
            {
                return new Response<Outage>(ResponseStatus.OK, message, outage);
            }
            else
            {
                return new Response<Outage>(ResponseStatus.Error, message, outage);
            }
        }

        public Response<int> Insert(Outage outage)
        {
            Console.WriteLine($"Request: Insert.");

            if (outage == null)
            {
                return new Response<int>(ResponseStatus.Error, "Outage is invalid.", 0);
            }

            int id = outageService.Insert(outage, out string message);

            if (id > 0)
            {
                return new Response<int>(ResponseStatus.OK, message, id);
            }
            else
            {
                return new Response<int>(ResponseStatus.Error, message, id);
            }
        }

        public Response<bool> Update(Outage outage)
        {
            Console.WriteLine($"Request: Update.");

            if (outage == null)
            {
                return new Response<bool>(ResponseStatus.Error, "Outage is invalid.", false);
            }

            Outage oldOutage = outageService.Get(outage.Id, out string message);

            if (oldOutage == null || oldOutage.State == OutageState.Closed)
            {
                return new Response<bool>(ResponseStatus.Error, "Outage is closed and cannot be further updated.", false);
            }

            bool result = outageService.Update(outage, out message);

            if (result)
            {
                return new Response<bool>(ResponseStatus.OK, message, result);
            }
            else
            {
                return new Response<bool>(ResponseStatus.Error, message, result);
            }
        }

        public Response<PDFReport> GenerateReport(int id)
        {
            Console.WriteLine($"Request: GenerateReport.");
            Outage outage = outageService.Get(id, out string message);

            if (outage == null || outage.Id <= 0)
            {
                return new Response<PDFReport>(ResponseStatus.Error, message, new PDFReport());
            }

            PDFReport report = reportService.GenerateReport(outage, out message);

            if (report != null && !string.IsNullOrWhiteSpace(report.FileName) && report.BinaryData.Any())
            {
                return new Response<PDFReport>(ResponseStatus.OK, message, report);
            }
            else
            {
                return new Response<PDFReport>(ResponseStatus.Error, message, report);
            }
        }
    }
}
