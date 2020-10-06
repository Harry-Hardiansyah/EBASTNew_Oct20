using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.BOL;
using ConsoleApplication1.DAL;
using ConsoleApplication1.Entities;
using System.Data;
using RestSharp;
using ConsoleApplication1.Helper;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    public class Program
    {
        static Program prog = new Program();
        public static void Main(string[] args)
        {
            //prog.connectToAGATATsel("");
            //prog.GetATPPending(); 
            prog.GetATPBTSPending();
            //prog.GetATPBSCPending();
        }
        
        public void GetATPBTSPending() {
            string[] connections = new string[3] { StaticConfig.ConnectionTI2G, StaticConfig.ConnectionTI3G, StaticConfig.ConnectionLTE };
            //string[] connections = new string[1] { StaticConfig.ConnectionLTE };
            BOL_ATP_Data bolAtp = new BOL_ATP_Data();

            for(int i = 0; i < connections.Length; i++)
            {
                LogFiles.createLogFile("Connection", connections[i]);
                DataTable GetListPending = bolAtp.GetATPPendingListBTS(connections[i]);
                Console.WriteLine("Start to Push E-ATP Data to Agata.");
                if (GetListPending.Rows.Count > 0)
                {
                    LogFiles.createLogFile("Total data", GetListPending.Rows.Count.ToString());
                    LogFiles.createLogFile("========================================", "========================================");
                    foreach (DataRow drw in GetListPending.Rows)
                    {
                        bool isSucceed = true;
                        string errMessage = string.Empty;
                        Console.WriteLine(Convert.ToString(drw["workpackageid"]));
                        LogFiles.createLogFile("WPID", Convert.ToString(drw["workpackageid"]));
                        SiteInfo siteInf = bolAtp.GetATPPendingBTS(connections[i], Convert.ToInt32(drw["atp_id"]));
                        if (siteInf != null)
                        {
                            Console.WriteLine("Test PONO: " + siteInf.po_number);
                            LogFiles.createLogFile("PONO", siteInf.po_number);

                            try
                            {
                                string sJsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(siteInf);
                                ResultInfo resultInfo = connectToAGATATsel_RestSharp(sJsonObject);
                                if (resultInfo != null)
                                {
                                    if (resultInfo.result_status.ToLower() == "failed")
                                    {
                                        LogFiles.createLogFile("Status", "failed");
                                        Console.WriteLine("Response Error from AGATA Server : ");
                                        errMessage = "";
                                        isSucceed = false;
                                        foreach (string err in resultInfo.error)
                                        {
                                            LogFiles.createLogFile("Error", err);
                                            Console.WriteLine(err);
                                            errMessage += err + ";";
                                        }
                                    }
                                    else LogFiles.createLogFile("Status", "Success");
                                }
                                else LogFiles.createLogFile("Status", "Failed");
                            }
                            catch (Exception ex)
                            {
                                isSucceed = false;
                                errMessage = ex.Message.ToString();
                                LogFiles.createLogFile("Exception", ex.Message);
                            }

                            if (isSucceed)
                            {
                                bolAtp.ATPIntegrationStatus_U(connections[i], Convert.ToInt32(drw["atp_id"]), StaticConfig.IntegrationStatus_Succeed, string.Empty);
                                Console.WriteLine("Finish & Success for " + connections[i]);
                            }
                            else
                            {
                                bolAtp.ATPIntegrationStatus_U(connections[i], Convert.ToInt32(drw["atp_id"]), StaticConfig.IntegrationStatus_Error, errMessage);
                                Console.WriteLine("Finish & Failed for " + connections[i]);
                            }
                            LogFiles.createLogFile("========================================", "========================================");
                        }
                        else
                            LogFiles.createLogFile("Status","Site info null");
                    }
                }
                else
                {
                    LogFiles.createLogFile("Total data", "0");
                    LogFiles.createLogFile("========================================", "========================================");
                }
            }
            Console.WriteLine("Finish for Pushing Data to Agata.");
        }

        public ResultInfo connectToAGATATsel_RestSharp(string jsonObject)
        {
            jsonObject = Regex.Replace(jsonObject, @"\bnull\b", "\"not applicable\"", RegexOptions.IgnoreCase);
            //jsonObject = jsonObject.Replace("null", "\"not applicable\"").Replace("\\r\\n", " ");
            //jsonObject = jsonObject.Replace("\\r\\n", " ");
            LogFiles.createLogFile("Request", jsonObject);
            ServicePointManager.MaxServicePointIdleTime = 1000;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback(delegate { return true; });
            
            var client = new RestClient(ConfigurationManager.AppSettings["apiurldemo"]);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("content-type", "application/x-www-form-urlencoded");
            _request.AddParameter("key", ConfigurationManager.AppSettings["apitokenkeydemo"]);
            _request.AddParameter("version", "1");
            _request.AddParameter("data", jsonObject);
            IRestResponse response = client.Execute(_request);
            Console.WriteLine(response.Content);
            LogFiles.createLogFile("Response", response.Content);
            LogFiles.createLogFile("Error Message When reachin web API", response.ErrorMessage);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResultInfo>(response.Content);
        }

        #region OLD
//        DataTable GetListPending = bolAtp.GetATPPendingListBTS(StaticConfig.ConnectionTI2G);
//        Console.WriteLine("Start to Push E-ATP Data to Agata.");           
//            if (GetListPending.Rows.Count > 0) {
//                foreach (DataRow drw in GetListPending.Rows) {
//                    bool isSucceed = true;
//        string errMessage = string.Empty;
//        Console.WriteLine(Convert.ToString(drw["workpackageid"]));
//                    SiteInfo siteInf = bolAtp.GetATPPendingBTS(StaticConfig.ConnectionTI2G, Convert.ToInt32(drw["atp_id"]));
//        Console.WriteLine("Test PONO: " + siteInf.po_number);
                    
//                    try
//                    {
//                        string sJsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(siteInf);
//        ResultInfo resultInfo = connectToAGATATsel_RestSharp(sJsonObject);
//                        if (resultInfo.result_status.ToLower() == "failed")
//                        {
//                            Console.WriteLine("Response Error from AGATA Server : ");
//                            errMessage = "";
//                            isSucceed = false;
//                            foreach (string err in resultInfo.error)
//                            {
//                                Console.WriteLine(err);
//                                errMessage += err + ";";
//                            }
//}
//                    }
//                    catch (Exception ex) {
//                        isSucceed = false;
//                        errMessage = ex.Message.ToString();
//                    }

//                    if (isSucceed)
//                    {
//                        bolAtp.ATPIntegrationStatus_U(StaticConfig.ConnectionTI2G, Convert.ToInt32(drw["atp_id"]), StaticConfig.IntegrationStatus_Succeed, string.Empty);
//                        Console.WriteLine("Finish & Success for " + StaticConfig.ConnectionTI2G);
//                    }
//                    else
//                    {
//                        bolAtp.ATPIntegrationStatus_U(StaticConfig.ConnectionTI2G, Convert.ToInt32(drw["atp_id"]), StaticConfig.IntegrationStatus_Error, errMessage);
//                        Console.WriteLine("Finish & Failed for " + StaticConfig.ConnectionTI2G);
//                    }
//                }
//            }
//            Console.WriteLine("Finish for Pushing Data to Agata.");
//            Console.ReadKey();
        #endregion
    }
}
