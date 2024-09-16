using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
