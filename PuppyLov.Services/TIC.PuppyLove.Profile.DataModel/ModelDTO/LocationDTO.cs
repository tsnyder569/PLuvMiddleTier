using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Location.Data;

namespace TIC.PuppyLove.Profile.DataModel
{
    public class LocationDTO
    {
        public Location location { get; set; }
        public List<Location> locationList { get; set; }
        public LocationData locData { get; set;  }
        public List<LocationData> locDataList { get; set; }
    }
}
