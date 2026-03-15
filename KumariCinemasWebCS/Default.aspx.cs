using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using KumariCinemasWebCS.DAL;

namespace KumariCinemasWebCS
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                LoadCounts();
                LoadTicketStatusForChart();
                LoadRecentBookings();
            }
        }

        private int ExecScalarInt(string sql)
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(sql, conn))
            {
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void LoadCounts()
        {
            try
            {
                litCustomers.Text = ExecScalarInt("SELECT COUNT(*) FROM CUSTOMER").ToString();
                litMovies.Text = ExecScalarInt("SELECT COUNT(*) FROM MOVIE").ToString();
                litTheatres.Text = ExecScalarInt("SELECT COUNT(*) FROM THEATERCITYHALL").ToString();
                litPaidTickets.Text = ExecScalarInt("SELECT COUNT(*) FROM TICKET WHERE TicketStatus='PAID'").ToString();
            }
            catch (OracleException ex)
            {
                lblMsg.Text = "Dashboard counts failed: " + ex.Message;
            }
        }

        private void LoadTicketStatusForChart()
        {
            hfPaid.Value = ExecScalarInt("SELECT COUNT(*) FROM TICKET WHERE TicketStatus='PAID'").ToString();
            hfBooked.Value = ExecScalarInt("SELECT COUNT(*) FROM TICKET WHERE TicketStatus='BOOKED'").ToString();
            hfCancelled.Value = ExecScalarInt("SELECT COUNT(*) FROM TICKET WHERE TicketStatus='CANCELLED'").ToString();
        }

        private void LoadRecentBookings()
        {
            string sql = @"
SELECT *
FROM (
    SELECT b.BookingID,
           c.Username,
           b.ShowID,
           TO_CHAR(s.ShowDate,'YYYY-MM-DD') AS ShowDate,
           s.ShowTime,
           b.BookingStatus,
           b.BookingTime
    FROM BOOKING b
    JOIN CUSTOMER c ON c.CustomerID = b.CustomerID
    JOIN SHOWTIME s ON s.ShowID = b.ShowID
    ORDER BY b.BookingTime DESC
)
WHERE ROWNUM <= 5";

            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(sql, conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                gvRecentBookings.DataSource = dt;
                gvRecentBookings.DataBind();
            }
        }
    }
}