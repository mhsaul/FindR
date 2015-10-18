using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace DataScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            const string key = ""; // Google places api key
            using (var client = new HttpClient())
            {
                Console.Write("Lon: ");
                double lon = double.Parse(Console.ReadLine());
                Console.Write("Lat: ");
                double lat = double.Parse(Console.ReadLine());

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Type: ");
                string type = Console.ReadLine();

                var sb = new StringBuilder("https://maps.googleapis.com/maps/api/place/nearbysearch/json?");
                sb.Append($"key={key}");
                sb.Append($"&location={lon},{lat}");
                sb.Append($"&name={name}");
                sb.Append("&radius=50000");
                var res = client.GetAsync(sb.ToString()).Result;

                var content = res.Content.ReadAsStringAsync().Result;

                var des = new System.Web.Script.Serialization.JavaScriptSerializer();
                var data = des.Deserialize<Rootobject>(content);

                var list = new List<Result>(data.results.ToList());

                while (!string.IsNullOrWhiteSpace(data.next_page_token))
                {
                    Thread.Sleep(500);
                    string toGet = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?pagetoken={data.next_page_token}&key={key}";
                    res = client.GetAsync(toGet).Result;
                    content = res.Content.ReadAsStringAsync().Result;

                    data = des.Deserialize<Rootobject>(content);

                    list.AddRange(data.results.ToList());
                }

                Console.WriteLine("items: " + list.Count);

                foreach (var x in list)
                {
                    Console.WriteLine(x.name);
                    var loc = x.geometry.location;
                    Console.WriteLine($"{loc.lat}, {loc.lng} ({x.vicinity})");
                    Console.WriteLine("Add location? (Y/n) ");
                    if (Console.ReadLine() != "n")
                    {
                        var param = new Dictionary<string, string>();
                        param.Add("name", x.name);
                        param.Add("lat", loc.lat.ToString());
                        param.Add("lon", loc.lng.ToString());
                        param.Add("type", type);

                        var resp = client.PostAsync("http://173.250.206.173:8080/findR/php/postLocation.php", new FormUrlEncodedContent(param)).Result;
                        Console.WriteLine("Post status: " + resp.IsSuccessStatusCode);
                    }
                }

                Console.Read();
            }
        }
    }
}
