<%@ Page Title="Bookings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Bookings.aspx.cs" Inherits="KumariCinemasWebCS.Bookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card mt-3">
    <div class="card-body">
      <h4 class="mb-3">Booking Details</h4>
      <asp:Label ID="lblMsg" runat="server" />

      <div class="row">
        <div class="col-md-4">
          <label>Customer</label>
          <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-4">
          <label>Show</label>
          <asp:DropDownList ID="ddlShow" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-4">
          <label>Status</label>
          <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
            <asp:ListItem Text="BOOKED" Value="BOOKED" />
            <asp:ListItem Text="PAID" Value="PAID" />
            <asp:ListItem Text="CANCELLED" Value="CANCELLED" />
            <asp:ListItem Text="EXPIRED" Value="EXPIRED" />
          </asp:DropDownList>
        </div>
      </div>

      <div class="mt-3">
        <asp:Button ID="btnAdd" runat="server" Text="Add Booking" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
      </div>

      <hr />

      <asp:GridView ID="gvBookings" runat="server" CssClass="table table-bordered table-striped"
          AutoGenerateColumns="false" DataKeyNames="BookingID"
          OnRowEditing="gvBookings_RowEditing"
          OnRowCancelingEdit="gvBookings_RowCancelingEdit"
          OnRowUpdating="gvBookings_RowUpdating"
          OnRowDeleting="gvBookings_RowDeleting">
        <Columns>
          <asp:BoundField DataField="BookingID" HeaderText="BookingID" ReadOnly="true" />
          <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" />
          <asp:BoundField DataField="ShowID" HeaderText="ShowID" />
          <asp:BoundField DataField="BookingTime" HeaderText="BookingTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" ReadOnly="true" />
          <asp:BoundField DataField="ExpiresAt" HeaderText="ExpiresAt" DataFormatString="{0:yyyy-MM-dd HH:mm}" ReadOnly="true" />
          <asp:TemplateField HeaderText="BookingStatus">
            <ItemTemplate><%# Eval("BookingStatus") %></ItemTemplate>
            <EditItemTemplate>
              <asp:DropDownList ID="ddlStatusEdit" runat="server" CssClass="form-control">
                <asp:ListItem Text="BOOKED" Value="BOOKED" />
                <asp:ListItem Text="PAID" Value="PAID" />
                <asp:ListItem Text="CANCELLED" Value="CANCELLED" />
                <asp:ListItem Text="EXPIRED" Value="EXPIRED" />
              </asp:DropDownList>
            </EditItemTemplate>
          </asp:TemplateField>
          <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
      </asp:GridView>
    </div>
  </div>
</asp:Content>