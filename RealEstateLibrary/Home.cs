using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class Home
    {
        private Address address;
        private string yearBuilt, heatingType, coolingType, waterType, sewageType,desc, type;
        private int numBedrooms,squareFootage,numCarsInGarage;
        private float numBathrooms;
        private List<Room> rooms;
        private List<string> newAmenities;
        private List<int> existingAmenities;
        private List<HomeImage> images;

        //for initialising existing house
        private List<string> amenities;
        private List<RawImage> rawImages;

        public Address Address { get { return address; } set { address = value; } }
        public string YearBuilt { get { return yearBuilt; } set { yearBuilt = value; } }
        public string HeatingType { get { return heatingType; } set { heatingType = value; } }
        public string CoolingType { get { return coolingType; } set { coolingType = value; } }
        public string WaterType { get { return waterType; } set { waterType = value; } }
        public string SewageType { get { return sewageType; } set { sewageType = value; } }
        public int NumBedrooms { get { return numBedrooms; } set { numBedrooms = value; } }
        public float NumBathrooms { get { return numBathrooms; } set { numBathrooms = value; } }
        public List<Room> Rooms { get { return rooms; } set { rooms = value; squareFootage = CalcSquareFootage(); } }
        public List<string> NewAmenities { get { return newAmenities; } set { newAmenities = value; } }
        public List<int> ExistingAmenities { get { return existingAmenities; } set { existingAmenities = value; } }
        public List<string> Amenities { get { return amenities; } set { amenities = value; } }
        public List<RawImage> RawImages { get { return rawImages; } set { rawImages = value; } }
        public List<HomeImage> Images { get { return images; } set { images = value; } }
        public int SquareFootage { get { return squareFootage; } }
        public int NumCarsInGarage { get { return numCarsInGarage; } set { numCarsInGarage = value; } }
        public string Desc { get { return desc; } set { desc = value; } }

        public string Type { get { return type; } set { type = value; } }

        public Home(Address address, string yearBuilt, string heatingType, string coolingType, string waterType, string sewageType, int numBedrooms, float numBathrooms,  int numCarsInGarage, string desc, string type, List<Room> rooms, List<string> newAmenities, List<int> existingAmenities, List<RawImage> images)
        {
            this.address = address;

            this.yearBuilt = yearBuilt;
            this.heatingType = heatingType;
            this.coolingType = coolingType;
            this.waterType = waterType;
            this.sewageType = sewageType;

            this.rooms = rooms;
            this.squareFootage = CalcSquareFootage();

            this.numBedrooms = numBedrooms;
            this.numBathrooms = numBathrooms;

            this.rawImages = images;
            this.newAmenities = newAmenities;
            this.existingAmenities = existingAmenities;

            this.numCarsInGarage = numCarsInGarage; 
            this.desc = desc;
            this.type = type;

        }
        public Home(Address address, string yearBuilt, string heatingType, string coolingType, string waterType, string sewageType, int numBedrooms, float numBathrooms,  int numCarsInGarage, string desc, string type, List<Room> rooms, List<string> amenities, List<HomeImage> images, int sqft)
        {
            this.address = address;

            this.yearBuilt = yearBuilt;
            this.heatingType = heatingType;
            this.coolingType = coolingType;
            this.waterType = waterType;
            this.sewageType = sewageType;

            this.rooms = rooms;
            this.squareFootage = sqft;

            this.numBedrooms = numBedrooms;
            this.numBathrooms = numBathrooms;

            this.images = images;
            this.amenities = amenities;

            this.numCarsInGarage = numCarsInGarage; 
            this.desc = desc;
            this.type = type;
        }

        private int CalcSquareFootage()
        {
            int total = 0;
            foreach (var room in this.rooms)
            {
                total += room.SquareFeet;
            }
            return total;
        }
    }
}
