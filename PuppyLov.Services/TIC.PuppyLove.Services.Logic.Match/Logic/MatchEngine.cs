using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;
using TIC.PuppyLove.Services.Contracts.Profile.Data;

namespace TIC.PuppyLove.Services.Logic.Match
{
    public class MatchEngine
    {
        // Specify any types to exclude from the match
        public List<long> ExcludedMatchList { get; set; }
        // Specify only these types to match
        public List<long> JoinOnMatchList { get; set; }

        private bool MatchProfile(List<ProfileEntity> matchProfile,
                                  List<ProfileEntity> userProfile,
                                  long? TypeID)
        {
            bool match = false;

            // Match profile response Ids to determone a match
            var resultingMatchResp = matchProfile.Join(userProfile,
                                            cw => cw.Id,
                                            ct => ct.Id,

                                            (cw, ct) => new ProfileEntity
                                            {
                                                Id = cw.Id,
                                                ResponseTypeID = cw.ResponseTypeID

                                            }
                                            )
                                            .Where(cwct => cwct.ResponseTypeID == TypeID).ToList();


            // We found at least one match for this response type 
            match = (resultingMatchResp.Count() > 0) ? true : false;

            return match;

        }

        public bool MatchProfileByResponseTypes(List<ProfileEntity> matchProfile,
                                                List<ProfileEntity> userProfile,
                                                List<long> matchOnResponseTypes)
        {

            // We need to find at least one match for each response type to be a match
            bool match = true;
           
            // exlude from match list based on the exlusion list
            var matchList = matchOnResponseTypes.Except(ExcludedMatchList);

            // Take resulting list and join on join list if there is one. 
            if (JoinOnMatchList != null && JoinOnMatchList.Count() > 0)
            {
                matchList = from matchObject in matchList
                            join includeObject in JoinOnMatchList on matchObject equals includeObject
                            select matchObject;
            }

            foreach (long relationshipType in matchList)
            {
                match = MatchProfile(matchProfile,
                                     userProfile,
                                     relationshipType);
                if (!match)
                    break;
            }

            return match;    
        }


    }
}
