<%@ Page Title="Theatre Movies" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TheatreMovies.aspx.cs" Inherits="KumariCinemasWebCS.TheatreMovies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card mt-3">
    <div class="card-body">
      <h4 class="mb-3">TheaterCityHall Movie (Movies + Showtimes)</h4>

      <div class="row">
        <div class="col-md-6">
          <label>Select Theatre</label>
          <asp:DropDownList ID="ddlTheatre" runat="server" CssClass="form-control" />
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