using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using PolyFilms.Web.Common;
using Microsoft.AspNetCore.Mvc.Routing;
using PolyFilms.Common;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Repositories;


namespace PolyFilms.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        #region Public Methods
        public override void OnActionExecuting(ActionExecutingContext filterContext)

        {
            base.OnActionExecuting(filterContext);

            if (User.Identity.IsAuthenticated)
            {
                #region Handle Session Time Out
                if (SessionHelper.UserId == 0)
                {
                    SetSessionValues(filterContext);
                }
                #endregion
            }
            else
            {
                RedirectToLoginPage(filterContext);
            }
        }
        #endregion

        #region Private Methods
        private void RedirectToLoginPage(ActionExecutingContext filterContext)
        {
            var url = new UrlHelper(filterContext);
            var loginUrl = url.Action("Login", "Login");
            if (loginUrl != null)
            {
                HttpContext.Session.Clear();
                HttpContext.SignOutAsync();
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
        }

        private void SetSessionValues(ActionExecutingContext filterContext)
        {
            var loggedInUser = HttpContext.User;
            var loggedInUserName = loggedInUser.Identity.Name;

            int userId = Convert.ToInt32(loggedInUserName);

            TblUser logedInUser = BaseContext.GetDbContext().TblUser.FirstOrDefault(u => u.UserId == userId && u.IsActive);

            if (logedInUser == null)
            {
                RedirectToLoginPage(filterContext);
            }
            else
            {
                SessionHelper.UserId = logedInUser.UserId;
                SessionHelper.RoleId = logedInUser.RoleId ?? 0;
                SessionHelper.WelcomeUser = logedInUser.Name;
                SessionHelper.IsSuperAdmin = logedInUser.IsSuperAdmin ? 1 : 0;
                SessionHelper.UserAccessPermissions = CustomRepository.UserAccessPermissions(SessionHelper.RoleId, logedInUser.IsSuperAdmin);
                CommonHelper.UserId = logedInUser.UserId;
            }
        }
        #endregion

    }
}
