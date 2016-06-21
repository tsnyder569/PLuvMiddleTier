using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Services.Contracts.Config.Data
{
    [DataContract]
    [Serializable]
    public class ConfigurationResponse : CommonResponse
    {   
        [DataMember]
        public List<ConfigEntry> Configurations { get; set;}
    }
}
