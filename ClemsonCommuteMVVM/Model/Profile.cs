using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
    public class Profile
    {
        public int ProfileID { get; set; }
        public int UserId { get; set; }
        public string ProfileName { get; set; }
        public int VehicleYear { get; set; }
        public string VehicleMake { get; set; }
        public string VehModel { get; set; }


    }
}
