using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AppointMeWeb.Components
{
    public class MainMenuComponent : ViewComponent
    {
       public async Task<IViewComponentResult> InvokeAsync()
       {
            return await Task.FromResult<IViewComponentResult>(View());
       }
    }

    
}
