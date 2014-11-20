using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
    public class Location
    {
        //public string LocationName { get; set; }        
        public float Latitude { get; set; }
        public float Longitude { get; set; }


        public bool Equals(Location l)
        {
            if (this.Latitude == l.Latitude & this.Longitude == l.Longitude)
                return true;
            
            
            //return base.Equals(obj);

            return false;
        }
    }
}
