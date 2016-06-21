using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Profile.Data
{
    [DataContract]
    public enum ProfileQuestionTypeEnum : int
    {

        UserProfile,
        MatchProfile,
        Both

    }
}