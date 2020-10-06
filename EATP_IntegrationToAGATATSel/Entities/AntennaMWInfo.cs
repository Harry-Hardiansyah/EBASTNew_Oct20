using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Entities
{
    public class AntennaMWInfo
    {
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string antennamw_brand { get; set; }
        public decimal antennamw_diameter { get; set; }
        public int antennamw_height { get; set; }
        public string product_sn { get; set; }
        public string leg_position { get; set; }
        public string picture_near { get; set; }
        public string picture_far { get; set; }
        public int antennamw_azimuth { get; set; }
        public int ref_id { get; set; }
        public List<RadioMWInfo> radiomw { get; set; }
    }
}
