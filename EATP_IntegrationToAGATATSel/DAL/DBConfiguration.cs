using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ConsoleApplication1.DAL
{
    public class DBConfiguration : IDisposable
    {
        #region field

        private bool _disposed;
        private SqlConnection _connectionTI2G;
        private SqlConnection _connectionTI3G;
        private SqlConnection _connectionLTE;
        private SqlCommand _command;
        private SqlDataReader _datareader;

        #endregion

        public DBConfiguration()
        {
            _connectionTI2G = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon_ti2g"].ConnectionString);
            _connectionTI3G = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon_ti3g"].ConnectionString);
            _connectionLTE = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcon_lte"].ConnectionString);
        }

        #region property
        protected SqlConnection ConnectionTI2G
        {
            get { return _connectionTI2G; }
            set { _connectionTI2G = value; }
        }

        protected SqlConnection ConnectionTI3G {
            get { return _connectionTI3G; }
            set { _connectionTI3G = value; }
        }

        protected SqlConnection ConnectionLTE
        {
            get { return _connectionLTE; }
            set { _connectionLTE = value; }
        }

        protected SqlCommand Command
        {
            get { return _command; }
            set { _command = value; }
        }

        protected SqlDataReader DataReader
        {
            get { return _datareader; }
            set { _datareader = value; }
        }
        #endregion


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ConnectionTI2G.Dispose();
                    ConnectionTI3G.Dispose();
                    ConnectionLTE.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
