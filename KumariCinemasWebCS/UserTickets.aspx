<%@ Page Title="User Ticket" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserTickets.aspx.cs" Inherits="KumariCinemasWebCS.UserTickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mt-3">
        <div class="card-body">
            <h4 class="mb-3">User Ticket (Last 6 Months - Paid Only)</h4>

            <!-- FIND CUSTOMER -->
            <div class="row">
                <div class="col-md-8">
                    <label>Find Customer (Email/Username)</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtFind" runat="server" CssClass="form-control"
                            placeholder="e.g. gmail or bigyan" />
                        <span class="input-group-btn">
                            <asp:Button ID="btnFind" runat="server" Text="Find"
                                CssClass="btn btn-default" CausesValidation="false"
                                OnClick="btnFind_Click" />
                        </span>
                    </div>
                </div>
            </div>

            <!-- SELECT + SEARCH -->
            <div class="row mt-3">
                <div class="col-md-8">
                    <label>Select Customer</label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-4" style="padding-top:25px;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search Tickets"
                        CssClass="btn btn-primary btn-block"
                        OnClick="btnSearch_Click" />
                </div>
            </div>

            <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mt-2" />

            <hr />

            <div class="table-responsive">
                <asp:GridView ID="gv" runat="server"
                    CssClass="table table-bordered table-striped"
                    AutoGenerateColumns="true" />
            </div>
        </div>
    </div>
</asp:Content>