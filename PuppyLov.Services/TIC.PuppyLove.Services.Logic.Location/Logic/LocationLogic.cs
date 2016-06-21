using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventera.VAF.Core.Helpers;
using Ventera.VAF.Core.Configuration;
using System.Reflection;
using System.ServiceModel.Activation;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Contracts.Location.Data;
using TIC.PuppyLove.Services.Contracts.Location.Service;
using TIC.PuppyLove.Profile.DataModel;
using TIC.PuppyLove.Services.Logic.Location.Data;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using TIC.PuppyLove.Helpers;

namespace TIC.PuppyLove.Services.Logic.Location
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)] 
    public class LocationLogic : ILocationService
    {
        #region UpdateLocation

        public LocationResponse UpdateLocation(LocationData request)
        {
            LocationResponse response = new LocationResponse
            {
                Status = true, 
                Locations = new List<LocationData>(),
                ReturnMessage = LocationResource.UpdateSuccessful 
            };
            DataManager dmgr = null;

            try
            {
                LogHelper.LogInformation(string.Concat(LocationResource.UpdateOp, MethodBase.GetCurrentMethod().Name));
                
                if (request != null && request.UserID.HasValue)
                {
                    LogHelper.LogInformation(string.Format(LocationResource.EchoLocationData,
                    request.Latitude.ToString(), request.Longitude.ToString(),
                    (request.UserID.HasValue) ? request.UserID.Value.ToString() : string.Empty,
                        request.Accuracy.ToString(), request.Timestamp.ToString()
                        ));

                    dmgr = new DataManager();

                    //If @@rowcount < 1 nothing got added or updated
                    if (dmgr.AddProfile(request) < 1)
                    {
                        response.Status = false;
                        response.ReturnMessage = LocationResource.NoRecordsInsertedOrUpdated;
                    }
                    else
                    {
                        //send back the updated location data if the merge was successful
                        LogHelper.LogInformation(LocationResource.UpdateWorkedGetLocRecord);
                        response.Locations.Add(dmgr.GetLocationByUserID(request));
                    }
                }
                else
                {
                    response.Status = false;
                    response.ReturnMessage = LocationResource.BadRequestObject;
                    LogHelper.LogWarning(response.ReturnMessage);
                }

            }

            catch (Exception tim)
            {
                if (tim is FormattedEntityValidationException)
                {
                    LogHelper.LogWarning(string.Format(LocationResource.EF_ErrorInOperation,
                        MethodBase.GetCurrentMethod().Name, GetInnerMessage(tim)));
                }
                else
                {
                    LogHelper.LogError(tim,
                    string.Format(LocationResource.ErrorInOperation, MethodBase.GetCurrentMethod().Name));
                }
              
                response.Status = false;
                response.ReturnMessage = LocationResource.UpdateFailed;
            }

            finally
            {
                dmgr = null;
                LogHelper.LogInformation(string.Concat(LocationResource.EndOfOperation, MethodBase.GetCurrentMethod().Name));
            }

            EchoResponseStatus(response);
            return response;
        }

        #endregion

        #region RemoveLocation

        public CommonResponse RemoveLocation(LocationData request)
        {
            DataManager dmgr = null;
            CommonResponse response = new CommonResponse { Status = true,
                ReturnMessage = LocationResource.RemoveSuccessful };

            try
            {
                LogHelper.LogInformation(string.Concat(LocationResource.RemoveOp, MethodBase.GetCurrentMethod().Name));

                if (request != null && request.UserID.HasValue)
                {
                    LogHelper.LogInformation(string.Format(LocationResource.EchoLocationData,
                        request.Latitude.ToString(), request.Longitude.ToString(), request.UserID.ToString()));
                    dmgr = new DataManager();

                    if (dmgr.RemoveLocation(request) < 1)
                    {
                        response.Status = false;
                        response.ReturnMessage = LocationResource.NoRecordsDeleted;
                    }
                }
                else
                {
                    response.Status = false;
                    response.ReturnMessage = LocationResource.BadRequestObject;
                    LogHelper.LogWarning(response.ReturnMessage);
                }
            }

            catch (Exception tim)
            {
                if (tim is FormattedEntityValidationException)
                {
                    LogHelper.LogWarning(string.Format(LocationResource.EF_ErrorInOperation,
                        MethodBase.GetCurrentMethod().Name, GetInnerMessage(tim)));
                }
                else
                {
                    LogHelper.LogError(tim,
                    string.Format(LocationResource.ErrorInOperation, MethodBase.GetCurrentMethod().Name));
                }

                response.Status = false;
                response.ReturnMessage = LocationResource.RemoveFailed;
            }

            finally
            {
                dmgr = null;
                LogHelper.LogInformation(string.Concat(LocationResource.EndOfOperation, MethodBase.GetCurrentMethod().Name));
            }

            EchoResponseStatus(response);
            return response;
        }

        #endregion

        #region GetCurrentLocations

        public LocationResponse GetCurrentLocations(LocationData request)
        {
            DataManager dmgr = null;
            LocationResponse response = new LocationResponse { Status = true, 
                ReturnMessage = LocationResource.GetCurrentLocationsSuccess, Locations = new List<LocationData>() };

            try
            {
                LogHelper.LogInformation(string.Concat(LocationResource.GetCurrentLocationsOp, MethodBase.GetCurrentMethod().Name));
                
                if (request != null &&
                    (request.UserID.HasValue ||
                    (request.Longitude != 0 && request.Latitude != 0)))
                {
                    LogHelper.LogInformation(string.Format(LocationResource.EchoLocationData,
                        request.Latitude.ToString(), request.Longitude.ToString(), 
                        (request.UserID.HasValue) ? request.UserID.Value.ToString() : string.Empty,
                        request.Accuracy.ToString(), request.Timestamp.ToString()
                        ));

                    dmgr = new DataManager();

                    if (!request.UserID.HasValue)
                    {
                        response.ReturnMessage = LocationResource.NoUserIdSupplied;
                        LogHelper.LogInformation(response.ReturnMessage);
                    }

                    response.Locations = (request.UserID.HasValue) ? dmgr.GetCurrentLocationsWithDistance(request)
                        : dmgr.GetCurrentLocations(request);
                }
                else
                {
                    response.Status = false;
                    response.ReturnMessage = LocationResource.BadRequestObject;
                    LogHelper.LogWarning(response.ReturnMessage);
                }
            }

            catch (Exception tim)
            {
                if (tim is FormattedEntityValidationException)
                {
                    LogHelper.LogWarning(string.Format(LocationResource.EF_ErrorInOperation,
                        MethodBase.GetCurrentMethod().Name, GetInnerMessage(tim)));
                }
                else
                {
                    LogHelper.LogError(tim,
                    string.Format(LocationResource.ErrorInOperation, MethodBase.GetCurrentMethod().Name));
                }

                response.Status = false;
                response.ReturnMessage = LocationResource.GetCurrentLocationsFailed;
            }

            finally
            {
                dmgr = null;
                LogHelper.LogInformation(string.Concat(LocationResource.EndOfOperation, MethodBase.GetCurrentMethod().Name));
            }

            EchoResponseStatus(response);

            //Filter out the request object UserID from the results for Ryan because I am such a good guy
            return FilterOutCurrentUser(request, response);
        }

        #endregion

        #region Nearest Neighbor

        public LocationResponse GetNearestNeighbors(NearestNeighborRequest request)
        {
            DataManager dmgr = new DataManager();
            LocationResponse response = new LocationResponse { Status = true, ReturnMessage = string.Empty, Locations = new List<LocationData>() };

            try
            {
                LogHelper.LogInformation(string.Concat(LocationResource.GetNearestNeighborsOp, MethodBase.GetCurrentMethod().Name));

                if (request != null &&
                    (request.UserID.HasValue || 
                    (request.Longitude != 0 && request.Latitude != 0)))
                {
                    LogHelper.LogInformation(string.Format(LocationResource.EchoNearestNeighborRequest,
                        request.Latitude.ToString(), request.Longitude.ToString(),
                        request.Radius.ToString(),
                        (request.UserID.HasValue) ? request.UserID.Value.ToString() : string.Empty
                        ));

                    //If a user id exists in the request, lookup the lat long from the db
                    if (request.UserID.HasValue)
                    {
                        LogHelper.LogInformation(LocationResource.UserIdFound);
                        var loc = dmgr.GetLocationByUserID(new LocationData { UserID = request.UserID });

                        //if the user id is not found in the database, the LocationData object will be null
                        //if found, use for nearest neighbor lookup
                        if (loc != null)
                        {
                            request.Latitude = loc.Latitude;
                            request.Longitude = loc.Longitude;
                        }
                        else
                        {
                            response.Status = false;
                            response.ReturnMessage = LocationResource.LocationLookupNotFound;
                            LogHelper.LogWarning(response.ReturnMessage);
                        }
                    }

                    //This will only be false if UserID passed from caller did not exist in the
                    //location table
                    if (response.Status)
                    {
                        response.Locations = dmgr.GetNearestNeighbors(request);
                        response.ReturnMessage = LocationResource.GetNearestNeighborsSuccess;
                    }   
                }
                else
                {
                    response.Status = false;
                    response.ReturnMessage = LocationResource.BadRequestObject;
                    LogHelper.LogWarning(response.ReturnMessage);
                }
            }
            catch (Exception tim)
            {
                if (tim is FormattedEntityValidationException)
                {
                    LogHelper.LogWarning(string.Format(LocationResource.EF_ErrorInOperation,
                        MethodBase.GetCurrentMethod().Name, GetInnerMessage(tim)));
                }
                else
                {
                    LogHelper.LogError(tim,
                    string.Format(LocationResource.ErrorInOperation, MethodBase.GetCurrentMethod().Name));
                }

                response.Status = false;
                response.ReturnMessage = LocationResource.GetNearestNeighborsFailed;
            }

            finally
            {
                dmgr = null;
                LogHelper.LogInformation(string.Concat(LocationResource.EndOfOperation, MethodBase.GetCurrentMethod().Name));
            }

            EchoResponseStatus(response);

            //Filter out the request object UserID from the results for Ryan because I am such a good guy
            return (request.UserID.HasValue) ? FilterOutCurrentUser(request, response) : response;
        }

        #endregion

        #region helpers

        private string GetInnerMessage (Exception tim)
        {
            return (tim.InnerException != null && tim.InnerException.Message != null) ?
                tim.InnerException.Message : LocationResource.InnerMessageNotFound;
        }

        private void EchoResponseStatus (CommonResponse cmnresp)
        {
            LogHelper.LogInformation(string.Format(
                LocationResource.EchoResponseObj,
                cmnresp.Status.ToString(), cmnresp.ReturnMessage));
        }

        //make public so this is available to the test project
        public UserData GetUserData (LocationData loc)
        {
            LogHelper.LogInformation(string.Concat(
                LocationResource.GetUserDataOp, MethodBase.GetCurrentMethod().Name));

            UserProfileRequest request = new UserProfileRequest 
            { 
                ProfileAttributeType = ProfileAttributeTypeEnum.UserData,
                UserID = loc.UserID };

            var json = new JsonHelper();
            var oper = VAFConfiguration.GetAppSettingValue(LocationResource.GetProfileDataOp);
            var url = VAFConfiguration.GetAppSettingValue(LocationResource.ProfileServiceUrl);
            var fullUrl = new StringBuilder().AppendFormat("{0}/{1}", url, oper);
            UserProfileData response =
                json.MakePostRequest<UserProfileRequest, UserProfileData>(fullUrl.ToString(), request);

            LogHelper.LogInformation(string.Concat(
                LocationResource.EndOfOperation, MethodBase.GetCurrentMethod().Name));

            return (response != null && response.Status && response.UserData != null) ? response.UserData : new UserData();
        }

        private LocationResponse FilterOutCurrentUser(LocationData request, LocationResponse locdata)
        {
            if (request.UserID.HasValue)
            {
                locdata.Locations =
                    (from l in locdata.Locations
                    where l.UserID.HasValue &&
                    request.UserID.HasValue &&
                    l.UserID.Value != request.UserID
                    select l).ToList();
            }
            
            return locdata;
        }

        #endregion helpers    
    }
}
