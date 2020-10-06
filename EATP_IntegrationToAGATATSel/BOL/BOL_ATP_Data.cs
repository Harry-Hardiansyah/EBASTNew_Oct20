using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleApplication1.DAL;
using System.Data;
using System.Configuration;

namespace ConsoleApplication1.BOL
{
    public class BOL_ATP_Data
    {
        //public BOL_ATP_Data() {
        //    using (Image image = Image.FromFile(Path))
        //    {
        //        using (MemoryStream m = new MemoryStream())
        //        {
        //            image.Save(m, image.RawFormat);
        //            byte[] imageBytes = m.ToArray();

        //            // Convert byte[] to Base64 String
        //            string base64String = Convert.ToBase64String(imageBytes);
        //            return base64String;
        //        }
        //    }
        //}
        ATP_Data atpData = new ATP_Data();
        public DataTable GetATPPendingListBTS(string dbconfigscope) {
            return atpData.GetATPPendingListBTS(dbconfigscope);
        }

        public Entities.SiteInfo GetATPPendingBTS(string dbconfigscope,Int32 atpid)
        {            
            return atpData.GetATPPendingBTS(dbconfigscope,atpid);
        }

        public bool ATPIntegrationStatus_U(string dbconfigscope, Int32 atpid, string integrationStatus, string remarks) {
            return atpData.ATPIntegrationStatus_U(dbconfigscope, atpid, integrationStatus, remarks);
        }
    }

}
