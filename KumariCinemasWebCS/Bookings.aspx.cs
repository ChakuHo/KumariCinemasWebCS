using System;
using System.Data;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using KumariCinemasWebCS.DAL;

namespace KumariCinemasWebCS
{
    public partial class Bookings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                BindCustomers();
                BindShows();
                BindGrid();
            }
        }

        private void BindCustomers()
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand("SELECT CustomerID, Username FROM CUSTOMER ORDER BY CustomerID", conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                ddlCustomer.DataSource = dt;
                ddlCustomer.DataTextField = "Username";
                ddlCustomer.DataValueField = "CustomerID";
                ddlCustomer.DataBind();
            }
        }

        private void BindShows()
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(
                "SELECT ShowID, TO_CHAR(ShowDate,'YYYY-MM-DD') || ' - ' || ShowTime AS DisplayText FROM SHOWTIME ORDER BY ShowID", conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                ddlShow.DataSource = dt;
                ddlShow.DataTextField = "DisplayText";
                ddlShow.DataValueField = "ShowID";
                ddlShow.DataBind();
            }
        }

        private void BindGrid()
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(
                "SELECT BookingID, CustomerID, ShowID, BookingTime, ExpiresAt, BookingStatus FROM BOOKING ORDER BY BookingID", conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                gvBookings.DataSource = dt;
                gvBookings.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    @"INSERT INTO BOOKING(BookingID, CustomerID, ShowID, BookingTime, ExpiresAt, BookingStatus)
                      VALUES (SEQ_BOOKING.NEXTVAL, :pCust, :pShow, SYSDATE, SYSDATE + (1/24), :pStatus)", conn))
                {
                    cmd.Parameters.Add(":pCust", OracleDbType.Int32).Value = int.Parse(ddlCustomer.SelectedValue);
                    cmd.Parameters.Add(":pShow", OracleDbType.Int32).Value = int.Parse(ddlShow.SelectedValue);
                    cmd.Parameters.Add(":pStatus", OracleDbType.Varchar2).Value = ddlStatus.SelectedValue;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Booking added.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Insert failed: " + ex.Message;
            }
        }

        protected void gvBookings_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBookings.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvBookings_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBookings.EditIndex = -1;
            BindGrid();
        }

        protected void gvBookings_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int bookingId = Convert.ToInt32(gvBookings.DataKeys[e.RowIndex].Value);
            var row = gvBookings.Rows[e.RowIndex];
            var ddl = (DropDownList)row.FindControl("ddlStatusEdit");

            string status = ddl.SelectedValue;

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    "UPDATE BOOKING SET BookingStatus=:pStatus WHERE BookingID=:pId", conn))
                {
                    cmd.Parameters.Add(":pStatus", OracleDbType.Varchar2).Value = status;
                    cmd.Parameters.Add(":pId", OracleDbType.Int32).Value = bookingId;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvBookings.EditIndex = -1;
                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Booking updated.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Update failed: " + ex.Message;
            }
        }

        protected void gvBookings_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookingId = Convert.ToInt32(gvBookings.DataKeys[e.RowIndex].Value);

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand("DELETE FROM BOOKING WHERE BookingID=:pId", conn))
                {
                    cmd.Parameters.Add(":pId", OracleDbType.Int32).Value = bookingId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Booking deleted.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Delete failed (referenced by tickets): " + ex.Message;
            }
        }
    }
}