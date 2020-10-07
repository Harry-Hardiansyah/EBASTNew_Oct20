using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Entities
{
    public class ModuleBBUInfo
    {
        public string slot_number { get; set; }
        public string module_code { get; set; }
        public string module_name { get; set; }
        public string module_brand { get; set; }
        public string module_sn { get; set; }
        public string picture_near { get; set; }
        public string picture_far { get; set; }
        public int ref_id { get; set; }
        public string available_slot { get; set; }
        public string cons_power_bb { get; set; }
    }
}
