using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace RealEstateLibrary
{
    public class HousingController
    {
        List<Listing> listings;
        List<Listing> listingQuery;
        SqlSelector selector = new SqlSelector();


        public string CreateHouseHTML(int num)
        {
            string type;
            if (listings[num].Home.Type.Equals("Single Family")) type = "Single Family Home";
            else type = listings[num].Home.Type;

            int numDays = (int)(DateTime.Now - listings[num].DateListed).TotalDays;
            Debug.WriteLine("Home display");

            string card = "<div class='col'>" +
                " <div class='card'>" +
                $" <img img alt='Card image cap' src='{listings[num].Home.Images[0].Link}' class='card-img-top'/>" +
                " <div class='card-body'>" +
                $" <h3 class='card-title'>${listings[num].AskingPrice}</h3>" +
                $" <p class='card-text'>{listings[num].Home.NumBedrooms}bds | {listings[num].Home.NumBathrooms}ba | <span class='bold'>{listings[num].Home.SquareFootage} Sqft</span>  - {type} {listings[num].Status}</p>" +
                $" <p class='card-text'>Time on market: {numDays} days</p>" +
                $" <p class='card-text'><small class='text-body-secondary'> {listings[num].Home.Address.Street} | {listings[num].Home.Address.City}, {listings[num].Home.Address.State} | {listings[num].Home.Address.ZipCode}</small></p>" +
                " <div class='card text-center'>";
            return card;
        }
        public string CreateQueryHouseHTML(int num)
        {
            string type;
            if (listingQuery[num].Home.Type.Equals("Single Family")) type = "Single Family Home";
            else type = listingQuery[num].Home.Type;

            int numDays = (int)(DateTime.Now - listingQuery[num].DateListed).TotalDays;

            string card = "<div class='col'>" +
                " <div class='card'>" +
                $" <img img alt='Card image cap' src='{listingQuery[num].Home.Images[0].Link}' class='card-img-top'/>" +
                " <div class='card-body'>" +
                $" <h3 class='card-title'>${listingQuery[num].AskingPrice}</h3>" +
                $" <p class='card-text'>{listingQuery[num].Home.NumBedrooms}bds | {listingQuery[num].Home.NumBathrooms}ba | <span class='bold'>{listingQuery[num].Home.SquareFootage} Sqft</span>  - {type} {listingQuery[num].Status}</p>" +
                $" <p class='card-text'>Time on market: {numDays} days</p>"+
                $" <p class='card-text'><small class='text-body-secondary'> {listingQuery[num].Home.Address.Street} | {listingQuery[num].Home.Address.City}, {listingQuery[num].Home.Address.State} | {listingQuery[num].Home.Address.ZipCode}</small></p>" +
                " <div class='card text-center'>";
            return card;
        }
        
        public void GetListings()
        {
            ListingCreator creator = new ListingCreator();
            listings = creator.GetListings();
        }

        public int GetNumListings()
        {
            return listings.Count;
        }
        public bool SearchExists()
        {
            if (listingQuery == null)
            {
                return false;
            }
            else return true;
        }
        public int GetNumQueriedListings()
        {
            return listingQuery.Count;
        }

        public Listing GetListingFromID(int id)
        {
            return listings[id];
        }
        public Listing GetQueriedListingFromID(int id)
        {
            return listingQuery[id];
        }

        public void SearchAttempt(string city, string state, string zip, int minPrice, int maxPrice, int sqft, string homeType, string beds, string baths, List<string> requiredAmenities)
        {
            HomeSearcher searcher = new HomeSearcher();
            List<Listing> newListingQuery = new List<Listing>();
            newListingQuery = searcher.TryAddressSearch(listings, city, state, zip);
            newListingQuery = searcher.TryPriceSearch(newListingQuery, minPrice,maxPrice);
            newListingQuery = searcher.TrySQFTSearch(newListingQuery, sqft);
            newListingQuery = searcher.TryBedBathSearch(newListingQuery, beds,baths);
            newListingQuery = searcher.TryAmenitySearch(newListingQuery, requiredAmenities);
            newListingQuery = searcher.TryTypeSearch(newListingQuery, homeType);

            listingQuery = newListingQuery;
        }

        public void ResetQuery()
        {
            listingQuery = null;
        }
    }
}
