using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using placeAPI.Model;


namespace placeAPI.Transfer
{
    public class Map
    {
        public static void testProgram()
        {
            Console.WriteLine(GetPlaces("-36.848461,174.763336", "bar"));
            Console.WriteLine(GetPlacesFromGeoCodenType("-36.848461,174.763336", "bar"));
            
            Console.ReadLine();
        }

        public static Main GetPlaces(String GeoCode, String Type)
        {

            Main main = new Main
            {
                PlaceName = "Location: " + GeoCode +"Type: "+Type
            };
            return main;
        }

            public static List<PlaceConverter> GetPlacesFromGeoCodenType(String GeoCode, String Type)
        {
            String APIKEY = "AIzaSyDxnLbITe46r20XEo51dgFm8yHeHL4nzT0";
            String LinkforPlaces = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + GeoCode + "&rankby=distance&type=" + Type + "&key=" + APIKEY;
            String PlacesInfoJSON = new WebClient().DownloadString(LinkforPlaces);
            dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(PlacesInfoJSON);

            List<PlaceConverter> PlacesFinal = new List<PlaceConverter>();

            for (int i = 0; i < 20; i++)
            {

                String name = jsonObj["results"][i]["name"];
                String placeidnum = jsonObj["results"][i]["place_id"];

                String LinkforMap = "https://maps.googleapis.com/maps/api/place/details/json?placeid=" + placeidnum + "&key=" + APIKEY;
                String PlacesInfoJSON2 = new WebClient().DownloadString(LinkforMap);
                dynamic jsonObj2 = JsonConvert.DeserializeObject<dynamic>(PlacesInfoJSON2);

                int rank = 100;

                if (jsonObj2["result"]["rating"] != null)
                {
                    rank = jsonObj2["result"]["rating"];
                }

                String photo = "https://shenandoahcountyva.us/bos/wp-content/uploads/sites/4/2018/01/picture-not-available-clipart-12.jpg";

                if (jsonObj2["result"]["photos"] != null)
                {
                    photo = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + jsonObj2["result"]["photos"][0]["photo_reference"] + "&key=" + APIKEY;
                }

                PlaceConverter Place = new PlaceConverter
                {
                    PlaceTitle = name,
                    Photo = photo,
                    PlaceRank = rank,
                    IsFavourite = false,
                    PlaceId = placeidnum
                };

                PlacesFinal.Add(Place);
            }


            return PlacesFinal;
        }


    }
}
