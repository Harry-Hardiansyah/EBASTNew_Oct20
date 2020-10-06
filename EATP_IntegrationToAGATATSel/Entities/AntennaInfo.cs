using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Entities
{
    public class AntennaInfo
    {
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string product_sn { get; set; }
        public int antenna_heightsect { get; set; }
        public int antenna_azimuthsect { get; set; }
        public int antenna_tiltingsect { get; set; }
        public int antenna_beamwidth { get; set; }
        public string picture_near { get; set; }
        public string picture_far { get; set; }
        public string barcode { get; set; }
        public long ref_id { get; set; }
        public int antenna_etilting { get; set; }
        public string antenna_brand { get; set; }
    }
}
