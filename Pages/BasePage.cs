using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HariCKInventry.Pages
{
    public class BasePage : PageModel
    {
        public override void OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext context)
        {
            var isLoggedIn = context.HttpContext.Session.GetString("IsLoggedIn");
            if (isLoggedIn != "true")
            {
                context.Result = new RedirectToPageResult("/Login");
            }
        }
    }
}