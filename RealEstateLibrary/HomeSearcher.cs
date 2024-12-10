using System;
using System.Collections.Generic;

namespace RealEstateLibrary
{
    internal class HomeSearcher
    {
        public HomeSearcher() { }

        public List<Listing> TryAddressSearch(List<Listing> listings, string city, string state, string zip)
        {
            List<Listing> result = new List<Listing>();

            if (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(state) && string.IsNullOrEmpty(zip))
            {
                return listings;
            }

            foreach (Listing listing in listings)
            {
                bool matches = true;

                if (!string.IsNullOrEmpty(city) && !listing.Home.Address.City.ToLower().Equals(city.ToLower()))
                {
                    matches = false;
                }

                if (!string.IsNullOrEmpty(state) && !listing.Home.Address.State.ToLower().Equals(state.ToLower()))
                {
                    matches = false;
                }

                if (!zip.Equals(string.Empty) && !listing.Home.Address.ZipCode.Equals(zip))
                {
                    matches = false;
                }

                if (matches)
                {
                    result.Add(listing);
                }
            }

            return result;
        }

        public List<Listing> TryPriceSearch(List<Listing> listings, int minPrice, int maxPrice)
        {
            List<Listing> result = new List<Listing>();

            if (minPrice == 0 && maxPrice == 0)
            {
                return listings;
            }

            foreach (Listing listing in listings)
            {
                bool matches = true;
                int price = listing.AskingPrice;

                if (maxPrice != 0 && price > maxPrice)
                {
                    matches = false;
                }

                if (price < minPrice)
                {
                    matches = false;
                }

                if (matches)
                {
                    result.Add(listing);
                }
            }

            return result;
        }

        public List<Listing> TrySQFTSearch(List<Listing> listings, int sqft)
        {
            List<Listing> result = new List<Listing>();

            if (sqft == 0)
            {
                return listings;
            }

            foreach (Listing listing in listings)
            {
                bool matches = true;
                int sqftListing = listing.Home.SquareFootage;

                if (sqftListing < sqft)
                {
                    matches = false;
                }

                if (matches)
                {
                    result.Add(listing);
                }
            }

            return result;
        }

        public List<Listing> TryBedBathSearch(List<Listing> listings, string beds, string baths)
        {
            List<Listing> result = new List<Listing>();

            if (string.IsNullOrEmpty(beds) && string.IsNullOrEmpty(baths))
            {
                return listings;
            }

            int minBeds;
            int minBaths;
            int.TryParse(beds, out minBeds);
            int.TryParse(baths, out minBaths);

            foreach (Listing listing in listings)
            {
                bool matches = true;

                if (!string.IsNullOrEmpty(beds) && listing.Home.NumBedrooms < minBeds)
                {
                    matches = false;
                }
                if (!string.IsNullOrEmpty(baths) && listing.Home.NumBathrooms < minBaths)
                {
                    matches = false;
                }

                if (matches)
                {
                    result.Add(listing);
                }
            }

            return result;
        }

        public List<Listing> TryAmenitySearch(List<Listing> listings, List<string> amenities)
        {
            List<Listing> result = new List<Listing>();

            if (amenities.Count == 0)
            {
                return listings;
            }

            foreach (Listing listing in listings)
            {
                bool matches = true;

                foreach (string amenity in amenities)
                {
                    if (!listing.Home.Amenities.Contains(amenity))
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                {
                    result.Add(listing);
                }
            }

            return result;
        }
        public List<Listing> TryTypeSearch(List<Listing> listings, string type)
        {
            List<Listing> result = new List<Listing>();

            if (type == "")
            {
                return listings;
            }

            foreach (Listing listing in listings)
            {
                bool matches = true;

                if (!listing.Home.Type.Equals(type))
                {
                    matches = false;
                }

                if (matches)
                {
                    result.Add(listing);
                }
            }

            return result;
        }
    }
}
