using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Match.Service;
using TIC.PuppyLove.Services.Contracts.Match.Data;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using TIC.PuppyLove.Services.Contracts.Location.Data;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Logic.Match.Data;
using Ventera.VAF.Core.Helpers;
using System.Reflection;
using System.ServiceModel.Activation;

using TIC.PuppyLove.Helpers;
using Ventera.VAF.Core.Configuration;


namespace TIC.PuppyLove.Services.Logic.Match
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class MatchLogic : IProfileMatchService
    {
        
        
        public Tr MakeServicePostRequest<Ti, Tr>(string url, 
                                                 string operation,
                                                 Ti request)
            where Ti : class
            where Tr : class
        {

            Tr result = default(Tr);

            var dispatchUrl = VAFConfiguration.GetAppSettingValue(url);
            var fullUrl = new StringBuilder().AppendFormat("{0}/{1}",
                dispatchUrl,
                VAFConfiguration.GetAppSettingValue(operation));

            var jsonHelper = new JsonHelper();
            result = jsonHelper.MakePostRequest<Ti, Tr>(fullUrl.ToString(), request);

            return result;
        }


        private bool IsUserInChoiceList(List<ChoiceProfile> userChoices,
                                        long? userID)
        {
            bool isInList = false;

            var result = (from choice in userChoices
                          where choice.MatchUser.UserID == userID
                          select choice
                              ).SingleOrDefault();
            isInList = (result != null) ? true : false;
            return isInList;

        }

        
        public MatchResponse GetMatchByLocation(MatchRequest request)
        {
            var response = new MatchResponse
                                         {
                                             Status = true,
                                             ReturnMessage = "Succeeded",

                                             MatchProfiles = new List<ChoiceProfile>()
                                         };
            try
            {
                LogHelper.LogInformation(string.Concat(Resource.GetMatchByLocation, MethodBase.GetCurrentMethod().Name));
                var dataManager = new DataManager();
                var matchEngine = new MatchEngine();

                if (request != null)
                {
                    // Get config data for Relationship Type name
                    string relationshipType = VAFConfiguration.GetAppSettingValue("RelationShipType");
                   
                    // Get user data and preferences from the profile service
                   
                    var profRequest = new UserProfileRequest { UserID = request.UserID, ProfileAttributeType = ProfileAttributeTypeEnum.All };
                    var user = MakeServicePostRequest<UserProfileRequest, UserProfileData>("ProfileUrl",
                                                                                           "ProfileProcessOp",
                                                                                            profRequest);

                    // Get the RelationTypoID for querying entities. 
                    long? relationshipTypeID = dataManager.GetResponseTypeIDByType(relationshipType);
                    
                    // Get the list of Response Types not included in the match process
                    var excludedReponseTypes = dataManager.ExcludedMatchingResponseTypes();
                    matchEngine.ExcludedMatchList = excludedReponseTypes;
                    // Get the list of response types sans any responses that include No Preference
                    var userProfileMatchList = dataManager.ExcludeNoPreferenceFromProfile(user.MatchResponses);

                    // Loop through all the profiles that are close and match both ways. Need to ignore running matches 
                    // on anyone that is in the profile choice table.       
                    
                    var locRequest = new NearestNeighborRequest { UserID = request.UserID, Radius = 20000 };
                    var userlocations = MakeServicePostRequest<NearestNeighborRequest, LocationResponse>("LocationUrlCloud",
                                                                                                          "GetNearestNeighborsOp",
                                                                                                          locRequest);

                    foreach (var userLoc in userlocations.Locations)
                    {
                        // Make sure user is not already in the profile choice list
                        bool isUserInChoices = false;
                        if (user.UserChoices != null && user.UserChoices.Count() > 0)
                        {
                            isUserInChoices = IsUserInChoiceList(user.UserChoices,
                                                                 userLoc.UserID);
                        }

                        if (!isUserInChoices)
                        {
                            /*
                            var match = GetUserProfileData(userLoc.UserID, 
                                                           ProfileAttributeTypeEnum.AllResponses);
                             */ 

                            var matchRequest = new UserProfileRequest { UserID = userLoc.UserID, ProfileAttributeType = ProfileAttributeTypeEnum.All };
                            var match = MakeServicePostRequest<UserProfileRequest, UserProfileData>("ProfileUrl",
                                                                                                   "ProfileProcessOp",
                                                                                                    matchRequest);


                            if (user.MatchResponses != null && match.UserResponses != null
                                && user.UserResponses != null && match.MatchResponses != null)
                            {

                                bool matchSucceeded = true;
                                // Only run the match for this type since it is a one way match that deviates from 
                                // the user preference to match preference match relationship.
                                matchEngine.JoinOnMatchList = new List<long> { relationshipTypeID.Value };

                                matchSucceeded = matchEngine.MatchProfileByResponseTypes(user.MatchResponses,
                                                                                         match.MatchResponses,
                                                                                         userProfileMatchList);
                                List<long> matchProfileMatchList = new List<long>();

                                if (!matchSucceeded)
                                {
                                    // Need to make sure if didn't fail because match user set No Preference
                                    matchProfileMatchList = dataManager.ExcludeNoPreferenceFromProfile(match.MatchResponses);

                                    matchSucceeded = matchEngine.MatchProfileByResponseTypes(user.MatchResponses,
                                                                                             match.MatchResponses,
                                                                                             matchProfileMatchList);
                                }

                                // only proceed with the other match if first one was successful.
                                if (matchSucceeded)
                                {
                                   
                                    matchEngine.JoinOnMatchList.Clear();
                                    // Now exlude matching on relationship type
                                    matchEngine.ExcludedMatchList.Add(relationshipTypeID.Value);

                                    matchSucceeded = matchEngine.MatchProfileByResponseTypes(user.MatchResponses,
                                                                                             match.UserResponses,
                                                                                             userProfileMatchList);
                                }

                                // only check if previous match succeeded. 
                                if (matchSucceeded)
                                {
                                    // Now flip it and check the match profile responses of the user close by with the
                                    // user profile of our user.
                                    if (matchProfileMatchList.Count() == 0)
                                        matchProfileMatchList = dataManager.ExcludeNoPreferenceFromProfile(match.MatchResponses);

                                    matchSucceeded = matchEngine.MatchProfileByResponseTypes(user.UserResponses,
                                                                                             match.MatchResponses,
                                                                                             matchProfileMatchList);
                                }

                                // If it is a match both ways, add to the list of matches returned to the mobile consumer.
                                if (matchSucceeded)
                                {

                                    // ctor a profile choice object and return. This will include the photo.
                                    var profileChoice = new ChoiceProfile
                                    {
                                        ChoiceType = ChoiceTypeEnum.None,
                                        MatchChoice = new MatchChoiceProfile { MatchChoiceType = ChoiceTypeEnum.None },
                                        MatchUser = match.UserData

                                    };

                                    // Check to see if this match already likes the user.
                                    var matchChoice = (from mc in match.UserChoices
                                                       where mc.MatchUser.UserID == user.UserData.UserID
                                                       select mc).SingleOrDefault();

                                    if (matchChoice != null)
                                    {
                                        if (matchChoice.ChoiceType != ChoiceTypeEnum.Dislike)
                                        {
                                            profileChoice.MatchChoice.MatchChoiceType = matchChoice.ChoiceType;
                                            profileChoice.MatchChoice.ID = matchChoice.ID;
                                            response.MatchProfiles.Add(profileChoice);
                                        }
                                    }
                                    else
                                    {
                                        response.MatchProfiles.Add(profileChoice);
                                    }
                                    
                                } 
                            }
                        }
                    }
                }
                else
                {
                    LogHelper.LogInformation("request object is null you miserable little failure");
                    response.Status = false;
                    response.ReturnMessage = "You miserable little failure. the operation failed the request object is null.";
                }
            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);
                response.Status = false;
                response.ReturnMessage = "You miserable little failure. the operation failed.";
            }

            // This will contian all the profiles that are close and satisfy the match criteria.

            return response;
        }
    }
}
