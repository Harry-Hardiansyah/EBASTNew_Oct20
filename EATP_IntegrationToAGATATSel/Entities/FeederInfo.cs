using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Entities
{
    public class FeederInfo
    {
        public string feeder_type { get; set; }
        public int feeder_length { get; set; }
        public int feeder_quantity { get; set; }
        public string picture_near { get; set; }
        public string picture_far { get; set; }
        public string barcode { get; set; }
        public long ref_id { get; set; }
    }
}
