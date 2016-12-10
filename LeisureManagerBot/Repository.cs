using ClassLibraryBot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LeisureManagerBot
{
    class Repository
    {
       const string url = "https://api.timepad.ru/v1/events.json?limit=25&skip=0&cities=Москва";
        string str;

    /*   private string MakeQuery(string q)
        {
            return url;
        }*/
        public RootObject Get(string q)
        {
            //var url1 = url;
            using (var webClient = new WebClient())
            {
                var JsonResult = webClient.DownloadString(url);
                var items= JsonConvert.DeserializeObject<RootObject>(JsonResult);
                return items;
            }

           
        }

    }
}
