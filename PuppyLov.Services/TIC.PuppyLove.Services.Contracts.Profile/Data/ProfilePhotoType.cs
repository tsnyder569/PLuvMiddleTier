using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Profile.Data
{
    [DataContract]
    public class ProfilePhotoType
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string PhotoType {get; set;}
    }
}
