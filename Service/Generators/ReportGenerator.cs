using IronPdf;
using OutageManagementSystem.Common.Models;
using OutageManagementSystem.Service.Interfaces;
using System;
using System.Text;

namespace OutageManagementSystem.Service.Generators
{
    public class ReportGenerator : IReportGenerator
    {
        private readonly HtmlToPdf renderer;

        public ReportGenerator()
        {
            renderer = new HtmlToPdf();
        }

        public PDFReport GenerateReport(Outage outage)
        {
            PDFReport report;

            if (outage != null && outage.Id > 0)
            {
                string body = GenerateBody(outage);
                string fileName = $"Outage{outage.Id}_Report";
                byte[] binaryData = renderer.RenderHtmlAsPdf(body).BinaryData;
                report = new PDFReport(fileName, binaryData);
            }
            else
            {
                report = new PDFReport();
            }

            return report;
        }

        private string GenerateBody(Outage outage)
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine($"<h1 align='center'>Outage Report - {DateTime.Now.ToLocalTime()}</h1>");
            body.AppendLine("<hr>");
            body.AppendLine("<h2 align='center'>Outage Information</h2>");
            body.AppendLine("<table width='100%' border='1' cellpadding='5'>");
            body.AppendLine("<tr>");
            body.AppendLine("<th width='20%' align='right'>ID</th>");
            body.AppendLine($"<td width='80%'>{outage.Id}</td>");
            body.AppendLine("</tr>");
            body.AppendLine("<tr>");
            body.AppendLine("<th width='20%' align='right'>Description</th>");
            body.AppendLine($"<td width='80%'>{SplitLines(outage.Description)}</td>");
            body.AppendLine("</tr>");
            body.AppendLine("<tr>");
            body.AppendLine("<th width='20%' align='right'>Creation Date</th>");
            body.AppendLine($"<td width='80%'>{outage.CreationDate.ToShortDateString()}</td>");
            body.AppendLine("</tr>");
            body.AppendLine("<tr>");
            body.AppendLine("<th width='20%' align='right'>Voltage Level</th>");
            body.AppendLine($"<td width='80%'>{outage.VoltageLevel.ToString().ToUpper()}</td>");
            body.AppendLine("</tr>");
            body.AppendLine("<tr>");
            body.AppendLine("<th width='20%' align='right'>State</th>");
            body.AppendLine($"<td width='80%'>{outage.State.ToString().ToUpper()}</td>");
            body.AppendLine("</tr>");
            body.AppendLine("<tr>");
            body.AppendLine("<th width='20%' align='right'>Faulty Element</th>");
            body.AppendLine($"<td width='80%'>{outage.ElementName}</td>");
            body.AppendLine("</tr>");
            body.AppendLine("<tr>");
            body.AppendLine("<th width='20%' align='right'>Longitude</th>");
            body.AppendLine($"<td width='80%'>{String.Format("{0:0.######}", outage.Longitude)}</td>");
            body.AppendLine("</tr>");
            body.AppendLine("<tr>");
            body.AppendLine("<th width='20%' align='right'>Latitude</th>");
            body.AppendLine($"<td width='80%'>{String.Format("{0:0.######}", outage.Latitude)}</td>");
            body.AppendLine("</tr>");
            body.AppendLine("</table>");
            body.AppendLine("<br>");
            body.AppendLine("<h2 align='center'>Executed Actions</h2>");
            body.AppendLine("<table width='100%' border='1' cellpadding='5'>");
            body.AppendLine("<tr>");
            body.AppendLine("<th width='20%'>Execution Date</th>");
            body.AppendLine("<th width='80%'>Description</th>");
            body.AppendLine("</tr>");
            foreach (var action in outage.Actions)
            {
                body.AppendLine("<tr>");
                body.AppendLine($"<td align='center'>{action.ExecutionDate.ToShortDateString()}</td>");
                body.AppendLine($"<td>{SplitLines(action.Description)}</td>");
                body.AppendLine("</tr>");
            }
            body.AppendLine("</table>");
            return body.ToString();
        }

        private string SplitLines(string text)
        {
            StringBuilder result = new StringBuilder();
            int rowLength = 78;
            int characterCount = text.Length;
            int numberOfLines = characterCount / rowLength;
            int remainder = characterCount % rowLength;
            for (int lineNumber = 0; lineNumber < numberOfLines; lineNumber++)
            {
                result.Append(text.Substring(lineNumber * rowLength, rowLength));
                result.Append("<br>");
            }
            result.Append(text.Substring(numberOfLines * rowLength, remainder));
            return result.ToString();
        }
    }
}
