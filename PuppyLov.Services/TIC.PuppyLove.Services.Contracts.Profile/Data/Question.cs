using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TIC.PuppyLove.Services.Contracts.Profile.Data
{
     [DataContract]
    public class Question
    {
         [DataMember]
         public ProfileEntity ProfileQuestion;

         [DataMember]
         public List<ProfileEntity> ProfileResponses;
    }

}
