using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utilities;

namespace RealEstateLibrary
{
    internal class SqlInserter
    {
        DBConnect objDB = new DBConnect();

        SqlCommand objCommand = new SqlCommand();
        public int InsertAgent(Agent agent, string username, string password)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "Insertagent";

            objCommand.Parameters.Add(CreateVarCharParameter("@AgentFirstName", agent.FirstName, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentLastName", agent.LastName, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentPersonalPhone", agent.PersonalPhone, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentPersonalEmail", agent.PersonalEmail, 100));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentPersonalStreet", agent.PersonalAddress.Street, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentPersonalCity", agent.PersonalAddress.City, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentPersonalZip", agent.PersonalAddress.ZipCode, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentPersonalState", agent.PersonalAddress.State, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentWorkStreet", agent.WorkAddress.Street, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentWorkCity", agent.WorkAddress.City, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentWorkZip", agent.WorkAddress.ZipCode, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentWorkState", agent.WorkAddress.State, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentWorkPhone", agent.WorkPhone, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentWorkEmail", agent.WorkEmail, 100));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgencyName", agent.CompanyName, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentUsername", username, 50)); 
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentPassword", password, 50));

            return objDB.DoUpdate(objCommand);
        }

        public int InsertHomeIntoDataBase(Home home)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertHome";

            Debug.WriteLine(home.Desc);

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

            SqlParameter HomeId = new SqlParameter("@NewHomeID", SqlDbType.Int);
            HomeId.Direction = ParameterDirection.Output;  
            objCommand.Parameters.Add(HomeId);

            objDB.DoUpdate(objCommand);


            return (int)HomeId.Value;
        }

        public void InsertRoomintoDatabase(Room room, int homeID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertRoom";

            objCommand.Parameters.Add(CreateIntParameter("@HomeID", homeID));
            objCommand.Parameters.Add(CreateIntParameter("@homeLength", room.Length));
            objCommand.Parameters.Add(CreateIntParameter("@Width", room.Width));
            objCommand.Parameters.Add(CreateIntParameter("@SquareFeet", room.SquareFeet));
            objCommand.Parameters.Add(CreateVarCharParameter("@RoomDescription", room.Description, 100));

            objDB.DoUpdate(objCommand);
        }
        public void InsertAmenityReference(int homeID, int amenityID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertHomeAmenity";


            objCommand.Parameters.Add(CreateIntParameter("@HomeID", homeID));
            objCommand.Parameters.Add(CreateIntParameter("@AmenityID", amenityID));

            objDB.DoUpdate(objCommand);
        }

        internal int InsertNewAmenity(string amenity)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertAmenity";

            objCommand.Parameters.Add(CreateVarCharParameter("@amenityDescription", amenity, 100));

            SqlParameter newId = new SqlParameter("@NewAmenityID", SqlDbType.Int);
            newId.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(newId);

            objDB.DoUpdate(objCommand);

            return (int)newId.Value;
        }

        internal void InsertListing(int homeID, int agentID, int askingPrice, string status, DateTime listingDate)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertListing";

            objCommand.Parameters.Add(CreateIntParameter("@AgentID", agentID));
            objCommand.Parameters.Add(CreateIntParameter("@HomeID", homeID));
            objCommand.Parameters.Add(CreateFloatParameter("@AskingPrice", askingPrice));
            objCommand.Parameters.Add(CreateVarCharParameter("@listingStatus", status, 50));
            objCommand.Parameters.Add(CreateDateParameter("@DateListed", listingDate.Date));

            objDB.DoUpdate(objCommand);
        }

        internal void InsertImage(string destinationFilePath, int homeID, string caption)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertImage";

            objCommand.Parameters.Add(CreateIntParameter("@HomeID", homeID));
            objCommand.Parameters.Add(CreateVarCharParameter("@filePath", destinationFilePath, -1));
            objCommand.Parameters.Add(CreateVarCharParameter("@caption", caption, 100));

            objDB.DoUpdate(objCommand);
        }

        internal void AddShowing(Showing showing, int listingID)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertShowing"; 

            objCommand.Parameters.Add(CreateIntParameter("@ListingID", listingID));
            objCommand.Parameters.Add(CreateVarCharParameter("@AppointmentTime", showing.TourDate.ToString("yyyy-MM-dd HH:mm:ss"), 100));
            objCommand.Parameters.Add(CreateVarCharParameter("@FirstName", showing.FirstName, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@LastName", showing.LastName, 100));
            objCommand.Parameters.Add(CreateVarCharParameter("@PhoneNumber", showing.Phone, 100));
            objCommand.Parameters.Add(CreateVarCharParameter("@Email", showing.Email, 100));

            objDB.DoUpdate(objCommand);
        }

        internal int InsertOffer(Offer offer, int listingID)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertOffer"; 

            objCommand.Parameters.Add(CreateIntParameter("@ListingID", listingID));
            objCommand.Parameters.Add(CreateDateParameter("@MoveInDate", offer.PreferredMoveIn));
            objCommand.Parameters.Add(CreateVarCharParameter("@FirstName", offer.FirstName, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@LastName", offer.LastName, 100));
            objCommand.Parameters.Add(CreateVarCharParameter("@PhoneNumber", offer.Phone, 100));
            objCommand.Parameters.Add(CreateVarCharParameter("@Email", offer.Email, 100));
            objCommand.Parameters.Add(CreateIntParameter("@OfferPrice", offer.OfferPrice));
            objCommand.Parameters.Add(CreateVarCharParameter("@SaleType", offer.PurchaseMethod, 50));
            objCommand.Parameters.Add(CreateBitParameter("@HasToSellHome", offer.NeedToSell));

            SqlParameter newOfferID = new SqlParameter("@NewOfferID", SqlDbType.Int);
            newOfferID.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(newOfferID);

            objDB.DoUpdate(objCommand);

            return (int)newOfferID.Value;
        }



        internal void InsertOfferContingencies(List<string> contingencies, int offerID)
        {
            foreach (var contingency in contingencies)
            {
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "InsertContingency"; // Ensure this stored procedure exists in your database

                objCommand.Parameters.Add(CreateIntParameter("@OfferID", offerID));
                objCommand.Parameters.Add(CreateVarCharParameter("@ContingencyDescription", contingency, 100));

                objDB.DoUpdate(objCommand);
            }
        }

        private SqlParameter CreateDateParameter(string paramName, DateTime dateListed)
        {
            SqlParameter inputParameter = new SqlParameter(paramName, dateListed.Date);

            inputParameter.Direction = ParameterDirection.Input;

            inputParameter.SqlDbType = SqlDbType.Date;

            return inputParameter;
        }

        private SqlParameter CreateVarCharParameter(string paramName, string value , int size)
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

        // Modify CreateIntParameter if not already defined
        private SqlParameter CreateIntParameter(string paramName, int value)
        {
            SqlParameter inputParameter = new SqlParameter(paramName, value);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            return inputParameter;
        }

        private SqlParameter CreateBitParameter(string paramName, bool needToSell)
        {
            SqlParameter inputParameter = new SqlParameter(paramName, needToSell);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Bit;
            return inputParameter;
        }


    }
}
