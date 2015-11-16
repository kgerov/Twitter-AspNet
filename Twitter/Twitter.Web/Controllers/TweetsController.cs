using System.Web.Mvc;

namespace Twitter.Web.Controllers
{
    public class TweetsController : Controller
    {
        // GET: Tweets
        public ActionResult Index()
        {
            return View();
        }
    }
}