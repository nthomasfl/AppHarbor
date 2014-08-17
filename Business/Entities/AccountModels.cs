using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Business.Entities
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }

    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessage = "Vul een wachtwoord in.")]
        [DataType(DataType.Password)]
        [Display(Name = "Huidig wachtwoord")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Vul een nieuw wachtwoord in.")]
        [StringLength(100, ErrorMessage = "Het {0} moet op zijn minst {2} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nieuw password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig nieuw password")]
        [Compare("NewPassword", ErrorMessage = "Het nieuwe wachtwoord en bevestigde wachtwoord komen niet overeen.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Vul een gebruikersnaam in.")]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vul een wachtwoord in.")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Display(Name = "Herinneren?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Vul een gebruikersnaam in.")]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vul een wachtwoord in.")]
        [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "Het nieuwe wachtwoord en bevestigde wachtwoord komen niet overeen.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
