using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace halp.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult Index()
        {
            string cs = @"server=localhost;userid=root;password=;database=findR";
            MySqlConnection conn =null;

            try {
                conn = new MySqlConnection(cs);
                conn.Open();
                
            }catch(MySqlException ex){
                Console.WriteLine("error: {0}", ex.ToString());
            }

            string state = "SELECT * FROM findr.locations";
            var cmd = new MySqlCommand(state,conn);
            var reader = cmd.ExecuteReader();
            List<place> places = new List<place>();
            while (reader.Read()){
                var newPlace = new place();
                newPlace.lat = reader.GetDouble(0);
                newPlace.lon = reader.GetDouble(1);
                newPlace.type = reader.GetString(2);
                newPlace.name = reader.GetString(3);
                newPlace.details = reader.GetString(4);
                newPlace.id = reader.GetInt32(5);
                places.Add(newPlace);
            }
            return Json(places, JsonRequestBehavior.AllowGet);
        }
    }
}