using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Common
{
    [DataContract]
    public class ChoiceProfile : UserBase
    {
        // PK
        [DataMember]
        public long? ID;

        [DataMember]
        public ChoiceTypeEnum ChoiceType;

        [DataMember]
        public MatchChoiceProfile MatchChoice;

        [DataMember]
        public UserData MatchUser; // Make this a separate class that has the user data and the choice type

    }
}