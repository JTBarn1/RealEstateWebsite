using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace RealEstateLibrary
{
    public class ListingViewController
    {
        private Listing Listing;
        public ListingViewController(Listing L)
        {
            this.Listing = L;
        }

        public int GetNumImages()
        {
            return Listing.Home.Images.Count;
        }

        public string GenerateCarouselImageHTML(int id)
        {
            HomeImage img = Listing.Home.Images[id];
            string html = "<div class ='carousel-item'>" +
                $"<img src='{img.Link}' class='d-block w-100'/>" +
                "<div class='carousel-caption d-none d-md-block'>" +
                $"<p>{img.Caption}</p>" +
                "</div>" +
                "</div>";
            return html;
        }

        public string GenerateCarouselActiveImageHTML(int id)
        {
            HomeImage img = Listing.Home.Images[id];
            string html = "<div class ='carousel-item active'>" +
                $"<img src='{img.Link}' class='d-block w-100'/>" +
                "<div class='carousel-caption d-none d-md-block'>" +
                $"<p>{img.Caption}</p>" +
                "</div>" +
                "</div>";
            return html;
        }
        public string GetHomeMainInfoHTML()
        {
            string numBeds = "beds";
            string numBaths = "baths";

            if (Listing.Home.NumBedrooms == 1) numBeds = "bed";
            if (Listing.Home.NumBathrooms == 1) numBaths = "bath";

            string html = $"<span class = 'fw-bold'>${Listing.AskingPrice.ToString()}</span>" +
            $"<span class = 'fs-2'> | {Listing.Home.NumBedrooms} {numBeds} | {Listing.Home.NumBathrooms} {numBaths} | </span>" +
            $"<span class = 'fs-2 fw-bold'>{Listing.Home.SquareFootage}SQFT</span>";
            return html;
        }
        public string GenerateAddress()
        {
            Address addy = Listing.Home.Address;
            return $"{addy.Street}, {addy.City}, {addy.State} {addy.ZipCode}";
        }

        public string GenerateAmenity(int i)
        {
            return $"<div class = 'col fs-3 text-center border rounded border-secondary'>{Listing.Home.Amenities[i]}</div>";
        }

        public string GetUtilituesListHTML()
        {
            return "<ul>" +
                $"<li>Heating: {Listing.Home.HeatingType}</li>" +
                $"<li>Cooling: {Listing.Home.CoolingType}</li>" +
                $"<li>Water: {Listing.Home.WaterType}</li>" +
                $"<li>Sewer: {Listing.Home.SewageType}</l1>" +
                "</ul>";
        }
        public string GetOtherInfoHTML()
        {
            string cars;
            if (Listing.Home.NumCarsInGarage == 0)
            {
                cars = "None";
            }
            else cars = Listing.Home.NumCarsInGarage.ToString() + " Car";


            int numDays = (int)(DateTime.Now - Listing.DateListed).TotalDays;

            return "<ul>" +
                $"<li>Built in {Listing.Home.YearBuilt}</li>" +
                $"<li>Garage Type: {cars} </li>" +
                $"<li>Time on Market: {numDays} Days</li>" +
                "</ul>";
        }

        public string GetAgentInfoHTML()
        {
            return  $"<p>{Listing.Agent.CompanyName}</p>" +
                    $"<p>{Listing.Agent.FirstName}  {Listing.Agent.LastName}</p>" +
                    $"<p>{Listing.Agent.WorkAddress.Street}, {Listing.Agent.WorkAddress.City} {Listing.Agent.WorkAddress.State} ,{Listing.Agent.WorkAddress.ZipCode}</p>" +
                    $"<p>{Listing.Agent.WorkPhone}</p>";
        }

        public string GetDescSring()
        {
            return Listing.Home.Desc;
        }

        public int getNumAmenities()
        {
            return Listing.Home.Amenities.Count;
        }

        public bool CheckIfDateInPast(DateTime appointmentTime)
        {
            if (appointmentTime <= DateTime.Now) return true;
            else return false;
        }

        public bool CheckIfConflicts(DateTime appointmentTime)
        {
            if (Listing.Showings == null)
            {
                return false;
            }

            foreach (Showing showing in Listing.Showings)
            {

                if (showing.TourDate == appointmentTime)
                {
                    return true;
                }

                if (showing.TourDate < appointmentTime && appointmentTime < showing.TourDate.AddMinutes(30))
                {
                    return true;
                }
            }

            Debug.WriteLine("No conflicts found.");
            return false;
        }

        public void CreateTourRequest(string firstName, string lastName, string email, string phoneNumber, DateTime appointmentTime)
        {
            SqlInserter inserter = new SqlInserter();

            Showing showing = new Showing(firstName, lastName, email, phoneNumber, appointmentTime);

            inserter.AddShowing(showing, Listing.ID);
            
        }

        public void CreateOffer(string firstName, string lastName, string email, string phoneNumber, int offerPrice, DateTime moveInDate, string paymentType, bool haveToSellFirst, List<string> contingencies)
        {
            SqlInserter inserter = new SqlInserter();
            if(contingencies.Count == 0)
            {
                Offer offer = new Offer(firstName, lastName, phoneNumber, email, paymentType + " as is", offerPrice, moveInDate, haveToSellFirst, contingencies);

                int ID = inserter.InsertOffer(offer, Listing.ID);

            }
            else
            {
                Offer offer = new Offer(firstName, lastName, phoneNumber, email, paymentType, offerPrice, moveInDate, haveToSellFirst, contingencies);

                int ID = inserter.InsertOffer(offer, Listing.ID);

                inserter.InsertOfferContingencies(offer.Contingencies, ID);
            }

            EmailSender emailSender = new EmailSender();
            emailSender.SendOfferNotice(Listing.Agent.WorkEmail, Listing.Home.Address.Street);
        }

        public string GenerateType()
        {
            return Listing.Home.Type + " Home";
        }

        public DataTable GetRoomDataTable()
        {
            DataTable dt = new DataTable("Rooms");

            dt.Columns.Add("RoomName");
            dt.Columns.Add("RoomLength");
            dt.Columns.Add("RoomWidth");
            dt.Columns.Add("RoomSize");

            foreach (Room room in Listing.Home.Rooms)
            {
                Debug.WriteLine("room");
                dt.Rows.Add(room.Description, room.Length, room.Width, room.SquareFeet);
            }
            return dt;
        }
    }
}
