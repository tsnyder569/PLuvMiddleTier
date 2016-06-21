using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;


namespace TIC.PuppyLove.Profile.DataModel
{
    public partial class TICPuppyLoveDbContext : TICPuppyLoveDBEntities
    {
        public System.Data.Entity.DbContextTransaction dbTransaction { get; set; }

        
        public bool DoesUserNameExist(string userName)
        {
            bool response = false;

            var result = (from user in base.Users
                          where user.UserName == userName
                          select user).SingleOrDefault();

            response = (result != null) ? true : false;
            return response;
        }

        public Choice GetProfileChoiceStatusByUser(long? userId,
                                                   long? matchId)
        {
            
            var result = (from choice in base.ProfileChocies
                          join choiceType in Ref_ChoiceType
                          on choice.ChoiceType equals choiceType.ID
                          where (choice.UserID == matchId && choice.ProfileChoiceUserID == userId)
                          select new Choice 
                                     {
                                        UserChoice = choice,
                                        ChoiceTypeStr = choiceType.MatchType
                                      }).SingleOrDefault();


            return result;


        }

        // TBD Move this once I create db context for match. This gets a list of no preferences for each response type
        //public List<ProfileResponse> GetNoPreferences(long? userID)
        public List<ProfileResponse> ExcludeNoPreferences(List<ProfileResponse> matchResponses)
                                                          
        {
            
            var result = (from userResp in matchResponses
                          join resp in Responses
                          on userResp.ProfileEntityResponse.Id equals resp.ID
                          where (resp.Responses != "No Preference")
                          select userResp
                          ).ToList();

            return result;

        }

        // TBD Move this once I create db context for match. This gets a list of no preferences for a response type
        //public List<ProfileResponse> GetNoPreferences(long? userID)
        public List<ProfileResponse> ExcludeNoPreferencesByType(List<ProfileResponse> matchResponses,
                                                                long? responseTypeID)
        {
            var result = (from userResp in matchResponses
                          join resp in Responses
                          on userResp.ProfileEntityResponse.Id equals resp.ID
                          where (resp.ResponseTypeID == responseTypeID &&
                                 resp.Responses != "No Preference")
                          select userResp
                          ).ToList();

            return result;

        }

        private List<ProfileResponse> GetResponsesByType(List<ProfileResponse> matchResponses,
                                                         long? responseTypeID)
        {
            var result = (from userResp in matchResponses
                          join resp in Responses
                          on userResp.ProfileEntityResponse.Id equals resp.ID
                          where (resp.ResponseTypeID == responseTypeID &&
                                 resp.Responses != "No Preference")
                          select userResp
                          ).ToList();

            return result;

        }

        /// <summary>
        /// Look up operations to return ref id based on the type
        /// </summary>
        /// <param name="userUpdate"></param>
        private long? QuestionTypeID(string questionType)
        {
            
            var result = (from type in base.Ref_QuestionType
                          where type.QuestionType == questionType
                          select type
                              ).SingleOrDefault();
            return result.ID;
        }

        /// <summary>
        /// Look up operations to return ref id based on the type
        /// </summary>
        /// <param name="userUpdate"></param>
        public long? ResponseTypeID(string responseType)
        {

            var result = (from type in base.Ref_ResponseType
                          where type.ResponseType == responseType
                          select type
                              ).SingleOrDefault();
            return result.ID;
        }

        /// <summary>
        /// Look up operations to return ref id based on the type
        /// </summary>
        /// <param name="userUpdate"></param>
        private long? ChoiceTypeID(string choiceType)
        {

            var result = (from type in base.Ref_ChoiceType
                          where type.MatchType == choiceType
                          select type
                              ).SingleOrDefault();
            return result.ID;
        }

        
        public string UserNameById(long userId)
        {
            string userName = string.Empty;

            var result = (from user in base.Users
                          where user.ID == userId
                          select user).SingleOrDefault();

            if (result != null)
                userName = result.UserName;
            return userName;
        }

        private void AddToResponses(QuestionTypeEnum questionType,
                                    long userId,
                                    List<ProfileResponse> profileList)
        {
            long? questionTypeId = QuestionTypeID(questionType.ToString());
            
            foreach (ProfileResponse response in profileList)
            {
                base.UserResponses.Add(response.CreateResponseModelInstance(userId,
                                                                            questionTypeId.Value));
            }

        }

        private void UpdateResponse(QuestionTypeEnum questionType, 
                                    long userID,
                                    List<ProfileResponse> profileList)        
        {
            foreach (ProfileResponse response in profileList)
            {
                
                // responses all could be completed later, so need to check if they are new or updates
                if (response.ProfileEntityResponse.ID == null)
                {
                    long questionTypeId = QuestionTypeID(questionType.ToString()).Value;
                    base.UserResponses.Add(response.CreateResponseModelInstance(userID,
                                                                                questionTypeId));
                }
                else
                {
                    var userResponse = response.CreateResponseModelInstanceForUpdate(userID);
                    base.UserResponses.Attach(userResponse);

                    userResponse = response.SetUserResponseModelInstance(userResponse);
                    
                }
                
            }

        }

        private long? UpdateStateOfMatchChoice(Services.Contracts.Common.ChoiceTypeEnum choiceType,
                                               Choice choice,
                                               long userID)
        {
            long? result = null;

            if (choice.ProfileChoice.ChoiceType == Services.Contracts.Common.ChoiceTypeEnum.Like)
            {
                if (choice.ProfileChoice.MatchChoice != null)
                {
                    if (choice.ProfileChoice.MatchChoice.MatchChoiceType == Services.Contracts.Common.ChoiceTypeEnum.Like)
                    {
                        // Transition the state to consent. 
                        result = ChoiceTypeID(choiceType.ToString());

                        // set the match user state to consent as well.
                        var matchChoice = choice.CreateProfileChoiceModelInstance(choice.ProfileChoice.MatchUser.UserID.Value,
                                                                                  userID,
                                                                                  choice.ProfileChoice.MatchChoice.ID.Value);
                        base.ProfileChocies.Attach(matchChoice);

                        matchChoice = choice.SetProfileChoiceModelInstance(matchChoice,
                                                                           result.Value);
                    }
                }
            }

            return result;
        }

        private void UpdateChoices(long userID,
                                   List<Choice> choiceList)
        {
            foreach (Choice choice in choiceList)
            {
                // responses all could be completed later, so need to check if they are new or updates
                long? choiceTypeId = null;

                // Check if user selected like. if so, need to see if we have a mutual lile
                choiceTypeId = UpdateStateOfMatchChoice(Services.Contracts.Common.ChoiceTypeEnum.Consent,
                                                        choice,
                                                        userID);
                // There is no mutual consent yet.
                if (choiceTypeId == null)
                {
                    choiceTypeId = ChoiceTypeID(choice.ProfileChoice.ChoiceType.ToString());
                }

                if (choice.ProfileChoice.ID == null)
                {
                    
                    base.ProfileChocies.Add(choice.CreateProfileChoiceModelInstance(userID,
                                                                                    choiceTypeId.Value));
                }
                else
                {
                    var userChoice = choice.CreateProfileChoiceModelInstance();
                    base.ProfileChocies.Attach(userChoice);

                    userChoice = choice.SetProfileChoiceModelInstance(userChoice,
                                                                      choiceTypeId.Value);

                }

            }

        }

        private void AddToChoices(long userId,
                                  List<Choice> choiceList)
        {
            

            foreach (Choice choice in choiceList)
            {

                long? choiceTypeId = ChoiceTypeID(choice.ProfileChoice.ChoiceType.ToString());
                base.ProfileChocies.Add(choice.CreateProfileChoiceModelInstance(userId,
                                                                                choiceTypeId.Value));
            }

        }

        private List<PLuvQuestion> GetQuestionsByType(QuestionTypeEnum questionType)
        {
            // Linq to SQL cannot handle ToString(). Consequently, need to convert to string here.
            string questionTypeeStr = questionType.ToString();

            // Getting User Profile Questions 
            var result = (from attr in base.Attributes
                          join qType in Ref_QuestionType
                          on attr.QuestionTypeID equals qType.ID
                          where qType.QuestionType == questionTypeeStr
                          select new PLuvQuestion
                          {
                              ProfileQuestion = new QuestionEntity
                              {
                                  Question = attr
                              },

                              ProfileResponses = (from resp in Responses
                                                  where resp.ResponseTypeID == attr.ResponseTypeID
                                                  select new ResponseEntity
                                                  {
                                                      Response = resp
                                                  }
                                                  ).ToList()

                          }).ToList();

            return result;
        }

        public override int SaveChanges()
        {
            try       
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedEntityValidationException(e);
                if (dbTransaction != null)
                    dbTransaction.Rollback();
                throw newException;
            }

            catch (DbUpdateException e)
            {
                var newException = new FormattedEntityValidationException(e);
                if (dbTransaction != null)
                    dbTransaction.Rollback();
                throw newException;
            }
        }

        public void Add(Profile profile)
        {
            var userData = profile.UserData.CreateUserModelInstance();

            base.Users.Add(userData);
            SaveChanges();
            var userId = userData.ID;

            if (profile.UserResponses != null && profile.UserResponses.Count() > 0)
            {
                AddToResponses(QuestionTypeEnum.UserProfile, userId, profile.UserResponses);
            }
                
            if (profile.MatchResponses != null && profile.MatchResponses.Count() > 0)
            {
                AddToResponses(QuestionTypeEnum.MatchPreference, userId, profile.MatchResponses);
            }

            if (profile.ChoiceResponses != null && profile.ChoiceResponses.Count() > 0)
            {
                AddToChoices(userId, profile.ChoiceResponses);
            }

            SaveChanges();
        }


        public void Attach(Profile profile)
        {
            
            var userData = profile.UserData.CreateUserModelInstance(profile.UserData.ProlieUserData.UserID.Value);
            base.Users.Attach(userData);
            userData = profile.UserData.SetUserModelInstance(userData);

            var isModified = base.Entry(userData).Property(u => u.DisplayName).IsModified; 
            
            if (profile.UserResponses != null && profile.UserResponses.Count() > 0)
            {
                // For the user response table given all the possibilities of checking and unchecking check boxes
                // and selecting No Preferences which automatically deselects everything else, easier to delete all
                // first and then do an add. 

                long questionTypeId = QuestionTypeID(QuestionTypeEnum.UserProfile.ToString()).Value;

                // Need to remove all the records for user responses before we do the add.
                base.UserResponses.RemoveRange(base.UserResponses.Where(x => x.QuestionTypeID == questionTypeId &&
                                                                             x.UserID == userData.ID));

                AddToResponses(QuestionTypeEnum.UserProfile, userData.ID, profile.UserResponses);
            
            }

            if (profile.MatchResponses != null && profile.MatchResponses.Count() > 0)
            {
                // For the user response table given all the possibilities of checking and unchecking check boxes
                // and selecting No Preferences which automatically deselects everything else, easier to delete all
                // first and then to an add. 

                long questionTypeId = QuestionTypeID(QuestionTypeEnum.MatchPreference.ToString()).Value;

                // Need to remove all the records for match responses before we do the add.
                base.UserResponses.RemoveRange(base.UserResponses.Where(x => x.QuestionTypeID == questionTypeId &&
                                                                             x.UserID == userData.ID));

                AddToResponses(QuestionTypeEnum.MatchPreference, userData.ID, profile.MatchResponses);

            }

            if (profile.ChoiceResponses != null && profile.ChoiceResponses.Count() > 0)
            {
                UpdateChoices(userData.ID, profile.ChoiceResponses);

            }

            SaveChanges();
        }

        public PLuvQuestionSurvey GetDataContractByProfileQuestions(QuestionTypeEnum questionType)
        {
            PLuvQuestionSurvey profileQuestions = new PLuvQuestionSurvey();

            switch (questionType)
            {
                case QuestionTypeEnum.UserProfile:
                    {
                        var result = GetQuestionsByType(questionType);
                        if (result != null)
                        {
                            profileQuestions.UserQuestions = result;
                        }
                    }
                    break;
                case QuestionTypeEnum.MatchPreference:
                    {
                        var result = GetQuestionsByType(questionType);
                        if (result != null)
                        {
                            profileQuestions.MatchQuestions = result;
                        }
                    }
                    break;
                case QuestionTypeEnum.All:
                    {
                        var result = GetQuestionsByType(QuestionTypeEnum.UserProfile);
                        if (result != null)
                        {
                            profileQuestions.UserQuestions = result;
                        }
                        result = GetQuestionsByType(QuestionTypeEnum.MatchPreference);
                        if (result != null)
                        {
                            profileQuestions.MatchQuestions = result;
                        }
                    }
                    break;

            }

            return profileQuestions;
        }
        
        
        public Profile GetDataContractByUser(string key, 
                                             QuestionTypeEnum questionType)
        {
            Profile userProfile = new Profile();
            long? userProfileQuestionTypeId = null;
            long? matchProfileQuestionTypeId = null;

            switch (questionType)
            {
                case QuestionTypeEnum.UserProfile:
                    {
                        // Get question tyoe id for adding user responses
                        userProfileQuestionTypeId = QuestionTypeID(QuestionTypeEnum.UserProfile.ToString());
                        var result = (from user in base.Users
                                      where user.UserName == key
                                      select new Profile
                                      {

                                          UserData = new ProfileUser
                                          {
                                              User = user
                                          },

                                          UserResponses = (from resp in user.UserResponses
                                                           where resp.QuestionTypeID == userProfileQuestionTypeId
                                                           select new ProfileResponse
                                                           {
                                                               Response = resp
                                                           }
                                                           ).ToList()

                                      }
                         );

                         userProfile = result.SingleOrDefault();
                    }
                    break;
                case QuestionTypeEnum.MatchPreference:
                    {
                        matchProfileQuestionTypeId = QuestionTypeID(QuestionTypeEnum.MatchPreference.ToString());

                        var result = (from user in base.Users
                                      where user.UserName == key
                                      select new Profile
                                      {

                                          UserData = new ProfileUser
                                          {
                                              User = user
                                          },

                                          
                                          MatchResponses = (from resp in user.UserResponses
                                                            where resp.QuestionTypeID == matchProfileQuestionTypeId
                                                            select new ProfileResponse
                                                            {
                                                                Response = resp
                                                            }
                                                           ).ToList(),

                                      }
                         );

                        userProfile = result.SingleOrDefault();
                    }
                    break;
                case QuestionTypeEnum.ChoiceResponses:
                    {
                        var result = (from user in base.Users
                                      where user.UserName == key
                                      select new Profile
                                      {

                                          UserData = new ProfileUser
                                          {
                                              User = user
                                          },

                                          
                                          ChoiceResponses = (from choice in user.ProfileChocies
                                                             join puser in Users
                                                             on choice.ProfileChoiceUserID equals puser.ID
                                                             join choiceType in Ref_ChoiceType
                                                             on choice.ChoiceType equals choiceType.ID
                                                             where choice.UserID == user.ID
                                                             select new Choice
                                                             {
                                                                 UserChoice = choice,
                                                                 MUser = puser,
                                                                 ChoiceTypeStr = choiceType.MatchType

                                                             }
                                                           ).ToList()

                                      }
                         );

                        userProfile = result.SingleOrDefault();
                    }
                    break;
                case QuestionTypeEnum.All:
                    {
                        // Get question tyoe id for adding user responses
                        //userProfileQuestionTypeId = QuestionTypeID(questionType.ToString());
                        userProfileQuestionTypeId = QuestionTypeID(QuestionTypeEnum.UserProfile.ToString());
                        matchProfileQuestionTypeId = QuestionTypeID(QuestionTypeEnum.MatchPreference.ToString());

                        var result = (from user in base.Users
                                      where user.UserName == key
                                      select new Profile
                                      {

                                          UserData = new ProfileUser
                                          {
                                              User = user
                                          },

                                          UserResponses = (from resp in user.UserResponses
                                                           where resp.QuestionTypeID == userProfileQuestionTypeId
                                                           select new ProfileResponse
                                                           {
                                                               Response = resp
                                                           }
                                                           ).ToList(),

                                          MatchResponses = (from resp in user.UserResponses
                                                            where resp.QuestionTypeID == matchProfileQuestionTypeId
                                                            select new ProfileResponse
                                                            {
                                                                Response = resp
                                                            }
                                                           ).ToList(),

                                          ChoiceResponses = (from choice in user.ProfileChocies
                                                             join puser in Users
                                                             on choice.ProfileChoiceUserID equals puser.ID
                                                             join choiceType in Ref_ChoiceType
                                                             on choice.ChoiceType equals choiceType.ID
                                                             where choice.UserID == user.ID
                                                             select new Choice 
                                                             {
                                                                 UserChoice = choice,
                                                                 MUser = puser,
                                                                 ChoiceTypeStr = choiceType.MatchType
                                                                  
                                                             }
                                                           ).ToList()

                                      }
                         );

                        userProfile = result.SingleOrDefault();
                    }
                    break;
                case QuestionTypeEnum.BothProfiles:
                    {
                        // Get question tyoe id for adding user responses
                        //userProfileQuestionTypeId = QuestionTypeID(questionType.ToString());
                        userProfileQuestionTypeId = QuestionTypeID(QuestionTypeEnum.UserProfile.ToString());
                        matchProfileQuestionTypeId = QuestionTypeID(QuestionTypeEnum.MatchPreference.ToString());

                        var result = (from user in base.Users
                                      where user.UserName == key
                                      select new Profile
                                      {

                                          UserData = new ProfileUser
                                          {
                                              User = user
                                          },

                                          UserResponses = (from resp in user.UserResponses
                                                           where resp.QuestionTypeID == userProfileQuestionTypeId
                                                           select new ProfileResponse
                                                           {
                                                               Response = resp
                                                           }
                                                           ).ToList(),

                                          MatchResponses = (from resp in user.UserResponses
                                                            where resp.QuestionTypeID == matchProfileQuestionTypeId
                                                            select new ProfileResponse
                                                            {
                                                                Response = resp
                                                            }
                                                           ).ToList(),

                                      }
                         );

                        userProfile = result.SingleOrDefault();
                    }
                    break;
                case QuestionTypeEnum.None:
                    {
                        var result = (from user in base.Users
                                      where user.UserName == key
                                      select new Profile
                                      {
                                          UserData = new ProfileUser
                                          {
                                              User = user
                                          },
                                      }
                                      );
                        userProfile = result.SingleOrDefault();
                    }
                    break;
            }

            return userProfile;
        }

        public bool DoesProfileMatch(Profile userProfile,
                                     Profile matchProfile)
        {
            /*
            foreach (ProfileResponse match in userProfile.MatchResponses)
            {
                if (matchProfile.UserResponses != null && matchProfile.UserResponses.Count() > 0)
                {
                    // 

                }
            }
             */ 

            

            return true;
        }

        #region Photo processing

        #region Photo Ref Type

        public List<Ref_PhotoType> GetRefPhotoTypes ()
        {
            return (from rpt in base.Ref_PhotoType
                    orderby rpt.ID
                    select rpt).ToList();
        }

        #endregion

        #region Photo

        public List<Photo> GetPhotosByUserID(Int64 UserID)
        {
            return (from pic in base.Photos
                    where pic.UserID == UserID
                    select pic).ToList();
        }

        public List<Photo> GetPrimaryPhoto(Int64 UserID)
        {
            var items = (from pic in base.Photos
                         where pic.UserID == UserID &&
                         pic.IsPrimary == true
                         select pic).ToList();

            return (items != null && items.Count > 0 ) ? items : new List<Photo>();
        }

        public void Add (PhotoDTO pdto)
        {
            foreach (Photo p in pdto.Photos)
            {
                //First, turn off the current IsPrimary for this user if this pic is priamary.  Code referenced from following url:
                //http://www.dotnetlearners.com/blogs/view/142/update-multiple-records-in-entity-framework-in-a-single-line-by-using-linq-query.aspx

                if (p.IsPrimary == true)
                {
                    base.Photos.Where(x => x.ID == p.ID && x.IsPrimary == true).ToList().ForEach(x =>
                    {
                        x.IsPrimary = false;
                    });
                }

                //now insert
                base.Photos.Add(p);
            }

            SaveChanges();
        }

        public void Update(PhotoDTO pdto)
        {
            foreach (Photo p in pdto.Photos)
            {
                //First, turn off the current IsPrimary for this user.  Code referenced from following url:
                //http://www.dotnetlearners.com/blogs/view/142/update-multiple-records-in-entity-framework-in-a-single-line-by-using-linq-query.aspx

                if (p.IsPrimary == true)
                {
                    base.Photos.Where(x => x.ID == p.ID && x.IsPrimary == true).ToList().ForEach(x =>
                    {
                        x.IsPrimary = false;
                    });
                }

                //Now, the update with new IsPrimary 
                base.Photos.Attach(p);
                base.Entry(p).Property(a => a.IsPrimary).IsModified = true;
            }

            SaveChanges();
        }


        #endregion

        #endregion

    }
   
}
