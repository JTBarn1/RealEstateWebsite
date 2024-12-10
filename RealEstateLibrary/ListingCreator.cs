using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    internal class ListingCreator
    {
        SqlSelector selector = new SqlSelector();
        public List<Listing> GetListings()
        {
            List<Listing> listings = new List<Listing>();

            DataSet ListingSets = selector.GetListings();

            foreach (DataRow row in ListingSets.Tables[0].Rows)
            {
                int homeID = (int)row["HomeID"];
                int listingID = (int)row["ListingID"];
                int agentID = (int)row["AgentID"];
                int askingPrice = (int)row["AskingPrice"];
                DateTime time = (DateTime)row["DateListed"];
                string status = (string)row["listingStatus"];


                Home listingHome = CreateHome(row, homeID);
                Agent agent = CreateAgent(agentID);
                List<Showing> showings = GetShowings(listingID);
                List<Offer> offers = GetOffers(listingID);

                Listing Listing = new Listing(listingID, listingHome, time, agent, askingPrice, status, offers, showings);
                Debug.WriteLine(Listing.ID);
                listings.Add(Listing);
            }
            return listings;
        }

        private Agent CreateAgent(int agentID)
        {
            DataRow agentRow = selector.GetAgentFromID(agentID);

            int agentId = int.Parse(agentRow["AgentID"].ToString());
            string firstName = agentRow["AgentFirstName"].ToString();
            string lastName = agentRow["AgentLastName"].ToString();
            string personalPhone = agentRow["AgentPersonalPhone"].ToString();
            string personalEmail = agentRow["AgentPersonalEmail"].ToString();
            string personalStreet = agentRow["AgentPersonalStreet"].ToString();
            string personalCity = agentRow["AgentPersonalCity"].ToString();
            string personalZip = agentRow["AgentPersonalZip"].ToString();
            string workStreet = agentRow["AgentWorkStreet"].ToString();
            string workCity = agentRow["AgentWorkCity"].ToString();
            string workZip = agentRow["AgentWorkZip"].ToString();
            string workPhone = agentRow["AgentWorkPhone"].ToString();
            string workEmail = agentRow["AgentWorkEmail"].ToString();
            string agencyName = agentRow["AgencyName"].ToString();
            string username = agentRow["AgentUsername"].ToString();
            string password = agentRow["AgentPassword"].ToString();
            string personalState = agentRow["AgentPersonalState"].ToString();
            string workState = agentRow["AgentWorkState"].ToString();

            Address personalAddress = new Address(personalStreet, personalState, personalCity, personalZip);
            Address workAddress = new Address(workStreet, workCity, workState, workZip);

            Agent agent = new Agent(firstName, lastName, personalPhone, workPhone, personalEmail, workEmail, workAddress, personalAddress, agencyName);
            agent.AddID(agentId);
            return agent;
        }

        public Home CreateHome(DataRow row, int id)
        {
            string StreetName = (string)row["StreetName"];
            string City = (string)row["City"];
            string HomeState = (string)row["homeState"];
            string Zip = (string)row["Zip"];
            int SquareFootage = Convert.ToInt32(row["SquareFootage"]);
            int NumBedrooms = Convert.ToInt32(row["NumBedrooms"]);
            float NumBathrooms = Convert.ToSingle(row["NumBathrooms"]);
            string YearBuilt = (string)row["YearBuilt"];
            int GarageType = (int)row["GarageType"];
            string WaterUtilityType = (string)row["WaterUtilityType"];
            string SewerUtilityType = (string)row["SewerUtilityType"];
            string HeatingType = (string)row["HeatingType"];
            string CoolingType = (string)row["CoolingType"];
            string HomeDescription = (string)row["HomeDescription"];
            string HomeType = (string)row["HomeType"];

            List<Room> rooms = CreateRoomListFromHomeID(id);
            List<string> amenities = CreateAmenityListFromHomeID(id);
            List<HomeImage> images = GetImagesFromHomeID(id);

            Address address = new Address(StreetName, City, HomeState, Zip);
            Home newHome = new Home(address, YearBuilt, HeatingType, CoolingType, WaterUtilityType, SewerUtilityType,
                NumBedrooms, NumBathrooms, GarageType, HomeDescription, HomeType, rooms, amenities, images, SquareFootage);
            return newHome;
        }

        private List<HomeImage> GetImagesFromHomeID(int id)
        {
            List<HomeImage> images = new List<HomeImage>();
            DataSet ds = selector.GetImagesFromHomeID(id);

            Debug.WriteLine("Image for HOME" + id);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string link = (string)row["ImgIURL"];
                Debug.WriteLine($"{link}");
                string caption = (string)row["Caption"];
                HomeImage img = new HomeImage(link, caption);
                images.Add(img);
            }
            return images;
        }

        private List<string> CreateAmenityListFromHomeID(int id)
        {
            List<string> amenities = new List<string>();
            DataSet ds = selector.GetAmenitiesFromHomeID(id);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                amenities.Add((string)row["amenityDescription"]);
            }
            return amenities;
        }


        private List<Room> CreateRoomListFromHomeID(int id)
        {
            List<Room> rooms = new List<Room>();
            DataSet ds = selector.GetRoomsFromHomeID(id);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int length = (int)row["homeLength"];
                int width = (int)row["Width"];
                string description = (string)row["RoomDescription"];

                Room room = new Room(length, width, description);
                rooms.Add(room);
            }
            return rooms;
        }

        private List<Offer> GetOffers(int listingID)

        {
            List<Offer> offers = new List<Offer>();
            DataSet offerDataSet = selector.GetOffersByListingID(listingID);

            foreach (DataRow row in offerDataSet.Tables[0].Rows)
            {
                int offerID = (int)row["OfferID"];
                string firstName = row["FirstName"].ToString();
                string lastName = row["LastName"].ToString();
                string phone = row["PhoneNumber"].ToString();
                string email = row["Email"].ToString();
                string purchaseMethod = row["SaleType"].ToString();
                int offerPrice = (int)row["OfferPrice"];
                DateTime moveInDate = (DateTime)row["MoveInDate"];
                bool hasToSellHome = (bool)row["HasToSellHome"];

                List<string> contingencies = new List<string>();
                DataSet ds = selector.GetContingenciesFromOfferID(offerID);
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    contingencies.Add(r["ContingencyDescription"].ToString());
                }

                Offer offer = new Offer(firstName, lastName, phone, email, purchaseMethod, offerPrice, moveInDate, hasToSellHome, contingencies);
                offers.Add(offer);
            }

            return offers;
        }

        private List<Showing> GetShowings(int listingID)
        {
            List<Showing> showings = new List<Showing>();
            DataSet showingDataSet = selector.GetShowingsByListingID(listingID); 

            foreach (DataRow row in showingDataSet.Tables[0].Rows)
            {
                string firstName = row["FirstName"].ToString();
                string lastName = row["LastName"].ToString();
                string phone = row["PhoneNumber"].ToString();
                string email = row["Email"].ToString();
                DateTime appointmentTime = Convert.ToDateTime(row["AppointmentTime"]);

                Showing showing = new Showing(firstName, lastName, email, phone, appointmentTime);
                showings.Add(showing);
            }

            return showings;
        }
    }

}
