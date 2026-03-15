using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using KumariCinemasWebCS.DAL;

namespace KumariCinemasWebCS
{
    public partial class MovieOccupancy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
                BindMovies();
        }

        private void BindMovies()
        {
            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(
                "SELECT MovieID, MovieID || ' - ' || MovieTitle AS DisplayText FROM MOVIE ORDER BY MovieID", conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                ddlMovie.DataSource = dt;
                ddlMovie.DataTextField = "DisplayText";
                ddlMovie.DataValueField = "MovieID";
                ddlMovie.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int movieId = int.Parse(ddlMovie.SelectedValue);

            string sql = @"
WITH shows AS (
  SELECT asg.ShowID, asg.TheatreID, h.HallCapacity
  FROM MOVIE_THEATRE_HALL_SHOW asg
  JOIN HALL h ON h.HallID = asg.HallID AND h.TheatreID = asg.TheatreID
  WHERE asg.MovieID = :pMovieID
),
paid AS (
  SELECT b.ShowID, COUNT(*) AS PaidTickets
  FROM BOOKING b
  JOIN TICKET tk ON tk.BookingID = b.BookingID
  WHERE tk.TicketStatus = 'PAID'
  GROUP BY b.ShowID
)
SELECT *
FROM (
  SELECT t.TheatreID, t.TheatreName,
         ROUND((SUM(NVL(p.PaidTickets,0)) / SUM(s.HallCapacity)) * 100, 2) AS OccupancyPct
  FROM shows s
  JOIN THEATERCITYHALL t ON t.TheatreID = s.TheatreID
  LEFT JOIN paid p ON p.ShowID = s.ShowID
  GROUP BY t.TheatreID, t.TheatreName
  ORDER BY OccupancyPct DESC
)
WHERE ROWNUM <= 3";

            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(sql, conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                cmd.Parameters.Add(":pMovieID", OracleDbType.Int32).Value = movieId;

                var dt = new DataTable();
                da.Fill(dt);

                gv.DataSource = dt;
                gv.DataBind();

                if (dt.Rows.Count == 0)
                    lblMsg.Text = "No occupancy data found for this movie.";
            }
        }
    }
}