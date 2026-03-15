<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="KumariCinemasWebCS.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mt-3">
        <div class="card-body">
            <h4>About KumariCinemas</h4>
            <p class="text-muted mb-0">
                This is a prototype cinema ticketing system developed for coursework.
                It demonstrates database design (3NF), Oracle 11g implementation, and an ASP.NET WebForms application
                with CRUD operations and complex reporting queries.
            </p>

            <hr />

            <ul class="mb-0">
                <li>Oracle 11g database (normalized ERD)</li>
                <li>CRUD forms: Customers, TheatreCityHall, Movies, Showtimes, Tickets</li>
                <li>Complex reports: User Tickets (6 months), Theatre Movies & Showtimes, Occupancy Top 3</li>
            </ul>
        </div>
    </div>
</asp:Content>