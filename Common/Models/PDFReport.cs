using System.Runtime.Serialization;

namespace OutageManagementSystem.Common.Models
{
    [DataContract]
    public class PDFReport
    {
        public PDFReport()
        {
            FileName = string.Empty;
            BinaryData = new byte[0];
        }

        public PDFReport(string fileName, byte[] binaryData)
        {
            FileName = fileName;
            BinaryData = binaryData;
        }

        [DataMember]
        public string FileName { get; private set; }
        [DataMember]
        public byte[] BinaryData { get; private set; }
    }
}
