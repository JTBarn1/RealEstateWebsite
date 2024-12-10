using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace RealEstateLibrary
{
    public class AgentCreator
    {
        private Agent agent;

        public void CreateAgent(string firstName, string lastName, string personalPhone, string personalEmail,
        string personalStreet, string personalCity, string personalState, string personalZip, string workPhone, string workEmail, string workStreet, string workCity, string workState, string workZip, string agencyName)
        {
            Address workAddress = new Address(workStreet, workCity, workState, workZip);
            Address personalAddress = new Address(personalStreet, personalCity, personalState, personalZip);

            agent = new Agent(firstName, lastName, personalPhone, workPhone, personalEmail, workEmail, workAddress, personalAddress, agencyName);
        }

        public int AddAgentToDataBase(string username, string password)
        {
            if (agent != null)
            {
                SqlInserter inserter = new SqlInserter();
                int fail = inserter.InsertAgent(agent, username, password);

                if (fail != -1)
                {
                    //little workaround to add agent ID to new agent

                    //if time fix? idk how i would do this without querying as i dont manually set the ID

                    SqlSelector selector = new SqlSelector();
                    DataRow row = selector.Login(username, password);
                    int agentId = int.Parse(row["AgentID"].ToString());
                    agent.AddID(agentId);
                    return fail;
                }
                else return fail;
            }
            return -1;
        }

        public Agent GetAgent()
        {
            return agent;
        }
    }
}
