using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace RealEstateLibrary
{
    internal class SqlUpdater
    {
        DBConnect objDB = new DBConnect();

        SqlCommand objCommand = new SqlCommand();
        internal void DropAllExistingAmenities(int homeID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "RemoveAmenitiesByHomeID";

            objCommand.Parameters.Add(CreateIntParameter("@HomeID", homeID));

            objDB.DoUpdate(objCommand);
        }

        internal void DropAllExistingRooms(int homeID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "RemoveRoomsByHomeID";

            objCommand.Parameters.Add(CreateIntParameter("@HomeID", homeID));

            objDB.DoUpdate(objCommand);
        }

        internal void DropAllImageReferences(int homeID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "RemoveImagesByHomeID";

            objCommand.Parameters.Add(CreateIntParameter("@HomeID", homeID));

            objDB.DoUpdate(objCommand);
        }

        internal void UpdateHomeData(Home home, int homeID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateHome";

            objCommand.Parameters.Add(CreateIntParameter("@HomeID", homeID));
            objCommand.Parameters.Add(CreateVarCharParameter("@StreetName", home.Address.Street, 100));
            objCommand.Parameters.Add(CreateVarCharParameter("@City", home.Address.City, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@homeState", home.Address.State, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@Zip", home.Address.ZipCode, 50));
            objCommand.Parameters.Add(CreateIntParameter("@SquareFootage", home.SquareFootage));
            objCommand.Parameters.Add(CreateIntParameter("@NumBedrooms", home.NumBedrooms));
            objCommand.Parameters.Add(CreateFloatParameter("@NumBathrooms", home.NumBathrooms));
            objCommand.Parameters.Add(CreateVarCharParameter("@YearBuilt", home.YearBuilt, 50));
            objCommand.Parameters.Add(CreateIntParameter("@GarageType", home.NumCarsInGarage));
            objCommand.Parameters.Add(CreateVarCharParameter("@WaterUtilityType", home.WaterType, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@SewerUtilityType", home.SewageType, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@HeatingType", home.HeatingType, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@CoolingType", home.CoolingType, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@HomeType", home.Type, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@HomeDescription", home.Desc, -1));

            objDB.DoUpdate(objCommand);
        }

        internal void UpdateListing(int iD, int askingPrice, string status)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateListing";

            objCommand.Parameters.Add(CreateIntParameter("@ListingID", iD));
            objCommand.Parameters.Add(CreateFloatParameter("@AskingPrice", askingPrice));
            objCommand.Parameters.Add(CreateVarCharParameter("@ListingStatus", status, 50));

            objDB.DoUpdate(objCommand);
        }

        internal void RemoveOffer(int iD, int offerPrice, string firstName, string lastName)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "RemoveOffer";

            objCommand.Parameters.Add(CreateIntParameter("@ListingID", iD));
            objCommand.Parameters.Add(CreateVarCharParameter("@FirstName", firstName, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@LastName", lastName, 100));
            objCommand.Parameters.Add(CreateIntParameter("@OfferPrice", offerPrice));

            objDB.DoUpdate(objCommand);

        }

        internal void RemoveAllOffers(int iD)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "RemoveOffers";

            objCommand.Parameters.Add(CreateIntParameter("@ListingID", iD));

            objDB.DoUpdate(objCommand);

        }

        internal void UpdateListingStatus(int iD, string status)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateListingStatus";

            objCommand.Parameters.Add(CreateIntParameter("@ListingID", iD));
            objCommand.Parameters.Add(CreateVarCharParameter("@Listingstatus", status, 50));

            objDB.DoUpdate(objCommand);
        }
        private SqlParameter CreateVarCharParameter(string paramName, string value, int size)
        {
            SqlParameter inputParameter = new SqlParameter(paramName, value);

            inputParameter.Direction = ParameterDirection.Input;

            inputParameter.SqlDbType = SqlDbType.VarChar;

            inputParameter.Size = size;

            return inputParameter;
        }
        private SqlParameter CreateFloatParameter(string paramName, float value)
        {
            SqlParameter inputParameter = new SqlParameter(paramName, value);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Float;
            return inputParameter;
        }

        private SqlParameter CreateIntParameter(string paramName, int value)
        {
            SqlParameter inputParameter = new SqlParameter(paramName, value);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            return inputParameter;
        }

        internal void RemoveListingAndAllReferences(int iD, int homeID)
        {
            throw new NotImplementedException();
        }

        internal void RemoveListing(int iD, int homeID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "RemoveListingData";

            objCommand.Parameters.Add(CreateIntParameter("@ListingID", iD));
            objCommand.Parameters.Add(CreateIntParameter("@HomeID", homeID));

            objDB.DoUpdate(objCommand);
        }
    }
}
