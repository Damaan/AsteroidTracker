using AsteroidTracker.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AsteroidTracker.Front.Views.Login
{
    public class IndexModel : PageModel
    {
        public LoginRequest Rq { get; set; }
        public IndexModel(LoginRequest rq)
        {
            Rq = rq;
        }

        public void OnGet()
        {
        }

        public void Login()
        {
        }

    }
}  