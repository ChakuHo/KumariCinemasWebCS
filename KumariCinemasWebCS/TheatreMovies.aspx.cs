using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using KumariCinemasWebCS.DAL;

namespace KumariCinemasWebCS
{
    public partial class TheatreMovies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
                BindTheatres();
        }

        private void BindTheatres()
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(
                "SELECT TheatreID, TheatreID || ' - ' || TheatreName AS DisplayText FROM THEATERCITYHALL ORDER BY TheatreID", conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                ddlTheatre.DataSource = dt;
                ddlTheatre.DataTextField = "DisplayText";
                ddlTheatre.DataValueField = "TheatreID";
                ddlTheatre.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int theatreId = int.Parse(ddlTheatre.SelectedValue);

            string sql = @"
SELECT t.TheatreID, t.TheatreName,
       m.MovieID, m.MovieTitle,
       s.ShowID, TO_CHAR(s.ShowDate,'YYYY-MM-DD') AS ShowDate, s.ShowTime,
       s.HolidayStatus, s.ReleaseWeekFlag
FROM THEATERCITYHALL t
JOIN MOVIE_THEATRE_HALL_SHOW asg ON asg.TheatreID = t.TheatreID
JOIN SHOWTIME s ON s.ShowID = asg.ShowID
JOIN MOVIE m ON m.MovieID = asg.MovieID
WHERE t.TheatreID = :pTheatreID
ORDER BY m.MovieTitle, s.ShowDate, s.ShowTime";

            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(sql, conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                cmd.Parameters.Add(":pTheatreID", OracleDbType.Int32).Value = theatreId;

                var dt = new DataTable();
                da.Fill(dt);

                gv.DataSource = dt;
                gv.DataBind();

                if (dt.Rows.Count == 0)
                    lblMsg.Text = "No shows found for this theatre.";
            }
        }
    }
}