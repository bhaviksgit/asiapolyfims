using System;
using System.Collections.Generic;

namespace PolyFilms.Web.Common
{
    public static class WebHelper
    {
        public const int PageSize = 25;

        public static int[] PageSizes = { 25, 50, 100, 200, 500 };

        public const string PleaseSelect = "--Select--";

        public static readonly Dictionary<string, object> ActionCenterColumnStyleWithCanEdit = new Dictionary<string, object> { { "align", "center" }, { "style", "text-align:center;vertical-align:middle !important;" }, { "width", "80px" } };

        public static readonly Dictionary<string, object> ActionCenterColumnStyleWithCanStatus = new Dictionary<string, object> { { "align", "center" }, { "style", "text-align:center;vertical-align:middle !important;" }, { "width", "120px" } };

        public static readonly Dictionary<string, object> StatusColumnStyle = new Dictionary<string, object> { { "align", "center" }, { "style", "text-align:center;vertical-align:middle !important;" }, { "width", "60px" } };

        public static readonly Dictionary<string, object> SmallColumnStyle = new Dictionary<string, object> { { "align", "center" }, { "style", "text-align:center;vertical-align:middle !important;" }, { "width", "30px" } };

        public static string SiteRootPathUrl
        {

            get
            {
                try
                {
                    string msRootUrl = HttpHelper.HttpContext.Request.Scheme + "://" + HttpHelper.HttpContext.Request.Host;

                    return msRootUrl;
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
        }

        public static string GetColumnName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }


    }
}
