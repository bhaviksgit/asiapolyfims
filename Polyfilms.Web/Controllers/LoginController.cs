using Microsoft.AspNetCore.Mvc;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Repositories;
using PolyFilms.Services.Login;
using PolyFilms.Web.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using PolyFilms.Data.Models;
using System.Security.Claims;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using PolyFilms.Common;

namespace PolyFilms.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _service;

        private readonly IHostingEnvironment _env;

        public LoginController(ILoginService service, IHostingEnvironment envrnmt)
        {
            _service = service;
            _env = envrnmt;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                TblUser logedInUser = _service.ValidateUser(model.UserName, EncryptionDecryption.GetEncrypt(model.Password));

                if (logedInUser == null)
                {
                    TempData[Enums.NotifyType.Error.GetDescription()] = "Invalid Login Credentials.";
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, logedInUser.UserId.ToString())
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal, new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(120),
                });

                SessionHelper.UserId = logedInUser.UserId;
                SessionHelper.RoleId = logedInUser.RoleId ?? 0;
                SessionHelper.WelcomeUser = logedInUser.Name;
                SessionHelper.IsSuperAdmin = logedInUser.IsSuperAdmin ? 1 : 0;
                SessionHelper.UserAccessPermissions = CustomRepository.UserAccessPermissions(SessionHelper.RoleId, logedInUser.IsSuperAdmin);
                CommonHelper.UserId = logedInUser.UserId;
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string Email)
        {
            try
            {
                TblUser model = _service.GetByEmail(Email);
                string token = Guid.NewGuid().ToString();
                if (model.UserId > 0)
                {
                    string link = Url.Action("ResetPassword", "Login", new RouteValueDictionary(new { Token = token }), HttpHelper.HttpContext.Request.Scheme);

                    string resetLink = "<a href='" + link + "'>" + link + "</a>";
                    string toEmail = model.EmailAddress;

                    string file = System.IO.Path.Combine(_env.WebRootPath, "MailTemplate/ForgotPassword.html");
                    string bodyTemplate = System.IO.File.ReadAllText(file);


                    bodyTemplate = bodyTemplate.Replace("[@FirstName]", model.Name);
                    bodyTemplate = bodyTemplate.Replace("[@link]", resetLink);

                    Task task = new Task(() => EmailHelper.SendAsyncEmail(toEmail, resLabels.ForgotPassword, bodyTemplate, true));
                    task.Start();


                    model.Token = token;
                    model.TokenExpiryDateTime = DateTime.Now.AddDays(1);
                    
                    using (PolyFilmsContext contex = BaseContext.GetDbContext())
                    {
                        contex.TblUser.Update(model);
                        contex.SaveChanges();
                    }


                    TempData[Enums.NotifyType.Success.GetDescription()] = "Email For Reset Passsword has been sent. Please check your email.";
                    return View("Login");
                }

                TempData[Enums.NotifyType.Error.GetDescription()] = "Invalid Email Address.";
                return View();
            }
            catch (Exception ex)
            {
                return Json(CommonHelper.GetErrorMessage(ex));
            }
        }

        [HttpGet]
        public IActionResult ResetPassword(string Token)
        {
            UserModel userModel = _service.GetByToken(Token);

            if (userModel.UserId > 0 && userModel.TokenExpiryDateTime >= DateTime.Now)
            {
                ForgotPasswordModel model = new ForgotPasswordModel { UserId = userModel.UserId };
                return View(model);

            }

            if (userModel.TokenExpiryDateTime < DateTime.Now)
            {
                TempData[Enums.NotifyType.Error.GetDescription()] = "Token for reset password has been expired.please try agian to reset password.";
                return View("Login");
            }

            TempData[Enums.NotifyType.Error.GetDescription()] = "Something Went wrong. Please try again later.";
            return View("Login");
        }

        [HttpPost]
        public IActionResult ResetPassword(ForgotPasswordModel userModel)
        {
            long id;

            if (userModel.ConfirmPassword != userModel.Password)
            {
                TempData[Enums.NotifyType.Error.GetDescription()] = "Password and Confirm Password must be same.";
                return View("ResetPassword");
            }

            TblUser model = _service.GetById(userModel.UserId);
            model.Password = EncryptionDecryption.GetEncrypt(userModel.Password);
            using (PolyFilmsContext contex = BaseContext.GetDbContext())
            {
                model.Token = null;
                model.TokenExpiryDateTime = null;
                contex.TblUser.Update(model);
                id = contex.SaveChanges();
            }
            if (id > 0)
            {
                TempData[Enums.NotifyType.Success.GetDescription()] = "Password reset successful.";
                return View("Login", new LoginModel());
            }
            TempData[Enums.NotifyType.Error.GetDescription()] = "Something went wrong. Please try again later.";
            return View("ResetPassword");
        }
    }
}