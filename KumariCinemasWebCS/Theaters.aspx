<%@ Page Title="TheaterCityHall" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Theaters.aspx.cs" Inherits="KumariCinemasWebCS.Theaters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card mt-3">
    <div class="card-body">
      <h4 class="mb-3">TheaterCityHall Details</h4>

      <asp:Label ID="lblMsg" runat="server" />

      <div class="row">
        <div class="col-md-6">
          <label>Theatre Name</label>
          <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName"
              ErrorMessage="Theatre name required." CssClass="text-danger" Display="Dynamic" />
        </div>

        <div class="col-md-6">
          <label>City</label>
          <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCity"
              ErrorMessage="City required." CssClass="text-danger" Display="Dynamic" />
        </div>
      </div>

      <div class="mt-3">
        <asp:Button ID="btnAdd" runat="server" Text="Add TheaterCityHall" CssClass="btn btn-primary"
            OnClick="btnAdd_Click" />
        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-secondary ml-2"
            CausesValidation="false" OnClick="btnClear_Click" />
      </div>

      <hr />

      <asp:GridView ID="gvTheaters" runat="server"
          CssClass="table table-striped table-bordered"
          AutoGenerateColumns="false"
          DataKeyNames="TheatreID"
          OnRowEditing="gvTheaters_RowEditing"
          OnRowCancelingEdit="gvTheaters_RowCancelingEdit"
          OnRowUpdating="gvTheaters_RowUpdating"
          OnRowDeleting="gvTheaters_RowDeleting">
        <Columns>
          <asp:BoundField DataField="TheatreID" HeaderText="TheatreID" ReadOnly="true" />

          <asp:TemplateField HeaderText="Theatre Name">
            <ItemTemplate><%# Eval("TheatreName") %></ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtNameEdit" runat="server" CssClass="form-control"
                  Text='<%# Bind("TheatreName") %>' />
            </EditItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderText="City">
            <ItemTemplate><%# Eval("TheatreCity") %></ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtCityEdit" runat="server" CssClass="form-control"
                  Text='<%# Bind("TheatreCity") %>' />
            </EditItemTemplate>
          </asp:TemplateField>

          <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
      </asp:GridView>

    </div>
  </div>
</asp:Content>