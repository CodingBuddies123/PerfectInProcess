using System.Web.Mvc;

namespace PerfectInProcess.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index(Models.ViewModel.BaseViewModel view)
        {
            view.Account = base.Account;
            return View(view);
        }
    }
}