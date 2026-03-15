<%@ Page Title="Showtimes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Showtimes.aspx.cs" Inherits="KumariCinemasWebCS.Showtimes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card mt-3">
        <div class="card-body">
            <h4 class="mb-3">Showtimes Details</h4>

            <asp:Label ID="lblMsg" runat="server" />

            <!-- ADD FORM -->
            <div class="row">
                <div class="col-md-3">
                    <label>Show Date</label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date" />
                    <asp:RequiredFieldValidator ID="rfvDate" runat="server"
                        ControlToValidate="txtDate"
                        ErrorMessage="Show Date is required."
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <div class="col-md-3">
                    <label>Show Time Slot</label>
                    <asp:DropDownList ID="ddlSlot" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Morning" Value="Morning" />
                        <asp:ListItem Text="Day" Value="Day" />
                        <asp:ListItem Text="Evening" Value="Evening" />
                        <asp:ListItem Text="Night" Value="Night" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label>Holiday Status</label>
                    <asp:DropDownList ID="ddlHoliday" runat="server" CssClass="form-control">
                        <asp:ListItem Text="N" Value="N" />
                        <asp:ListItem Text="Y" Value="Y" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label>Release Week Flag</label>
                    <asp:DropDownList ID="ddlRelease" runat="server" CssClass="form-control">
                        <asp:ListItem Text="N" Value="N" />
                        <asp:ListItem Text="Y" Value="Y" />
                    </asp:DropDownList>
                </div>
            </div>

            <div class="mt-3">
                <asp:Button ID="btnAdd" runat="server" Text="Add Showtime"
                    CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear"
                    CssClass="btn btn-outline-secondary ml-2"
                    CausesValidation="false" OnClick="btnClear_Click" />
            </div>

            <hr />

            <!-- GRID -->
            <asp:GridView ID="gvShow" runat="server"
                CssClass="table table-bordered table-striped"
                AutoGenerateColumns="false"
                DataKeyNames="ShowID"
                OnRowEditing="gvShow_RowEditing"
                OnRowCancelingEdit="gvShow_RowCancelingEdit"
                OnRowUpdating="gvShow_RowUpdating"
                OnRowDeleting="gvShow_RowDeleting"
                OnRowDataBound="gvShow_RowDataBound">

                <Columns>
                    <asp:BoundField DataField="ShowID" HeaderText="ShowID" ReadOnly="true" />

                    <asp:TemplateField HeaderText="ShowDate">
                        <ItemTemplate>
                            <%# Eval("ShowDate", "{0:yyyy-MM-dd}") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDateEdit" runat="server" CssClass="form-control"
                                TextMode="Date" Text='<%# Bind("ShowDate", "{0:yyyy-MM-dd}") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ShowTime">
                        <ItemTemplate><%# Eval("ShowTime") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTimeEdit" runat="server" CssClass="form-control"
                                Text='<%# Bind("ShowTime") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="HolidayStatus">
                        <ItemTemplate><%# Eval("HolidayStatus") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlHolEdit" runat="server" CssClass="form-control">
                                <asp:ListItem Text="N" Value="N" />
                                <asp:ListItem Text="Y" Value="Y" />
                            </asp:DropDownList>
                            <asp:HiddenField ID="hidHol" runat="server" Value='<%# Eval("HolidayStatus") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ReleaseWeekFlag">
                        <ItemTemplate><%# Eval("ReleaseWeekFlag") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlRelEdit" runat="server" CssClass="form-control">
                                <asp:ListItem Text="N" Value="N" />
                                <asp:ListItem Text="Y" Value="Y" />
                            </asp:DropDownList>
                            <asp:HiddenField ID="hidRel" runat="server" Value='<%# Eval("ReleaseWeekFlag") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>

        </div>
    </div>

</asp:Content>