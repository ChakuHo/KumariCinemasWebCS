using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using KumariCinemasWebCS.DAL;

namespace KumariCinemasWebCS
{
    public partial class Movies : System.Web.UI.Page
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
                "SELECT MovieID, MovieTitle, Duration, Language, Genre, ReleaseDate FROM MOVIE ORDER BY MovieID", conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                gvMovies.DataSource = dt;
                gvMovies.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            DateTime? releaseDate = null;
            if (!string.IsNullOrWhiteSpace(txtReleaseDate.Text))
                releaseDate = DateTime.Parse(txtReleaseDate.Text);

            int? duration = null;
            if (int.TryParse(txtDuration.Text.Trim(), out int d)) duration = d;

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    @"INSERT INTO MOVIE(MovieID, MovieTitle, Duration, Language, Genre, ReleaseDate)
                      VALUES (SEQ_MOVIE.NEXTVAL, :pTitle, :pDur, :pLang, :pGenre, :pRel)", conn))
                {
                    cmd.Parameters.Add(":pTitle", OracleDbType.Varchar2).Value = txtTitle.Text.Trim();
                    cmd.Parameters.Add(":pDur", OracleDbType.Int32).Value = (object)duration ?? DBNull.Value;
                    cmd.Parameters.Add(":pLang", OracleDbType.Varchar2).Value = (object)txtLanguage.Text.Trim() ?? DBNull.Value;
                    cmd.Parameters.Add(":pGenre", OracleDbType.Varchar2).Value = (object)txtGenre.Text.Trim() ?? DBNull.Value;
                    cmd.Parameters.Add(":pRel", OracleDbType.Date).Value = (object)releaseDate ?? DBNull.Value;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                txtTitle.Text = txtDuration.Text = txtLanguage.Text = txtGenre.Text = txtReleaseDate.Text = "";
                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Movie added.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Insert failed: " + ex.Message;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle.Text = txtDuration.Text = txtLanguage.Text = txtGenre.Text = txtReleaseDate.Text = "";
        }

        protected void gvMovies_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvMovies.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvMovies_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvMovies.EditIndex = -1;
            BindGrid();
        }

        protected void gvMovies_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int movieId = Convert.ToInt32(gvMovies.DataKeys[e.RowIndex].Value);
            var row = gvMovies.Rows[e.RowIndex];

            string title = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtTitleEdit")).Text.Trim();
            string durText = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtDurationEdit")).Text.Trim();
            string lang = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtLanguageEdit")).Text.Trim();
            string genre = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtGenreEdit")).Text.Trim();
            string relText = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtReleaseEdit")).Text.Trim();

            int? duration = null;
            if (int.TryParse(durText, out int d)) duration = d;

            DateTime? rel = null;
            if (!string.IsNullOrWhiteSpace(relText)) rel = DateTime.Parse(relText);

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    @"UPDATE MOVIE
                      SET MovieTitle=:pTitle, Duration=:pDur, Language=:pLang, Genre=:pGenre, ReleaseDate=:pRel
                      WHERE MovieID=:pId", conn))
                {
                    cmd.Parameters.Add(":pTitle", OracleDbType.Varchar2).Value = title;
                    cmd.Parameters.Add(":pDur", OracleDbType.Int32).Value = (object)duration ?? DBNull.Value;
                    cmd.Parameters.Add(":pLang", OracleDbType.Varchar2).Value = (object)lang ?? DBNull.Value;
                    cmd.Parameters.Add(":pGenre", OracleDbType.Varchar2).Value = (object)genre ?? DBNull.Value;
                    cmd.Parameters.Add(":pRel", OracleDbType.Date).Value = (object)rel ?? DBNull.Value;
                    cmd.Parameters.Add(":pId", OracleDbType.Int32).Value = movieId;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvMovies.EditIndex = -1;
                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Movie updated.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Update failed: " + ex.Message;
            }
        }

        protected void gvMovies_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int movieId = Convert.ToInt32(gvMovies.DataKeys[e.RowIndex].Value);

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand("DELETE FROM MOVIE WHERE MovieID=:pId", conn))
                {
                    cmd.Parameters.Add(":pId", OracleDbType.Int32).Value = movieId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                BindGrid();
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Movie deleted.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Delete failed (referenced by other tables): " + ex.Message;
            }
        }
    }
}