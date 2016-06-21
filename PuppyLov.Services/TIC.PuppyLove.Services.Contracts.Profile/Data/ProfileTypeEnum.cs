using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Profile.Data
{
    [DataContract]

    public enum ProfileTypeEnum : int
    {

        MatchPreference,
        UserProfile,
        All

    }
}