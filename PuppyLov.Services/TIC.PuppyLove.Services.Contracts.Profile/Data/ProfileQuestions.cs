using System.Collections.Generic;
using System.Runtime.Serialization;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Services.Contracts.Profile.Data
{
    [DataContract]
    public class ProfileQuestions : CommonResponse
    {

        [DataMember]
        public List<Question> UserQuestions;

        [DataMember]
        public List<Question> MatchQuestions;

    }
}