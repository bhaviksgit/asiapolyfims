using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PolyFilms.Data.CustomModel;
using System.Collections.Generic;

namespace PolyFilms.Web.Common
{
    public static class SessionHelper
    {
        public static int UserId
        {
            get => HttpHelper.HttpContext.Session.GetInt32("UserId").HasValue ? HttpHelper.HttpContext.Session.GetInt32("UserId").Value : 0;
            set => HttpHelper.HttpContext.Session.SetInt32("UserId", value);
        }

        public static int RoleId
        {
            get => HttpHelper.HttpContext.Session.GetInt32("RoleId").HasValue ? HttpHelper.HttpContext.Session.GetInt32("RoleId").Value : 0;
            set => HttpHelper.HttpContext.Session.SetInt32("RoleId", value);
        }

        public static string WelcomeUser
        {

            get => HttpHelper.HttpContext.Session.GetString("WelcomeUser") == null ? "Guest" : HttpHelper.HttpContext.Session.GetString("WelcomeUser");
            set => HttpHelper.HttpContext.Session.SetString("WelcomeUser", value);
        }

        public static int IsSuperAdmin
        {

            get => HttpHelper.HttpContext.Session.GetInt32("IsSuperAdmin") == null ? HttpHelper.HttpContext.Session.GetInt32("IsSuperAdmin").Value : 0;
            set => HttpHelper.HttpContext.Session.SetInt32("IsSuperAdmin", value);
        }

        public static List<UserAccessPermission> UserAccessPermissions
        {
            get => HttpHelper.HttpContext.Session.GetString("UserAccessPermissions") != string.Empty ?
                JsonConvert.DeserializeObject<List<UserAccessPermission>>(HttpHelper.HttpContext.Session.GetString("UserAccessPermissions")) :
                new List<UserAccessPermission>();
            set => HttpHelper.HttpContext.Session.SetString("UserAccessPermissions", JsonConvert.SerializeObject(value));
        }
    }
}
