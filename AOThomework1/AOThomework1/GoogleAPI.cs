using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AOThomework1
{
    public static class GoogleAPI
    {
        public static Items GetItems(string query)
        {
            var urls = new string[] {
                                     $"https://www.googleapis.com/customsearch/v1?fields=items(title,link,snippet)&key=AIzaSyAtdPKN0_4x2KLYo9jKO8c8ItKsapHhPyk&cr=countryRU&cx=002029405749213240538:prftwyrihe6&q={query}"
                                     , $"https://www.googleapis.com/customsearch/v1?fields=items(title,link,snippet)&key=AIzaSyAtdPKN0_4x2KLYo9jKO8c8ItKsapHhPyk&start=10&cr=countryRU&cx=002029405749213240538:prftwyrihe6&q={query}"
                                    };

            Items items = new Items();

            foreach (var url in urls)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                    {
                        var answerJson = reader.ReadToEnd();
                        items.ListItems.AddRange(JsonConvert.DeserializeObject<Items>(answerJson).ListItems);
                    }
                }

                response.Close();
            }

            return items;
        }
    }
}
