<%@ Page Title="User Tickets" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserTickets.aspx.cs" Inherits="KumariCinemasWebCS.UserTickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card mt-3">
    <div class="card-body">
      <h4 class="mb-3">User Ticket (Last 6 Months - Paid Only)</h4>

      <div class="row">
        <div class="col-md-6">
          <label>Select Customer</label>
          <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 d-flex align-items-end">
          <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
              OnClick="btnSearch_Click" />
        </div>
      </div>

      <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mt-2" />

      <hr />

      <asp:GridView ID="gv" runat="server" CssClass="table table-bordered table-striped"
          AutoGenerateColumns="true" />
    </div>
  </div>
</asp:Content>