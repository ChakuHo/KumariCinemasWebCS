<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Contact.aspx.cs" Inherits="KumariCinemasWebCS.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mt-3">
        <div class="card-body">
            <h4>Contact</h4>
            <p class="text-muted">Coursework prototype support/contact page.</p>

            <div class="row">
                <div class="col-md-6">
                    <h6>Info</h6>
                    <p class="mb-1"><b>Project:</b> KumariCinemas Ticketing System</p>
                    <p class="mb-1"><b>Tech:</b> Oracle 11g + ASP.NET WebForms (C#)</p>
                    <p class="mb-0"><b>Email:</b> no-reply@kumaricinemas.com</p>
                </div>

                <div class="col-md-6">
                    <h6>Send a message (demo)</h6>
                    <asp:Label ID="lblMsg" runat="server" />

                    <label>Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />

                    <label class="mt-2">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />

                    <label class="mt-2">Message</label>
                    <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />

                    <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary mt-3"
                        OnClick="btnSend_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>