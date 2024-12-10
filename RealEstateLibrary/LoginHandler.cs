using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class LoginHandler
    {
        private DataRow agentRow;
        private Agent agent;
        public string Loginattempt(string username, string password)
        {
            SqlSelector selector = new SqlSelector();

            string error = "";
            try
            {
                agentRow = selector.Login(username, password);
                return error;
            }
            catch (Exception ex)
            {
                error = "ERROR: Account not found.";
                return error;
            }
        }

        public void CreateAgentFromTable()
        {
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
            Address workAddress = new Address(workState, workStreet, workCity, workZip);

            agent = new Agent(firstName, lastName, personalPhone, workPhone, personalEmail, workEmail, workAddress, personalAddress, agencyName);
            agent.AddID(agentId);
        }

        public Agent GetAgent()
        {
            return agent;
        }
    }
}
