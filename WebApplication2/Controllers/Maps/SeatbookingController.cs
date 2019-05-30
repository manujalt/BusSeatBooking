using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApplication2;

namespace EJ2MVCSampleBrowser.Controllers.Maps
{
    public partial class MapsController : Controller
    {
        private TestEntities db = new TestEntities();
        // GET: Seatbooking
        public ActionResult Seatbooking()
        {
            //use from file for development
            //ViewBag.shapeData = this.SeatData();
            ViewBag.shapeData = SeatDataFromDatabase();
            return View();
        }
        public object SeatData()
        {
            string allText = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MapData/Seat.json"));
            return JsonConvert.DeserializeObject(allText, typeof(object));
        }

        public object SeatDataFromDatabase()
        {
            var v= db.SeatSelections.ToList();
            System.Text.StringBuilder builder = new System.Text.StringBuilder("");
            builder.Append("{\'type\': \'FeatureCollection\', \'features\': [ \r\n ");

            foreach (var eachItem in v)
            {
                builder.Append("{ \'type\': \'Feature\', \'geometry\': { \'type\': \'MultiPolygon\', \'coordinates\': ");
                builder.Append("[[[[");

                string s = eachItem.seat.AsText();
                int count = 0;
                string[] numbers = Regex.Split(s, @"\D+");
                foreach (string value in numbers)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        int i = int.Parse(value);

                        if (count == 1)
                        {
                            builder.Append(i);
                        }
                        else if (count == 20)
                        {
                            builder.Append(", ");
                            builder.Append(i);
                        }
                        else if (count == 11)
                        {
                            
                            builder.Append("]]], [[[");
                            builder.Append(i);
                        }
                        else 
                        {
                            if (count % 2 == 0)
                            {
                                builder.Append(", ");
                                builder.Append(i);
                            }
                            else
                            {
                                builder.Append("], [");
                                builder.Append(i);
                            }
                        }   
                    }
                    count++;
                }
                builder.Append("]]]] ");
                builder.Append("}, \'properties\': { \'seatno\': " + eachItem.seatno + ", \'fill\': \'"+ eachItem.fill + "\' } }, \r\n");
            }
            builder.Remove(builder.Length - 4, 4);
            builder.Append("] }");
            builder.Replace("'", "\"");
            string allText =  builder.ToString();
            return JsonConvert.DeserializeObject(allText.ToString(), typeof(object));
        }
    }
}