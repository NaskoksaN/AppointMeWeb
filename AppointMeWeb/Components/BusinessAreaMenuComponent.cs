using Microsoft.AspNetCore.Mvc;

namespace AppointMeWeb.Components
{
    public class BusinessAreaMenuComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View());
        }
    }
}
