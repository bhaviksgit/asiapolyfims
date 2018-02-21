using System.Linq;

namespace PolyFilms.Web.Common
{
    public class AuthorizationHelper
    {
        public static bool CanAdd(string controllerName)
        {
            return SessionHelper.UserAccessPermissions.Any(item => item.IsInsert && item.Controller != null && item.Controller.ToLower() == controllerName.ToLower());
        }
        
        public static bool CanEdit(string controllerName)
        {
            return SessionHelper.UserAccessPermissions.Any(item => item.IsEdit && item.Controller != null && item.Controller.ToLower() == controllerName.ToLower());
        }
        
        public static bool CanDelete(string controllerName)
        {
            return SessionHelper.UserAccessPermissions.Any(item => item.IsDelete && item.Controller != null && item.Controller.ToLower() == controllerName.ToLower());
        }

        public static bool CanChangeStatus(string controllerName)
        {
            return SessionHelper.UserAccessPermissions.Any(item => item.IsChangeStatus && item.Controller != null && item.Controller.ToLower() == controllerName.ToLower());
        }

        public static bool CanEditDelete(string controllerName)
        {
            return SessionHelper.UserAccessPermissions.Any(item => (item.IsDelete || item.IsEdit) 
            
            && item.Controller != null && item.Controller.ToLower() == controllerName.ToLower());
        }

    }
}
