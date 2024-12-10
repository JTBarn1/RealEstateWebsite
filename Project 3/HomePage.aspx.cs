using RealEstateLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Project_3
{
    public partial class HomePage : System.Web.UI.Page
    {
        AgentDashboardController dashboard;
        HousingController housingController;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (Session["Agent"] != null)
                {
                    realtorViewDiv.Visible = true;
                    standardViewDiv.Visible = false;
                    divLogin.Visible = false;
                    divAgentUtils.Visible = true;

                    dashboard = new AgentDashboardController((Agent)Session["Agent"]);
                    string firstName = dashboard.GetFirstName();
                    lblHello.Text = "Hello, " + firstName;

                    dashboard.GetAgentListings();
                    Session["Dashboard"] = dashboard;
                    Session["housingController"] = null;
                    addRealtorListings();
                }
                else
                {
                    housingController = new HousingController();
                    housingController.GetListings();
                    Session["housingController"] = housingController;

                    UpdateListings();
                }
            }
            else
            {
                if (Session["housingController"] != null) UpdateListings();
                if (Session["Dashboard"] != null)
                {
                    addRealtorListings();
                    refreshOffers(dashboard.GetSelectedOffer());
                }
            }
        }

        protected void RealtorLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginAndRegisterPage.aspx");
        }

        protected void btnAddListing_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListingCreation.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["Agent"] = null;
            Response.Redirect("HomePage.aspx");
        }
        protected void btnViewMore_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int id = int.Parse(button.ID);

            if (housingController.SearchExists()) Session["SelectedListing"] = housingController.GetQueriedListingFromID(id);
            else Session["SelectedListing"] = housingController.GetListingFromID(id);

            Response.Redirect("ListingViewPage.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string state = txtStateSearch.Text.ToUpper();
            string city = txtCitySearch.Text;
            string zip = txtZipSearch.Text;


            int minPrice;
            int.TryParse(txtMinPrice.Text, out minPrice);
            int maxPrice;
            int.TryParse(txtMaxPrice.Text, out maxPrice);
            int sqft;
            int.TryParse(txtSQFT.Text, out sqft);


            string homeType = ddlHomeType.Text;
            string beds = ddlBeds.SelectedValue;
            string baths = ddlBaths.SelectedValue;

            List<string> requiredAmenities = new List<string>();
            foreach(ListItem item in cbListAmenities.Items)
            {
                if (item.Selected)
                {
                    requiredAmenities.Add(item.Value);
                }
            }

            housingController.SearchAttempt(city,state,zip,minPrice,maxPrice,sqft,homeType, beds, baths,requiredAmenities);

            UpdateListings();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtCitySearch.Text = "";
            txtZipSearch.Text = "";
            txtStateSearch.Text = "";
            txtMinPrice.Text = "";
            txtMaxPrice.Text = "";
            txtSQFT.Text = "";
            ddlBaths.SelectedIndex = 0;
            ddlBeds.SelectedIndex = 0;
            ddlHomeType.SelectedIndex = 0;
            cbListAmenities.ClearSelection();
            
            housingController.ResetQuery();
            UpdateListings() ;
        }

        private void UpdateListings()
        {
            phHouses.Controls.Clear();

            housingController = (HousingController)Session["housingController"];

            int numListings;
            if (housingController.SearchExists()) numListings = housingController.GetNumQueriedListings();
            else numListings = housingController.GetNumListings();

            phHouses.Controls.Add(new LiteralControl("<div class='row row-cols-2 row-cols-lg-3'>"));

            for (int i = 0; i < numListings; i++)
            {
                PlaceHolder ph = new PlaceHolder();

                if (housingController.SearchExists()) ph.Controls.Add(new LiteralControl(housingController.CreateQueryHouseHTML(i)));
                else ph.Controls.Add(new LiteralControl(housingController.CreateHouseHTML(i)));

                Button btnViewMore = new Button
                {
                    ID = i.ToString(),
                    Text = "View More",
                    CssClass = "btn btn-primary"
                };
                btnViewMore.Click += new EventHandler(btnViewMore_Click);

                ph.Controls.Add(btnViewMore);

                ph.Controls.Add(new LiteralControl("</div></div></div></div>"));

                phHouses.Controls.Add(ph);
            }
            phHouses.Controls.Add(new LiteralControl("</div>"));
        }

        private void addRealtorListings()
            {
            dashboard = (AgentDashboardController)Session["Dashboard"];
            DataTable listingsTable = dashboard.CreateListingTable(); // Get the DataTable
            int i = 0;

            // Clear existing rows
            tbodyListings.Controls.Clear();

            foreach (DataRow row in listingsTable.Rows)
            {
                TableRow agentRow = new TableRow();

                TableCell addressCell = new TableCell { Text = row["StreetName"].ToString() };
                TableCell priceCell = new TableCell { Text = "$" + row["AskingPrice"].ToString() };
                TableCell statusCell = new TableCell { Text = row["Status"].ToString() };

                // Add cells to the row
                agentRow.Cells.Add(addressCell);
                agentRow.Cells.Add(priceCell);
                agentRow.Cells.Add(statusCell);

                TableCell btnShowingsCell = new TableCell();
                if (dashboard.OfferPending(i)) btnShowingsCell.Text = "Offer Pending";
                else
                {
                    Button btnViewShowings = new Button
                    {
                        Text = $"View {row["NumShowings"]} Showings",
                        CommandArgument = i.ToString()
                    };
                    btnViewShowings.Click += new EventHandler(ViewShowingsButton_Click);
                    btnShowingsCell.Controls.Add(btnViewShowings);
                }
                    agentRow.Cells.Add(btnShowingsCell);


                TableCell btnOffersCell = new TableCell();
                if (dashboard.OfferPending(i)) btnOffersCell.Text = "Offer Pending";
                else
                {
                    Button btnViewOffers = new Button
                    {
                        Text = $"View {row["NumOffers"]} Offers",
                        CommandArgument = i.ToString()
                    };
                    btnViewOffers.Click += new EventHandler(ViewOffersButton_Click);
                    btnOffersCell.Controls.Add(btnViewOffers);
                }
                    agentRow.Cells.Add(btnOffersCell);
                

                TableCell btnModifyCell = new TableCell();
                Button btnModify = new Button
                {
                    Text = "Modify",
                    CommandArgument = i.ToString()
                };
                btnModify.Click += new EventHandler(ModifyButton_Click);
                btnModifyCell.Controls.Add(btnModify);
                agentRow.Cells.Add(btnModifyCell);

                tbodyListings.Controls.Add(agentRow);
                i++;
            }
        }

        protected void gvAgentListings_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Ensure DataItem is not null
                if (e.Row.DataItem != null)
                {
                    DataRowView dataRowView = (DataRowView)e.Row.DataItem;

                    
                    int showingsCount = Convert.ToInt32(dataRowView["NumShowings"]);
                    int offersCount = Convert.ToInt32(dataRowView["NumOffers"]);

                    string pluralShowings = "";
                    if (showingsCount > 1) pluralShowings = "s";

                    string pluralOffers = "";
                    if(offersCount > 1) pluralOffers = "s";
                   
                    Button btnShowings = (Button)e.Row.FindControl("btnViewShowings");
                    Button btnOffers = (Button)e.Row.FindControl("btnViewOffers");

                    
                    btnShowings.Text = $"View {showingsCount} Showing{pluralShowings}";
                    btnOffers.Text = $"View {offersCount} Offer{pluralOffers}";
                } 
            }
        }
        protected void ViewShowingsButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);
            divShowingsView.Visible = true;
            divListingsDashboard.Visible = false;

            tbodyShowings.Controls.Clear();

            DataTable showingsTable = dashboard.CreateShowingTable(id);

            foreach (DataRow row in showingsTable.Rows)
            {
                TableRow showingRow = new TableRow();

                TableCell nameCell = new TableCell { Text = row["ShowingName"].ToString() };
                TableCell phoneCell = new TableCell { Text = row["ShowingPhone"].ToString() };
                TableCell emailCell = new TableCell { Text = row["ShowingEmail"].ToString() };
                TableCell dateCell = new TableCell { Text = row["ShowingDate"].ToString() };
                TableCell timeCell = new TableCell { Text = row["ShowingTime"].ToString() };

                showingRow.Cells.Add(nameCell);
                showingRow.Cells.Add(phoneCell);
                showingRow.Cells.Add(emailCell);
                showingRow.Cells.Add(dateCell);
                showingRow.Cells.Add(timeCell);

                tbodyShowings.Controls.Add(showingRow);
            }
        }

        protected void ViewOffersButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);
            divOffersView.Visible = true;
            divListingsDashboard.Visible = false;

            dashboard.SetSelectedOffer(id);
            refreshOffers(id);
        }

        private void refreshOffers(int id)
        {
            DataTable dt = dashboard.CreateOfferTable(id);

            tbodyOffers.Controls.Clear();
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                TableRow offerRow = new TableRow();

                TableCell nameCell = new TableCell { Text = row["OfferName"].ToString() };
                TableCell priceCell = new TableCell { Text = "$" + row["OfferPrice"].ToString() };
                TableCell typeCell = new TableCell { Text = row["OfferType"].ToString() };

                string contingencies = row["OfferContingencies"].ToString();
                TableCell contingenciesCell = new TableCell();
                if (contingencies.Equals("")) { contingenciesCell.Text = "None"; }
                else
                {
                    string[] contingenciesList = contingencies.Split('|');
                    contingenciesCell.Controls.Add(new LiteralControl("<ul class='list-unstyled'>"));
                    foreach (string contingency in contingenciesList)
                    {
                        Debug.WriteLine(contingency);
                        contingenciesCell.Controls.Add(new LiteralControl($"<li>{contingency}</li>"));
                    }
                    contingenciesCell.Controls.Add(new LiteralControl("</ul>"));
                }


                TableCell moveInCell = new TableCell { Text = row["OfferDate"].ToString() };
                TableCell sellHomeCell = new TableCell { Text = row["OfferNeedSell"].ToString() };
                TableCell actionCell = new TableCell();


                Button btnAccept = new Button
                {
                    Text = "Accept Offer",
                    ID = "AcceptOffer" + i,
                    CommandArgument = i.ToString()
                };
                btnAccept.Click += new EventHandler(AcceptOfferButton_Click);
                actionCell.Controls.Add(btnAccept);
                Button btnReject = new Button
                {
                    Text = "Reject",
                    ID = "RejectOffer" + i,
                    CommandArgument = i.ToString()
                };
                btnReject.Click += new EventHandler(RejectOfferButton_Click);
                actionCell.Controls.Add(btnReject);

                offerRow.Cells.Add(nameCell);
                offerRow.Cells.Add(priceCell);
                offerRow.Cells.Add(typeCell);
                offerRow.Cells.Add(contingenciesCell);
                offerRow.Cells.Add(moveInCell);
                offerRow.Cells.Add(sellHomeCell);
                offerRow.Cells.Add(actionCell);

                tbodyOffers.Controls.Add(offerRow);
                i++;
            }
        }

        private void AcceptOfferButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("ACCEPTED");

            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            dashboard.AcceptOffer(id);

            divOffersView.Visible = false;
            divListingsDashboard.Visible = true;

            UpdateListings();

        }
        private void RejectOfferButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Rejected");

            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            dashboard.RejectOffer(id);
            refreshOffers(dashboard.GetSelectedOffer());
        }

        protected void ModifyButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            Session["ListingToModify"] = dashboard.GetListingToBeModified(id);
            Response.Redirect("ListingModifyPage.aspx");
            }


        protected void btnOffersBack_Click(object sender, EventArgs e)
        {
            divOffersView.Visible = false;
            divListingsDashboard.Visible = true;
            
        }

        protected void btnShowingsBack_Click(object sender, EventArgs e)
        {
            divShowingsView.Visible = false;
            divListingsDashboard.Visible = true;
        }
    }
}