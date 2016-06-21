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
    public class LocationResponse : CommonResponse
    {
        [DataMember]
        public List<LocationData> Locations { get; set; }

        //[DataMember]
        //public UserData UserInfo { get; set; }
    }
}
