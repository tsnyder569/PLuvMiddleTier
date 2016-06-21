using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Profile.DataModel
{
    public class ProfileResponse
    {
        
        public UserRespons Response { get; set; }
        public ProfileEntity ProfileEntityResponse { get; set; }

        public ProfileEntity CreateProfileResponseInstance()
        {
            return new ProfileEntity
            {
                ID = Response.ID,
                Id = Response.ResponseID,
                QuestionID = Response.QuestionTypeID,
                ResponseTypeID = Response.ResponseTypeID
            };
        }

        // children need the parent FK
        public UserRespons CreateResponseModelInstance(long userID,
                                                       long questionID)
        {
            return new UserRespons
                            {
                                ResponseID = ProfileEntityResponse.Id,
                                UserID = userID,
                                QuestionTypeID = questionID,
                                ResponseTypeID = ProfileEntityResponse.ResponseTypeID
                            };
        }

        // Updates
        public UserRespons CreateResponseModelInstanceForUpdate(long userID)
        {

            return new UserRespons
            {
                ID = ProfileEntityResponse.ID.Value,
                UserID = userID,
                QuestionTypeID = ProfileEntityResponse.QuestionID,
                ResponseTypeID = ProfileEntityResponse.ResponseTypeID
            };

        }

        public UserRespons SetUserResponseModelInstance(UserRespons userResponse)
        {

            userResponse.ResponseID = ProfileEntityResponse.Id;

            return userResponse;
        }


    }
}
