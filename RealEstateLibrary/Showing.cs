using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class Showing
    {
        private string firstName, lastName, email, phone;
        private DateTime tourDate;
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public DateTime TourDate { get { return tourDate; } set { tourDate = value; } }


        public Showing(string firstName, string lastName, string email, string phone, DateTime tourDate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phone = phone;
            this.tourDate = tourDate;
        }
    }
}
