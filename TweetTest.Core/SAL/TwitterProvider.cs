using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tweets.BL;
using Tweets.SAL.Abstract;
//using Newtonsoft.Json;
using System.Json;

namespace Tweets.SAL
{
    public class TwitterProvider : ITwitterProvider
    {
        const string format = @"\""ddd, dd MMM yyyy HH:mm:ss zzzz\""";
        private const string fuckingBackslash = "\"";
        string url = "http://search.twitter.com/search.json?from={0}&rpp={1}&include_entities=false&result_type=recent";
        public TwitterProvider(){}
        public List<Tweet> GetTweetsFromTwitter(string twitterUser, int count)
        {
            var webClient = new WebClient();
            var s = webClient.DownloadString(new Uri(String.Format(url, twitterUser, count)));

            var j = (JsonObject)JsonValue.Parse(s);
            if (j == null) return new List<Tweet>();

            var results = from result in (JsonArray)j["results"]
                          let jResult = result as JsonObject
                          select new Tweet()
                          {
                              Text = jResult["text"].ToString().Replace(fuckingBackslash, ""),
                              CreatedAt = DateTime.ParseExact(jResult["created_at"].ToString(), format, CultureInfo.InvariantCulture),
                              User = jResult["from_user_name"].ToString().Replace(fuckingBackslash, ""),
                              ID = Convert.ToInt64(jResult["id"].ToString().Replace(fuckingBackslash, "")),
                              IsFavorite = false
                          };
            return results.ToList();
        }
    }
}
