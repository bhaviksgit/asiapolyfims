using Microsoft.AspNetCore.Html;
using PolyFilms.Data.CustomModel;
using PolyFilms.Web.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    public static class HtmlHelperExtension 
    {
        public static string SetStatusClientTemplate(this IHtmlHelper helper, string isActive, string controllerName, string action, string parameter, string id, string gridId, string entityName)
        {

            string deactivteMessage = "Are you sure you want to deactivate this " + entityName;


            string activteMessage = "Are you sure you want to activate this " + entityName;

            var deactiveAttributes = " onclick='changeStatus(" + @"""" + controllerName + @"""" + ", " + @"""" +
                                    action + @"""" + ", " + @"""""" + ", "
                                    + @"""" + parameter + @"""" + ", " + @"""" + deactivteMessage + @"""" + ", " + id
                                    + ", " + @"""" + gridId + @"""" + @")'";

            var activeAttributes = " onclick='changeStatus(" + @"""" + controllerName + @"""" + ", " + @"""" +
                                   action + @"""" + ", " + @"""""" + ", "
                                   + @"""" + parameter + @"""" + ", " + @"""" + activteMessage + @"""" + ", " + id
                                   + ", " + @"""" + gridId + @"""" + @")'";


            return "# if (" + isActive + ")    {#" +
                     @"<a class='k-button' " + deactiveAttributes + @"><span class='k-icon k-i-check'></span></a>" +
                     "#}else { #" +
                     @"<a class='k-button' " + activeAttributes + @"><span class='k-icon k-i-close'></span></a>"
                     + "#}#";
        }

        public static HtmlString GenerateMenu(this IHtmlHelper helper)
        {
            List<UserAccessPermission> parentMenuList = SessionHelper.UserAccessPermissions.Where(x => x.ParentMenuId == null && x.IsView == true).OrderBy(item => item.DisplayOrder).ToList();

            List<UserAccessPermission> childMenuList = SessionHelper.UserAccessPermissions.Where(x => x.ParentMenuId != null && x.IsView == true).OrderBy(item => item.DisplayOrder).ToList();

            if (parentMenuList.Any())
            {
                TagBuilder ul = new TagBuilder("ul");
                ul.MergeAttribute("class", "nav nav-list");
                //ul.MergeAttribute("id", "sideBar");

                StringBuilder sb = new StringBuilder();

                foreach (UserAccessPermission menu in parentMenuList)
                {
                    List<UserAccessPermission> childList = childMenuList.Where(x => x.ParentMenuId == menu.MenuId).ToList();

                    if (childList.Any())
                    {
                        TagBuilder liWithChild = new TagBuilder("li");
                        //liWithChild.AddCssClass("sidenav-item has-subnav");

                        TagBuilder firstITag = ITag(menu.ImagePath);

                        TagBuilder firstSpan = SpanTag("");
                        firstSpan.InnerHtml.Append(menu.MenuName);

                        TagBuilder firstBTag = bTag("arrow fa fa-angle-right");
                        //TagBuilder secondSpan = SpanTag("sidenav-label");
                        //secondSpan.InnerHtml.Append(menu.MenuName);

                        TagBuilder aLink = AnchorLink("","", true);
                        aLink.MergeAttribute("class", "dropdown-toggle");
                        aLink.InnerHtml.AppendHtml(firstITag);
                        aLink.InnerHtml.AppendHtml(firstSpan);
                        aLink.InnerHtml.AppendHtml(firstBTag);

                        liWithChild.InnerHtml.AppendHtml(aLink);

                        TagBuilder ulChild = new TagBuilder("ul");
                        ulChild.AddCssClass("submenu");

                        //TagBuilder ulliChild = new TagBuilder("li");
                        //ulliChild.AddCssClass("sidenav-subheading");
                        //ulliChild.InnerHtml.Append(menu.MenuName);
                        //ulChild.InnerHtml.AppendHtml(ulliChild);

                        foreach (UserAccessPermission childMenu in childList)
                        {

                            TagBuilder ulliChild = new TagBuilder("li");
                            TagBuilder ulliChildaLink = AnchorLink(childMenu.Action, childMenu.Controller, false);
                            ulliChildaLink.InnerHtml.Append(childMenu.MenuName);
                            ulliChild.InnerHtml.AppendHtml(ulliChildaLink);
                            ulChild.InnerHtml.AppendHtml(ulliChild);
                        }

                        liWithChild.InnerHtml.AppendHtml(ulChild);
                        ul.InnerHtml.AppendHtml(liWithChild);
                    }
                    else
                    {
                        TagBuilder liWithChild = new TagBuilder("li");
                        //liWithChild.AddCssClass("sidenav-item");
                        TagBuilder firstITag = ITag(menu.ImagePath);

                        //TagBuilder firstSpan = SpanTag("sidenav-icon " + menu.ImagePath);

                        TagBuilder firstSpan = SpanTag("");
                        firstSpan.InnerHtml.Append(menu.MenuName);

                        TagBuilder firstBTag = bTag("arrow fa fa-angle-right");

                        TagBuilder aLink = AnchorLink(menu.Action, menu.Controller, false);
                        aLink.MergeAttribute("class", "dropdown-toggle");
                        aLink.InnerHtml.AppendHtml(firstITag);
                        aLink.InnerHtml.AppendHtml(firstSpan);
                        //aLink.InnerHtml.AppendHtml(firstBTag);

                        liWithChild.InnerHtml.AppendHtml(aLink);
                        ul.InnerHtml.AppendHtml(liWithChild);
                    }
                }

                var writer = new StringWriter();
                ul.WriteTo(writer, HtmlEncoder.Default);
                return new HtmlString(writer.ToString());
            }

            return new HtmlString(string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="isParent"></param>
        /// <returns></returns>
        private static TagBuilder AnchorLink(string actionName, string controllerName, bool isParent)
        {
            TagBuilder aLink = new TagBuilder("a");

            if (isParent)
            {
                aLink.MergeAttribute("class", "dropdown-toggle");
                aLink.MergeAttribute("data-action", "click-trigger");
            }

            if (string.IsNullOrEmpty(actionName) || string.IsNullOrEmpty(controllerName))
            {
                aLink.MergeAttribute("href", "javascript:void(0);");
            }
            else
            {
                aLink.MergeAttribute("id", controllerName);
                aLink.MergeAttribute("href", WebHelper.SiteRootPathUrl + "/" + controllerName + "/" + actionName);
            }
            return aLink;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        private static TagBuilder SpanTag(string className)
        {
            TagBuilder span = new TagBuilder("span");
            span.MergeAttribute("class", className);
            return span;
        }

        private static TagBuilder ITag(string className)
        {
            TagBuilder span = new TagBuilder("i");
            span.MergeAttribute("class", className);
            return span;
        }

        private static TagBuilder bTag(string className)
        {
            TagBuilder span = new TagBuilder("b");
            span.MergeAttribute("class", className);
            return span;
        }

    }
}
