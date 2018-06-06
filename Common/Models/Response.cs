using OutageManagementSystem.Common.Enums;
using System.Runtime.Serialization;

namespace OutageManagementSystem.Common.Models
{
    [DataContract]
    public class Response<TData>
    {
        public Response(ResponseStatus status, string message, TData data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        [DataMember]
        public ResponseStatus Status { get; private set; }
        [DataMember]
        public string Message { get; private set; }
        [DataMember]
        public TData Data { get; private set; }
    }
}
