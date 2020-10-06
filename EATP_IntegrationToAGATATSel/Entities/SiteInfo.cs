using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Entities
{
    public class SiteInfo
    {
        public string site_id { get; set; }
        public string site_name { get; set; }
        public string site_address { get; set; }
        public string kelurahan { get; set; }
        public string kecamatan { get; set; }
        public string kabupaten { get; set; }
        public string province { get; set; }
        public string region { get; set; }
        public string postal_code { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string po_number { get; set; }
        public DateTime date_on_air { get; set; }
        public string project_id { get; set; }
        public string site_type { get; set; }
        //public string bscrnc_name { get; set; }

        private BTSInfo _btsInfo = new BTSInfo();
        public BTSInfo bts
        {
            get { return _btsInfo; }
            set { _btsInfo = value; }
        }

        private BSCRNCInfo _bscrncInfo = new BSCRNCInfo();
        public BSCRNCInfo bscrnc
        {
            get { return _bscrncInfo; }
            set { _bscrncInfo = value; }
        }

        private List<moduleBTSInfo> _moduleBTS;
        public List<moduleBTSInfo> module_bts
        {
            get { return _moduleBTS; }
            set { _moduleBTS = value; }
        }

        private List<moduleBSCRNCInfo> _moduleBSCRNC;
        public List<moduleBSCRNCInfo> module_bscrnc
        {
            get { return _moduleBSCRNC; }
            set { _moduleBSCRNC = value; }
        }

        private List<AntennaInfo> _antenna;
        public List<AntennaInfo> antenna
        {
            get { return _antenna; }
            set { _antenna = value; }
        }


        private List<FeederInfo> _feederInfo;
        public List<FeederInfo> feeder
        {
            get { return _feederInfo; }
            set { _feederInfo = value; }
        }

        private List<TRMConfigInfo> _trm_config;
        public List<TRMConfigInfo> trm_config
        {
            get { return _trm_config; }
            set { _trm_config = value; }
        }

        private List<AntennaMWInfo> _antenna_mw;
        public List<AntennaMWInfo> antenna_mw
        {
            get { return _antenna_mw; }
            set { _antenna_mw = value; }
        }

        private List<RadioMWInfo> _radio_mw;
        public List<RadioMWInfo> radio_mw
        {
            get { return _radio_mw; }
            set { _radio_mw = value; }
        }

        private List<AntennaGPSInfo> _antenna_gps;
        public List<AntennaGPSInfo> antenna_gps
        {
            get { return _antenna_gps; }
            set { _antenna_gps = value; }
        }

        private List<ModuleMWInfo> _module_mw;
        public List<ModuleMWInfo> module_mw
        {
            get { return _module_mw; }
            set { _module_mw = value; }
        }

        private List<RFUInfo> _rfu;
        public List<RFUInfo> rfu
        {
            get { return _rfu; }
            set { _rfu = value; }
        }

        private List<OtherModuleInfo> _other_module;
        public List<OtherModuleInfo> other_module
        {
            get { return _other_module; }
            set { _other_module = value; }
        }
    }
}
