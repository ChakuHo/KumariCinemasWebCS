<%@ Page Title="Tickets" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Tickets.aspx.cs" Inherits="KumariCinemasWebCS.Tickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card mt-3">
    <div class="card-body">
      <h4 class="mb-3">Ticket Details</h4>
      <asp:Label ID="lblMsg" runat="server" />

      <div class="row">
        <div class="col-md-4">
          <label>Booking (FK)</label>
          <asp:DropDownList ID="ddlBooking" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-4">
          <label>Seat Number</label>
          <asp:TextBox ID="txtSeat" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSeat"
              ErrorMessage="Seat required." CssClass="text-danger" Display="Dynamic"/>
        </div>
        <div class="col-md-4">
          <label>Status</label>
          <asp:DropDownList ID="ddlTStatus" runat="server" CssClass="form-control">
            <asp:ListItem Text="BOOKED" Value="BOOKED" />
            <asp:ListItem Text="PAID" Value="PAID" />
            <asp:ListItem Text="CANCELLED" Value="CANCELLED" />
          </asp:DropDownList>
        </div>
      </div>

      <div class="mt-3">
        <asp:Button ID="btnAdd" runat="server" Text="Add Ticket" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
      </div>

      <hr />

      <asp:GridView ID="gvTickets" runat="server" CssClass="table table-bordered table-striped"
          AutoGenerateColumns="false" DataKeyNames="TicketID"
          OnRowEditing="gvTickets_RowEditing"
          OnRowCancelingEdit="gvTickets_RowCancelingEdit"
          OnRowUpdating="gvTickets_RowUpdating"
          OnRowDeleting="gvTickets_RowDeleting"
          OnRowDataBound="gvTickets_RowDataBound">
        <Columns>
          <asp:BoundField DataField="TicketID" HeaderText="TicketID" ReadOnly="true" />
          <asp:TemplateField HeaderText="BookingID (FK)">
            <ItemTemplate><%# Eval("BookingID") %></ItemTemplate>
            <EditItemTemplate>
              <asp:DropDownList ID="ddlBookingEdit" runat="server" CssClass="form-control" />
              <asp:HiddenField ID="hidBookingId" runat="server" Value='<%# Eval("BookingID") %>' />
            </EditItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderText="SeatNumber">
            <ItemTemplate><%# Eval("SeatNumber") %></ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtSeatEdit" runat="server" CssClass="form-control" Text='<%# Bind("SeatNumber") %>' />
            </EditItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderText="TicketStatus">
            <ItemTemplate><%# Eval("TicketStatus") %></ItemTemplate>
            <EditItemTemplate>
              <asp:DropDownList ID="ddlStatusEdit" runat="server" CssClass="form-control">
                <asp:ListItem Text="BOOKED" Value="BOOKED" />
                <asp:ListItem Text="PAID" Value="PAID" />
                <asp:ListItem Text="CANCELLED" Value="CANCELLED" />
              </asp:DropDownList>
              <asp:HiddenField ID="hidStatus" runat="server" Value='<%# Eval("TicketStatus") %>' />
            </EditItemTemplate>
          </asp:TemplateField>

          <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
      </asp:GridView>
    </div>
  </div>
</asp:Content>