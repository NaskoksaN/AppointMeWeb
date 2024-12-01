using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;

namespace AppointMeWeb.Areas.BusinessArea.Controllers
{
    [Area("BusinessArea")]
    [Authorize(Roles = BusinessRole)]
    public class BaseController : Controller
    {
        
    }
}
