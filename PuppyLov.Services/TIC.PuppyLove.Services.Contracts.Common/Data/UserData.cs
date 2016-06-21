
using System.Runtime.Serialization;
using System;

namespace TIC.PuppyLove.Services.Contracts.Common
{
    [DataContract]
    public class UserData : UserBase
    {
        [DataMember]
        public string DisplayName;

        [DataMember]
        public string Email;

        [DataMember]
        public string FirstName;

        [DataMember]
        public string LastName;

        [DataMember]
        public string MiddleName;

        [DataMember]
        public string PhoneNumber;

        [DataMember]
        public string Photo;

        [DataMember]
        public string UserName;

        [DataMember]
        public DateTime? Birthday;

        [DataMember]
        public string Bio;


    }
}