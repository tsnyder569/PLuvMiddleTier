
using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Common
{
    [DataContract]
    public class UserBase
    {
        [DataMember]
        public long? UserID;
    }
}
