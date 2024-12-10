using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class ListingCreationController
    {
        private DataSet AmenitySet;
        Agent agent;
        private List<int> checkedAmenities = new List<int>();
        private List<Room> rooms = new List<Room>();
        private List<string> newAmenities = new List<string>();
        private List<RawImage> images = new List<RawImage>();
        public void SetAgent(Agent agent)
        {
            this.agent = agent;
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
        public List<int> GetCheckedamenities()
        {
            return checkedAmenities;
        }
        public void SetCheckedAmenities(List<int> checkedAmenities)
        {
            this.checkedAmenities = checkedAmenities;
        }

        public void AddRoomToList(int length, int width, string description)
        {
            Room newRoom = new Room(length, width, description);
            rooms.Add(newRoom);
        }

        public void AddCustomAmenityToList(string amenitydescription)
        {
            newAmenities.Add(amenitydescription);
        }

        public void AddImage(byte[] imageData, string imageName, string imageType, string caption, string path)
        {
            RawImage img = new RawImage(imageData, imageType, imageName, caption, path);
            images.Add(img);
        }

        public void CreateListing(string steet, string city, string state, string zip, string yearBuilt, string heatingType, string coolingType,
        string waterType, string sewageType, int numBedrooms, float numbBathrooms, int askingPrice, int numCarsInGarage, string desc, string type)
        {
            Address addy = new Address(steet, city, state, zip);

            Home home = new Home(addy, yearBuilt, heatingType, coolingType, waterType, sewageType, numBedrooms, numbBathrooms, numCarsInGarage, desc, type, rooms, newAmenities, checkedAmenities, images);

            SqlInserter inserter = new SqlInserter();
            SqlSelector selector = new SqlSelector();

            int homeID = inserter.InsertHomeIntoDataBase(home);

            foreach (Room room in home.Rooms)
            {
                inserter.InsertRoomintoDatabase(room, homeID);
            }
            foreach (int id in home.ExistingAmenities)
            {
                inserter.InsertAmenityReference(homeID, id);
            }
            foreach (string amenity in home.NewAmenities)
            {
                DataTable dt = selector.GetDupes(amenity);
                if (dt.Rows.Count != 0)
                {
                    inserter.InsertAmenityReference(homeID, Convert.ToInt32(dt.Rows[0]["AmenityID"]));
                }
                else
                {
                    int id = inserter.InsertNewAmenity(amenity);
                    inserter.InsertAmenityReference(homeID, id);
                }
            }

            int i = 0;
            foreach (RawImage img in home.RawImages)
            {
                string addressNoSpaces = home.Address.Street.Trim(' ');
                string regpath = "https://cis-iis2.temple.edu/Fall2024/CIS3342_tuo64086/Project3/FileStorage/";

                File.WriteAllBytes(img.ImagePath + addressNoSpaces + i + img.ImageName, img.ImageData);
                inserter.InsertImage(regpath + addressNoSpaces + + i + img.ImageName, homeID, img.Caption);
                i++;
            }

            DateTime listingTime= DateTime.Now;
            inserter.InsertListing(homeID,agent.GetId(),askingPrice,"For Sale", listingTime);
        }

        public DataTable GetRoomDataTable()
        {
            DataTable dt = new DataTable("Rooms");

            dt.Columns.Add("RoomName");
            dt.Columns.Add("RoomLength");
            dt.Columns.Add("RoomWidth");
            dt.Columns.Add("RoomSize");

            foreach (Room room in rooms)
            {
                dt.Rows.Add(room.Description,room.Length,room.Width,room.SquareFeet);
            }
            return dt;
        }
        public List<string> GetCustomAmenities()
        {
            return newAmenities;
        }

        public DataTable GetImageDataTable()
        {
            DataTable dt = new DataTable("Images");
            dt.Columns.Add("ImageName");
            dt.Columns.Add("ImageType");

            foreach(RawImage img in images)
            {
                dt.Rows.Add(img.ImageName, img.ImageType);
            }
            return dt;
        }

        public void RemoveRoom(int id)
        {
            rooms.RemoveAt(id);
        }

        public void RemoveAmenity(int id)
        {
            newAmenities.RemoveAt(id);
        }

        public void RemoveImage(int id)
        {
            images.RemoveAt(id);
        }

        public string ValidateImages()
        {
            if (images.Count == 0)
            {
                return "<p> ERROR: You must Upload at least one image. </p> <br/>";
            }
            else return "";
        }

        public string ValidateAmenities()
        {
            if (checkedAmenities.Count == 0 && newAmenities.Count == 0)
            {
                return "<p> ERROR: You must Select or create at least one amenity. </p> <br/>";
            }
            else return "";
        }
        public string ValidateRooms()
        {
            if (rooms.Count == 0)
            {
                return "<p> ERROR: You must create at least one room. </p>";
            }
            else return "";
        }
    }
}
