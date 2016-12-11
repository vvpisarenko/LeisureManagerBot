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
        
        /*   private string MakeQuery(string q)
            {
                return url;
            }*/
        public RootObject Get()
        {
            //var url1 = url;
            using (var webClient = new WebClient())
            {
                var JsonResult = webClient.DownloadString(url);
                var items = JsonConvert.DeserializeObject<RootObject>(JsonResult);
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

        public class RootObject
        {
            public int total { get; set; }
            public List<Value> values { get; set; }
        }
    
}
