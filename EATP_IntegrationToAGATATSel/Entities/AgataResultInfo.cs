using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Entities
{
    public class ResultInfo
    {
        public string result_status { get; set; }
        public string transaction_id { get; set; }
        public List<string> error { get; set; }
    }
}
