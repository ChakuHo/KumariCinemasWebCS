using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using KumariCinemasWebCS.DAL;

namespace KumariCinemasWebCS
{
    public partial class UserTickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
                BindCustomers();
        }

        private void BindCustomers()
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(
                "SELECT CustomerID, CustomerID || ' - ' || Username AS DisplayText FROM CUSTOMER ORDER BY CustomerID", conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                ddlCustomer.DataSource = dt;
                ddlCustomer.DataTextField = "DisplayText";
                ddlCustomer.DataValueField = "CustomerID";
                ddlCustomer.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int customerId = int.Parse(ddlCustomer.SelectedValue);

            string sql = @"
SELECT c.CustomerID, c.Username,
       tk.TicketID, tk.SeatNumber, tk.TicketStatus,
       b.BookingTime,
       s.ShowID, TO_CHAR(s.ShowDate,'YYYY-MM-DD') AS ShowDate, s.ShowTime,
       m.MovieTitle,
       t.TheatreName,
       pr.TicketPrice
FROM CUSTOMER c
JOIN BOOKING b ON b.CustomerID = c.CustomerID
JOIN TICKET tk ON tk.BookingID = b.BookingID
JOIN SHOWTIME s ON s.ShowID = b.ShowID
JOIN MOVIE_THEATRE_HALL_SHOW asg ON asg.ShowID = s.ShowID
JOIN MOVIE m ON m.MovieID = asg.MovieID
JOIN THEATERCITYHALL t ON t.TheatreID = asg.TheatreID
LEFT JOIN SHOW_PRICING sp ON sp.ShowID = s.ShowID
LEFT JOIN PRICE_RULE pr ON pr.RuleID = sp.RuleID
WHERE c.CustomerID = :pCustomerID
  AND tk.TicketStatus = 'PAID'
  AND b.BookingTime >= ADD_MONTHS(TRUNC(SYSDATE), -6)
ORDER BY b.BookingTime DESC";

            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(sql, conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                cmd.Parameters.Add(":pCustomerID", OracleDbType.Int32).Value = customerId;

                var dt = new DataTable();
                da.Fill(dt);

                gv.DataSource = dt;
                gv.DataBind();

                if (dt.Rows.Count == 0)
                    lblMsg.Text = "No PAID tickets found for the last 6 months.";
            }
        }
    }
}