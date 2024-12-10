<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginAndRegisterPage.aspx.cs" Inherits="Project_3.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Homes R Us</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
    <link rel="stylesheet" href="Style.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <header class ="row align-items-center">
        <div class ="col-3 text-start ps-5">
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-light" Text="Go Back" Onclick="btnBack_Click"/>
        </div>
        <h1 class ="col-6">Homes R Us</h1>
    </header>
    <div class="container">

            <div id="divSignin" runat="server">
                <h2>Sign in</h2>
                <p>Don't have an account? 
                    <asp:LinkButton ID="linkCreateAccount" Text="Create Now" runat="server" OnClick="createAccount_Click" CssClass="btn btn-link"></asp:LinkButton>
                </p>
                <div class="form-group">
                    <label for="txtUsername">Username:</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqUsernameLogin" runat="server" ControlToValidate="txtUsername" ErrorMessage="* Username Required" ForeColor="#CC0000" ValidationGroup="valLogin" />
                </div>
                <div class="form-group">
                    <label for="txtPassword">Password:</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqPasswordLogin" runat="server" ControlToValidate="txtPassword" ErrorMessage="* Password Required" ForeColor="#CC0000" ValidationGroup="valLogin" />
                </div>
                <asp:Label ID="lblerror" runat="server" Text="" CssClass="text-danger" Visible="false"></asp:Label>
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" ValidationGroup="valLogin" CssClass="btn btn-primary mt-3" />
            </div>

            <div id="divRegister" runat="server" visible="false">
                <h2>Sign up</h2>
                <div><strong>Personal Information</strong></div>
                
                <div class="form-group">
                    <label for="txtUsernameRegister">Username:</label>
                    <asp:TextBox ID="txtUsernameRegister" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" ControlToValidate="txtUsernameRegister" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                </div>
                
                <div class="form-group">
                    <label for="txtPasswordRegister">Password:</label>
                    <asp:TextBox ID="txtPasswordRegister" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPasswordRegister" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                </div>

                <div class="form-group">
                    <label for="txtFirstName">First Name:</label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                </div>

                <div class="form-group">
                    <label for="txtLastName">Last Name:</label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                </div>

                <div class="form-group">
                    <label for="txtPersonalPhone">Personal Phone:</label>
                    <asp:TextBox ID="txtPersonalPhone" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqPersonalPhone" runat="server" ControlToValidate="txtPersonalPhone" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                </div>

                <div class="form-group">
                    <label for="txtPersonalEmail">Personal Email:</label>
                    <asp:TextBox ID="txtPersonalEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqPersonalEmail" runat="server" ControlToValidate="txtPersonalEmail" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                </div>

                <div>Personal Address:</div>
                <div class="form-group">
                    <label for="txtHomeStreet">Street:</label>
                    <asp:TextBox ID="txtHomeStreet" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqHomeStreet" runat="server" ControlToValidate="txtHomeStreet" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                </div>

                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label for="txtHomeCity">City:</label>
                        <asp:TextBox ID="txtHomeCity" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqHomeCity" runat="server" ControlToValidate="txtHomeCity" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                    </div>
                    <div class="form-group col-md-4">
                        <label for="txtHomeState">State:</label>
                        <asp:TextBox ID="txtHomeState" runat="server" CssClass="form-control" MaxLength="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqHomeState" runat="server" ControlToValidate="txtHomeState" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                    </div>
                    <div class="form-group col-md-4">
                        <label for="txtHomeZip">Zip:</label>
                        <asp:TextBox ID="txtHomeZip" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqHomeZip" runat="server" ControlToValidate="txtHomeZip" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                    </div>
                </div>

                <div><strong>Work Information</strong></div>
                <div class="form-group">
                    <label for="txtWorkPhone">Work Phone:</label>
                    <asp:TextBox ID="txtWorkPhone" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqWorkPhone" runat="server" ControlToValidate="txtWorkPhone" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                </div>

                <div class="form-group">
                    <label for="txtWorkEmail">Work Email:</label>
                    <asp:TextBox ID="txtWorkEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqWorkEmail" runat="server" ControlToValidate="txtWorkEmail" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                </div>

                <div>Work Address:</div>
                <br />
                <div class="form-group">
                    <label for="txtWorkStreet">Street:</label>
                    <asp:TextBox ID="txtWorkStreet" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqWorkStreet" runat="server" ControlToValidate="txtWorkStreet" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                </div>

                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label for="txtWorkCity">City:</label>
                        <asp:TextBox ID="txtWorkCity" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqWorkCity" runat="server" ControlToValidate="txtWorkCity" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                    </div>
                    <div class="form-group col-md-4">
                        <label for="txtWorkState">State:</label>
                        <asp:TextBox ID="txtWorkState" runat="server" CssClass="form-control" MaxLength="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqWorkState" runat="server" ControlToValidate="txtWorkState" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                    </div>
                    <div class="form-group col-md-4">
                        <label for="txtWorkZip">Zip:</label>
                        <asp:TextBox ID="txtWorkZip" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqWorkZip" runat="server" ControlToValidate="txtWorkZip" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                    </div>
                </div>

                <div class="form-group">
                    <label for="txtRealEstateCompany">Real estate company:</label>
                    <asp:TextBox ID="txtRealEstateCompany" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqRealEstateCompany" runat="server" ControlToValidate="txtRealEstateCompany" ErrorMessage="* Required" ForeColor="#CC0000" ValidationGroup="valRegister" />
                </div>

                <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" ValidationGroup="valRegister" CssClass="btn btn-success mt-3" />
            </div>
        </div>
        </form>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
