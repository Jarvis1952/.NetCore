using Microsoft.AspNetCore.Mvc;
using ViewComponents.Models;

namespace ViewComponents.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PersonGridModels grid)
        {
            return View("Default",grid); // Invoked a partial view Views/Shared/Components/Grid/Default.cshtml
        }
    }
}
