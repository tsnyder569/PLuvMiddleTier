using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Logic.Profile.Data.Logic;
using TIC.PuppyLove.Services.Logic.Profile;
using Ventera.VAF.Core.Helpers;
using System.Reflection;
using TIC.PuppyLove.Profile.DataModel;
using TIC.PuppyLove.Services.Logic.Match;
using TIC.PuppyLove.Services.Contracts.Match.Data;

namespace TIC.PuppyLove.Profile.Data.UnitTest
{
    [TestClass]
    public class DataAccessTest
    {
        [TestMethod]
        public void TestCreateProfileData()
        {
            try
            {
                var dataManager = new DataManager();

                var userData = new UserData
                {
                    UserID = 69,
                    UserName = "DPuddy",
                    FirstName = "David",
                    LastName = "Puddy",
                    Email = "DPuddy@seinfeld.com",
                    DisplayName = "Eisenburg",
                    PhoneNumber = "3016969696",
                     
                };

                ProfileEntity userResponse = new ProfileEntity
                {
                    Id = 17,
                    ResponseTypeID = 2
                };

                ProfileEntity userResponse2 = new ProfileEntity
                {
                    Id = 12,
                    ResponseTypeID = 1
                };

                ProfileEntity userResponse3 = new ProfileEntity
                {
                    Id = 7,
                    ResponseTypeID = 3
                };

                ProfileEntity userResponse4 = new ProfileEntity
                {
                    Id = 20,
                    ResponseTypeID = 2
                };

                ProfileEntity userResponse5 = new ProfileEntity
                {
                    Id = 19,
                    ResponseTypeID = 2
                };
                

                List<ProfileEntity> userRespounses = new List<ProfileEntity>();
                List<ProfileEntity> matchRespounses = new List<ProfileEntity>();

                bool hasValue = false;

                
                userRespounses.Add(userResponse);
                userRespounses.Add(userResponse2);
                userRespounses.Add(userResponse3);
                userRespounses.Add(userResponse4);
                userRespounses.Add(userResponse5);

                ProfileEntity matchResponse = new ProfileEntity
                {
                    Id = 18,
                    ResponseTypeID = 2
                };

                ProfileEntity matchResponse2 = new ProfileEntity
                {
                    Id = 19,
                    ResponseTypeID = 2
                };

                ProfileEntity matchResponse3 = new ProfileEntity
                {
                    Id = 8,
                    ResponseTypeID = 3
                };
                ProfileEntity matchResponse4 = new ProfileEntity
                {
                    Id = 25,
                    ResponseTypeID = 4
                };

                ProfileEntity matchResponse5 = new ProfileEntity
                {
                    Id = 11,
                    ResponseTypeID = 1
                };

                matchRespounses.Add(matchResponse);
                matchRespounses.Add(matchResponse2);
                matchRespounses.Add(matchResponse3);
                matchRespounses.Add(matchResponse4);
                matchRespounses.Add(matchResponse5);

                var userProfileData = new UserProfileData { UserData = userData,
                                                            UserResponses = userRespounses,
                                                            MatchResponses = matchRespounses
                };

                dataManager.AddProfile(userProfileData);
            }
            catch (FormattedEntityValidationException ex)
            {
                LogHelper.LogInformation(ex.Message);
                
            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);
                
            }

        }

        [TestMethod]
        public void TestFetchProfileData()
        {
            try
            {
                var dataManager = new DataManager();

                var userData = new UserProfileRequest
                {
                    //UserID = 69,
                    UserName = "pjlove",
                    ProfileAttributeType = ProfileAttributeTypeEnum.All
                };
               

                UserProfileData data = dataManager.FetchProfile(userData);

                
                    // No user found
                if (data.UserData == null)
                {
                    string msg = "miserable little failure";
                }
            }
            catch (Exception ex)
            {
                string theEx = ex.InnerException.InnerException.Message;
            }

        }

        [TestMethod]
        public void TestUpdateProfileData()
        {
            var dataManager = new DataManager();

            

            /*
            var userData = new UserData
            {
                UserID = 34,
                UserName = "DPuddy",
                FirstName = "David",
                LastName = "Puddy",
                Email = "DPuddy@seinfeld.com",
                DisplayName = "Pud6dy9",
                PhoneNumber = "3016969696",
                Password = "Six6969"
            };

            ProfileEntity userResponse = new ProfileEntity
            {
                Id = 8,
                ID = 12,
                QuestionID = 1
            };

            
            ProfileEntity matchResponseUpdate = new ProfileEntity
            {
                Id = 11,
                ID = 13,
                QuestionID = 2
            };

            ProfileEntity matchResponseNew = new ProfileEntity
            {
                Id = 27,
                
            };

            List<ProfileEntity> userRespounses = new List<ProfileEntity>();
            userRespounses.Add(userResponse);

            
            List<ProfileEntity> matchRespounses = new List<ProfileEntity>();
            matchRespounses.Add(matchResponseUpdate);
            matchRespounses.Add(matchResponseNew);
              

            var userProfileData = new UserProfileData { UserData = userData,
                                                        UserResponses = userRespounses,
                                                        MatchResponses = matchRespounses
                                                      };

            dataManager.ModifyProfile(userProfileData);
             */ 



            /*
            if (data.UserData != null)
            {
                data.UserData.DisplayName = "Giddy6969";
                
                ProfileEntity userResponse = new ProfileEntity
                {
                    Id = 17,
                    ResponseTypeID = 2
                };

                List<ProfileEntity> userRespounses = new List<ProfileEntity>();
                userRespounses.Add(userResponse);
                 

                ProfileEntity matchResponse = new ProfileEntity
                {
                    Id = 19,
                    ResponseTypeID = 2
                };

                ProfileEntity matchResponse2 = new ProfileEntity
                {
                    Id = 20,
                    ResponseTypeID = 2
                };


                List<ProfileEntity> matchRespounses = new List<ProfileEntity>();
                matchRespounses.Add(matchResponse);
                matchRespounses.Add(matchResponse2);

                var userProfileData = new UserProfileData
                {
                    UserData = data.UserData,
                    //UserResponses = userRespounses,
                    MatchResponses = matchRespounses
                };

                dataManager.ModifyProfile(userProfileData);
            }
             */ 

            
            var userDataPuddy = new UserProfileRequest
            {
                //UserID = 69,
                UserName = "pjlove",
                ProfileAttributeType = ProfileAttributeTypeEnum.All
            };

            UserProfileData match = dataManager.FetchProfile(userDataPuddy);

            if (match.UserData != null)
            {
                //match.UserData.DisplayName = "Giddy6969";
                ProfileEntity userResponse = new ProfileEntity
                {
                    Id = 17,
                    ResponseTypeID = 2
                };

                ProfileEntity userResponse2 = new ProfileEntity
                {
                    Id = 12,
                    ResponseTypeID = 1
                };

                ProfileEntity userResponse3 = new ProfileEntity
                {
                    Id = 6,
                    ResponseTypeID = 3
                };

                List<ProfileEntity> userRespounses = new List<ProfileEntity>();
                userRespounses.Add(userResponse);
                userRespounses.Add(userResponse2);
                userRespounses.Add(userResponse3);

                ProfileEntity matchResponse = new ProfileEntity
                {
                    Id = 7,
                    ResponseTypeID = 3
                };

                ProfileEntity matchResponse2 = new ProfileEntity
                {
                    Id = 24,
                    ResponseTypeID = 4
                };

                ProfileEntity matchResponse3 = new ProfileEntity
                {
                    Id = 10,
                    ResponseTypeID = 1
                };

                ProfileEntity matchResponse4 = new ProfileEntity
                {
                    Id = 23,
                    ResponseTypeID = 2
                };




                List<ProfileEntity> matchRespounses = new List<ProfileEntity>();
                matchRespounses.Add(matchResponse);
                matchRespounses.Add(matchResponse2);
                matchRespounses.Add(matchResponse3);
                matchRespounses.Add(matchResponse4);

                var userProfileData = new UserProfileData
                {
                    UserData = match.UserData,
                    UserResponses = userRespounses,
                    MatchResponses = matchRespounses
                };

                dataManager.ModifyProfile(userProfileData);
             
            }
             

            /*
            ProfileEntity matchResponseNew = new ProfileEntity
            {
                Id = 3,

            };
             
            
            ChoiceProfile choiceResponse = new ChoiceProfile
            {
                ChoiceType = ChoiceTypeEnum.Like,
                UserID = 34,
                MatchUser = new UserData
                {
                    UserID = 12,
                    UserName = "DPuddy"
                }

            };
             
            List<ChoiceProfile> userChoices = new List<ChoiceProfile>();

            userChoices.Add(choiceResponse);

            var userProfileData = new UserProfileData
            {
                UserData = data.UserData,
                UserChoices = userChoices
            };

            dataManager.ModifyProfile(userProfileData);
             */ 
             
        }

        [TestMethod]
        public void TestLogin()
        {
            UserProfileRequest loginRequest = new UserProfileRequest
                                                  {
                                                      UserName = "TBagely",
                                                      
                                                  };

            var dataManager = new DataManager();
            var result = dataManager.LoginToProfile(loginRequest);

        }

        [TestMethod]
        public void TestGetQuestions()
        {
            var dataManager = new DataManager();

            ProfileQuestionRequest userRequest = new ProfileQuestionRequest
            {
                ProfileQuestionType = ProfileQuestionTypeEnum.Both
            };

            ProfileQuestions response = dataManager.FetchQuestions(userRequest);
        }

        [TestMethod]
        public void TestMatch()
        {
            MatchLogic matchLogic = new MatchLogic();
            var dataManager = new DataManager();
            MatchRequest req = new MatchRequest();

            var userDataPuddy = new UserProfileRequest
            {
                //UserID = 69,
                UserName = "SRoss",
                ProfileAttributeType = ProfileAttributeTypeEnum.All
            };

            UserProfileData match = dataManager.FetchProfile(userDataPuddy);

            // This is elaine. 
            req.UserID = 6969;

            MatchResponse response = matchLogic.GetMatchByLocation(req);

            if (response != null)
            {

                if (response.MatchProfiles != null && response.MatchProfiles.Count() > 0)
                {
                    foreach (ChoiceProfile cp in response.MatchProfiles)
                    {
                        cp.ChoiceType =  ChoiceTypeEnum.Like;
                        
                    }

                    // Add the match profiles
                    var userProfileData = new UserProfileData
                    {
                        UserData = match.UserData,
                        UserChoices = response.MatchProfiles
                    };

                    dataManager.ModifyProfile(userProfileData);
                }
       
            }
        }

        [TestMethod]
        public void TestValidateUsername()
        {
            UserProfileRequest loginRequest = new UserProfileRequest
            {
                UserName = "SGiddy",
                
            };

            var profileLogic = new ProfileLogic();

            CommonResponse response = profileLogic.ValidateLoginUserName(loginRequest);
        }

        [TestMethod]
        public void TestGetChoiceStatus()
        {
            var ProfileChoiceRequest = new ProfileChoiceRequest
                                           {
                                               UserID = 150318895368012,
                                               MatchUserID = new UserBase { UserID = 69696 }
                                           };

            var dataManager = new DataManager();
            ChoiceProfile choiceProfile = dataManager.GetProfileChoiceStatus(ProfileChoiceRequest);
        }

        #region Photo Processing

        [TestMethod]
        public void TestGetProfilePhotosData()
        {
            List<ProfilePhoto> results = new List<ProfilePhoto>();
            ProfilePhoto request = new ProfilePhoto { UserID = 137179126684623 };

            LogHelper.LogInformation("Here we go...");
            try
            {
                var dmgr = new DataManager();
                results = dmgr.GetProfilePhotosById(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                results = null;
            }

            Assert.IsTrue(results != null && results.Count > 0);

        }

        [TestMethod]
        public void TestGetProfilePhotosLogic()
        {
            ProfilePhotos results = new ProfilePhotos { ProfilePhotoList = new List<ProfilePhoto>() };
            ProfilePhoto request = new ProfilePhoto { UserID = 137179126684623 };

            LogHelper.LogInformation("Here we go...");
            try
            {
                var logic = new ProfileLogic();
                results = logic.GetPhotosByUserID(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                results = null;
            }

            Assert.IsTrue(results != null && results.ProfilePhotoList != null && results.ProfilePhotoList.Count > 0);
        }

        [TestMethod]
        public void TestGetPrimaryPhotoLogic()
        {
            ProfilePhoto result = new ProfilePhoto();
            ProfilePhoto request = new ProfilePhoto { UserID = 137179126684623 };

            LogHelper.LogInformation("Here we go...");
            try
            {
                var logic = new ProfileLogic();
                result = logic.GetPrimaryProfilePhoto(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                result = null;
            }

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void TestGetProfilePhotoTypesLogic ()
        {

            List<ProfilePhotoType> results = new List<ProfilePhotoType>();

            LogHelper.LogInformation("Here we go...");
            try
            {
                var logic = new ProfileLogic();
                results = logic.GetProfilePhotoTypes();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                results = null;
            }

            Assert.IsTrue(results != null && results.Count > 0);
        }

        [TestMethod]
        public void TestGetRefPhotoTypesData()
        {
            List<ProfilePhotoType> results = new List<ProfilePhotoType>();
            LogHelper.LogInformation("Here we go...");
            try
            {
                var dmgr = new DataManager();
                results = dmgr.GetProfilePhotoTypes();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                results = null;
            }

            Assert.IsTrue(results != null && results.Count > 0);
        }

        [TestMethod]
        public void TestSetProfilePhotoypesDTO ()
        {
            var results = new PhotoTypeDTO();
            LogHelper.LogInformation("Here we go...");
            try
            {
                var dmgr = new DataManager();
                results.refPhotoTypes = dmgr.GetRefPhotoTypes();
                results.SetProfilePhotoypes();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                results = null;
            }

            Assert.IsFalse(results.profilePhotoTypes == null);
        }

        #endregion
    
    }
}
