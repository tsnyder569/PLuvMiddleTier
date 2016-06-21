using System;
using System.Runtime.Serialization;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Services.Contracts.Profile.Data
{
    [DataContract]
    public class LoginResponse : CommonResponse
    {
        [DataMember]
        public string LoginStatus;

    }
}