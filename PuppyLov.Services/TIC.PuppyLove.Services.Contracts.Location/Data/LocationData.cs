using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Services.Contracts.Location.Data
{
    [DataContract]
    [Serializable]
    public class LocationData : UserBase
    {
        //this is needed to get the user id back from the scalar sp.  EF will not give it to us otherwise.
        //There appears to be an issue mapping from stored procedures to base class attributes.
        public long UserID_nn { get; set; }
        [DataMember]
        public decimal Latitude { get; set; }
        [DataMember]
        public decimal Longitude { get; set; }
        [DataMember]
        public decimal Accuracy { get; set; }
        [DataMember]
        public decimal Timestamp { get; set; }
        [DataMember]
        public double? Distance { get; set; }
    }
}
