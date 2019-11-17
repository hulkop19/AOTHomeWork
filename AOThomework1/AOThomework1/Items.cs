using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AOThomework1
{
    public class Items
    {
        [JsonProperty(PropertyName = "items")]
        public List<Item> ListItems { get; set;}

        public Items()
        {
            ListItems = new List<Item>();
        }
    }

    public class Item
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }
        [JsonProperty(PropertyName = "snippet")]
        public string Snippet { get; set; }
    }
}
