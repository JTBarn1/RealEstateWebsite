using RealEstateLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void createAccount_Click(object sender, EventArgs e)
        {
            divRegister.Visible = true;
            divSignin.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            LoginHandler handler = new LoginHandler();
            string error = handler.Loginattempt(username, password);
            if(error == "")
            {
                handler.CreateAgentFromTable();
                Session["Agent"] = handler.GetAgent();
                Response.Redirect("HomePage.aspx");
            }
            else
            {
                lblerror.Text = error;
                lblerror.Visible = true;
            }

            
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //ooh boy, this gonna be a lot
            string userName = txtUsernameRegister.Text;
            string password = txtPasswordRegister.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string personalPhone = txtPersonalPhone.Text;
            string personalEmail = txtPersonalEmail.Text;
            string personalStreet = txtHomeStreet.Text;
            string personalCity = txtHomeCity.Text;
            string personalState  = txtHomeState.Text.ToUpper();
            string personalZip = txtHomeZip.Text;
            string workPhone = txtWorkPhone.Text;
            string workEmail = txtWorkEmail.Text;
            string workStreet = txtWorkStreet.Text;
            string workCity = txtWorkCity.Text;
            string workState = txtWorkState.Text.ToUpper();
            string workZip = txtWorkZip.Text;
            string agencyName = txtRealEstateCompany.Text;

            AgentCreator creator = new AgentCreator();
            creator.CreateAgent(firstName, lastName, personalPhone, personalEmail, personalStreet, personalCity, personalState, personalZip, workPhone, workEmail ,workStreet, workCity, workState, workZip, agencyName);

            int fail = creator.AddAgentToDataBase(userName, password);

            if(fail == -1)
            {
                Response.Write("<script>alert('Account Creation Failed.')</script>");
            }
            else
            {
                Session["Agent"] = creator.GetAgent();
                Response.Redirect("HomePage.aspx");
            }



        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
}