using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using KumariCinemasWebCS.DAL;

namespace KumariCinemasWebCS
{
    public partial class Theaters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (!IsPostBack) BindGrid();
        }

        private void BindGrid()
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(
                "SELECT TheatreID, TheatreName, TheatreCity FROM THEATERCITYHALL ORDER BY TheatreID", conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                gvTheaters.DataSource = dt;
                gvTheaters.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    @"INSERT INTO THEATERCITYHALL(TheatreID, TheatreName, TheatreCity)
                      VALUES (SEQ_THEATRE.NEXTVAL, :pName, :pCity)", conn))
                {
                    cmd.Parameters.Add(":pName", OracleDbType.Varchar2).Value = txtName.Text.Trim();
                    cmd.Parameters.Add(":pCity", OracleDbType.Varchar2).Value = txtCity.Text.Trim();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                txtName.Text = ""; txtCity.Text = "";
                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "TheaterCityHall added.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Insert failed: " + ex.Message;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = ""; txtCity.Text = "";
        }

        protected void gvTheaters_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvTheaters.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvTheaters_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvTheaters.EditIndex = -1;
            BindGrid();
        }

        protected void gvTheaters_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int theatreId = Convert.ToInt32(gvTheaters.DataKeys[e.RowIndex].Value);
            var row = gvTheaters.Rows[e.RowIndex];

            string name = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtNameEdit")).Text.Trim();
            string city = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtCityEdit")).Text.Trim();

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    @"UPDATE THEATERCITYHALL
                      SET TheatreName = :pName, TheatreCity = :pCity
                      WHERE TheatreID = :pId", conn))
                {
                    cmd.Parameters.Add(":pName", OracleDbType.Varchar2).Value = name;
                    cmd.Parameters.Add(":pCity", OracleDbType.Varchar2).Value = city;
                    cmd.Parameters.Add(":pId", OracleDbType.Int32).Value = theatreId;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvTheaters.EditIndex = -1;
                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Updated.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Update failed: " + ex.Message;
            }
        }

        protected void gvTheaters_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int theatreId = Convert.ToInt32(gvTheaters.DataKeys[e.RowIndex].Value);

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    "DELETE FROM THEATERCITYHALL WHERE TheatreID = :pId", conn))
                {
                    cmd.Parameters.Add(":pId", OracleDbType.Int32).Value = theatreId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Deleted.";
            }
            catch (OracleException ex)
            {
                // Likely referenced by HALL, MOVIE_THEATRE, PRICE_RULE, etc.
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Delete failed (referenced by other records): " + ex.Message;
            }
        }
    }
}