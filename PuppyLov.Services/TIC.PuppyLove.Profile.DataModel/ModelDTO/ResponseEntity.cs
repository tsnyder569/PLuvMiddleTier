using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Profile.DataModel
{
    public class ResponseEntity
    {
        public Respons Response { get; set; }

        // Responbses
        public ProfileEntity CreateResponseEntityInstance()
        {
            return new ProfileEntity
            {
                ID = Response.ID,
                Text = Response.Responses,
                ResponseTypeID = Response.ResponseTypeID
            };

        }
    }
}
