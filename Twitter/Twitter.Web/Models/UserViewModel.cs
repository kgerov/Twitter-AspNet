using System.ComponentModel.DataAnnotations;
using Twitter.Models;

namespace Twitter.Web.Models
{
    public class UserViewModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        public string Email { get; set; }

        // public IEnumerable<LanguageViewModel> Languages { get; set; }

        public static object FromModel(User user)
        {
            return new UserViewModel()
            {

            };
        }
    }
}