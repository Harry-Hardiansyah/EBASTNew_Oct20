using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using ConsoleApplication1.Entities;
using ConsoleApplication1.Helper;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleApplication1.DAL
{
    public class ATP_Data : DBConfiguration
    {
        decimal maxSizeImg = 500;

        public DataTable GetATPPendingListBTS(string dbconfigscope)
        {
            DataTable dtResult = new DataTable();
            Command = new SqlCommand("uspATP_GetIntegrationPendingList_BTS", GetConnectionType(dbconfigscope));
            Command.CommandType = CommandType.StoredProcedure;
            try
            {
                GetConnectionType(dbconfigscope).Open();
                DataReader = Command.ExecuteReader();
                if (DataReader.HasRows)
                {
                    dtResult.Load(DataReader);
                }
            }
            catch (Exception ex) { }
            finally
            {
                GetConnectionType(dbconfigscope).Close();
            }
            return dtResult;
        }

        public Entities.SiteInfo GetATPPendingBTS(string dbconfigscope, Int32 atpid)
        {
            string pathImage = "";
            Entities.SiteInfo info = new Entities.SiteInfo();
            DataTable GeotagList = GetGeotagDetail(dbconfigscope);

            Command = new SqlCommand("uspATP_GetIntegrationPending_BTS", GetConnectionType(dbconfigscope));
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmATPID = new SqlParameter("@atpid", SqlDbType.BigInt);
            prmATPID.Value = atpid;
            Command.Parameters.Add(prmATPID);
            try
            {
                GetConnectionType(dbconfigscope).Open();
                DataReader = Command.ExecuteReader();
                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        string[] GetBTSInfo_geoTag = GetValueFromDT(GeotagList, "bts", Convert.ToString(DataReader["workpackageid"])).Split(';');
                        string[] getAntennaInf_geoTag = GetValueFromDT(GeotagList, "antenna", Convert.ToString(DataReader["workpackageid"])).Split(';');
                        string[] getModuleInf_geoTag = GetValueFromDT(GeotagList, "module", Convert.ToString(DataReader["workpackageid"])).Split(';');
                        string[] getRFUInf_geoTag = GetValueFromDT(GeotagList, "rfu", Convert.ToString(DataReader["workpackageid"])).Split(';');
                        string[] getOtherModuleInf_geoTag = GetValueFromDT(GeotagList, "other module", Convert.ToString(DataReader["workpackageid"])).Split(';');
                        string[] getAntennaGPSInf_near_geoTag = GetValueFromDT(GeotagList, "antenna_gps_near", Convert.ToString(DataReader["workpackageid"])).Split(';');
                        string[] getAntennaGPSInf_far_geoTag = GetValueFromDT(GeotagList, "antenna_gps_far", Convert.ToString(DataReader["workpackageid"])).Split(';');
                        info.bscrnc = null;
                        info.site_id = Convert.ToString(DataReader["siteno"]);
                        info.site_name = Convert.ToString(DataReader["sitename"]);
                        info.site_address = Convert.ToString(DataReader["site_address"]);
                        info.kelurahan = "not applicable";
                        info.kecamatan = "not applicable";
                        info.province = "not applicable";
                        info.region = Convert.ToString(DataReader["rgnname"]);
                        info.postal_code = "not applicable";
                        //info.Longitude = Convert.ToString(DataReader["longitude"]);
                        //info.Latitude = Convert.ToString(DataReader["latitude"]);
                        info.longitude = GetBTSInfo_geoTag[0].ToString();
                        info.latitude = GetBTSInfo_geoTag[1].ToString();
                        info.po_number = Convert.ToString(DataReader["pono"]);
                        //DateTime onairdated = DataReader["onairdated"].Equals(DBNull.Value) ? null : Convert.ToDateTime(DataReader["onairdated"]);

                        if(!DataReader["onairdated"].Equals(DBNull.Value))
                            info.date_on_air = Convert.ToDateTime(DataReader["onairdated"]);
                        info.project_id = DataReader["projectid"].ToString();
                        info.site_type = DataReader["site_type"].ToString();
                        //info.bscrnc_name = Convert.ToString(DataReader["bscrnc_name"]);
                        info.bts.vendor_rbsnodeb = "Nokia";
                        info.bts.site_status = Convert.ToString(DataReader["site_status"]);
                        info.bts.cab_type = Convert.ToString(DataReader["cab_type"]);
                        info.bts.connect_bscrnc = Convert.ToString(DataReader["connect_to_bsc_rnc_mme"]);
                        info.bts.ne_id = Convert.ToString(DataReader["ne_id"]);
                        if (dbconfigscope == StaticConfig.ConnectionTI2G)
                            info.bts.band = "2G";
                        else if (dbconfigscope == StaticConfig.ConnectionTI3G)
                            info.bts.band = "3G";
                        else
                            info.bts.band = "4G";
                        info.bts.number_sector = Convert.ToInt16(DataReader["bts_sector_no"]);
                        info.bts.bts_configuration = Convert.ToString(DataReader["bts_configuration"]);
                        info.bts.btsnodeb_type = Convert.ToString(DataReader["bts_nodeb_type"]);
                        info.bts.capacity = Convert.ToInt16(DataReader["ce_qty"]);
                        info.bts.bts_installationtype = Convert.ToString(DataReader["bts_installation_type"]);
                        info.bts.rack_sn = Convert.ToString(DataReader["rack_sn"]);
                        info.bts.rack_productcode = Convert.ToString(DataReader["rack_productcode"]);
                        info.bts.ref_id = Convert.ToInt64(DataReader["workpackageid"]);

                        int bandwidth = DataReader["bandwidth"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(DataReader["bandwidth"]);
                        int license_power = DataReader["license_power"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(DataReader["license_power"]);
                        int rru_power = DataReader["rru_power"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(DataReader["rru_power"]);
                        int rru_quantity = DataReader["rru_quantity"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(DataReader["rru_quantity"]);
                        int dcpdb_quantity = DataReader["dcpdb_quantity"].Equals(DBNull.Value) ? 0 : Convert.ToInt16(DataReader["dcpdb_quantity"]);

                        info.bts.ne_name = Convert.ToString(DataReader["ne_name"]);
                        info.bts.bandtype_code = Convert.ToString(DataReader["bandtype_code"]);
                        info.bts.band_type = Convert.ToString(DataReader["band_type"]);
                        info.bts.bts_cabinet_type = Convert.ToString(DataReader["bts_cabinet_type"]);
                        info.bts.actual_config = Convert.ToString(DataReader["actual_config"]);
                        info.bts.bandwidth = bandwidth;
                        info.bts.license_power = license_power;
                        info.bts.baseband_type = Convert.ToString(DataReader["baseband_type"]);
                        info.bts.rru_type = Convert.ToString(DataReader["rru_type"]);
                        info.bts.rru_power = rru_power;
                        info.bts.rru_quantity = rru_quantity;
                        info.bts.dcpdb_quantity = dcpdb_quantity;
                        info.bts.dcpdb_type = Convert.ToString(DataReader["dcpdb_type"]);
                        info.bts.antenna_gps = Convert.ToString(DataReader["antenna_gps"]);
                        info.bts.bscrnc_name = Convert.ToString(DataReader["bscrnc_name"]);

                        pathImage = (Path.Combine(ConfigurationManager.AppSettings["pathPhoto"], Convert.ToString(DataReader["workpackageid"]), Convert.ToString(GetBTSInfo_geoTag[2]))).Replace("/", "\\");
                        if (!string.IsNullOrEmpty(Convert.ToString(GetBTSInfo_geoTag[2])) && File.Exists(pathImage))
                        {
                            info.bts.picture_near = GetStringBase64(pathImage);
                            if (needCompress(info.bts.picture_near))
                                info.bts.picture_near = GetStringBase64(createCompressedImage(pathImage));
                        }
                        else
                            info.bts.picture_near = GetStringBase64(ConfigurationManager.AppSettings["pathNA"]);

                        if (!string.IsNullOrEmpty(Convert.ToString(GetBTSInfo_geoTag[2])) && File.Exists(pathImage))
                        {
                            info.bts.picture_far = GetStringBase64(pathImage);
                            if (needCompress(info.bts.picture_far))
                                info.bts.picture_far = GetStringBase64(createCompressedImage(pathImage));
                        }
                        else
                            info.bts.picture_far = GetStringBase64(ConfigurationManager.AppSettings["pathNA"]);

                        if (!string.IsNullOrEmpty(Convert.ToString(DataReader["moduleList"])))
                        {
                            info.module_bts = new List<moduleBTSInfo>();
                            string[] getModuleList = Convert.ToString(DataReader["modulelist"]).Split(',');
                            for (int x = 0; x < getModuleList.Count(); x++)
                            {
                                moduleBTSInfo moduleDetail = new moduleBTSInfo();
                                string[] getDetailModuleList = getModuleList[x].Split('|');
                                moduleDetail.ddf_count = getDetailModuleList[0];
                                moduleDetail.ddf_type = getDetailModuleList[1];
                                moduleDetail.product_name = getDetailModuleList[3];
                                moduleDetail.product_sn = getDetailModuleList[5];
                                moduleDetail.product_code = getDetailModuleList[4];
                                moduleDetail.barcode = "not applicable";
                                moduleDetail.ref_id = Convert.ToInt32(getDetailModuleList[6]);

                                pathImage = (Path.Combine(ConfigurationManager.AppSettings["pathPhoto"], Convert.ToString(DataReader["workpackageid"]), Convert.ToString(getModuleInf_geoTag[2]))).Replace("/", "\\");
                                if (!string.IsNullOrEmpty(Convert.ToString(getModuleInf_geoTag[2])) && File.Exists(pathImage))
                                {
                                    moduleDetail.picture_near = GetStringBase64(pathImage);
                                    if (needCompress(moduleDetail.picture_near))
                                        moduleDetail.picture_near = GetStringBase64(createCompressedImage(pathImage));
                                }
                                else
                                    moduleDetail.picture_near = GetStringBase64(ConfigurationManager.AppSettings["pathNA"]);

                                if (!string.IsNullOrEmpty(Convert.ToString(getModuleInf_geoTag[2])) && File.Exists(pathImage))
                                {
                                    moduleDetail.picture_far = GetStringBase64(pathImage);
                                    if (needCompress(moduleDetail.picture_far))
                                        moduleDetail.picture_far = GetStringBase64(createCompressedImage(pathImage));
                                }
                                else
                                    moduleDetail.picture_far = GetStringBase64(ConfigurationManager.AppSettings["pathNA"]);

                                info.module_bts.Add(moduleDetail);
                            }
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(DataReader["antennaList"])))
                        {
                            info.antenna = new List<AntennaInfo>();
                            string[] getAntennaList = Convert.ToString(DataReader["antennaList"]).Split(',');
                            for (int x = 0; x < getAntennaList.Count(); x++)
                            {
                                AntennaInfo antennaDetail = new AntennaInfo();
                                string[] getDetailAntennaList = getAntennaList[x].Split('|');
                                antennaDetail.product_code = Convert.ToString(getDetailAntennaList[0]);
                                antennaDetail.antenna_heightsect = int.Parse(getDetailAntennaList[1]);
                                antennaDetail.antenna_azimuthsect = int.Parse(getDetailAntennaList[2]);
                                antennaDetail.antenna_tiltingsect = int.Parse(getDetailAntennaList[3]);
                                antennaDetail.antenna_beamwidth = int.Parse(getDetailAntennaList[4]);
                                antennaDetail.product_name = Convert.ToString(getDetailAntennaList[5]);
                                antennaDetail.product_sn = Convert.ToString(getDetailAntennaList[6]);
                                antennaDetail.ref_id = Convert.ToInt32(getDetailAntennaList[7]);
                                antennaDetail.antenna_etilting = Convert.ToInt32(getDetailAntennaList[8]);
                                antennaDetail.antenna_brand = Convert.ToString(getDetailAntennaList[9]);

                                pathImage = (Path.Combine(ConfigurationManager.AppSettings["pathPhoto"], Convert.ToString(DataReader["workpackageid"]), Convert.ToString(getAntennaInf_geoTag[2]))).Replace("/", "\\");
                                if (!string.IsNullOrEmpty(Convert.ToString(getAntennaInf_geoTag[2])) && File.Exists(pathImage))
                                {
                                    antennaDetail.picture_near = GetStringBase64(pathImage);
                                    if (needCompress(antennaDetail.picture_near))
                                        antennaDetail.picture_near = GetStringBase64(createCompressedImage(pathImage));
                                }
                                else
                                    antennaDetail.picture_near = GetStringBase64(ConfigurationManager.AppSettings["pathNA"]);

                                if (!string.IsNullOrEmpty(Convert.ToString(getModuleInf_geoTag[2])) && File.Exists(pathImage))
                                {
                                    antennaDetail.picture_far = GetStringBase64(pathImage);
                                    if (needCompress(antennaDetail.picture_far))
                                        antennaDetail.picture_far = GetStringBase64(createCompressedImage(pathImage));
                                }
                                else
                                    antennaDetail.picture_far = GetStringBase64(ConfigurationManager.AppSettings["pathNA"]);

                                info.antenna.Add(antennaDetail);
                            }
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(DataReader["TRMConfigList"])))
                        {
                            info.trm_config = new List<TRMConfigInfo>();
                            string[] getTRMConfig = Convert.ToString(DataReader["TRMConfigList"]).Split(';');
                            for (int x = 0; x < getTRMConfig.Count(); x++)
                            {
                                TRMConfigInfo TRMConfigDetail = new TRMConfigInfo();
                                string[] getTRMConfigDetail = getTRMConfig[x].Split('|');
                                TRMConfigDetail.ne_ipaddress = Convert.ToString(getTRMConfigDetail[0]);
                                TRMConfigDetail.vlan_id = Convert.ToString(getTRMConfigDetail[1]);
                                TRMConfigDetail.actual_bandwidth = Convert.ToString(getTRMConfigDetail[2]);
                                info.trm_config.Add(TRMConfigDetail);
                            }
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(DataReader["radio_mw"])))
                        {
                            info.radio_mw = new List<RadioMWInfo>();
                            string[] getradio_mw = Convert.ToString(DataReader["radio_mw"]).Split(';');
                            for (int x = 0; x < getradio_mw.Count(); x++)
                            {
                                RadioMWInfo radiomwinfo = new RadioMWInfo();
                                string[] getradio_mwdetail = getradio_mw[x].Split('|');
                                radiomwinfo.product_code = Convert.ToString(getradio_mwdetail[0]);
                                radiomwinfo.product_name = Convert.ToString(getradio_mwdetail[1]);
                                radiomwinfo.radiomw_brand = Convert.ToString(getradio_mwdetail[2]);                                
                                radiomwinfo.radiomw_height = Convert.ToInt32(getradio_mwdetail[3]);
                                radiomwinfo.product_sn = Convert.ToString(getradio_mwdetail[4]);                                
                                radiomwinfo.picture_near = Convert.ToString(getradio_mwdetail[5]);
                                radiomwinfo.picture_far = Convert.ToString(getradio_mwdetail[6]);                                
                                radiomwinfo.ref_id = Convert.ToInt32(getradio_mwdetail[7]);
                                radiomwinfo.trm_config = info.trm_config;
                                info.radio_mw.Add(radiomwinfo);
                            }
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(DataReader["antenna_mw"])))
                        {
                            info.antenna_mw = new List<AntennaMWInfo>();
                            string[] getantenna_mw = Convert.ToString(DataReader["antenna_mw"]).Split(';');
                            for (int x = 0; x < getantenna_mw.Count(); x++)
                            {
                                AntennaMWInfo antennamwinfo = new AntennaMWInfo();
                                string[] getantenna_mwdetail = getantenna_mw[x].Split('|');
                                antennamwinfo.product_code = Convert.ToString(getantenna_mwdetail[0]);
                                antennamwinfo.product_name = Convert.ToString(getantenna_mwdetail[1]);
                                antennamwinfo.antennamw_brand = Convert.ToString(getantenna_mwdetail[2]);
                                antennamwinfo.antennamw_diameter = Convert.ToDecimal(getantenna_mwdetail[3]);
                                antennamwinfo.antennamw_height = Convert.ToInt32(getantenna_mwdetail[4]);
                                antennamwinfo.product_sn = Convert.ToString(getantenna_mwdetail[5]);
                                antennamwinfo.leg_position = Convert.ToString(getantenna_mwdetail[6]);
                                antennamwinfo.picture_near = Convert.ToString(getantenna_mwdetail[7]);
                                antennamwinfo.picture_far = Convert.ToString(getantenna_mwdetail[8]);
                                antennamwinfo.antennamw_azimuth = Convert.ToInt32(getantenna_mwdetail[9]);
                                antennamwinfo.ref_id = Convert.ToInt32(getantenna_mwdetail[10]);
                                antennamwinfo.radiomw = info.radio_mw;
                                info.antenna_mw.Add(antennamwinfo);
                            }
                        }

                        //if (!string.IsNullOrEmpty(Convert.ToString(DataReader["antenna_gps"])))
                        if (!string.IsNullOrEmpty(Convert.ToString(DataReader["antenna_gps_detail"])))
                        {
                            string pathImageNear = string.Empty;
                            string pathImageFar = string.Empty;
                            info.antenna_gps = new List<AntennaGPSInfo>();
                            //string[] getantenna_gps = Convert.ToString(DataReader["antenna_gps"]).Split(';');
                            string[] getantenna_gps = Convert.ToString(DataReader["antenna_gps_detail"]).Split(';');
                            
                            for (int x = 0; x < getantenna_gps.Count(); x++)
                            {
                                AntennaGPSInfo antennagpsinfo = new AntennaGPSInfo();
                                string[] getantenna_gpsdetail = getantenna_gps[x].Split('|');
                                antennagpsinfo.product_code = Convert.ToString(getantenna_gpsdetail[0]);
                                antennagpsinfo.product_name = Convert.ToString(getantenna_gpsdetail[1]);
                                antennagpsinfo.antennagps_brand = Convert.ToString(getantenna_gpsdetail[2]);
                                antennagpsinfo.antennagps_height = Convert.ToInt32(getantenna_gpsdetail[3]);
                                antennagpsinfo.product_sn = Convert.ToString(getantenna_gpsdetail[4]);
                                //antennagpsinfo.picture_near = Convert.ToString(getantenna_gpsdetail[5]);
                                //antennagpsinfo.picture_far = Convert.ToString(getantenna_gpsdetail[6]);
                                antennagpsinfo.ref_id = Convert.ToInt32(getantenna_gpsdetail[7]);

                                pathImageNear = (Path.Combine(ConfigurationManager.AppSettings["pathPhoto"], Convert.ToString(DataReader["workpackageid"]), Convert.ToString(getAntennaGPSInf_near_geoTag[2]))).Replace("/", "\\");
                                pathImageFar = (Path.Combine(ConfigurationManager.AppSettings["pathPhoto"], Convert.ToString(DataReader["workpackageid"]), Convert.ToString(getAntennaGPSInf_far_geoTag[2]))).Replace("/", "\\");
                                if (!string.IsNullOrEmpty(Convert.ToString(getAntennaGPSInf_near_geoTag[2])) && File.Exists(pathImageNear))
                                {
                                    antennagpsinfo.picture_near = GetStringBase64(pathImageNear);
                                    if (needCompress(antennagpsinfo.picture_near))
                                        antennagpsinfo.picture_near = GetStringBase64(createCompressedImage(pathImageNear));
                                }
                                else
                                    antennagpsinfo.picture_near = GetStringBase64(ConfigurationManager.AppSettings["pathNA"]);

                                if (!string.IsNullOrEmpty(Convert.ToString(getAntennaGPSInf_far_geoTag[2])) && File.Exists(pathImageFar))
                                {
                                    antennagpsinfo.picture_far = GetStringBase64(pathImageFar);
                                    if (needCompress(antennagpsinfo.picture_far))
                                        antennagpsinfo.picture_far = GetStringBase64(createCompressedImage(pathImageFar));
                                }
                                else
                                    antennagpsinfo.picture_far = GetStringBase64(ConfigurationManager.AppSettings["pathNA"]);

                                info.antenna_gps.Add(antennagpsinfo);
                            }
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(DataReader["module_mw"])))
                        {
                            info.module_mw = new List<ModuleMWInfo>();
                            string[] getmodule_mw = Convert.ToString(DataReader["module_mw"]).Split(';');
                            for (int x = 0; x < getmodule_mw.Count(); x++)
                            {
                                ModuleMWInfo moduleinfoinfo = new ModuleMWInfo();
                                string[] getmodule_mwdetail = getmodule_mw[x].Split('|');
                                moduleinfoinfo.product_code = Convert.ToString(getmodule_mwdetail[0]);
                                moduleinfoinfo.product_name = Convert.ToString(getmodule_mwdetail[1]);
                                moduleinfoinfo.modulemw_brand = Convert.ToString(getmodule_mwdetail[2]);                                
                                moduleinfoinfo.product_sn = Convert.ToString(getmodule_mwdetail[3]);
                                moduleinfoinfo.picture_near = Convert.ToString(getmodule_mwdetail[4]);
                                moduleinfoinfo.picture_far = Convert.ToString(getmodule_mwdetail[5]);
                                moduleinfoinfo.ref_id = Convert.ToInt32(getmodule_mwdetail[6]);
                                info.module_mw.Add(moduleinfoinfo);
                            }
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(DataReader["rfu"])))
                        {
                            info.rfu = new List<RFUInfo>();
                            string[] getrfu = Convert.ToString(DataReader["rfu"]).Split(';');
                            for (int x = 0; x < getrfu.Count(); x++)
                            {
                                RFUInfo rfuinfo = new RFUInfo();
                                string[] getrfudetail = getrfu[x].Split('|');
                                rfuinfo.product_code = Convert.ToString(getrfudetail[0]);
                                rfuinfo.product_name = Convert.ToString(getrfudetail[1]);
                                rfuinfo.rfu_brand = Convert.ToString(getrfudetail[2]);
                                rfuinfo.product_sn = Convert.ToString(getrfudetail[3]);
                                rfuinfo.picture_near = Convert.ToString(getrfudetail[4]);
                                rfuinfo.picture_far = Convert.ToString(getrfudetail[5]);                                
                                rfuinfo.rfu_height = Convert.ToInt32(getrfudetail[6]);                                
                                rfuinfo.leg_position = Convert.ToString(getrfudetail[7]);
                                rfuinfo.rfu_location = Convert.ToString(getrfudetail[8]);                                
                                rfuinfo.ref_id = Convert.ToInt32(getrfudetail[9]);
                                rfuinfo.rru_power = Convert.ToInt32(getrfudetail[10]);
                                rfuinfo.rru_quantity = Convert.ToInt32(getrfudetail[11]);
                                info.rfu.Add(rfuinfo);
                            }
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(DataReader["other_module"])))
                        {
                            info.other_module = new List<OtherModuleInfo>();
                            string[] getothermodule = Convert.ToString(DataReader["other_module"]).Split(';');
                            for (int x = 0; x < getothermodule.Count(); x++)
                            {
                                OtherModuleInfo othermoduleinfo = new OtherModuleInfo();
                                string[] getothermoduledetail = getothermodule[x].Split('|');
                                othermoduleinfo.product_code = Convert.ToString(getothermodule[0]);
                                othermoduleinfo.product_name = Convert.ToString(getothermodule[1]);
                                othermoduleinfo.product_brand = Convert.ToString(getothermodule[2]);
                                othermoduleinfo.product_sn = Convert.ToString(getothermodule[3]);
                                othermoduleinfo.picture_near = Convert.ToString(getothermodule[4]);
                                othermoduleinfo.picture_far = Convert.ToString(getothermodule[5]);
                                othermoduleinfo.ref_id = Convert.ToInt32(getothermodule[6]);
                                info.other_module.Add(othermoduleinfo);
                            }
                        }

                    }
                }
                else
                    info = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetSitelist " + dbconfigscope + ": " + ex.Message.ToString());
                info = null;
            }
            finally
            {
                GetConnectionType(dbconfigscope).Close();
            }
            return info;
        }

        public bool ATPIntegrationStatus_U(string dbconfigscope, Int32 atpid, string integrationStatus, string remarks)
        {
            bool isSucceed = true;
            Command = new SqlCommand("uspATP_IntegrationStatus_U", GetConnectionType(dbconfigscope));
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmATPID = new SqlParameter("@atpid", SqlDbType.BigInt);
            SqlParameter prmIntegrationStatus = new SqlParameter("@integrationStatus", SqlDbType.VarChar, 20);
            SqlParameter prmRemarks = new SqlParameter("@remarks", SqlDbType.VarChar, 200);
            prmATPID.Value = atpid;
            prmIntegrationStatus.Value = integrationStatus;
            if (!string.IsNullOrEmpty(remarks))
            {
                prmRemarks.Value = remarks;
                Command.Parameters.Add(prmRemarks);
            }
            Command.Parameters.Add(prmATPID);
            Command.Parameters.Add(prmIntegrationStatus);
            try
            {
                GetConnectionType(dbconfigscope).Open();
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                isSucceed = false;
            }
            finally
            {
                GetConnectionType(dbconfigscope).Close();
            }

            return isSucceed;
        }


        private DataTable GetGeotagDetail(string dbconfigscope)
        {
            DataTable dtResult = new DataTable();
            Command = new SqlCommand("uspATP_GetIntegrationPending_Geotag", GetConnectionType(dbconfigscope));
            Command.CommandType = CommandType.StoredProcedure;
            try
            {
                GetConnectionType(dbconfigscope).Open();
                DataReader = Command.ExecuteReader();
                if (DataReader.HasRows)
                {
                    dtResult.Load(DataReader);
                }
            }
            catch (Exception ex)
            {
                dtResult = null;
            }
            finally
            {
                GetConnectionType(dbconfigscope).Close();
            }
            return dtResult;
        }


        private bool needCompress(string base64)
        {
            byte[] bImg = Convert.FromBase64String(base64);
            decimal size = Math.Ceiling((decimal)bImg.Length / 1024); //in KB
            if (size <= maxSizeImg) return false;
            else return true;
        }

        private string GetStringBase64(string path)
        {
            using (Stream s = File.Open(path, FileMode.Open))
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(s, false))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        return Convert.ToBase64String(imageBytes);
                    }
                }
            }
        }

        private string createCompressedImage(string path)
        {
            string newFile;
            using (Bitmap bmp = (Bitmap)Bitmap.FromFile(path))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = myEncoderParameter = new EncoderParameter(myEncoder, 43L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                newFile = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "_Compressed" + Path.GetExtension(path);
                bmp.Save(newFile, jpgEncoder, myEncoderParameters);
            }
            return newFile;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public string GetValueFromDT(DataTable dtSource, string filter, string wpid)
        {
            string GetResult = string.Empty;
            DataRow[] result = dtSource.Select("data_category ='" + filter + "' and wpid = '" + wpid + "'");
            foreach (DataRow row in result)
            {
                GetResult = Convert.ToString(row["longitude"]) + ";" + Convert.ToString(row["latitude"]) + ";" + Convert.ToString(row["photoPath"]);

            }
            return GetResult;
        }

        private SqlConnection GetConnectionType(string dbconfigscope)
        {
            if (dbconfigscope == StaticConfig.ConnectionTI2G)
                return ConnectionTI2G;
            else if (dbconfigscope == StaticConfig.ConnectionTI3G)
                return ConnectionTI3G;
            else if (dbconfigscope == StaticConfig.ConnectionLTE)
                return ConnectionLTE;

            return ConnectionTI2G;
        }


    }
}
