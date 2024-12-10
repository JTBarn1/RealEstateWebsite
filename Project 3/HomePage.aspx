<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Project_3.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Style.css"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
</head>
<body>
    <form id="form1" runat="server">
        <header class ="row align-items-center">
            <div class ="col-3 text-start ps-5">
            </div>
            <h1 class ="col-6">Homes R Us</h1>
            <div class ="col-3 text-end pe-5">
                <div id ="divLogin" runat="server">
                    <asp:Button ID="RealtorLogin" CssClass="btn btn-light" runat="server" Text="Realtor Login" OnClick="RealtorLogin_Click" />
                </div>
                <div id="divAgentUtils" runat="server" visible="false">
                     <asp:Label ID="lblHello" runat="server" Text="Hello, Jason"></asp:Label>
                     <asp:Button ID="btnAddListing" runat="server" CssClass="btn btn-light" Text="Add new listing" OnClick="btnAddListing_Click" />
                     <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-light" Text="Logout" OnClick="btnLogout_Click" />
                </div>
            </div>
        </header>
        <div class="container-fluid">
        <div id="standardViewDiv" runat="server">
            <div id ="HomeSearchFeatures" runat="server">
        <p class="fs-1">Search By:</p>
       <div class="search-container">
            <p class="mb-0">Location:</p>
            <asp:TextBox ID="txtCitySearch" runat="server" CssClass="form-control" Placeholder="City" Style="width: 150px;"></asp:TextBox>
            <asp:TextBox ID="txtStateSearch" runat="server" CssClass="form-control" Placeholder="State" Style="width: 100px;" MaxLength="2"></asp:TextBox>
            <asp:TextBox ID="txtZipSearch" runat="server" CssClass="form-control" Placeholder="Zip Code" Style="width: 100px;"></asp:TextBox>
            <p class="mb-0">Price:</p>
            <asp:TextBox ID="txtMinPrice" runat="server" CssClass="form-control" Placeholder="Min" Style="width: 120px;"></asp:TextBox>
            <asp:TextBox ID="txtMaxPrice" runat="server" CssClass="form-control" Placeholder="Max" Style="width: 120px;"></asp:TextBox>
            <p class="mb-0">Size and Type:</p>
            <asp:TextBox ID="txtSQFT" runat="server" CssClass="form-control" Placeholder="Min Sqft" Style="width: 130px;"></asp:TextBox>
            <asp:DropDownList ID="ddlHomeType" runat="server" CssClass="form-select" Style="width: 140px;">
                <asp:ListItem Text="Home Type:" Value=""></asp:ListItem>
                <asp:ListItem Text="Single Family" Value="Single Family"></asp:ListItem>
                <asp:ListItem Text="Townhome" Value="Townhome"></asp:ListItem>
                <asp:ListItem Text="Multi Family" Value="Multi Family"></asp:ListItem>
                <asp:ListItem Text="Condo" Value="Condo"></asp:ListItem>
            </asp:DropDownList>
           </div>
           <br />
           <div class="search-container">
            Misc:
            <asp:DropDownList ID="ddlBeds" runat="server" CssClass="form-select" Style="width: 100px;">
                <asp:ListItem Text="Beds" Value=""></asp:ListItem>
                <asp:ListItem Text="1+" Value="1"></asp:ListItem>
                <asp:ListItem Text="2+" Value="2"></asp:ListItem>
                <asp:ListItem Text="3+" Value="3"></asp:ListItem>
                <asp:ListItem Text="4+" Value="4"></asp:ListItem>
                <asp:ListItem Text="5+" Value="5"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlBaths" runat="server" CssClass="form-select" Style="width: 100px;">
                <asp:ListItem Text="Baths" Value=""></asp:ListItem>
                <asp:ListItem Text="1+" Value="1"></asp:ListItem>
                <asp:ListItem Text="2+" Value="2"></asp:ListItem>
                <asp:ListItem Text="3+" Value="3"></asp:ListItem>
                <asp:ListItem Text="4+" Value="4"></asp:ListItem>
                <asp:ListItem Text="5+" Value="5"></asp:ListItem>
            </asp:DropDownList>
           <p class="mb-0">Amenities:</p>
           <asp:CheckBoxList ID="cbListAmenities" runat="server" RepeatDirection="Horizontal" CssClass ="boxSpacing">
               <asp:ListItem Value="Pool">Pool</asp:ListItem>
               <asp:ListItem Value="Waterfront">Waterfront</asp:ListItem>
               <asp:ListItem Value="Finished Basement">Finished Basement</asp:ListItem>
               <asp:ListItem Value="Deck">Deck</asp:ListItem>
            </asp:CheckBoxList>
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search!" OnClick="btnSearch_Click" />
               <asp:Button ID="btnReset" runat="server" CssClass="btn btn-secondary" Text="Reset" OnClick="btnReset_Click"/>
           </div>
                </div>
                <div id ="HomeContainer" runat="server">
                    <asp:PlaceHolder ID="phHouses" runat="server"></asp:PlaceHolder>
                </div>
            </div>
        <div id="realtorViewDiv" visible="false" runat="server">
            <div id="divListingsDashboard" runat="server">
            <div class="container mt-4">
                <h1>Your Listings</h1>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Address</th>
                            <th>Price</th>
                            <th>Status</th>
                            <th>Showings</th>
                            <th>Offers</th>  
                            <th>Modify</th>
                        </tr>
                    </thead>
                    <tbody id="tbodyListings" runat="server">
                        <!-- Dynamic rows will be added here by C# code -->
                    </tbody>
                </table>
                </div>
            </div>
            <div id="divShowingsView" runat="server" visible="false">
                <div class="container mt-4">
                <h1 class="fs-1">Showings:</h1>
                <asp:Button ID="btnShowingsBack" class="btn btn-secondary" runat="server" Text="Go Back" OnClick="btnShowingsBack_Click" />
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Phone</th>
                            <th>Email</th>
                            <th>Date</th>
                            <th>Time</th>
                        </tr>
                    </thead>
                    <tbody id="tbodyShowings" runat="server">
                    </tbody>
                </table>
               </div>
            </div>
             <div id="divOffersView" runat="server" visible="false">
                 <div class="container mt-4">
                    <h1 class="fs-1">Offers:</h1>
                    <asp:Button ID="btnOffersBack" class="btn btn-secondary" runat="server" Text="Go Back" OnClick="btnOffersBack_Click" />
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Price</th>
                                <th>Sale Type</th>
                                <th>Contingencies</th>
                                <th>Move in</th>
                                <th>Sell Home</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody id="tbodyOffers" runat="server">
                        </tbody>
                    </table>
                </div>
             </div>
            </div>
            
       
    
        </div>
        </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
