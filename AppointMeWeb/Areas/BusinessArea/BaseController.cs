using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;

namespace AppointMeWeb.Areas.BusinessArea
{
    [Area("AdminArea")]
    [Authorize(Roles = BusinessRole)]
    public class BaseController : Controller
    {

    }
}
