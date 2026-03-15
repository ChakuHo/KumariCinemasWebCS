<%@ Page Title="Movies" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Movies.aspx.cs" Inherits="KumariCinemasWebCS.Movies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card mt-3">
    <div class="card-body">
      <h4 class="mb-3">Movie Details</h4>
      <asp:Label ID="lblMsg" runat="server" />

      <div class="row">
        <div class="col-md-4">
          <label>Movie Title</label>
          <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle"
              ErrorMessage="Title required." CssClass="text-danger" Display="Dynamic"/>
        </div>
        <div class="col-md-2">
          <label>Duration (min)</label>
          <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-2">
          <label>Language</label>
          <asp:TextBox ID="txtLanguage" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-2">
          <label>Genre</label>
          <asp:TextBox ID="txtGenre" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-2">
          <label>Release Date</label>
          <asp:TextBox ID="txtReleaseDate" runat="server" CssClass="form-control" TextMode="Date" />
        </div>
      </div>

      <div class="mt-3">
        <asp:Button ID="btnAdd" runat="server" Text="Add Movie" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-secondary ml-2"
            CausesValidation="false" OnClick="btnClear_Click" />
      </div>

      <hr />

      <asp:GridView ID="gvMovies" runat="server" CssClass="table table-bordered table-striped"
          AutoGenerateColumns="false" DataKeyNames="MovieID"
          OnRowEditing="gvMovies_RowEditing"
          OnRowCancelingEdit="gvMovies_RowCancelingEdit"
          OnRowUpdating="gvMovies_RowUpdating"
          OnRowDeleting="gvMovies_RowDeleting">
        <Columns>
          <asp:BoundField DataField="MovieID" HeaderText="MovieID" ReadOnly="true" />

          <asp:TemplateField HeaderText="Title">
            <ItemTemplate><%# Eval("MovieTitle") %></ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtTitleEdit" runat="server" CssClass="form-control"
                  Text='<%# Bind("MovieTitle") %>' />
            </EditItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderText="Duration">
            <ItemTemplate><%# Eval("Duration") %></ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtDurationEdit" runat="server" CssClass="form-control"
                  Text='<%# Bind("Duration") %>' />
            </EditItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderText="Language">
            <ItemTemplate><%# Eval("Language") %></ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtLanguageEdit" runat="server" CssClass="form-control"
                  Text='<%# Bind("Language") %>' />
            </EditItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderText="Genre">
            <ItemTemplate><%# Eval("Genre") %></ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtGenreEdit" runat="server" CssClass="form-control"
                  Text='<%# Bind("Genre") %>' />
            </EditItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderText="ReleaseDate">
            <ItemTemplate><%# Eval("ReleaseDate", "{0:yyyy-MM-dd}") %></ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtReleaseEdit" runat="server" CssClass="form-control"
                  TextMode="Date" Text='<%# Bind("ReleaseDate", "{0:yyyy-MM-dd}") %>' />
            </EditItemTemplate>
          </asp:TemplateField>

          <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
      </asp:GridView>
    </div>
  </div>
</asp:Content>