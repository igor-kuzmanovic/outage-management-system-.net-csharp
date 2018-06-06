using OutageManagementSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace OutageManagementSystem.Common.Models
{
    [DataContract]
    public class Outage : Entity
    {
        public Outage()
        {
            Actions = new List<ExecutedAction>();
        }

        [DataMember]
        public string Description { get; set; } = string.Empty;
        [DataMember]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [DataMember]
        public VoltageLevel VoltageLevel { get; set; } = VoltageLevel.Medium;
        [DataMember]
        public OutageState State { get; set; } = OutageState.New;
        [DataMember]
        public string ElementName { get; set; } = string.Empty;
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public virtual List<ExecutedAction> Actions { get; set; }
    }
}
