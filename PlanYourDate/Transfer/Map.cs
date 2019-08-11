using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlanYourDate.Model;

namespace PlanYourDate.Transfer
{
    public class Map
    {
        public static void TestProgram()
        {
            Console.WriteLine(GetReviews("ChIJaeh98trYbW0R1ZunnXwIot4"));
            Console.ReadLine();
        }

        public static Places GetPlaceFromId(String PlaceID)
        {
            String APIKEY = "AIzaSyDxnLbITe46r20XEo51dgFm8yHeHL4nzT0";
            String LinkforMap = "https://maps.googleapis.com/maps/api/place/details/json?placeid=" + PlaceID + "&key=" + APIKEY;

            String PlacesInfoJSON = new WebClient().DownloadString(LinkforMap);

            dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(PlacesInfoJSON);

            String Name = jsonObj["result"]["name"];
            String Address = jsonObj["result"]["formatted_address"];
            String PhoneNo = jsonObj["result"]["formatted_phone_number"];
            int Rating = jsonObj["result"]["rating"];
            Boolean OpenNow = jsonObj["result"]["opening_hours"]["open_now"];
            String Photo = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + jsonObj["result"]["photos"][0]["photo_reference"] + "&key=" + APIKEY;


            Places place = new Places
            {
                PlaceName = Name,
                PlaceAddress = Address,
                Comment = "No Comments Shown.. Please Comment Me!!",
                RankBy = Rating,
                PhotoRef = Photo,
                IsFavourited = false,
                PhoneNumber = PhoneNo,
                OpenNow = OpenNow
            };
            return place;
        }

        public static List<Reviews> GetReviews(String PlaceID)
        {
            String APIKEY = "AIzaSyDxnLbITe46r20XEo51dgFm8yHeHL4nzT0";
            String LinkforMap = "https://maps.googleapis.com/maps/api/place/details/json?placeid=" + PlaceID + "&key=" + APIKEY;

            String PlacesInfoJSON = new WebClient().DownloadString(LinkforMap);

            dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(PlacesInfoJSON);
            List<Reviews> reviewFinal = new List<Reviews>();

            for (int i = 0; i < 5; i++)
            {

                String Name = jsonObj["result"]["reviews"][i]["author_name"];
                int ratingPt = jsonObj["result"]["reviews"][i]["rating"];
                String comment = jsonObj["result"]["reviews"][i]["text"];

                Reviews Review = new Reviews
                {
                    AuthorName = Name,
                    Rating = ratingPt,
                    Comment = comment
                };

                reviewFinal.Add(Review);
            }

            return reviewFinal;
        }






    }
}
