<%@ Page Title="Movie Occupancy Top 3" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MovieOccupancy.aspx.cs" Inherits="KumariCinemasWebCS.MovieOccupancy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card mt-3">
    <div class="card-body">
      <h4 class="mb-3">MovieTheatreCityHall Occupancy Performer (Top 3)</h4>
      <p class="text-muted mb-2">Only PAID tickets are counted as seat occupancy.</p>

      <div class="row">
        <div class="col-md-6">
          <label>Select Movie</label>
          <asp:DropDownList ID="ddlMovie" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 d-flex align-items-end">
          <asp:Button ID="btnSearch" runat="server" Text="Search Top 3" CssClass="btn btn-primary"
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