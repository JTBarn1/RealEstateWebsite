using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class Offer
    {
        private string firstName, lastName, phone, email, purchaseMethod;
        private int offerPrice;
        private DateTime preferredMoveIn;
        private List<string> contingencies;
        private bool needToSell;

        public bool NeedToSell {  get { return needToSell; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string PurchaseMethod { get { return purchaseMethod; } set { purchaseMethod = value; } }
        public int OfferPrice { get { return offerPrice; } set { offerPrice = value; } }
        public DateTime PreferredMoveIn { get { return preferredMoveIn; } set { preferredMoveIn = value; } }
        public List<string> Contingencies { get { return contingencies; } set { contingencies = value; } }

        public Offer(string firstName, string lastName, string phone, string email, string purchaseMethod, int offerPrice, DateTime preferredMoveIn, bool needTosell, List<string> contingencies)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.email = email;
            this.purchaseMethod = purchaseMethod;
            this.offerPrice = offerPrice;
            this.preferredMoveIn = preferredMoveIn;
            this.needToSell = needTosell;
            this.contingencies = contingencies;
        }
    }
}
