using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Twitter.Data.UnitOfWork;
using Twitter.Models;
using Twitter.Web.Models;

namespace Twitter.Web.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(ITwitterData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Profile(string username)
        {
            var userProfile = this.Data.Users.All()
               .Include(x => x.Tweets)
               .Include(x => x.Image)
               .Where(x => x.UserName == username)
               .Select(UserViewModel.ViewModel)
               .FirstOrDefault();

            return View(userProfile);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Settings()
        {
            var userProfile = this.Data.Users.All()
               .Include(x => x.Image)
               .Where(x => x.UserName == this.UserProfile.UserName)
               .Select(UserViewModel.EditModel)
               .FirstOrDefault();

            if (userProfile.Image == null)
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("/Images/") + "default_user.jpg", true);

                using (var ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                    userProfile.Image = ms.ToArray();
                }
            }

            return this.View(userProfile);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Settings(UserViewModel user)
        {
            var currentUser = this.UserProfile;

            currentUser.FullName = user.FullName;
            currentUser.HomeTown = user.HomeTown;
            currentUser.WebSite = user.WebSite;

            this.Data.Users.Update(currentUser);
            this.Data.SaveChanges();

            return this.RedirectToAction("Profile", "Users", new { username = this.UserProfile.UserName });
        }

        [HttpPost]
        public ActionResult UploadPic()
        {
            bool isUploaded = false;

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];

                if (pic != null)
                {
                    byte[] fileData = null;
                    var user = this.UserProfile;

                    using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                    {
                        fileData = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                    }

                    user.Image = new Image() {Photo = fileData};
                    this.Data.Users.Update(user);
                    isUploaded = true;

                    this.Data.SaveChanges();
                }
            }


            return this.Json(new {isUploaded = isUploaded});
        }

        [HttpDelete]
        public ActionResult DeletePic()
        {
            //var user = this.UserProfile;
            //Image image = user.Image;
            //user.Image = null;
            //this.Data.Users.Update(user);
            //this.Data.Images.Delete(image);
            //this.Data.SaveChanges();

            return this.Json(true);
        }
    }
}