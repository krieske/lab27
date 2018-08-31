using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace lab27.Controllers
{
    public class HomeController : Controller

    { 

        public ActionResult Index()
    {
        return View();

    }
        public ActionResult GetWeather(string lat, string lon)
        {
            if (lat == null)
            {
                lat = "38.4247341";
            }
            else if (lon == null)
            {
                lon = "-86.9624086";
            }

            HttpWebRequest apiRequest = WebRequest.CreateHttp($"https://forecast.weather.gov/MapClick.php?lat={lat}&lon={lon}&FcstType=json");
            apiRequest.UserAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";

            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();
            // (httpwebresponse) is telling the api request.getresponse that its coming from an http response

            if (apiResponse.StatusCode == HttpStatusCode.OK) // if we got a status == 200, everything is A-Okay!
            {
                // get the data and then parse it
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());

                string forecast = responseData.ReadToEnd(); // reads the data from the response

                //To Do: parse the JSON data
                JObject jsonForecast = JObject.Parse(forecast);

                ViewBag.forecastPic = jsonForecast["data"]["iconLink"];
                ViewBag.forecast = jsonForecast["data"]["text"];
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
