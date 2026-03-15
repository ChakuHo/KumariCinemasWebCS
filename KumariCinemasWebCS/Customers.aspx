<%@ Page Title="Customers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Customers.aspx.cs" Inherits="KumariCinemasWebCS.Customers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card mt-3">
        <div class="card-body">
            <h4 class="mb-3">Customer Details</h4>

            <asp:Label ID="lblMsg" runat="server" />

            <!-- ADD FORM -->
            <div class="row">
                <div class="col-md-3">
                    <label>Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server"
                        ControlToValidate="txtUsername" ErrorMessage="Username is required."
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <div class="col-md-3">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="Email is required."
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <div class="col-md-3">
                    <label>Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ControlToValidate="txtPassword" ErrorMessage="Password is required."
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <div class="col-md-3">
                    <label>Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" />
                </div>
            </div>

            <div class="mt-3">
                <asp:Button ID="btnAdd" runat="server" Text="Add Customer" CssClass="btn btn-primary"
                    OnClick="btnAdd_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-secondary ml-2"
                    CausesValidation="false" OnClick="btnClear_Click" />
            </div>

            <hr />

            <!-- SEARCH (does not trigger validators) -->
            <div class="row mb-2">
                <div class="col-md-6">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                        placeholder="Search by Username or Email..." />
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                        CausesValidation="false" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-outline-secondary ml-2"
                        CausesValidation="false" OnClick="btnReset_Click" />
                </div>
            </div>

            <!-- GRID -->
            <asp:GridView ID="gvCustomers" runat="server"
                CssClass="table table-striped table-bordered"
                AutoGenerateColumns="false"
                DataKeyNames="CustomerID"
                AllowPaging="true" PageSize="8"
                OnPageIndexChanging="gvCustomers_PageIndexChanging"
                OnRowEditing="gvCustomers_RowEditing"
                OnRowCancelingEdit="gvCustomers_RowCancelingEdit"
                OnRowUpdating="gvCustomers_RowUpdating"
                OnRowDeleting="gvCustomers_RowDeleting"
                OnRowDataBound="gvCustomers_RowDataBound">

                <Columns>
                    <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="true" />

                    <asp:TemplateField HeaderText="Username">
                        <ItemTemplate><%# Eval("Username") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUsernameEdit" runat="server" CssClass="form-control"
                                Text='<%# Bind("Username") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate><%# Eval("Address") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAddressEdit" runat="server" CssClass="form-control"
                                Text='<%# Bind("Address") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate><%# Eval("Email") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmailEdit" runat="server" CssClass="form-control"
                                Text='<%# Bind("Email") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Password">
                        <ItemTemplate><%# Eval("UserPassword") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPasswordEdit" runat="server" CssClass="form-control"
                                Text='<%# Bind("UserPassword") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>

        </div>
    </div>

</asp:Content>