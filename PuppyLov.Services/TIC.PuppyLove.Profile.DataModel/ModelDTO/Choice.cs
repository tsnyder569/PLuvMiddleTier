using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Profile.DataModel
{
    public class Choice
    {
        public ProfileChocy UserChoice { get; set; }
        public ChoiceProfile ProfileChoice { get; set; }
        public string ChoiceTypeStr { get; set; }
        public ChoiceTypeEnum ChoiceTypeValue { get; set; }
        public User MUser { get; set; }

        public ChoiceProfile CreateProfileChoiceInstance()
        {
            UserData matchUser = new UserData();
            if (MUser != null)
            {
                    matchUser.UserID = MUser.ID;
                    matchUser.DisplayName = MUser.DisplayName;
                    matchUser.UserName = MUser.UserName;
                    matchUser.Email = MUser.Email;
                    matchUser.FirstName = MUser.FirstName;
                    matchUser.LastName = MUser.LastName;
                    matchUser.PhoneNumber = MUser.PhoneNumber;
            }

            return new ChoiceProfile
            {
                ID = UserChoice.ID,
                UserID = UserChoice.UserID,
                ChoiceType = (ChoiceTypeEnum)Enum.Parse(typeof(ChoiceTypeEnum), ChoiceTypeStr),
                MatchUser = matchUser
                
            };

        }

        // pass in parent PK
        public ProfileChocy CreateProfileChoiceModelInstance(long userID,
                                                             long choiceID)
        {

            return new ProfileChocy
            {
                ProfileChoiceUserID = ProfileChoice.MatchUser.UserID.Value,
                ChoiceType = choiceID,
                UserID = userID

            };

        }

        // Updates

        public ProfileChocy CreateProfileChoiceModelInstance()
        {

            return new ProfileChocy
            {
                ProfileChoiceUserID = ProfileChoice.MatchUser.UserID.Value,
                ID = ProfileChoice.ID.Value,
                UserID = ProfileChoice.UserID.Value
            };

        }

        public ProfileChocy CreateProfileChoiceModelInstance(long profileUserID,
                                                             long userID,
                                                             long Id)
        {

            return new ProfileChocy
            {
                ProfileChoiceUserID = userID,
                ID = Id,
                UserID = profileUserID
            };

        }

        public ProfileChocy SetProfileChoiceModelInstance(ProfileChocy choice,
                                                          long choiceID)
        {

            choice.ChoiceType = choiceID;

            return choice;

        }

    }
}
