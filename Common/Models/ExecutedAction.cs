using System;
using System.Runtime.Serialization;

namespace OutageManagementSystem.Common.Models
{
    [DataContract]
    public class ExecutedAction : Entity
    {
        [DataMember]
        public string Description { get; set; } = string.Empty;
        [DataMember]
        public DateTime ExecutionDate { get; set; } = DateTime.Now;
        [DataMember]
        public int OutageId { get; set; }
    }
}
