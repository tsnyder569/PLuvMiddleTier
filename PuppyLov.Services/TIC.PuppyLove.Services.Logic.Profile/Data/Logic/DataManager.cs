using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Profile.DataModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;


namespace TIC.PuppyLove.Services.Logic.Profile.Data.Logic
{
    public class DataManager
    {
        
        public void AddProfile(UserProfileData userUpdate)
        {
            using (var context = new TICPuppyLoveDbContext())
            {
                
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    context.dbTransaction = dbContextTransaction;
                    
                    ProfileUser profileUser = new ProfileUser
                                                  {
                                                      ProlieUserData = userUpdate.UserData
                                                  };

                    List<ProfileResponse> profileResponse = new List<ProfileResponse>();
                    List<ProfileResponse> matchResponse = new List<ProfileResponse>();

                    if (userUpdate.UserResponses != null && userUpdate.UserResponses.Count() > 0)
                    {
                        
                        foreach (ProfileEntity response in userUpdate.UserResponses)
                        {
                            ProfileResponse prof = new ProfileResponse
                                                       {
                                                           ProfileEntityResponse = response
                                                       };
                            profileResponse.Add(prof);
                            
                        }
                    }

                    if (userUpdate.MatchResponses != null && userUpdate.MatchResponses.Count() > 0)
                    {

                        foreach (ProfileEntity response in userUpdate.MatchResponses)
                        {
                            ProfileResponse prof = new ProfileResponse
                            {
                                ProfileEntityResponse = response
                            };
                            matchResponse.Add(prof);

                        }
                    }

                    TIC.PuppyLove.Profile.DataModel.Profile profile = new PuppyLove.Profile.DataModel.Profile
                                                                          {
                                                                              UserData = profileUser,
                                                                              UserResponses = profileResponse,
                                                                              MatchResponses = matchResponse
                                                                          };

                    context.Add(profile);

                    // everything good, commit transaction
                    dbContextTransaction.Commit();
                }
            }
        }
        
        

        public UserProfileData FetchProfile(UserProfileRequest userequest)
        {
            var userData = new UserProfileData();

            // instead of evaluating all the enum choices again, set bool value for each object to return
            long questionID = 0;

            using (var dbEntities = new TICPuppyLoveDbContext())
            {
                string userName = string.Empty;

                // Determine whether to use PK or the username
                if (userequest.UserID != null)
                {
                    // Look up username by ID
                    userName = dbEntities.UserNameById(userequest.UserID.Value);
                }
                else
                {
                    userName = userequest.UserName;

                }

                if (userName != string.Empty)
                {

                    // translate to the data model enum
                    QuestionTypeEnum questionType = QuestionTypeEnum.None;

                    switch (userequest.ProfileAttributeType)
                    {
                        case ProfileAttributeTypeEnum.UserData:
                            {
                                questionType = QuestionTypeEnum.None;
                                var result = dbEntities.GetDataContractByUser(userName, questionType);
                                if (result != null)
                                {
                                    userData = new UserProfileData
                                       {
                                           UserData = result.UserData.CreateProfileInstance()
                                       };
                                }
                            }
                            break;

                        case ProfileAttributeTypeEnum.Responses:
                            {
                                questionType = QuestionTypeEnum.UserProfile;
                                var result = dbEntities.GetDataContractByUser(userName, questionType);
                                if (result != null)
                                {
                                    userData = new UserProfileData
                                    {
                                       UserData = result.UserData.CreateProfileInstance(),

                                       UserResponses = (from resp in result.UserResponses
                                                        select resp.CreateProfileResponseInstance()

                                                       ).ToList()
                                   };
                                }
                            }
                            break;

                        case ProfileAttributeTypeEnum.Matches:
                            {
                                questionType = QuestionTypeEnum.MatchPreference;
                                var result = dbEntities.GetDataContractByUser(userName, questionType);
                                if (result != null)
                                {
                                    userData = new UserProfileData
                                    {
                                        UserData = result.UserData.CreateProfileInstance(),

                                        MatchResponses = (from resp in result.MatchResponses
                                                          select resp.CreateProfileResponseInstance()

                                                          ).ToList()
                                    };
                                }
                            
                            }
                            break;

                        case ProfileAttributeTypeEnum.All:
                            {
                                questionType = QuestionTypeEnum.All;
                                var result = dbEntities.GetDataContractByUser(userName, questionType);
                                if (result != null)
                                {
                                    userData = new UserProfileData
                                    {
                                        UserData = result.UserData.CreateProfileInstance(),

                                        UserResponses = (from resp in result.UserResponses
                                                         select resp.CreateProfileResponseInstance()

                                                        ).ToList(),

                                        MatchResponses = (from resp in result.MatchResponses
                                                          select resp.CreateProfileResponseInstance()

                                                          ).ToList(),

                                        UserChoices = (from resp in result.ChoiceResponses
                                                       select resp.CreateProfileChoiceInstance()

                                                       ).ToList()
                                    };
                                }
                            }
                            break;

                        case ProfileAttributeTypeEnum.UserChoices:
                            {
                                questionType = QuestionTypeEnum.ChoiceResponses;
                                var result = dbEntities.GetDataContractByUser(userName, questionType);
                                if (result != null)
                                {
                                    userData = new UserProfileData
                                    {
                                        //UserData = result.UserData.CreateProfileInstance(),

                                        UserChoices = (from resp in result.ChoiceResponses
                                                       select resp.CreateProfileChoiceInstance()

                                                       ).ToList()
                                    };
                                }
                            }
                            break;

                        case ProfileAttributeTypeEnum.AllResponses:
                            {
                                questionType = QuestionTypeEnum.BothProfiles;
                                var result = dbEntities.GetDataContractByUser(userName, questionType);
                                if (result != null)
                                {
                                    userData = new UserProfileData
                                    {
                                        UserData = result.UserData.CreateProfileInstance(),

                                        UserResponses = (from resp in result.UserResponses
                                                         select resp.CreateProfileResponseInstance()

                                                        ).ToList(),

                                        MatchResponses = (from resp in result.MatchResponses
                                                          select resp.CreateProfileResponseInstance()

                                                          ).ToList(),

                                    };
                                }
                            }
                            break;

                    }
                }

            }
            
            return userData;
        }

        public ProfileQuestions FetchQuestions(ProfileQuestionRequest userRequest)
        {
            ProfileQuestions questionResponse = new ProfileQuestions();

            using (var dbEntities = new TICPuppyLoveDbContext())
            {
                // Need to convert to EF model enum. This is what loose coupling is all about. Model layer knows
                // nothing about the WCF data contratcs. Giddy-up!!!
                QuestionTypeEnum questionType = QuestionTypeEnum.None;

                switch (userRequest.ProfileQuestionType)
                {
                    case ProfileQuestionTypeEnum.UserProfile:
                            {
                                questionType = QuestionTypeEnum.UserProfile;
                            }
                            break;
                    case ProfileQuestionTypeEnum.MatchProfile:
                            {
                                questionType = QuestionTypeEnum.MatchPreference;
                            }
                            break;
                    case ProfileQuestionTypeEnum.Both:
                            {
                                questionType = QuestionTypeEnum.All;
                            }
                            break;
                }

                PLuvQuestionSurvey questions = dbEntities.GetDataContractByProfileQuestions(questionType);
                if (questions != null)
                {
                    
                    if (questions.UserQuestions != null)
                    {
                        questionResponse.UserQuestions = (from profQuestion in questions.UserQuestions
                                                          select new Question
                                                          {
                                                              ProfileQuestion = profQuestion.ProfileQuestion.CreateQuestionEntityInstance(),
                                                              ProfileResponses = (from profResponse in profQuestion.ProfileResponses
                                                                                  select profResponse.CreateResponseEntityInstance()

                                                                                 ).ToList()
                                                          }
                                                         ).ToList();

                    }

                    if (questions.MatchQuestions != null)
                    {
                        questionResponse.MatchQuestions = (from profQuestion in questions.MatchQuestions
                                                          select new Question
                                                          {
                                                              ProfileQuestion = profQuestion.ProfileQuestion.CreateQuestionEntityInstance(),
                                                              ProfileResponses = (from profResponse in profQuestion.ProfileResponses
                                                                                  select profResponse.CreateResponseEntityInstance()

                                                                                 ).ToList()
                                                          }
                                                         ).ToList();

                    }

                    
                }
            }
            return questionResponse;
        }

        public LoginResponse LoginToProfile(UserProfileRequest userRequest)
        {
            LoginResponse response = new LoginResponse();

            return new LoginResponse { Status = false, ReturnMessage = "Operation is no longer used" };
        }

        public void ModifyProfile(UserProfileData userRequest)
        {
            using (var context = new TICPuppyLoveDbContext())
            {
                
                ProfileUser profileUser = new ProfileUser
                {
                    ProlieUserData = userRequest.UserData
                };

                List<ProfileResponse> profileResponse = new List<ProfileResponse>();
                List<ProfileResponse> matchResponse = new List<ProfileResponse>();
                List<Choice> userChoices = new List<Choice>();

                if (userRequest.UserResponses != null && userRequest.UserResponses.Count() > 0)
                {

                    foreach (ProfileEntity response in userRequest.UserResponses)
                    {
                        ProfileResponse prof = new ProfileResponse
                        {
                            ProfileEntityResponse = response
                        };
                        profileResponse.Add(prof);

                    }
                }

                if (userRequest.MatchResponses != null && userRequest.MatchResponses.Count() > 0)
                {

                    foreach (ProfileEntity response in userRequest.MatchResponses)
                    {
                        ProfileResponse prof = new ProfileResponse
                        {
                            ProfileEntityResponse = response
                        };
                        matchResponse.Add(prof);

                    }
                }

                if (userRequest.UserChoices != null && userRequest.UserChoices.Count() > 0)
                {
                    foreach (ChoiceProfile choice in userRequest.UserChoices)
                    {
                        Choice userChoice = new Choice
                        {
                            ProfileChoice = choice
                            
                        };
                        userChoices.Add(userChoice);

                        // Also check to see if the match user likes this user as well. Then the state can be
                        // set to 
                        if (choice.ChoiceType == ChoiceTypeEnum.Like)
                        {

                        }

                    }
                }

                TIC.PuppyLove.Profile.DataModel.Profile profile = new PuppyLove.Profile.DataModel.Profile
                {
                    UserData = profileUser,
                    UserResponses = profileResponse,
                    MatchResponses = matchResponse,
                    ChoiceResponses = userChoices
                    
                };

                if (userRequest.UserData != null)
                {
                    var id = userRequest.UserData.UserID;

                    if (context.Users.Any(e => e.ID == id))
                    {
                        context.Attach(profile);
                    }
                    else
                    {
                        context.Add(profile);
                    }
                } 
            }
        }

        public bool IsUserNameValid(string userName)
        {
            bool response = false;

            using (var context = new TICPuppyLoveDbContext())
            {
                response = context.DoesUserNameExist(userName);
            }
            return response;
        }

        public ChoiceProfile GetProfileChoiceStatus(ProfileChoiceRequest request)
        {
            var response = new ChoiceProfile();

            using (var context = new TICPuppyLoveDbContext())
            {
                Choice choiceProfile = context.GetProfileChoiceStatusByUser(request.UserID, request.MatchUserID.UserID);
                if (choiceProfile != null)
                    response = choiceProfile.CreateProfileChoiceInstance();
                else
                    response = null;

            }

            return response;
        }

        #region Photo Processing

        #region GET

        #region Photo Types

        public List<ProfilePhotoType> GetProfilePhotoTypes()
        {
            var result = new PhotoTypeDTO();

            result.refPhotoTypes = GetRefPhotoTypes();
            result.SetProfilePhotoypes();

            return (result.profilePhotoTypes != null) ? result.profilePhotoTypes : new List<ProfilePhotoType>();
        }

        public List<Ref_PhotoType> GetRefPhotoTypes()
        {
            var response = new List<Ref_PhotoType>();
            using (var dbEntities = new TICPuppyLoveDbContext())
            {
                response = dbEntities.GetRefPhotoTypes();
            }

            return response;

        }

        #endregion

        #region Photos

        public ProfilePhoto GetPrimaryProfilePhoto (ProfilePhoto request)
        {
            var result = new PhotoDTO();
            result.Photos = GetPrimaryPhotos(request);
            result.SetPrimaryProfilePhoto(GetProfilePhotoTypes());

            return result.profilePhoto;
        }

        public List<ProfilePhoto> GetProfilePhotosById(ProfilePhoto request)
        {
            var result = new PhotoDTO();
            result.Photos = GetPhotosByUserID(request);
            result.SetProfilePhotos(GetProfilePhotoTypes());

            return (result.profilePhotos != null) ? result.profilePhotos : new List<ProfilePhoto>();
        }

        private List<Photo> GetPhotosByUserID(ProfilePhoto request)
        {
            var response = new List<Photo>();
            using (var dbEntities = new TICPuppyLoveDbContext())
            {
                response = dbEntities.GetPhotosByUserID(request.UserID);
            }

            return response;
        }

        private List<Photo> GetPrimaryPhotos(ProfilePhoto request)
        {
            var response = new List<Photo>();
            using (var dbEntities = new TICPuppyLoveDbContext())
            {
                response = dbEntities.GetPrimaryPhoto(request.UserID);
            }

            return response; 
        }

        #endregion

        #endregion

        #region AddUpdate

        public void AddUpdatePhotos(ProfilePhotos items)
        {
            using (var context = new TICPuppyLoveDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    context.dbTransaction = dbContextTransaction;

                    PhotoDTO pdto = new PhotoDTO { profilePhotos = items.ProfilePhotoList };

                    //create the instances for INSERT and add them to db
                    pdto.CreateNewDBPhotoInstances(GetProfilePhotoTypes());

                    if (pdto.Photos.Count > 0)
                    {
                        context.Add(pdto);
                    }

                    //create instances for UPDATE and send to db
                    pdto.CreateUpdateDBPhotoInstances(GetProfilePhotoTypes());

                    if (pdto.Photos.Count > 0)
                    {
                        context.Update(pdto);
                    }


                    // everything good, commit transaction
                    dbContextTransaction.Commit();
                }
            }
        }


        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Static Constructor to load Entity framework required assemblies
        /// </summary>
        static DataManager()
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        #endregion

    }
}
