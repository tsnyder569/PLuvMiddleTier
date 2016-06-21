using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Common
{
    [DataContract]
    public class CommonResponse
    {
        [DataMember]
        public string ReturnMessage;

        [DataMember]
        public bool Status;

    }
}