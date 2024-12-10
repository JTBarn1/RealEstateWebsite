using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class Address
    {

        public string Street { get { return street; } }
        public string City { get { return city; } }
        public string State { get { return state; } }
        public string ZipCode { get { return zipCode; } }

        private string street;
        private string city;
        private string state;
        private string zipCode;

        public Address(string street, string city, string state, string zip)
        {
            this.street = street;
            this.city = city;
            this.state = state;
            this.zipCode = zip;
        }
    }
}
