using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Profile.Data
{
    [DataContract]
    public class ProfileQuestionRequest
    {
        [DataMember]
        public ProfileQuestionTypeEnum ProfileQuestionType;
    }
}