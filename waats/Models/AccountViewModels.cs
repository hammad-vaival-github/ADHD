using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using waats.Classes;
using waats.Helper;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace waats.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        private ManageQueries _Managequeries = new ManageQueries();
        public Guid? UserGUID { get; set; }
        [Required]
        [Display(Name = "Activation code")]
        public string Activationcode { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "First Name")]
        [MaxLength(256)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Surname")]
        [MaxLength(256)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "* Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of birth")]
        [MaxLength(256)]
        public string Dob { get; set; }


        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Gender")]
        [MaxLength(256)]
        public string Gender { get; set; }
        public List<SelectListItem> GenderList
        {
            get
            {
                List<SelectListItem> list = _Managequeries.GetSelectlist("GenderT");
                return list;
            }
        }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Ethnic Origin")]
        [MaxLength(256)]
        public string EthnicOrigin { get; set; }
        public List<SelectListItem> EthnicOriginList
        {
            get
            {
                List<SelectListItem> list = _Managequeries.GetSelectlist("EOT");
                return list;
            }
        }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Employment status")]
        [MaxLength(256)]
        public string EmploymentStatus { get; set; }

        public List<SelectListItem> EmploymentStatusList
        {
            get
            {
                List<SelectListItem> list = _Managequeries.GetSelectlist("EST");
                return list;
            }
        }



        ///[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

        ///[Required(ErrorMessage = "* Required")]
        
        [Display(Name = "Profile picture (Optional)")]
        public bool? ProfilePic { get; set; }

        [Required(ErrorMessage = "* Required")]
        [EnforceTrue(ErrorMessage = "* Required")]
        [Display(Name = "I consent to share data for clinical & research purposes only.")]
        public bool ConsentToSharedata { get; set; }

        [Required(ErrorMessage = "* Required")]
        [EnforceTrue(ErrorMessage = "* Required")]
        [Display(Name = "I agree to ADHD Assistant Ts & Cs")]
        public bool AgreeToADHD { get; set; }
        public int AccountType { get; set; }
        public bool bDeleted { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
