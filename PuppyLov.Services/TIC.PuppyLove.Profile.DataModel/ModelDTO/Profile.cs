using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIC.PuppyLove.Profile.DataModel
{
    public class Profile
    {
        public ProfileUser UserData { get; set; }
        public List<ProfileResponse> UserResponses { get; set; }
        public List<ProfileResponse> MatchResponses { get; set; }
        public List<Choice> ChoiceResponses { get; set; }
    }
}
