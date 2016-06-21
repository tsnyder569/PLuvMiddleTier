using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Services.Contracts.Match.Data
{
    [DataContract]
    [Serializable]
    public class MatchRequest : UserBase
    {
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
    }
}
