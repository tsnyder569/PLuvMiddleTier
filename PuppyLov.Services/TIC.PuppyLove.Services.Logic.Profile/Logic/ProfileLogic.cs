using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Profile.Service;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Logic.Profile.Data.Logic;
using Ventera.VAF.Core.Helpers;
using System.Reflection;
using System.ServiceModel.Activation;


namespace TIC.PuppyLove.Services.Logic.Profile
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)] 

    public class ProfileLogic : IProfileService
    {
        public CommonResponse CreateProfile(UserProfileData request)
        {
            LogHelper.LogInformation(string.Concat(Resource.CreateProfile, MethodBase.GetCurrentMethod().Name));
            var response = new CommonResponse { Status = true, ReturnMessage = "Create Profile Successful" };
            try
            {
                if (request != null)
                {
                    LogHelper.LogInformation(string.Concat(request.UserData.FirstName, " ", request.UserData.LastName));

                    var dataManager = new DataManager();
                    dataManager.AddProfile(request);
                
                }
                else
                {
                    LogHelper.LogInformation("request object is null");
                    response.Status = false;
                    response.ReturnMessage = "You miserable little failure. the operation failed the object is null.";
                }

            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);
                response.Status = false;
                response.ReturnMessage = "You miserable little failure. the operation failed.";
            }

            return response;
        }

        public UserProfileData GetProfileData(UserProfileRequest request)
        {
            LogHelper.LogInformation(string.Concat(Resource.GetProfileData, MethodBase.GetCurrentMethod().Name));
            var response = new UserProfileData();
            try
            {
                if (request != null)
                {
                    var dataManager = new DataManager();

                    response = dataManager.FetchProfile(request);
                    // No user found
                    if (response.UserData == null)
                    {
                        LogHelper.LogInformation("user response object is null. User does not exist");
                        response.Status = false;
                        response.ReturnMessage = "You miserable little failure. the user does not exist.";
                    }
                    else
                    {
                        if (response.UserData.UserID.HasValue)
                        {
                            var pphoto = GetPrimaryProfilePhoto(
                                new ProfilePhoto { UserID = response.UserData.UserID.Value });
                            response.UserData.Photo = pphoto.PhotoImage;
                        }
                    }

                } 
                else
                {
                    LogHelper.LogInformation("request object is null you miserable little failure");
                    response.Status = false;
                    response.ReturnMessage = "You miserable little failure. the operation failed the object is null.";
                }

            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);
                // Additional EF info if it is there
                if (exception.InnerException != null)
                {
                    if (exception.InnerException.InnerException != null)
                    {
                        if (exception.InnerException.InnerException.Message != null)
                        {
                            string theEx = exception.InnerException.InnerException.Message;
                            LogHelper.LogInformation("Exception with GetProfileData: " + theEx);
                        }
                    }
                }
            }

            response.Status = true;
            response.ReturnMessage = "Get Profile Data Succeeded.";

            return response;
        }

        public ProfileQuestions GetQuestions(ProfileQuestionRequest request)
        {
            LogHelper.LogInformation(string.Concat(Resource.GetQuestions, MethodBase.GetCurrentMethod().Name));
            var response = new ProfileQuestions();
            try
            {
                if (request != null)
                {
                    var dataManager = new DataManager();

                    response = dataManager.FetchQuestions(request);
                    
                    if (response == null)
                    {
                        LogHelper.LogInformation("response object is null. No questions exist");
                        response.Status = false;
                        response.ReturnMessage = "You miserable little failure. The questions do not exist.";
                    }

                }
                else
                {
                    LogHelper.LogInformation("request object is null you miserable little failure");
                    response.Status = false;
                    response.ReturnMessage = "You miserable little failure. the operation failed the object is null.";
                }

            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);

                // Additional EF info if it is there
                if (exception.InnerException != null)
                {
                    if (exception.InnerException.InnerException != null)
                    {
                        if (exception.InnerException.InnerException.Message != null)
                        {
                            string theEx = exception.InnerException.InnerException.Message;
                            LogHelper.LogInformation("Exception with GetQuestions: " + theEx);
                        }
                    }
                }
            }

            response.Status = true;
            response.ReturnMessage = "Get Questions and Responses Succeeded.";

            return response;
        }

        public LoginResponse Login(UserProfileRequest request)
        {
            LogHelper.LogInformation(string.Concat(Resource.Login, MethodBase.GetCurrentMethod().Name));
            var response = new LoginResponse { Status = true, ReturnMessage = "giddy-up" };
            try
            {
                if (request != null)
                {
                    var dataManager = new DataManager();
                    response = dataManager.LoginToProfile(request);
                }
                else
                {
                    LogHelper.LogInformation("request object is null you miserable little failure");
                    response.Status = false;
                    response.ReturnMessage = "You miserable little failure. the operation failed with null object.";
                }
            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);

                // Additional EF info if it is there
                if (exception.InnerException != null)
                {
                    if (exception.InnerException.InnerException != null)
                    {
                        if (exception.InnerException.InnerException.Message != null)
                        {
                            string theEx = exception.InnerException.InnerException.Message;
                            LogHelper.LogInformation("Exception with Login: " + theEx);
                        }
                    }
                }

                response.Status = false;
                response.ReturnMessage = "You miserable little failure. the operation failed.";
            }

            return response;
                                    

        }

        public CommonResponse UpdateProfile(UserProfileData request)
        {
            LogHelper.LogInformation(string.Concat(Resource.UpdateProfile, MethodBase.GetCurrentMethod().Name));
            var response = new CommonResponse { Status = true, ReturnMessage = "giddyup" };

            try
            {
                if (request != null)
                {
                    var dataManager = new DataManager();
                    dataManager.ModifyProfile(request);
                }
                else
                {
                    LogHelper.LogInformation("request object is null you miserable little failure");
                    response.Status = false;
                    response.ReturnMessage = "You miserable little failure. the operation failed with null object.";
                }

            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);

                // Additional EF info if it is there
                if (exception.InnerException != null)
                {
                    if (exception.InnerException.InnerException != null)
                    {
                        if (exception.InnerException.InnerException.Message != null)
                        {
                            string theEx = exception.InnerException.InnerException.Message;
                            LogHelper.LogInformation("Exception with UpdateProfile: " + theEx);
                        }
                    }
                }

                response.Status = false;
                response.ReturnMessage = "You miserable little failure. the operation failed.";
            }

            return response;
        }

        public ChoiceProfile GetMatchProfileChoiceStatus(ProfileChoiceRequest request)
        {

            LogHelper.LogInformation(string.Concat(Resource.GetMatchProfileChoiceStatus, MethodBase.GetCurrentMethod().Name));
            var response = new ChoiceProfile();

            try
            {
                if (request != null)
                {
                    var dataManager = new DataManager();
                    response = dataManager.GetProfileChoiceStatus(request);
                }
                else
                {
                    LogHelper.LogInformation("request object is null you miserable little failure");
                }

            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);

                // Additional EF info if it is there
                if (exception.InnerException != null)
                {
                    if (exception.InnerException.InnerException != null)
                    {
                        if (exception.InnerException.InnerException.Message != null)
                        {
                            string theEx = exception.InnerException.InnerException.Message;
                            LogHelper.LogInformation("Exception with UpdateProfile: " + theEx);
                        }
                    }
                }
            }

            return response;
        }

        public CommonResponse ValidateLoginUserName(UserProfileRequest request)
        {
            LogHelper.LogInformation(string.Concat(Resource.ValidateLoginUserName, MethodBase.GetCurrentMethod().Name));
            var response = new CommonResponse { Status = true, ReturnMessage = "Username is valid" };

            try
            {
                if (request != null)
                {
                    var dataManager = new DataManager();
                    bool userNameExists = dataManager.IsUserNameValid(request.UserName);
                    if (userNameExists)
                    {
                        response.Status = false;
                        response.ReturnMessage = "Username already exists. Usernam is invalid";
                    }
                }
                else
                {
                    LogHelper.LogInformation("request object is null you miserable little failure");
                    response.Status = false;
                    response.ReturnMessage = "You miserable little failure. the operation failed with null object.";
                }

            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);

                // Additional EF info if it is there
                if (exception.InnerException != null)
                {
                    if (exception.InnerException.InnerException != null)
                    {
                        if (exception.InnerException.InnerException.Message != null)
                        {
                            string theEx = exception.InnerException.InnerException.Message;
                            LogHelper.LogInformation("Exception with UpdateProfile: " + theEx);
                        }
                    }
                }

                response.Status = false;
                response.ReturnMessage = "You miserable little failure. the operation failed.";
            }

            return response;
        }

        #region photo processing

        #region GET

        public List<ProfilePhotoType> GetProfilePhotoTypes()
        {
            LogHelper.LogInformation(string.Concat
                (Resource.GetPhotoTypes, MethodBase.GetCurrentMethod().Name));
            var dmgr = new DataManager();
            var response = new List<ProfilePhotoType>();

            try
            {
                response = dmgr.GetProfilePhotoTypes();
            }

            catch (Exception ex)
            {
                LogHelper.LogError(ex,
                    string.Format(Resource.ErrorInOperation, MethodBase.GetCurrentMethod().Name));
                // Additional EF info if it is there
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    !string.IsNullOrWhiteSpace(ex.InnerException.InnerException.Message))
                {
                    LogHelper.LogWarning(string.Concat(Resource.InnerExceptionReceived,
                                ex.InnerException.InnerException.Message));
                }

            }
            finally
            {
                dmgr = null;
                LogHelper.LogInformation(string.Concat(Resource.EndOfOperation,
                    MethodBase.GetCurrentMethod().Name));
            }


            return response;
        }

        public ProfilePhoto GetPrimaryProfilePhoto(ProfilePhoto request)
        {
            LogHelper.LogInformation(string.Concat
                (Resource.GetPrimaryPhoto, MethodBase.GetCurrentMethod().Name));
            var dmgr = new DataManager();
            var response = new ProfilePhoto();

            try
            {
                if (request.UserID > 0)
                {
                    EchoProfilePhotoSearchCriteria(request);
                    response = dmgr.GetPrimaryProfilePhoto(request);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex,
                    string.Format(Resource.ErrorInOperation, MethodBase.GetCurrentMethod().Name));
                // Additional EF info if it is there
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    !string.IsNullOrWhiteSpace(ex.InnerException.InnerException.Message))
                {
                    LogHelper.LogWarning(string.Concat(Resource.InnerExceptionReceived,
                                ex.InnerException.InnerException.Message));
                }

            }
            finally
            {
                dmgr = null;
                LogHelper.LogInformation(string.Concat(Resource.EndOfOperation,
                    MethodBase.GetCurrentMethod().Name));
            }

            return response;
        }

        public ProfilePhotos GetPhotosByUserID(ProfilePhoto request)
        {
            LogHelper.LogInformation(string.Concat
                (Resource.GetPhotosByUserID, MethodBase.GetCurrentMethod().Name));
            var dmgr = new DataManager();
            var response = new ProfilePhotos { ProfilePhotoList = new List<ProfilePhoto>() };

            try
            {
                if (request.UserID > 0)
                {
                    EchoProfilePhotoSearchCriteria(request);
                    response.ProfilePhotoList = dmgr.GetProfilePhotosById(request);
                }
                else
                {
                    LogHelper.LogWarning(Resource.ValidateUserIDMustBeGreaterThanZero);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex,
                    string.Format(Resource.ErrorInOperation, MethodBase.GetCurrentMethod().Name));
                // Additional EF info if it is there
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    !string.IsNullOrWhiteSpace(ex.InnerException.InnerException.Message))
                {
                    LogHelper.LogWarning(string.Concat(Resource.InnerExceptionReceived,
                                ex.InnerException.InnerException.Message));
                }

            }
            finally
            {
                dmgr = null;

                if (response != null && response.ProfilePhotoList != null)
                {
                    LogHelper.LogInformation(
                        string.Format(Resource.RowsReturnedFromPhotoSearch,
                        response.ProfilePhotoList.Count.ToString()));
                }

                LogHelper.LogInformation(string.Concat(Resource.EndOfOperation,
                    MethodBase.GetCurrentMethod().Name));
            }


            return response;
        }

        private void EchoProfilePhotoSearchCriteria(ProfilePhoto request)
        {
            LogHelper.LogInformation(
                        string.Format(Resource.EchoPhotoSearchCriteria,
                        request.UserID.ToString())
                        );
        }

        #endregion

        #endregion
    }
}
