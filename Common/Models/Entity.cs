using System.Runtime.Serialization;

namespace OutageManagementSystem.Common.Models
{
    [DataContract]
    public class Entity
    {
        [DataMember]
        public int Id { get; set; } = 0;
    }
}
