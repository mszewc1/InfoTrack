using System.Collections.Generic;


namespace InfoTrackInterview.Service
{
    public interface IGoogleReaderService
    {
        string GetSiteContentByUrl(string url);
        List<int> GetGoogleSearchResultIndexes(string url, string keyword);
        List<int> GetGoogleSearchResults(string keyWord, string phrase, out string searchedUrl);
    }
}