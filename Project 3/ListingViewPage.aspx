<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListingViewPage.aspx.cs" Inherits="Project_3.ListingViewPage" %>

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
            <asp:Button ID="btnBack" CssClass="btn btn-light" runat="server" Text="Go Back" Onclick="btnBack_Click"/>
        </div>
        <h1 class ="col-6">Homes R Us</h1>
    </header>
        <div class="InfoContainer" id ="divInfoContainer" runat="server">
        <div id="HomeImages" class="carousel slide">
            <div id="ImagesCarousel" class="carousel-inner" runat="server">
            </div>
            <button class="carousel-control-prev" type="button" data-bs-target="#HomeImages" data-bs-slide="prev">
              <span class="carousel-control-prev-icon" aria-hidden="true"></span>
              <span class="visually-hidden">Previous</span>
            </button>
            <button class="carousel-control-next" type="button" data-bs-target="#HomeImages" data-bs-slide="next">
              <span class="carousel-control-next-icon" aria-hidden="true"></span>
              <span class="visually-hidden">Next</span>
            </button>
          </div>
        <div class = "row">
            <div class = "col-10 fs-1" id="divHomeMainInfo" runat="Server"></div>
            <div class = "col-2 d-flex justify-content-end"><button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#TourModal">Book a showing</button></div>
        </div>
        <asp:label ID="lblAddress" class = "fs-5" runat="server"/> 
        <div class = "row">
            <div class = "col-10 fs-2">
                <asp:Label ID="lblHomeType" runat="server" Text="Label"></asp:Label>

            </div>
            <div class = "col-2 d-flex justify-content-end"><button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#OfferModal">Submit an Offer</button></div>
        </div>

        <div class = "row justify-content-center">
            <div class = "col-4 fs-1 text-center fw-boldish">Home Amenities:</div>
        </div>
        <div class ="row row-cols-4 justify-content-md-center" id="divAmenityContainer" runat="server">
          
        </div>
        <div class = "row">
            <div class = "col-10 fs-2">More Info:</div>
         </div>
        <div class = "row">
                <asp:label ID="lblDesc" runat="server" class = "col-10 fs-5"/>
        </div>
        <div class = "row">
            <div class = "col-6 fs-2">Utilities:</div>
            <div class = "col-6 fs-2">Other info:</div>
        </div>
        <div class = "row">
            <div class = "col-6 fs-5" id="divUtilities" runat="server">
            </div>
            <div class = "col-6 fs-5 " id="divOther" runat="server">
            </div>
        </div>
        <div class = "row">
            <div class = "col-6 fs-2">Agency Info:</div>
        </div>
        <div class = "row"  id="divAgentInfo" runat="server">
            <div class = "col-6 fs-6">
                </div>  
        </div>
         <h4 class="col-10 fs-2">Rooms:</h4>
         <div id="divRoomTable" runat="server" class="mb-3">
             <table class="table">
                 <thead>
                     <tr>
                         <th>Type</th>
                         <th>Length</th>
                         <th>Width</th>
                         <th>Size</th>
                     </tr>
                 </thead>
                 <tbody id="tbodyRooms" runat="server">
                 </tbody>
             </table>
         </div>
        </div>
        <div class="modal fade" id="TourModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h1 class="modal-title fs-5" id="exampleModalLabel">Submit Tour Request</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                Enter Contact Info:
                <div class="mb-6">
                    <asp:TextBox ID="txtTourFirstName" runat="server" placeholder="First Name"></asp:TextBox>
                    <asp:TextBox ID="txtTourLastName" runat="server" placeholder="Last Name"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="reqTourFirst" runat="server" ControlToValidate="txtTourFirstName" ErrorMessage="* First Name Required" ForeColor="#CC0000" validationGroup ="grpTour"/>
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:RequiredFieldValidator ID="reqTourLast" runat="server" ControlToValidate="txtTourLastName" ErrorMessage="* Last Name Required" ForeColor="#CC0000" validationGroup ="grpTour"/>
                    </div>
                <div class="mb-6">
                <asp:TextBox ID="txtTourEmail" runat="server" placeholder="Email:"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="reqTourEmail" runat="server" ControlToValidate="txtTourEmail" ErrorMessage="* Email Required" ForeColor="#CC0000" validationGroup ="grpTour"/>
                </div>
                  <div class="mb-6">
                    <asp:TextBox ID="txtTourPhone" runat="server" placeholder="Phone:" TextMode="Phone"></asp:TextBox>
                      <br />
                        <asp:RequiredFieldValidator ID="reqTourPhone" runat="server" ControlToValidate="txtTourPhone" ErrorMessage="* Phone Required" ForeColor="#CC0000" validationGroup ="grpTour"/>
                  </div>
                  Select A Time:
                  <div class="mb-6">
                  <asp:TextBox ID="txtDate" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="reqDate" runat="server" ControlToValidate="txtDate" ErrorMessage="* Date Required" ForeColor="#CC0000" validationGroup ="grpTour"/>
                  </div>
                  <small>Appointment times last 30 minutes. please only select times in 30 minute increments.</small>
                  </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <asp:button class="btn btn-primary" ID="btnSubmitTourRequest" runat="server" Text="Submit Request" OnClick="btnSubmitTourRequest_Click" ValidationGroup ="grpTour" />
              </div>
            </div>
              </div>
          </div>
            <div class="modal fade" id="OfferModal" tabindex="-1" aria-labelledby="exampleOfferLabel" aria-hidden="true">
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleOfferLabel">Submit Offer</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                    Enter Contact Info:
                    <div class="mb-6">
                        <asp:TextBox ID="txtOfferFirstName" runat="server" placeholder="First Name"></asp:TextBox>
                        <asp:TextBox ID="txtOfferLastName" runat="server" placeholder="Last Name"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="reqFirstOffer" runat="server" ControlToValidate="txtOfferFirstName" ErrorMessage="* First Name Required" ForeColor="#CC0000" validationGroup ="grpOffer"/>
                        &nbsp;
                        &nbsp;
                        &nbsp;
                        <asp:RequiredFieldValidator ID="reqLastOffer" runat="server" ControlToValidate="txtOfferLastName" ErrorMessage="* Last Name Required" ForeColor="#CC0000" validationGroup ="grpOffer"/>
                        </div>
                    <div class="mb-6">
                    <asp:TextBox ID="txtOfferEmail" runat="server" placeholder="Email:"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="reqOfferEmail" runat="server" ControlToValidate="txtOfferEmail" ErrorMessage="* Email Required" ForeColor="#CC0000" validationGroup ="grpOffer"/>
                    </div>
                      <div class="mb-6">
                        <asp:TextBox ID="txtOfferPhoneNum" runat="server" placeholder="Phone:"  TextMode="Phone"></asp:TextBox>
                          <br />
                            <asp:RequiredFieldValidator ID="reqOfferPhone" runat="server" ControlToValidate="txtOfferPhoneNum" ErrorMessage="* Phone Required" ForeColor="#CC0000" validationGroup ="grpOffer"/>
                      </div>
                      What Price are you offering?
                      <div class="mb-6">
                          <asp:TextBox ID="txtOfferPrice" runat="server" placeholder="Price"></asp:TextBox>
                          <br />
                          <asp:RequiredFieldValidator ID="reqPrice" runat="server" ControlToValidate="txtOfferPrice" ErrorMessage="* Price Required" ForeColor="#CC0000" validationGroup ="grpOffer"/>
                       </div>
                      When would you like to move in?:
                      <div class="mb-6">
                      <asp:TextBox ID="txtOfferDate" runat="server" TextMode="Date"></asp:TextBox><br />
                      <asp:RequiredFieldValidator ID="reqOfferDate" runat="server" ControlToValidate="txtOfferDate" ErrorMessage="* Date Required" ForeColor="#CC0000" validationGroup ="grpOffer"/>
                      </div>
                      Do you need to sell your current house?
                      <asp:CheckBox ID="cbSellHouse" runat="server" /><br /><br />
                      Type Of Sale:
                      <asp:DropDownList ID="ddlPurchaseType" runat="server">
                          <asp:ListItem>Mortgage</asp:ListItem>
                          <asp:ListItem>Cash</asp:ListItem>
                      </asp:DropDownList><br /><br />
                      Contingencies:
                      <asp:CheckBoxList ID="CblContingencies" runat="server">
                          <asp:ListItem Value="1">Full Inspection</asp:ListItem>
                           <asp:ListItem Value="2">Fix Roof</asp:ListItem>
                           <asp:ListItem Value="3">Fix Plumbing</asp:ListItem>
                            <asp:ListItem Value="4">Fix Foundation</asp:ListItem>
                           <asp:ListItem Value="5">Fix Wall Damage</asp:ListItem>
                          <asp:ListItem Value="6">Fix Flooring</asp:ListItem>
                          <asp:ListItem Value="7">Fix HVAC</asp:ListItem>
                          <asp:ListItem Value="8">Fix Appliances</asp:ListItem>
                      </asp:CheckBoxList>
                      </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <asp:button class="btn btn-primary" ID="btnSubmitOffer" runat="server" Text="Submit Request" OnClick="btnSubmitOffer_Click" ValidationGroup="grpOffer" />
                  </div>
                </div>
                  </div>
              </div>
                </form>

            <div id="divSuccess" runat="Server" visible="false">
                <div class="d-flex justify-content-center" style="height: 100vh;">
                <div class="text-center align-self-center">
                    <div class="fs-1">Success!</div>
                    <div class="spinner-border" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                    <div></div>
                    <small>Redirecting to Home page...</small>
                </div>
                </div>
             </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
