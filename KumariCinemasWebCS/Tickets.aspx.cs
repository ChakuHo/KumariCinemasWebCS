using System;
using System.Data;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using KumariCinemasWebCS.DAL;

namespace KumariCinemasWebCS
{
    public partial class Tickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                BindBookingsDropDown(ddlBooking);
                BindGrid();
            }
        }

        private void BindBookingsDropDown(DropDownList ddl)
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(
                @"SELECT b.BookingID,
                         'Booking '||b.BookingID||' (Cust '||b.CustomerID||', Show '||b.ShowID||')' AS DisplayText
                  FROM BOOKING b
                  ORDER BY b.BookingID", conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                ddl.DataSource = dt;
                ddl.DataTextField = "DisplayText";
                ddl.DataValueField = "BookingID";
                ddl.DataBind();
            }
        }

        private void BindGrid()
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(
                "SELECT TicketID, BookingID, SeatNumber, TicketStatus FROM TICKET ORDER BY TicketID", conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                gvTickets.DataSource = dt;
                gvTickets.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    @"INSERT INTO TICKET(TicketID, BookingID, SeatNumber, TicketStatus)
                      VALUES (SEQ_TICKET.NEXTVAL, :pBook, :pSeat, :pStatus)", conn))
                {
                    cmd.Parameters.Add(":pBook", OracleDbType.Int32).Value = int.Parse(ddlBooking.SelectedValue);
                    cmd.Parameters.Add(":pSeat", OracleDbType.Varchar2).Value = txtSeat.Text.Trim();
                    cmd.Parameters.Add(":pStatus", OracleDbType.Varchar2).Value = ddlTStatus.SelectedValue;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                txtSeat.Text = "";
                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Ticket added.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Insert failed: " + ex.Message;
            }
        }

        protected void gvTickets_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTickets.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvTickets_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTickets.EditIndex = -1;
            BindGrid();
        }

        protected void gvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                var ddlBook = (DropDownList)e.Row.FindControl("ddlBookingEdit");
                var hidBook = (HiddenField)e.Row.FindControl("hidBookingId");

                var ddlStatus = (DropDownList)e.Row.FindControl("ddlStatusEdit");
                var hidStatus = (HiddenField)e.Row.FindControl("hidStatus");

                BindBookingsDropDown(ddlBook);

                if (hidBook != null) ddlBook.SelectedValue = hidBook.Value;
                if (hidStatus != null) ddlStatus.SelectedValue = hidStatus.Value;
            }
        }

        protected void gvTickets_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ticketId = Convert.ToInt32(gvTickets.DataKeys[e.RowIndex].Value);
            var row = gvTickets.Rows[e.RowIndex];

            int bookingId = int.Parse(((DropDownList)row.FindControl("ddlBookingEdit")).SelectedValue);
            string seat = ((TextBox)row.FindControl("txtSeatEdit")).Text.Trim();
            string status = ((DropDownList)row.FindControl("ddlStatusEdit")).SelectedValue;

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    @"UPDATE TICKET
                      SET BookingID=:pBook, SeatNumber=:pSeat, TicketStatus=:pStatus
                      WHERE TicketID=:pId", conn))
                {
                    cmd.Parameters.Add(":pBook", OracleDbType.Int32).Value = bookingId;
                    cmd.Parameters.Add(":pSeat", OracleDbType.Varchar2).Value = seat;
                    cmd.Parameters.Add(":pStatus", OracleDbType.Varchar2).Value = status;
                    cmd.Parameters.Add(":pId", OracleDbType.Int32).Value = ticketId;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvTickets.EditIndex = -1;
                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Ticket updated.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Update failed: " + ex.Message;
            }
        }

        protected void gvTickets_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ticketId = Convert.ToInt32(gvTickets.DataKeys[e.RowIndex].Value);

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand("DELETE FROM TICKET WHERE TicketID=:pId", conn))
                {
                    cmd.Parameters.Add(":pId", OracleDbType.Int32).Value = ticketId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Ticket deleted.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Delete failed (referenced by PAYMENT/USER_SHOW_TICKET): " + ex.Message;
            }
        }
    }
}