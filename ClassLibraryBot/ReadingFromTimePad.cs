using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryBot
{
    
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
