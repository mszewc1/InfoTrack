using InfoTrackInterview.Models;
using InfoTrackInterview.Service;
using System.Web.Mvc;

namespace InfoTrackInterview.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGoogleReaderService _googleReaderService;

        public HomeController()
        {
            this._googleReaderService = new GoogleReaderService();
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Search(HomeViewModel viewModel)
        {
            var searchedUrl = string.Empty;

            if (ModelState.IsValid)
            {
                viewModel.GoogleSearchResultIndexes =
                    _googleReaderService.GetGoogleSearchResults(viewModel.KeyWord, viewModel.Phrase, out searchedUrl);
                viewModel.SearchedUrl = searchedUrl;

                return View("Search", viewModel);
            }

            return View("Index");
        }

    }
}