<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="KumariCinemasWebCS._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mt-3">
        <h3 class="mb-3">Dashboard</h3>

        <asp:Label ID="lblMsg" runat="server" CssClass="text-danger" />

        <!-- Hidden values for chart -->
        <asp:HiddenField ID="hfPaid" runat="server" />
        <asp:HiddenField ID="hfBooked" runat="server" />
        <asp:HiddenField ID="hfCancelled" runat="server" />

        <!-- COUNT CARDS -->
        <div class="row">
            <div class="col-md-3 mb-3">
                <div class="card"><div class="card-body">
                    <div class="text-muted">Customers</div>
                    <h3 class="mb-0"><asp:Literal ID="litCustomers" runat="server" Text="0" /></h3>
                </div></div>
            </div>

            <div class="col-md-3 mb-3">
                <div class="card"><div class="card-body">
                    <div class="text-muted">Movies</div>
                    <h3 class="mb-0"><asp:Literal ID="litMovies" runat="server" Text="0" /></h3>
                </div></div>
            </div>

            <div class="col-md-3 mb-3">
                <div class="card"><div class="card-body">
                    <div class="text-muted">Theatres</div>
                    <h3 class="mb-0"><asp:Literal ID="litTheatres" runat="server" Text="0" /></h3>
                </div></div>
            </div>

            <div class="col-md-3 mb-3">
                <div class="card"><div class="card-body">
                    <div class="text-muted">Paid Tickets</div>
                    <h3 class="mb-0"><asp:Literal ID="litPaidTickets" runat="server" Text="0" /></h3>
                </div></div>
            </div>
        </div>

        <!-- QUICK LINKS -->
        <div class="card mt-2">
            <div class="card-body">
                <h5 class="mb-2">Quick Links</h5>

                <a class="btn btn-primary mr-2 mb-2" href="Customers.aspx">Customers</a>
                <a class="btn btn-primary mr-2 mb-2" href="Theaters.aspx">TheaterCityHall</a>
                <a class="btn btn-primary mr-2 mb-2" href="Movies.aspx">Movies</a>
                <a class="btn btn-primary mr-2 mb-2" href="Showtimes.aspx">Showtimes</a>
                <a class="btn btn-primary mr-2 mb-2" href="Tickets.aspx">Tickets</a>
                

                <!-- Complex forms 
                <a class="btn btn-primary mr-2 mb-2" href="Bookings.aspx">Bookings</a>
                <a class="btn btn-primary mr-2 mb-2" href="UserTickets.aspx">User Ticket</a>
                <a class="btn btn-primary mr-2 mb-2" href="TheatreMovies.aspx">Theatre Movie</a>
                <a class="btn btn-primary mb-2" href="MovieOccupancy.aspx">Occupancy Top 3</a>
                -->
            </div>
        </div>

        <div class="row mt-3">
            <!-- CHART -->
            <div class="col-lg-5 mb-3">
                <div class="card kc-fixed-card">
                    <div class="card-body">
                        <h5 class="mb-2">Ticket Status Overview</h5>
                        <div class="kc-chart-wrap">
                            <canvas id="ticketChart"></canvas>
                        </div>
                    </div>
                </div>
            </div>

            <!-- RECENT BOOKINGS -->
            <div class="col-lg-7 mb-3">
                <div class="card kc-fixed-card">
                    <div class="card-body">
                        <h5 class="mb-2">Recent Bookings (Top 5)</h5>

                        <div class="table-responsive">
                            <asp:GridView ID="gvRecentBookings" runat="server"
                                CssClass="table table-bordered table-striped mb-0"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="BookingID" HeaderText="BookingID" />
                                    <asp:BoundField DataField="Username" HeaderText="Username" />
                                    <asp:BoundField DataField="ShowID" HeaderText="ShowID" />
                                    <asp:BoundField DataField="ShowDate" HeaderText="ShowDate" />
                                    <asp:BoundField DataField="ShowTime" HeaderText="ShowTime" />
                                    <asp:BoundField DataField="BookingStatus" HeaderText="Status" />
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Chart.js -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js@4.4.1/dist/chart.umd.min.js"></script>
    <script>
        (function () {
            var paid = parseInt(document.getElementById('<%= hfPaid.ClientID %>').value || '0');
          var booked = parseInt(document.getElementById('<%= hfBooked.ClientID %>').value || '0');
        var cancelled = parseInt(document.getElementById('<%= hfCancelled.ClientID %>').value || '0');

            var canvas = document.getElementById('ticketChart');
            if (!canvas) return;

            new Chart(canvas, {
                type: 'doughnut',
                data: {
                    labels: ['PAID', 'BOOKED', 'CANCELLED'],
                    datasets: [{
                        data: [paid, booked, cancelled],
                        backgroundColor: ['#0a2558', '#6c757d', '#e63946']
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: { legend: { position: 'bottom' } }
                }
            });
        })();
    </script>
</asp:Content>