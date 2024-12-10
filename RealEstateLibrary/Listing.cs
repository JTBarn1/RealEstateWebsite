using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class Listing
    {
        private Home home;
        private DateTime dateListed;
        private Agent agent;
        private List<Showing> showings = new List<Showing>();
        private List<Offer> offerList = new List<Offer>();
        private int askingPrice;
        private string status;
        private int id;

        public Home Home { get { return home; } }
        public DateTime DateListed { get { return dateListed; } }
        public Agent Agent { get { return agent; } }
        public int AskingPrice { get { return askingPrice; } }
        public string Status { get { return status; } }
        public int ID { get { return id; } }

        public List<Showing> Showings { get { return showings; } set { showings = value; } }
        public List<Offer> Offers { get { return offerList; } set { offerList = value; } }

        public Listing(Home home, DateTime date, Agent agent, int askingPrice, string status)
        {
            this.home = home;
            this.dateListed = date;
            this.agent = agent;
            this.askingPrice = askingPrice;
            this.status = status;
        }


        public Listing(int id, Home home, DateTime dateListed, Agent agent, int askingPrice, string status, List<Offer> offers, List<Showing> showings)
        {
            this.id = id;
            this.home = home;
            this.dateListed = dateListed;
            this.agent = agent;
            this.askingPrice = askingPrice;
            this.status = status;
            this.offerList = offers;
            this.showings = showings;
        }
    }
    }
