using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Profile.Data
{
    [DataContract]
    public class ProfileEntity 
    {
        // Response PK
        [DataMember]
        public long? ID;

        // This records the ID of the response
        [DataMember]
        public long Id;

        [DataMember]
        public long QuestionID;

        [DataMember]
        public long ResponseTypeID;

        [DataMember]
        public string Text;

    }
}