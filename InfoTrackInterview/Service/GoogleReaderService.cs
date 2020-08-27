using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace InfoTrackInterview.Service
{
    public class GoogleReaderService : IGoogleReaderService
    {
        public List<int> GetGoogleSearchResults(string keyWord, string phrase, out string searchedUrl)
        {
            searchedUrl = BuildUrl(phrase);

            return GetGoogleSearchResultIndexes(searchedUrl, keyWord);
        }

        public List<int> GetGoogleSearchResultIndexes(string url, string keyword)
        {
            var siteContent = GetSiteContentByUrl(url);

            var regex = new Regex("<div class=\"g\">(.*?)</div>");
            var matches = regex.Matches(siteContent).Cast<Match>().ToList();
            var listOfIndexes = matches.Select((match, index) => new { match, index })
                .Where(x => x.ToString().Contains(keyword)).Select(y => y.index + 1)
                .ToList();

            return listOfIndexes;
        }

        public string GetSiteContentByUrl(string url)
        {
            var request = GetHttpWebRequest(url);

            return GetResponseFromRequest(request);
        }

        private string BuildUrl(string phrase)
        {
            return @"https://www.google.co.uk/search?num=100&q=" + phrase.Trim().Replace(' ', '+');
        }

        private string GetResponseFromRequest(HttpWebRequest request)
        {
            var siteContent = string.Empty;

            using (var httpWebResponse = (HttpWebResponse)request.GetResponse())
            using (var responseStream = httpWebResponse.GetResponseStream())
            {
                using (var streamReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    siteContent = streamReader.ReadToEnd();
                }
            }

            return siteContent;
        }

        private HttpWebRequest GetHttpWebRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = @"Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1667.0 Safari/537.36";
            request.Method = "GET";

            return request;
        }
    }
}