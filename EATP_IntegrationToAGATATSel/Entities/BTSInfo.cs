using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1.Entities
{
    public class BTSInfo
    {
        public string vendor_rbsnodeb { get; set; }
        public string site_status { get; set; }
        public string cab_type { get; set; }
        public string connect_bscrnc { get; set; }
        public string ne_id { get; set; }
        public string band { get; set; }
        public int number_sector { get; set; }
        public string bts_configuration { get; set; }
        public string btsnodeb_type { get; set; }
        public int capacity { get; set; }
        public string bts_installationtype { get; set; }
        public string rack_sn { get; set; }
        public string rack_productcode { get; set; }
        public string picture_near { get; set; }
        public string picture_far { get; set; }
        public string barcode { get; set; }
        public long ref_id { get; set; }

        // add 
        public DateTime date_on_air { get; set; }
        public string ne_name { get; set; }
        public string bandtype_code { get; set; }
        public string band_type { get; set; }
        public string bscrnc_name { get; set; }
        public string bts_cabinet_type { get; set; }
        public string actual_config { get; set; }
        public int bandwidth { get; set; }
        public int license_power { get; set; }
        public string baseband_type { get; set; }
        public int baseband_number { get; set; }
        public string rru_type { get; set; }
        public int rru_power { get; set; }
        public int rru_quantity { get; set; }
        public int dcpdb_quantity { get; set; }
        public string dcpdb_type { get; set; }
        public string antenna_gps { get; set; }
    }
}
