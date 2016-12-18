using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryBot
{
    public class Repository
    {
        const string url = "https://api.timepad.ru/v1/events.json?limit=25&skip=0&cities=Москва";

        public Event Get()
        {
            string str;
            using (var webClient = new WebClient())
            {
                str = webClient.DownloadString(url);
                var JsonResult = Encoding.UTF8.GetString(Encoding.Default.GetBytes(str));
                var items = JsonConvert.DeserializeObject<Event>(JsonResult);
                return items;
            }
        }
    }

    public class PosterImage
    {
        public string default_url { get; set; }
        public string uploadcare_url { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Value
    {
        public int id { get; set; }
        public string starts_at { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public PosterImage poster_image { get; set; }
        public List<Category> categories { get; set; }
        public string moderation_status { get; set; }
    }

    public class Event
    {
        public int total { get; set; }
        public List<Value> values { get; set; }
    }

}
