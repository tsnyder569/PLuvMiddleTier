using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Profile.DataModel;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Contracts.Location.Data;

namespace TIC.PuppyLove.Services.Logic.Location.Data
{
    public class DataManager
    {
        #region Constructors
        /// <summary>
        /// Static Constructor to load Entity framework required assemblies
        /// </summary>
        static DataManager()
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        #endregion

        #region AddUpdate

        public Int32 AddProfile(LocationData request)
        {

            Int32 response = -1;

            using (var context = new TICPuppyLoveDbContext())
            {
                using (var dbTran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var oParm = new SqlParameter
                        {
                            ParameterName = "@TotalCount",
                            DbType = DbType.Int32,
                            Direction = ParameterDirection.Output
                        };
                        context.Database.ExecuteSqlCommand(
                            "EXEC SP_AddUpdateLocation @UserID, @Latitude, @Longitude, @Accuracy, @TimeStamp, @TotalCount OUTPUT",
                            new SqlParameter("@UserID", request.UserID),
                            new SqlParameter("@Latitude", request.Latitude),
                            new SqlParameter("@Longitude", request.Longitude),
                            new SqlParameter("@Accuracy", request.Accuracy),
                            new SqlParameter("@TimeStamp", request.Timestamp),
                            oParm
                            );
                        context.SaveChanges();
                        response = Convert.ToInt32(oParm.Value);
                        dbTran.Commit();
                    }

                    catch (Exception ex)
                    {
                        dbTran.Rollback();
                        throw;
                    }
                }
            }

            return response;
        }

        #endregion

        #region GetCurrentLocations
   
        /// <summary>
        /// Gets all current locations via ORM.  Used for getting all locations with no distance calculation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<LocationData> GetCurrentLocations(LocationData request)
        {
            List<LocationData> response = new List<LocationData>();

            using (var dbEntities = new TICPuppyLoveDbContext())
            {
                response = (from loc in dbEntities.Locations
                            //WHERE blah blah blah - Currently we are not filtering based on the request - return all
                            orderby loc.UserID
                            select new LocationData
                            {
                                UserID = loc.UserID,
                                Latitude = loc.Latitude,
                                Longitude = loc.Longitude,
                                Accuracy = loc.Accuracy,
                                Timestamp = loc.TimeStamp
                            }
                                ).ToList();
            }

            return response;
        }

        public List<LocationData> GetCurrentLocationsWithDistance (LocationData request)
        {
            List<LocationData> response = new List<LocationData>();
            
            using (var context = new TICPuppyLoveDbContext())
            {
                var UserID = new SqlParameter("@UserID", request.UserID);
                var Latitude = new SqlParameter("@Latitude", 
                    (request.Latitude > 0 && request.Longitude < 0) ? request.Latitude : SqlDecimal.Null);
                var Longitude = new SqlParameter("@Longitude",
                    (request.Latitude > 0 && request.Longitude < 0) ? request.Longitude : SqlDecimal.Null);

                response = context.Database
                    .SqlQuery<LocationData>("SP_GetAllLocationsWithDistance @UserID, @Latitude, @Longitude",
                    UserID, Latitude, Longitude)
                    .ToList();
            }

            //EF not mapping UserID to the data contract but was able to map it to an alternate name.  
            //Map it back

            response = GetLocBaseProperties(response);

            return response;
        }
        
        #endregion

        #region GetNearestNeighbor

        public List<LocationData> GetNearestNeighbors (NearestNeighborRequest request)
        {
            List<LocationData> response = new List<LocationData>();

            using (var context = new TICPuppyLoveDbContext())
            {
                var Latitude = new SqlParameter("@Latitude", request.Latitude);
                var Longitude = new SqlParameter("@Longitude", request.Longitude);
                var Radius = new SqlParameter("@Radius", request.Radius);

                response = context.Database
                    .SqlQuery<LocationData>("SP_GetNearestNeighbors @Latitude, @Longitude, @Radius",
                    Latitude, Longitude, Radius )
                    .ToList();
            }

            //EF not mapping UserID to the data contract but was able to map it to an alternate name.  
            //Map it back

            response = GetLocBaseProperties(response);


            return response;
        }

        #endregion

        #region Get Location By UserID

        public LocationData GetLocationByUserID (LocationData request)
        {
            LocationData response = new LocationData();
            using (var dbEntities = new TICPuppyLoveDbContext())
            {
                response = (from loc in dbEntities.Locations
                            where loc.UserID == request.UserID
                            select new LocationData
                            {
                                UserID = loc.UserID,
                                Latitude = loc.Latitude,
                                Longitude = loc.Longitude,
                                Accuracy = loc.Accuracy,
                                Timestamp = loc.TimeStamp
                            }
                            ).SingleOrDefault();
            }

            return response;
        }

        #endregion

        #region Remove Location

        public Int32 RemoveLocation (LocationData request)
        {
            Int32 response = -1;

            using (var dbEntities = new TICPuppyLoveDbContext())
            {
                dbEntities.Locations.RemoveRange(
                    dbEntities.Locations.Where(x => x.UserID == request.UserID));
                response = dbEntities.SaveChanges();
            }

            return response;
        }

        #endregion

        #region helpers

        private List<LocationData> GetLocBaseProperties (List<LocationData> response)
        {
            //EF not mapping UserID to the data contract but was able to map it to an alternate name.  
            //Map it back

            foreach (LocationData l in response)
            {
                l.UserID = l.UserID_nn;
            }

            return response;
        }

        #endregion
    
    }
}
