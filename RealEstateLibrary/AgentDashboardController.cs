using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class AgentDashboardController
    {
        private Agent agent;
        private List<Listing> listings = new List<Listing>();
        private int selectedOffer;
        
        public AgentDashboardController(Agent agent)
        {
            this.agent = agent;
        }

        public void GetAgentListings()
        {
            ListingCreator creator = new ListingCreator();
            List<Listing> uncheckedlistings = creator.GetListings();
            foreach(Listing l in uncheckedlistings){
                if (l.Agent.GetId() == agent.GetId())
                {
                    listings.Add(l);
                }
            }
        }

        public DataTable CreateListingTable()
        {
            DataTable dt = new DataTable("Listings");

            dt.Columns.Add("ID");
            dt.Columns.Add("StreetName");
            dt.Columns.Add("AskingPrice");
            dt.Columns.Add("Status");
            dt.Columns.Add("NumShowings");
            dt.Columns.Add("NumOffers");

            foreach (Listing listing in listings)
            {
                dt.Rows.Add(listing.ID,listing.Home.Address.Street,listing.AskingPrice,listing.Status,listing.Showings.Count,listing.Offers.Count);
            }

            return dt;
        }

        public DataTable CreateShowingTable(int id)
        {
            DataTable dt = new DataTable("Listings");

            dt.Columns.Add("ShowingName");
            dt.Columns.Add("ShowingEmail");
            dt.Columns.Add("ShowingPhone");
            dt.Columns.Add("ShowingDate");
            dt.Columns.Add("ShowingTime");

            Listing listing = listings[id];

            foreach (Showing show in listing.Showings) {
                dt.Rows.Add(show.FirstName + " " + show.LastName, show.Email, show.Phone,
                    show.TourDate.Date.ToString("MM/dd/yyyy"), show.TourDate.TimeOfDay.ToString());
                }
            return dt;
        }

        public DataTable CreateOfferTable(int id)
        {
                DataTable dt = new DataTable("Listings");

                dt.Columns.Add("OfferName");
                dt.Columns.Add("OfferPrice");
                dt.Columns.Add("OfferType");
                dt.Columns.Add("OfferContingencies");
                dt.Columns.Add("OfferNeedSell");
                dt.Columns.Add("OfferDate");

                if (listings.Count != 0)
                {
                    Listing listing = listings[id];

                    foreach (Offer offer in listing.Offers)
                    {
                        string offercContingencies = "";
                        foreach (string contingency in offer.Contingencies)
                        {
                            offercContingencies += contingency + "|";
                        }
                        dt.Rows.Add(offer.FirstName + " " + offer.LastName, offer.OfferPrice, offer.PurchaseMethod, offercContingencies,
                            offer.NeedToSell, offer.PreferredMoveIn.Date.ToString("MM/dd/yyyy"));
                    }
                }
                return dt;
        }

        public string GetFirstName()
        {
            return agent.FirstName;
        }

        public Listing GetListingToBeModified(int id)
        {
            return listings[id];    
        }

        public void SetSelectedOffer(int id)
        {
            selectedOffer = id;
        }

        public int GetSelectedOffer()
        {
            return selectedOffer;
        }

        public bool OfferPending(int id)
        {
            if (listings[id].Status.Equals("Pending")) return true;
            else return false;
        }

        public void AcceptOffer(int id)
        {
            Listing listing = listings[GetSelectedOffer()];
            Offer offer = listing.Offers[id];


            EmailSender emailSender = new EmailSender();
            emailSender.SendAcceptEmail(listing.Agent.WorkEmail, offer.Email, offer.FirstName, listing.Home.Address.Street);


            SqlUpdater updater = new SqlUpdater();
            updater.RemoveAllOffers(listing.ID);
            updater.UpdateListingStatus(listing.ID, "Pending");
        }

        public void RejectOffer(int id)
        {
            Listing listing = listings[GetSelectedOffer()];
            Offer offer = listing.Offers[id];

            listings[GetSelectedOffer()].Offers.RemoveAt(id);

            
            EmailSender emailSender = new EmailSender();
            emailSender.SendRejectEmail(listing.Agent.WorkEmail, offer.Email, offer.FirstName, listing.Home.Address.Street);

            SqlUpdater updater = new SqlUpdater();
            updater.RemoveOffer(listing.ID, offer.OfferPrice, offer.FirstName, offer.LastName);
        }
    }
}
