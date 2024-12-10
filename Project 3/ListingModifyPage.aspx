<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="ListingModifyPage.aspx.cs" Inherits="Project_3.LIstingModifyPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add a new listing</title>
    <link rel="stylesheet" href="Style.css"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
</head>
<body class="formBody">
    <form id="form1" runat="server">
        <header class ="row align-items-center">
            <div class ="col-3 text-start ps5">
                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-light" Text="Go Back" Onclick="btnBack_Click"/>
            </div>
            <h1 class ="col-6">Homes R Us</h1>
        </header>
        <div class="container-lg mt-4">
            <h1 class="mb-4">Edit Listing
            </h1>
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="Remove Listing" Onclick="btnDelete_Click" />
            <div class="mb-3">
                <label for="Street" class="form-label">Address</label>
                <asp:TextBox ID="Street" runat="server" CssClass="form-control" placeholder="Street"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqStreet" runat="server" ControlToValidate="Street" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
            </div>
            <div class="row mb-3">
                <div class="col ">
                    <asp:TextBox ID="City" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqCity" runat="server" ControlToValidate="City" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
                </div>
                <div class="col">
                    <asp:TextBox ID="State" runat="server" CssClass="form-control" placeholder="State" MaxLength="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqState" runat="server" ControlToValidate="State" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit" />
                </div>
                <div class="col">
                    <asp:TextBox ID="Zip" runat="server" CssClass="form-control" placeholder="Zip"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqZip" runat="server" ControlToValidate="Zip" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
                </div>
            </div>

            <div class="mb-3">
                <label for="tbaskingPrice" class="form-label">Asking Price</label>
                <asp:TextBox ID="tbaskingPrice" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqPrice" runat="server" ControlToValidate="tbaskingPrice" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
                <asp:CompareValidator ID="compPrice" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbaskingPrice" ErrorMessage="Value must be a whole number" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
            </div>
              <div class="mb-3">
                  <label for="tbStatus" class="form-label">Listing Status</label>
                  <asp:TextBox ID="tbStatus" runat="server" CssClass="form-control"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="reqStatus" runat="server" ControlToValidate="tbStatus" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
              </div>

            <h2>Home Details</h2>
            <div class="mb-3">
                <label for="tbNumBedrooms" class="form-label">Number of Bedrooms</label>
                <asp:TextBox ID="tbNumBedrooms" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqBedrooms" runat="server" ControlToValidate="tbNumBedrooms" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
                <asp:CompareValidator ID="CompBedroomInt" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbNumBedrooms" ErrorMessage="Value must be a whole number" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
            </div>
            <div class="mb-3">
                <label for="tbNumBathrooms" class="form-label">Number of Bathrooms</label>
                <asp:TextBox ID="tbNumBathrooms" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqBathrooms" runat="server" ControlToValidate="tbNumBathrooms" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
                <asp:CompareValidator ID="CompBathroomFloat" runat="server" Operator="DataTypeCheck" Type="Double" ControlToValidate="tbNumBathrooms" ErrorMessage="Value must be a number" ForeColor="#CC0000" validationGroup ="grpSubmit"  />
            </div>
            <div class="mb-3">
                <label for="tbYearBuilt" class="form-label">Year Built</label>
                <asp:TextBox ID="tbYearBuilt" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqYear" runat="server" ControlToValidate="tbYearBuilt" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
                <asp:CompareValidator ID="CompYearInt" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbYearBuilt" ErrorMessage="Value must be a whole number" ForeColor="#CC0000" validationGroup ="grpSubmit" />
            </div>

            <div class="mb-3">
                <label for="ddlHomeType" class="form-label">Home Type</label>
                <asp:DropDownList ID="ddlHomeType" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Single Family" Value="Single Family"></asp:ListItem>
                    <asp:ListItem Text="Townhome" Value="Townhome"></asp:ListItem>
                    <asp:ListItem Text="Multi Family" Value="Multi Family"></asp:ListItem>
                    <asp:ListItem Text="Condo" Value="Condo"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label for="ddlGarageSize" class="form-label">Garage Size</label>
                <asp:DropDownList ID="ddlGarageSize" runat="server" CssClass="form-select">
                    <asp:ListItem>0</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5+</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label for="tbHomeDescription" class="form-label">Home Description</label>
                <asp:TextBox ID="tbHomeDescription" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqDesc" runat="server" ControlToValidate="tbHomeDescription" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
            </div>

            <h4>Utilities</h4>
            <div class="row mb-3">
                <div class="col">
                    <label>Home Heating</label>
                    <asp:RadioButtonList ID="radioHeat" runat="server" CssClass="form-check">
                        <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem>Forced Air</asp:ListItem>
                        <asp:ListItem>Radiator</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="reqHeat" runat="server" ControlToValidate="radioHeat" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
                </div>
                <div class="col">
                    <label>Home Cooling</label>
                    <asp:RadioButtonList ID="radioCooling" runat="server" CssClass="form-check">
                        <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem>HVAC</asp:ListItem>
                        <asp:ListItem>Wall Mount AC</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="reqCooling" runat="server" ControlToValidate="radioCooling" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
                </div>
                <div class="col">
                    <label>Water Type</label>
                    <asp:RadioButtonList ID="radioWater" runat="server" CssClass="form-check">
                        <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem>Well Water</asp:ListItem>
                        <asp:ListItem>City Water</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="reqWater" runat="server" ControlToValidate="radioWater" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
                </div>
                <div class="col">
                    <label>Sewage Type</label>
                    <asp:RadioButtonList ID="radioSewer" runat="server" CssClass="form-check">
                        <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem>City Sewage</asp:ListItem>
                        <asp:ListItem>Septic Tank</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="reqSewer" runat="server" ControlToValidate="radioSewer" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpSubmit"/>
                </div>
            </div>

            <h4>Rooms:</h4>
            <div id="divRoomTable" runat="server" visible="true" class="mb-3">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Type</th>
                            <th>Length</th>
                            <th>Width</th>
                            <th>Size</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody id="tbodyRoomsAdd" runat="server">
                    </tbody>
                </table>
            </div>

            <div class="row mb-3 align-items-center">
                <div class="col">
                    <label for="tbRoomLength" class="form-label">Length</label>
                    <asp:TextBox ID="tbRoomLength" runat="server" placeholder="Length in Feet" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqLength" runat="server" ControlToValidate="tbRoomLength" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpRoomAdd" CssClass="text-danger" />
                    <asp:CompareValidator ID="reqIntLength" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbRoomLength" ErrorMessage="Value must be a whole number" ForeColor="#CC0000" validationGroup ="grpRoomAdd" CssClass="text-danger" />
                </div>
                <div class="col">
                    <label for="tbRoomWidth" class="form-label">Width</label>
                    <asp:TextBox ID="tbRoomWidth" runat="server" placeholder="Width in Feet" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbRoomWidth" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpRoomAdd" CssClass="text-danger" />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbRoomWidth" ErrorMessage="Value must be a whole number" ForeColor="#CC0000" validationGroup ="grpRoomAdd" CssClass="text-danger" />
                </div>
                <div class="col">
                    <label for="tbRoomDesc" class="form-label">Description</label>
                    <asp:TextBox ID="tbRoomDesc" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbRoomDesc" ErrorMessage="* Required" ForeColor="#CC0000" validationGroup ="grpRoomAdd" CssClass="text-danger" />
                </div>
                <div class="col">
                    <asp:Button ID="btnAddRoom" CssClass="btn btn-secondary" runat="server" Text="Add room to home" OnClick="btnAddRoom_Click" validationGroup ="grpRoomAdd" />
                </div>
            </div>


            <h4>Amenities</h4>
            <div id="divamenityAddCheckbox" runat="server" class="mb-3">
                <label>Select From List:</label>
                <asp:GridView ID="gvSelectAmenity" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="hidden" AllowPaging="True" PageSize="8" OnPageIndexChanging="gvSelectAmenity_PageIndexChanging" OnRowDataBound="gvSelectAmenity_RowDataBound" CssClass="table">
                    <Columns>
                        <asp:BoundField ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" DataField="AmenityID"/>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="amenityDescription" />
                    </Columns>
                </asp:GridView>
            </div>

            <div id="divNewAmenityAdd" runat="server" class="mb-3">
                <div id="divExtraAmenities" runat="server" visible="false">
                    <table class="table">
                         <thead>
                             <tr>
                                 <th></th>
                                 <th>Description</th>
                                 <th>Action</th>
                             </tr>
                         </thead>
                        <tbody id="tbodyAmenities" runat="server">
                        </tbody>
                    </table>
                </div>
                <label>Or create new amenity</label>
                <div class="mb-3 d-flex">
                <asp:TextBox ID="tbAmenityNew" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="buttonAddAmenity" runat="server" CssClass="btn btn-secondary" OnClick="buttonAddAmenity_Click" Text="Add" />
                </div>
            </div>

            <div id="divImages" runat="server" class="mb-3">
                <h4>Images:</h4>
                <div id="divImageTable" runat="server" visible="true" class="mb-3">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Image Name Or Location</th>
                                <th>Caption</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody id="tbodyImages" runat="server">
                        </tbody>
                    </table>
                </div>
                <asp:FileUpload ID="fileImageAdd" runat="server" AllowMultiple="False" CssClass="form-control"/>
                <div class="mb-3 d-flex">
                    <asp:TextBox ID="txtImageCaption" runat="server" placeholder="Image Caption" CssClass="form-control" />
                    <asp:Button ID="btnAddImage" runat="server" CssClass="btn btn-secondary" Text="Add" OnClick="btnAddImage_Click" ValidationGroup="grpImage" />
                </div>
                <asp:RequiredFieldValidator ID="reqImage" runat="server" ControlToValidate="fileImageAdd" ErrorMessage="* Image Required" ForeColor="#CC0000" validationGroup ="grpImage"/>
                <asp:RequiredFieldValidator ID="reqCaption" runat="server" ControlToValidate="txtImageCaption" ErrorMessage="* Caption Required" ForeColor="#CC0000" validationGroup ="grpImage"/>
                <div id="divError" runat="server" class="text-danger"></div>
            </div>
            <div class="d-flex justify-content-center">
                <asp:Button ID="btnSaveHouse" runat="server" CssClass="btn btn-primary" Text="Save Changes" OnClick="btnSaveHouse_Click" ValidationGroup="grpSubmit" />
                
            </div>
        </div>
    </form>
    <br />
    <br />

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
