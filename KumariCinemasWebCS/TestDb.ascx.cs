using System;
using Oracle.ManagedDataAccess.Client;
using KumariCinemasWebCS.DAL;

namespace KumariCinemasWebCS
{
    public partial class TestDb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand("SELECT SYSDATE FROM DUAL", conn))
            {
                conn.Open();
                lbl.Text = "Connected OK. SYSDATE = " + cmd.ExecuteScalar();
            }
        }
    }
}