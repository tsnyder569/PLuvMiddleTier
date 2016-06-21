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
using System.Collections;
using System.Collections.Specialized;

namespace TIC.PuppyLove.Services.Logic.Match.Data
{
    public class DataManager
    {

        public List<long> ExcludeNoPreferenceFromProfile(List<ProfileEntity> matchProfile)
                                                        //long? responseTypeID,
                                                        //bool byType)
        {

            var response = new List<long>();
            using (var dbEntities = new TICPuppyLoveDbContext())
            {
                
                var profileRequest = new List<ProfileResponse>();
                var profileResponse = new List<ProfileResponse>();

                foreach (ProfileEntity matchResp in matchProfile)
                {
                    ProfileResponse prof = new ProfileResponse
                    {
                        ProfileEntityResponse = matchResp
                    };
                    profileRequest.Add(prof);

                }

                /*
                profileResponse = (byType) ? dbEntities.ExcludeNoPreferencesByType(profileRequest, responseTypeID)
                                           : dbEntities.ExcludeNoPreferences(profileRequest, responseTypeID);
                 */

                profileResponse = dbEntities.ExcludeNoPreferences(profileRequest);

                response = (from resp in profileResponse
                            select resp.ProfileEntityResponse.ResponseTypeID
                            ).Distinct().ToList();
            }


            return response;
        }

        public long? GetResponseTypeIDByType(string responseType)
        {
            long? response = null;
            using (var dbEntities = new TICPuppyLoveDbContext())
            {
                response = dbEntities.ResponseTypeID(responseType);
            }
            return response;
        }

        public List<long> ExcludedMatchingResponseTypes()
        {

            // Hardcoding for now. However, we probably want this table driven
            StringCollection excludeedMatchTyeps = new StringCollection
                                                       {
                                                           "DistanceType"
                                                       };

            List<long> excludedTypes = new List<long>();
            foreach (string type in excludeedMatchTyeps)
            {
                long? excludedType = GetResponseTypeIDByType(type);
                excludedTypes.Add(excludedType.Value);
            }

            return excludedTypes;
        }
        /// <summary>
        /// Static Constructor to load Entity framework required assemblies
        /// </summary>
        static DataManager()
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
