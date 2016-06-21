using System.Collections.Generic;
using System.Runtime.Serialization;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Services.Contracts.Profile.Data
{

    [DataContract]
    public class UserProfileData : CommonResponse
    {
        
        [DataMember]
        public UserData UserData;

        [DataMember]
        public List<ProfileEntity> MatchResponses;

        [DataMember]
        public List<ChoiceProfile> UserChoices;

        [DataMember]
        public List<ProfileEntity> UserResponses;

    }
}