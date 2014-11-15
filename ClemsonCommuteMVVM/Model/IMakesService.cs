using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClemsonCommuteMVVM.Model
{
   public interface IMakesService
    {
       Task<IEnumerable<Make>> Refresh();
    }
}
