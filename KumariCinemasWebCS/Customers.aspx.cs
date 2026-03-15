using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using KumariCinemasWebCS.DAL;

namespace KumariCinemasWebCS
{
    public partial class Customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";
                lblMsg.CssClass = "";
                BindGrid();
            }
        }

        // Keep keyword between paging/edit/update
        private string CurrentKeyword
        {
            get { return (ViewState["KW"] ?? "").ToString(); }
            set { ViewState["KW"] = value ?? ""; }
        }

        private void BindGrid(string keyword = null)
        {
            CurrentKeyword = keyword ?? CurrentKeyword;

            string sql = @"SELECT CustomerID, Username, Address, Email, UserPassword
                           FROM CUSTOMER";

            bool hasK = !string.IsNullOrWhiteSpace(CurrentKeyword);

            if (hasK)
                sql += " WHERE LOWER(Username) LIKE :pK OR LOWER(Email) LIKE :pK";

            sql += " ORDER BY CustomerID";

            using (var conn = Db.GetConn())
            using (var cmd = new OracleCommand(sql, conn))
            using (var da = new OracleDataAdapter(cmd))
            {
                if (hasK)
                    cmd.Parameters.Add(":pK", OracleDbType.Varchar2).Value = "%" + CurrentKeyword.Trim().ToLower() + "%";

                var dt = new DataTable();
                da.Fill(dt);

                gvCustomers.DataSource = dt;
                gvCustomers.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    @"INSERT INTO CUSTOMER(CustomerID, Username, Address, Email, UserPassword)
                      VALUES (SEQ_CUSTOMER.NEXTVAL, :pUsername, :pAddress, :pEmail, :pPassword)", conn))
                {
                    cmd.Parameters.Add(":pUsername", OracleDbType.Varchar2).Value = txtUsername.Text.Trim();

                    string addr = (txtAddress.Text ?? "").Trim();
                    cmd.Parameters.Add(":pAddress", OracleDbType.Varchar2).Value =
                        string.IsNullOrWhiteSpace(addr) ? (object)DBNull.Value : addr;

                    cmd.Parameters.Add(":pEmail", OracleDbType.Varchar2).Value = txtEmail.Text.Trim();
                    cmd.Parameters.Add(":pPassword", OracleDbType.Varchar2).Value = txtPassword.Text.Trim();

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                ClearForm();
                BindGrid(CurrentKeyword);

                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Customer added successfully.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Insert failed: " + ex.Message;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            lblMsg.Text = "";
            lblMsg.CssClass = "";
        }

        private void ClearForm()
        {
            txtUsername.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
        }

        // SEARCH/RESET
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvCustomers.PageIndex = 0;
            BindGrid(txtSearch.Text);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            CurrentKeyword = "";
            gvCustomers.PageIndex = 0;
            BindGrid();
        }

        // PAGING
        protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomers.PageIndex = e.NewPageIndex;
            BindGrid(CurrentKeyword);
        }

        // INLINE EDIT
        protected void gvCustomers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCustomers.EditIndex = e.NewEditIndex;
            BindGrid(CurrentKeyword);
        }

        protected void gvCustomers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCustomers.EditIndex = -1;
            BindGrid(CurrentKeyword);
        }

        protected void gvCustomers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int customerId = Convert.ToInt32(gvCustomers.DataKeys[e.RowIndex].Value);

            var row = gvCustomers.Rows[e.RowIndex];
            string username = ((TextBox)row.FindControl("txtUsernameEdit")).Text.Trim();
            string address = ((TextBox)row.FindControl("txtAddressEdit")).Text.Trim();
            string email = ((TextBox)row.FindControl("txtEmailEdit")).Text.Trim();
            string password = ((TextBox)row.FindControl("txtPasswordEdit")).Text.Trim();

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    @"UPDATE CUSTOMER
                      SET Username = :pUsername,
                          Address = :pAddress,
                          Email = :pEmail,
                          UserPassword = :pPassword
                      WHERE CustomerID = :pCustomerID", conn))
                {
                    cmd.Parameters.Add(":pUsername", OracleDbType.Varchar2).Value = username;
                    cmd.Parameters.Add(":pAddress", OracleDbType.Varchar2).Value =
                        string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address;
                    cmd.Parameters.Add(":pEmail", OracleDbType.Varchar2).Value = email;
                    cmd.Parameters.Add(":pPassword", OracleDbType.Varchar2).Value = password;
                    cmd.Parameters.Add(":pCustomerID", OracleDbType.Int32).Value = customerId;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvCustomers.EditIndex = -1;
                BindGrid(CurrentKeyword);

                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Customer updated.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Update failed: " + ex.Message;
            }
        }

        protected void gvCustomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int customerId = Convert.ToInt32(gvCustomers.DataKeys[e.RowIndex].Value);

            try
            {
                using (var conn = Db.GetConn())
                using (var cmd = new OracleCommand(
                    "DELETE FROM CUSTOMER WHERE CustomerID = :pCustomerID", conn))
                {
                    cmd.Parameters.Add(":pCustomerID", OracleDbType.Int32).Value = customerId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                BindGrid(CurrentKeyword);

                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Customer deleted.";
            }
            catch (OracleException ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Delete failed (customer may be referenced by Booking): " + ex.Message;
            }
        }

        // DELETE CONFIRM POPUP
        protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // find Delete LinkButton in CommandField and attach confirm
                foreach (Control c in e.Row.Cells[e.Row.Cells.Count - 1].Controls)
                {
                    var btn = c as LinkButton;
                    if (btn != null && btn.CommandName == "Delete")
                    {
                        btn.OnClientClick = "return confirm('Are you sure you want to delete this customer?');";
                    }
                }
            }
        }
    }
}