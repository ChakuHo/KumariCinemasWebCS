using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace KumariCinemasWebCS.DAL
{
    public static class Db
    {
        public static OracleConnection GetConn()
        {
            var cs = ConfigurationManager.ConnectionStrings["KumariDb"].ConnectionString;
            return new OracleConnection(cs);
        }
    }
}