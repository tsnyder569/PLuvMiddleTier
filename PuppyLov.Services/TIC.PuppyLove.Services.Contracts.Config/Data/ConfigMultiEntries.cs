using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Config.Data
{
    [DataContract]
    [Serializable]
    public class ConfigMultiEntries
    {
        [DataMember]
        public List<ConfigEntry> ConfigEntries { get; set; }
    }
}
