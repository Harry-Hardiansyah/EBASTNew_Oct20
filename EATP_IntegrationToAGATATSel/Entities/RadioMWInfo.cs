using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Entities
{
    public class RadioMWInfo
    {
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string radiomw_brand { get; set; }
        public int radiomw_height { get; set; }
        public string product_sn { get; set; }
        public string picture_near { get; set; }
        public string picture_far { get; set; }
        public int ref_id { get; set; }
        public List<TRMConfigInfo> trm_config { get; set; }
    }
}
