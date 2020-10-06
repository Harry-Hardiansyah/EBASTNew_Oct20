using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Entities
{
    public class BSCRNCInfo
    {
        public string vendor_bscrnc { get; set; }
        public string bscrnc_name { get; set; }
        public string project_id { get; set; }
        public string bscrnc_type { get; set; }
        public string connect_mscsgsn { get; set; }
        public string rack_sn { get; set; }
        public string rack_productcode { get; set; }
        public string product_sn { get; set; }
        public string product_code { get; set; }
        public string picture_near { get; set; }
        public string picture_far { get; set; }
        public string barcode { get; set; }
        public long ref_id { get; set; }
    }
}
