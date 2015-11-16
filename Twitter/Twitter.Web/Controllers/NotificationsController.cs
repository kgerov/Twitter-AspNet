using System.Web.Mvc;

namespace Twitter.Web.Controllers
{
    public class NotificationsController : Controller
    {
        // GET: Notifications
        public ActionResult Index()
        {
            return View();
        }
    }
}