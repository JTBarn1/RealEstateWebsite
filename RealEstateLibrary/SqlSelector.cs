using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace RealEstateLibrary
{
    internal class SqlSelector
    {
        DBConnect objDB = new DBConnect();

        SqlCommand objCommand = new SqlCommand();

        public DataRow Login(string username, string password)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "Login";

            objCommand.Parameters.Add(CreateVarCharParameter("@AgentUsername", username, 50));
            objCommand.Parameters.Add(CreateVarCharParameter("@AgentPassword", password, 50));

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds.Tables[0].Rows[0];
        }

        public DataSet GetAmenities()
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAmenities";

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds;
        }

        public DataSet GetListings()
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetListingsAndHomes";

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds;

        }

        internal DataSet GetAmenitiesFromHomeID(int id)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getHomeAmenities";

            objCommand.Parameters.Add(CreateIntParameter("@homeID", id));

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds;
        }

        internal DataSet GetRoomsFromHomeID(int id)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "getHomeRooms";

            objCommand.Parameters.Add(CreateIntParameter("@homeID", id));

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds;
        }

        internal DataRow GetAgentFromID(int agentID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAgentFromID";

            objCommand.Parameters.Add(CreateIntParameter("@agentID", agentID));

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds.Tables[0].Rows[0];
        }

        internal DataSet GetImagesFromHomeID(int id)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetImagesFromHomeID";

            objCommand.Parameters.Add(CreateIntParameter("@homeID", id));

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds;
        }
        

        internal DataSet GetOffersByListingID(int listingID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetOffersByListingID";

            objCommand.Parameters.Add(CreateIntParameter("@ListingID", listingID));

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds;
        }

        internal DataSet GetShowingsByListingID(int listingID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetShowingsByListingID";

            objCommand.Parameters.Add(CreateIntParameter("@ListingID", listingID));

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds;
        }

        internal DataSet GetContingenciesFromOfferID(int offerID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetContingencyByOfferID";

            objCommand.Parameters.Add(CreateIntParameter("@OfferID", offerID));

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds;
        }
        internal DataTable GetCheckedAmenities(int ID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "FindCheckedAmenities";

            objCommand.Parameters.Add(CreateIntParameter("@ListingID", ID));

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds.Tables[0];
        }
        private SqlParameter CreateIntParameter(string paramName, int value)
        {
            SqlParameter inputParameter = new SqlParameter(paramName, value);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            return inputParameter;
        }
        private SqlParameter CreateVarCharParameter(string paramName, string value, int size)
        {

            SqlParameter inputParameter = new SqlParameter(paramName, value);

            inputParameter.Direction = ParameterDirection.Input;

            inputParameter.SqlDbType = SqlDbType.VarChar;

            inputParameter.Size = size;

            return inputParameter;
        }

        internal int GetHomeIDfromListing(int listingID)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetHomeID";

            objCommand.Parameters.Add(CreateIntParameter("@ListingID", listingID));

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return Convert.ToInt32(ds.Tables[0].Rows[0]["HomeID"]);
        }

        internal DataTable GetDupes(string amenity)
        {
            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CheckForAmenityDupes";

            objCommand.Parameters.Add(CreateVarCharParameter("@AmenityDesc", amenity,50));

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            return ds.Tables[0];
        }
    }
}
