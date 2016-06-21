using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Profile.Data
{
    [DataContract]
    public class ProfilePhoto
    {
        [DataMember]
        public Int64? ID { get; set; }
        [DataMember]
        public string PhotoImage { get; set; }
        [DataMember]
        public string PhotoType { get; set; }
        [DataMember]
        public Int64 UserID { get; set; }
        [DataMember]
        public bool IsPrimary { get; set; }
    }

    [DataContract]
    public class ProfilePhotos
    {
        [DataMember]
        public List<ProfilePhoto> ProfilePhotoList { get; set; }
    }
}
