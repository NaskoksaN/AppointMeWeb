using AppointMeWeb.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;

namespace AppointMeWeb.Areas.BusinessArea.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IRazorViewEngine _razorViewEngine;

        public HomeController(IActionContextAccessor actionContextAccessor, IRazorViewEngine razorViewEngine)
        {
            _actionContextAccessor = actionContextAccessor;
            _razorViewEngine = razorViewEngine;
        }

        public IActionResult BusinessHomeIndex()
        {
            //    var context = _actionContextAccessor.ActionContext;
            //    //var viewResult = _razorViewEngine.GetView(executingFilePath: null, viewPath: "BusinessHomeIndex", isMainPage: false);
            //    var viewResult = _razorViewEngine.GetView(
            //executingFilePath: null,
            //viewPath: "/Areas/BusinessArea/Views/Home/BusinessHomeIndex.cshtml",
            //isMainPage: false);

            //    if (!viewResult.Success)
            //    {
            //        // Log or debug view locations
            //        var searchedLocations = viewResult.SearchedLocations;
            //        foreach (var location in searchedLocations)
            //        {
            //            Console.WriteLine($"Searched location: {location}");
            //        }

            //        return NotFound();
            //    }

            return View();
        }
    }
}
