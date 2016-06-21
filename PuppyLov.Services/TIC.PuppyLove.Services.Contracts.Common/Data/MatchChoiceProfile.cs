using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Common
{
    [DataContract]
    public class MatchChoiceProfile
    {
        // PK
        [DataMember]
        public long? ID;
        
        [DataMember]
        public ChoiceTypeEnum MatchChoiceType;
    }
}
