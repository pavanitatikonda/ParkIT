using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkIT.Models
{
    public class ParkingSlot
    {
        public int ID { get; set; }
        public string SlotNo { get; set; }
        public string InputPin { get; set; }
        public string Status { get; set; }
    }
}
