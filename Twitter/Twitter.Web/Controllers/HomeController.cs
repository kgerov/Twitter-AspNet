using System.Web.Mvc;
using Twitter.Data.UnitOfWork;
using System.Web.Mvc.Expressions;

namespace Twitter.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ITwitterData data)
            : base(data)
        {
        }

        public ActionResult Index(int? id)
        {
            if (this.UserProfile != null)
            {
                this.ViewBag.Username = this.UserProfile.UserName;

            }

            this.ViewBag.Swag = id;

            return this.View();
        }

        public ActionResult About()
        {
            //return this.RedirectToAction(x => x.Contact());
            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}