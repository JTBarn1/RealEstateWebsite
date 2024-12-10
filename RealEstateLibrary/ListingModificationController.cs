using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace RealEstateLibrary
{
    public class ListingModificationController
    {
        private DataSet AmenitySet;
        private Listing listing;
        private List<string> amenities = new List<string>();
        private List<RawImage> newImages = new List<RawImage>();
        private List<int> checkedAmenities = new List<int>();
        public ListingModificationController(Listing listing)
        {
            this.listing = listing;

            SqlSelector selector = new SqlSelector();
            DataTable dt = selector.GetCheckedAmenities(listing.ID);
            foreach(DataRow row in dt.Rows)
            {
                checkedAmenities.Add(Convert.ToInt32(row["AmenityID"]));
            }
        }

        public void AddCustomAmenityToList(string amenitydescription)
        {
            amenities.Add(amenitydescription);
        }

        public void AddImage(byte[] imageData, string imageName, string imageType, string caption, string path)
        {
            RawImage img = new RawImage(imageData, imageType, imageName, caption, path);
            newImages.Add(img);
        }

        public void AddRoomToList(int length, int width, string description)
        {
            Room newRoom = new Room(length, width, description);
            listing.Home.Rooms.Add(newRoom);
        }

        public DataSet RetreiveAmenities()
        {
            SqlSelector selector = new SqlSelector();

            DataSet ds = selector.GetAmenities();

            AmenitySet = ds;

            return ds;
        }
        public DataSet GetSet()
        {
            return AmenitySet;
        }

        public DataRow GetAllListingInfo()
        {
            DataTable dt = new DataTable("Listings");

            dt.Columns.Add("ListingStreet");
            dt.Columns.Add("ListingCity");
            dt.Columns.Add("ListingState");
            dt.Columns.Add("ListingZip");
            dt.Columns.Add("ListingPrice");
            dt.Columns.Add("ListingStatus");
            dt.Columns.Add("ListingNumBeds");
            dt.Columns.Add("ListingNumBathrooms");
            dt.Columns.Add("ListingYear");
            dt.Columns.Add("ListingDescription");
            dt.Columns.Add("ListingHomeType");
            dt.Columns.Add("ListingGarageSize");
            dt.Columns.Add("ListingHeatType");
            dt.Columns.Add("ListingCoolingType");
            dt.Columns.Add("ListingWaterType");
            dt.Columns.Add("ListingSewageType");

            dt.Rows.Add(listing.Home.Address.Street,
                listing.Home.Address.City,
                listing.Home.Address.State,
                listing.Home.Address.ZipCode,
                listing.AskingPrice,
                listing.Status,
                listing.Home.NumBedrooms,
                listing.Home.NumBathrooms,
                listing.Home.YearBuilt,
                listing.Home.Desc,
                listing.Home.Type,
                listing.Home.NumCarsInGarage,
                listing.Home.HeatingType,
                listing.Home.CoolingType,
                listing.Home.WaterType,
                listing.Home.SewageType);

            return dt.Rows[0];
        }

        public DataTable GetRoomDataTable()
        {
            DataTable dt = new DataTable("Rooms");

            dt.Columns.Add("RoomName");
            dt.Columns.Add("RoomLength");
            dt.Columns.Add("RoomWidth");
            dt.Columns.Add("RoomSize");

            foreach (Room room in listing.Home.Rooms)
            {
                Debug.WriteLine("room");
                dt.Rows.Add(room.Description, room.Length, room.Width, room.SquareFeet);
            }
            return dt;
        }
        public List<string> GetCustomAmenities()
        {
            return amenities;
        }

        public DataTable GetExistingImageDataTable()
        {
            DataTable dt = new DataTable("Images");
            dt.Columns.Add("ImageName");
            dt.Columns.Add("ImageCaption");

            foreach (HomeImage img in listing.Home.Images)
            {
                dt.Rows.Add(img.Link, img.Caption);
            }
            return dt;
        }
        public DataTable GetNewImageDataTable()
        {
            DataTable dt = new DataTable("Images");
            dt.Columns.Add("ImageName");
            dt.Columns.Add("ImageCaption");

            foreach (RawImage img in newImages)
            {
                dt.Rows.Add(img.ImageName, img.Caption);
            }
            return dt;
        }

        public void RemoveRoom(int id)
        {
            listing.Home.Rooms.RemoveAt(id);
        }

        public void RemoveAmenity(int id)
        {
            amenities.RemoveAt(id);
        }
        public void RemoveImage(int id)
        {
            listing.Home.Images.RemoveAt(id);
        }
        public void RemoveNewImage(int id)
        {
            newImages.RemoveAt(id);
        }


        public List<int> GetCheckedamenities()
        {
            return checkedAmenities;
        }

        public void UpdateListingData(string steet, string city, string state, string zip, string yearBuilt, string heatingType, string coolingType,
        string waterType, string sewageType, int numBedrooms, float numbBathrooms, int askingPrice, int numCarsInGarage, string desc, string type, string status)
        {
            Address addy = new Address(steet, city, state, zip);

            Home home = new Home(addy, yearBuilt, heatingType, coolingType, waterType, sewageType, numBedrooms, numbBathrooms, numCarsInGarage, desc, type, listing.Home.Rooms, amenities, checkedAmenities, newImages);

            SqlUpdater updater = new SqlUpdater();
            SqlInserter inserter = new SqlInserter();
            SqlSelector selector = new SqlSelector();

            int homeID = selector.GetHomeIDfromListing(listing.ID);
            updater.UpdateHomeData(home, homeID);

            updater.DropAllExistingRooms(homeID);
            foreach (Room room in home.Rooms)
            {
                inserter.InsertRoomintoDatabase(room, homeID);
            }
            updater.DropAllExistingAmenities(homeID);
            foreach (int id in home.ExistingAmenities)
            {
                inserter.InsertAmenityReference(homeID, id);
            }
            foreach (string amenity in home.NewAmenities)
            {
                int id = inserter.InsertNewAmenity(amenity);
                inserter.InsertAmenityReference(homeID, id);
            }

            updater.DropAllImageReferences(homeID);

            int i = 0;
            foreach (HomeImage img in listing.Home.Images)
            {
                inserter.InsertImage(img.Link, homeID, img.Caption);
                i++;
            }
            foreach (RawImage img in home.RawImages)
            {
                string addressNoSpaces = home.Address.Street.Trim(' ');
                string regpath = "https://cis-iis2.temple.edu/Fall2024/CIS3342_tuo64086/Project3/FileStorage/";

                File.WriteAllBytes(img.ImagePath + addressNoSpaces + i + img.ImageName, img.ImageData);
                inserter.InsertImage(regpath + addressNoSpaces + +i + img.ImageName, homeID, img.Caption);
                i++;
            }
            updater.UpdateListing(listing.ID, askingPrice, status);
        }

        public void DeleteHome()
        {
            SqlUpdater updater = new SqlUpdater();
            SqlSelector selector = new SqlSelector();

            int homeID = selector.GetHomeIDfromListing(listing.ID);

            EmailSender emailSender = new EmailSender();

            foreach(Offer offer in listing.Offers)
            {
                emailSender.SendRejectEmail(listing.Agent.WorkEmail, offer.Email, offer.FirstName, listing.Home.Address.Street);
            }
            updater.RemoveListingAndAllReferences(listing.ID,homeID);
        }

        public void RemoveListing()
        {
            SqlUpdater updater = new SqlUpdater();
            SqlSelector selector = new SqlSelector();

            int homeID = selector.GetHomeIDfromListing(listing.ID);
            updater.RemoveListing(listing.ID,homeID);
        }
        public string ValidateImages()
        {
            if (newImages.Count == 0 && listing.Home.Images.Count == 0)
            {
                return "<p> ERROR: You must Upload at least one image. </p>";
            }
            else return "";
        }

        public string ValidateAmenities()
        {
            if (checkedAmenities.Count == 0 && amenities.Count == 0)
            {
                return "<p> ERROR: You must Select or create at least one amenity. </p>";
            }
            else return "";
        }

        public string ValidateRooms()
        {
            if (listing.Home.Rooms.Count == 0)
            {
                return "<p> ERROR: You must create at least one room. </p>";
            }
            else return "";
        }
    }
}
