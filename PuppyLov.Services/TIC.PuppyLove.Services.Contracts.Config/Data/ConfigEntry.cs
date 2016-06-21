using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Config.Data
{
    [DataContract]
    [Serializable]
    public class ConfigEntry
    {
        [DataMember]
        public Int32? ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}
