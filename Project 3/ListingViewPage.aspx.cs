using RealEstateLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_3
{
    public partial class ListingViewPage : System.Web.UI.Page
    {

        ListingViewController controller;
        protected void Page_Load(object sender, EventArgs e)
        {
                controller = new ListingViewController((Listing)Session["SelectedListing"]);

                ImagesCarousel.InnerHtml = "";
                for (int i = 0; i < controller.GetNumImages(); i++)
                {
                    if (i == 0)
                    {
                        ImagesCarousel.InnerHtml += controller.GenerateCarouselActiveImageHTML(i);
                    }
                    else ImagesCarousel.InnerHtml += controller.GenerateCarouselImageHTML(i);
                }
                divHomeMainInfo.InnerHtml = controller.GetHomeMainInfoHTML();
                lblAddress.Text = controller.GenerateAddress();
                lblHomeType.Text = controller.GenerateType();

                divAmenityContainer.InnerHtml = "";
                for (int i = 0; i < controller.getNumAmenities(); i++)
                {
                    divAmenityContainer.InnerHtml += controller.GenerateAmenity(i);
                }


                lblDesc.Text = controller.GetDescSring();
                divUtilities.InnerHtml = controller.GetUtilituesListHTML();
                divOther.InnerHtml = controller.GetOtherInfoHTML();
                divAgentInfo.InnerHtml = controller.GetAgentInfoHTML();

                UpdateRooms(); 
            
        }

        protected void btnSubmitTourRequest_Click(object sender, EventArgs e)
        {
            string firstName = txtTourFirstName.Text;
            string lastName = txtTourLastName.Text;
            string email = txtTourEmail.Text;
            string phoneNumber = txtTourPhone.Text;

            DateTime appointmentTime = DateTime.Parse(txtDate.Text);

            //limitation of bootstrap modals... cant update a label because it auto closes.
            if (controller.CheckIfDateInPast(appointmentTime))
            {
                Response.Write("<script>alert('Tour request failed. Appointments can not be in the past.')</script>");
            }
            else if (controller.CheckIfConflicts(appointmentTime))
            {
                Response.Write("<script>alert('Tour request failed. An appointment already exists at this time.')</script>");
            }
            else
            {
                controller.CreateTourRequest(firstName, lastName, email, phoneNumber, appointmentTime);

                divSuccess.Visible = true;
                divInfoContainer.Visible = false;

                //found this online as a solution for my loading screen not showing 
                //i wanted it to pause to give visual indication of completeness
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "setTimeout(function() { window.location='HomePage.aspx'; }, 1500);", true);
            }

           
        }
        protected void btnSubmitOffer_Click(object sender, EventArgs e)
        {
            string firstName = txtOfferFirstName.Text;
            string lastName = txtOfferLastName.Text;
            string email = txtOfferEmail.Text;
            string phoneNumber = txtOfferPhoneNum.Text;

            int offerPrice = int.Parse(txtOfferPrice.Text);

            DateTime moveInDate = DateTime.Parse(txtOfferDate.Text);

            string paymentType = ddlPurchaseType.Text;

            bool haveToSellFirst = cbSellHouse.Checked;

            List<string> contingencies = new List<string>();

            foreach (ListItem item in CblContingencies.Items)
            {
                if (item.Selected)
                {
                    contingencies.Add(item.Text);
                }
            }

            if (controller.CheckIfDateInPast(moveInDate))
            {
                Response.Write("<script>alert('Offer Submission Failed: Move in date can not be in the past.')</script>");
            }
            else
            {
                controller.CreateOffer(firstName, lastName, email, phoneNumber,offerPrice,moveInDate,paymentType,haveToSellFirst,contingencies);

                divSuccess.Visible = true;
                divInfoContainer.Visible = false;

                //found this online as a solution for my loading screen not showing 
                //i wanted it to pause to give visual indication of completeness
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "setTimeout(function() { window.location='HomePage.aspx'; }, 1500);", true);
            }


        }

        private void UpdateRooms()
        {
            tbodyRooms.Controls.Clear();

            int id = 0;
            foreach (DataRow row in controller.GetRoomDataTable().Rows)
            {
                TableRow roomRow = new TableRow();

                TableCell nameCell = new TableCell { Text = row["RoomName"].ToString() };
                TableCell lengthCell = new TableCell { Text = row["RoomLength"].ToString() + " ft" };
                TableCell widthCell = new TableCell { Text = row["RoomWidth"].ToString() + " ft" };
                TableCell sizeCell = new TableCell { Text = row["RoomSize"].ToString() + " SQFT" };

                roomRow.Cells.Add(nameCell);
                roomRow.Cells.Add(lengthCell);
                roomRow.Cells.Add(widthCell);
                roomRow.Cells.Add(sizeCell);

                tbodyRooms.Controls.Add(roomRow);
                id++;
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
}