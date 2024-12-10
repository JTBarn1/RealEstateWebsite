using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class Agent
    {
        private string firstName;
        private string lastName;

        private string personalPhone;
        private string workPhone;

        private string personalEmail;
        private string workEmail;

        private Address workAddress;
        private Address personalAddress;

        private string companyName;

        private int id;

        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string PersonalPhone { get { return personalPhone; } set { personalPhone = value; } }
        public string WorkPhone { get { return workPhone; } set { workPhone = value; } }
        public string PersonalEmail { get { return personalEmail; } set { personalEmail = value; } }
        public string WorkEmail { get { return workEmail; } set { workEmail = value; } }
        public Address WorkAddress { get { return workAddress; } set { workAddress = value; } }
        public Address PersonalAddress { get { return personalAddress; } set { personalAddress = value; } }
        public string CompanyName { get { return companyName; } set { companyName = value; } }


        public Agent(string firstName, string lastName, string personalPhone, string workPhone, string personalEmail, string workEmail, Address workAddress, Address personalAddress, string companyName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.personalPhone = personalPhone;
            this.workPhone = workPhone;
            this.workAddress = workAddress;
            this.personalAddress = personalAddress;
            this.companyName = companyName;
            this.personalEmail = personalEmail;
            this.workEmail = workEmail;
        }

        public void AddID(int id)
        {
            this.id = id;
        }
        public int GetId() { return id; }   
    }
}
