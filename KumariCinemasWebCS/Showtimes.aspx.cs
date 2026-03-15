using System;
using System.Data;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using KumariCinemasWebCS.DAL;

namespace KumariCinemasWebCS
{
    public partial class Showtimes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
                BindGrid();
        }

        private void BindGrid()
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(
                "SELECT ShowID, ShowDate, ShowTime, HolidayStatus, ReleaseWeekFlag FROM SHOWTIME ORDER BY ShowID", conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                gvShow.DataSource = dt;
                gvShow.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                DateTime showDate = DateTime.Parse(txtDate.Text);

                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    @"INSERT INTO SHOWTIME(ShowID, ShowDate, ShowTime, HolidayStatus, ReleaseWeekFlag)
                      VALUES (SEQ_SHOW.NEXTVAL, :pDate, :pTime, :pHol, :pRel)", conn))
                {
                    cmd.Parameters.Add(":pDate", OracleDbType.Date).Value = showDate;
                    cmd.Parameters.Add(":pTime", OracleDbType.Varchar2).Value = ddlSlot.SelectedValue;
                    cmd.Parameters.Add(":pHol", OracleDbType.Char).Value = ddlHoliday.SelectedValue;
                    cmd.Parameters.Add(":pRel", OracleDbType.Char).Value = ddlRelease.SelectedValue;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                ClearForm();
                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Showtime added successfully.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Insert failed: " + ex.Message;
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error: " + ex.Message;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            lblMsg.Text = "";
        }

        private void ClearForm()
        {
            txtDate.Text = "";
            ddlSlot.SelectedIndex = 0;
            ddlHoliday.SelectedValue = "N";
            ddlRelease.SelectedValue = "N";
        }

        protected void gvShow_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvShow.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvShow_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvShow.EditIndex = -1;
            BindGrid();
        }

        // Set dropdown values correctly during EDIT mode
        protected void gvShow_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
                (e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                var ddlHol = (DropDownList)e.Row.FindControl("ddlHolEdit");
                var ddlRel = (DropDownList)e.Row.FindControl("ddlRelEdit");
                var hidHol = (HiddenField)e.Row.FindControl("hidHol");
                var hidRel = (HiddenField)e.Row.FindControl("hidRel");

                if (ddlHol != null && hidHol != null) ddlHol.SelectedValue = hidHol.Value;
                if (ddlRel != null && hidRel != null) ddlRel.SelectedValue = hidRel.Value;
            }
        }

        protected void gvShow_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int showId = Convert.ToInt32(gvShow.DataKeys[e.RowIndex].Value);
            var row = gvShow.Rows[e.RowIndex];

            string dateText = ((TextBox)row.FindControl("txtDateEdit")).Text.Trim();
            string timeText = ((TextBox)row.FindControl("txtTimeEdit")).Text.Trim();

            string hol = ((DropDownList)row.FindControl("ddlHolEdit")).SelectedValue;
            string rel = ((DropDownList)row.FindControl("ddlRelEdit")).SelectedValue;

            try
            {
                DateTime showDate = DateTime.Parse(dateText);

                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    @"UPDATE SHOWTIME
                      SET ShowDate = :pDate,
                          ShowTime = :pTime,
                          HolidayStatus = :pHol,
                          ReleaseWeekFlag = :pRel
                      WHERE ShowID = :pId", conn))
                {
                    cmd.Parameters.Add(":pDate", OracleDbType.Date).Value = showDate;
                    cmd.Parameters.Add(":pTime", OracleDbType.Varchar2).Value = timeText;
                    cmd.Parameters.Add(":pHol", OracleDbType.Char).Value = hol;
                    cmd.Parameters.Add(":pRel", OracleDbType.Char).Value = rel;
                    cmd.Parameters.Add(":pId", OracleDbType.Int32).Value = showId;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvShow.EditIndex = -1;
                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Showtime updated.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Update failed: " + ex.Message;
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error: " + ex.Message;
            }
        }

        protected void gvShow_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int showId = Convert.ToInt32(gvShow.DataKeys[e.RowIndex].Value);

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    "DELETE FROM SHOWTIME WHERE ShowID = :pId", conn))
                {
                    cmd.Parameters.Add(":pId", OracleDbType.Int32).Value = showId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Showtime deleted.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Delete failed (referenced by other tables): " + ex.Message;
            }
        }
    }
}