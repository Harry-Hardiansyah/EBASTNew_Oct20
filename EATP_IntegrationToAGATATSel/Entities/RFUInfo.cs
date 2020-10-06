using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Entities
{
    public class RFUInfo
    {
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string rfu_brand { get; set; }        
        public string product_sn { get; set; }        
        public string picture_near { get; set; }
        public string picture_far { get; set; }
        public int rfu_height { get; set; }
        public string leg_position { get; set; }
        public string rfu_location { get; set; }        
        public int ref_id { get; set; }
        public int rru_power { get; set; }
        public int rru_quantity { get; set; }
    }
}
