﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using waats.Classes;
using waats.Models;
using waats.ViewModel;

namespace waats.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ManageQueries _Managequeries = new ManageQueries();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            try
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }
        public List<string> RegisterViaADHD(WaatsFormVM model,ApplicationUserManager um,string activationCode)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Activationcode = activationCode,
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.Firstname,
                    Lastname = model.Surname,
                    Dob = model.DoB,
                    Gender = model.Gender,
                    EmploymentStatus =null,
                    EthnicOrigin = null,
                    AccountType = 0,
                    ConsentToSharedata = true,
                    ProfilePic = false,
                    AgreeToADHD = true,
                    bDeleted = false,
                    EmailConfirmed = false,
                    TwoFactorEnabled = true,
                    LockoutEnabled = true,
                    Dateinserted = DateTime.Now

                };
                string password = Helper.HelpFunctions.GenerateRandomPassword(um.PasswordValidator as PasswordValidator);
                var result =  um.Create(user, password);
                if (result.Succeeded)
                {                  
                    _Managequeries.AddLogs(999, "New user register Email= " + model.Email);
                    List<string> t = new List<string> { "Username=<strong>" + model.Email+"</strong>", "password=<strong>" + password + "</strong>" };
                    return t;
                }
                else
                {
                    AddErrors(result);
                    return null;
                }
                ///
            }
            return null;
        }

            //
            // GET: /Account/Register
            [AllowAnonymous]
        public ActionResult Register(string UserGUID)
        {
            var vm = _Managequeries.getRVM(UserGUID);
            return View(vm);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, HttpPostedFileBase ProfilePicfile)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Activationcode = new Random().Next(100, 1000).ToString(),
                    UserName = model.Email, Email = model.Email, FirstName = model.FirstName,
                    Lastname = model.Surname,Dob =model.Dob, Gender=model.Gender,EmploymentStatus=model.EmploymentStatus, EthnicOrigin=model.EthnicOrigin,
                    AccountType=model.AccountType, ConsentToSharedata=model.ConsentToSharedata, ProfilePic =model.ProfilePic??false,
                    AgreeToADHD=model.AgreeToADHD,bDeleted = false,EmailConfirmed=false,TwoFactorEnabled=true,LockoutEnabled=true,
                    Dateinserted =DateTime.Now

                };
                string password = Helper.HelpFunctions.GenerateRandomPassword(UserManager.PasswordValidator as PasswordValidator);
                var result = await UserManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    //////   await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);                  
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    string NewName = model.FirstName;
                    var emailConfirmBody = $@"<font size='3' face='Calibri'>Dear {NewName},<br /><br />
                            To complete the registration process please click the following link:<br /><br />
                            <a href='{callbackUrl}'>{callbackUrl}</a><br /><br />
                            <strong style='color:red;'>Please keep these details in a safe place for future reference.</strong><br /><br />
                            If you have any queries or need further assistance, please contact Wholly Automated ADHD Triage Service.<br /><br />
                            Wholly Automated ADHD Triage Service<br/><hr></font>";
                    string subject = "Wholly Automated ADHD Triage Service - Confirm Email";
                    EmailManager e = new EmailManager();
                    e.SendEmail(model.Email, null, subject, MvcHtmlString.Create(emailConfirmBody));
                    _Managequeries.UploadWaiverDoc(ProfilePicfile, user.Id);
                    _Managequeries.AddLogs(999,"New user register Email= "+model.Email);
                    return RedirectToAction("EmailConfirmation", "Account");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult EmailConfirmation()
        {
            return View();
        }
        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            if (user.EmailConfirmed)
            {
                return View("ConfirmEmail", new EmailConfirmationViewModel() { IsAlreadyConfirmed = true });
            }

            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (user != null)
            {
                var newPassword = Helper.HelpFunctions.GenerateRandomPassword(UserManager.PasswordValidator as PasswordValidator);
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(newPassword);
                await UserManager.UpdateAsync(user);
                string NewName = user.FirstName;
                string subject = "Welcome to Wholly Automated ADHD Triage Service.";
                string link = System.Web.Configuration.WebConfigurationManager.AppSettings["LoginLink"];
                MvcHtmlString mailBody = MvcHtmlString.Create("<font size=3 face= Calibri>Dear " + NewName + ",<br />" +
                                                               "<br />A account has been created for you.<br />" +
                                                               "Please see below for your user name and password:" + "<br />" +
                                                               "User Name:   " + user.Email + "<br />" +
                                                               "Password:   " + newPassword + "<br /><br />" +
                                                               "These log in details can be used to access the following systems " + "<br /><br />" +
                                                               link + "<br /><br /></font>" +
                                                               "<font size=3 face= Calibri color=red><strong>Please keep these details in a safe place for future reference.</strong></font>" + "<br />" +

                                                               "<font size=3 face= Calibri>If you have any queries or need further assistance please contact Wholly Automated ADHD Triage Service." + "<br /><br />" +
                                                               "Wholly Automated ADHD Triage Service" + "<br/>" + "</font>");
                EmailManager e = new EmailManager();
                e.SendEmail(user.Email, null, subject, mailBody);
            }

            if (result.Succeeded)
                return View("ConfirmEmail", new EmailConfirmationViewModel() { IsAlreadyConfirmed = false });
            else
                return View("Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user != null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    //    // Don't reveal that the user does not exist or is not confirmed
                    //    return View("ForgotPasswordConfirmation");
                    //}

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    string toEmail = await UserManager.GetEmailAsync(user.Id).WithCurrentCulture();
                    ////await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    EmailManager e = new EmailManager();
                    //ThreadStart action = () =>
                    //{
                        e.SendEmail(toEmail, null, "Reset Password", MvcHtmlString.Create("Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>"));//to,cc,subject,body
                    //};
                    //Thread thread = new Thread(action) { IsBackground = true };
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> IsEmailExistsAsync(string email)
        {
            var exists = await UserManager.FindByEmailAsync(email);
            return Json(new { exists });
        }
        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
           /// return RedirectToAction("Index", "Home");
            return RedirectToAction("Index", "UProfile");
        }


        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}