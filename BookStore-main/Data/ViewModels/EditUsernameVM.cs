using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace BookStore.Data.ViewModels
{
    public class EditUsernameVM
    {
        public string UserId { get; set; }
        [Display(Name = "Trenutno korisničko ime")]
        public string CurrentUserName { get; set; }
        [Display(Name = "Novo korisničko ime")]
        [Required(ErrorMessage = "Unos novog korisničkog imena je obavezan")]
        public string NewUserName { get; set; }
    }
}
