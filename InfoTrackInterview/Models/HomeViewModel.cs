using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace InfoTrackInterview.Models
{
    public class HomeViewModel
    {
        [RegularExpression(@"^(?:.*[a-z]){3,}$", ErrorMessage = "String length must be greater than or equal 3 characters.")]
        [Required(ErrorMessage = "Please provide key word")]
        public string KeyWord { get; set; }
        [Required(ErrorMessage = "Please provide some phrase")]
        public string Phrase { get; set; }
        public List<int> GoogleSearchResultIndexes { get; set; }
        public string SearchedUrl { get; set; }
        public bool ShowResult { get; set; }
    }
}