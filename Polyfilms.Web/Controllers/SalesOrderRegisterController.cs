using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Common;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;

namespace PolyFilms.Web.Controllers
{
    public class SalesOrderRegisterController : BaseController
    {
        

        public IActionResult Index()
        {
            //ViewBag.RoleList = SelectionList.RoleList().Select(x => new { x.RoleId, x.RoleName });
            return View();
        }

        public IActionResult KendoReadSalesOrderDetail([DataSourceRequest] DataSourceRequest request,DateTime? month = null)
        {
            int? monthValue = null;
            int? yearValue = null;
            if (month != null)
            {
                monthValue = month.Value.Month;
                yearValue = month.Value.Year;
            }
            return Json(CustomRepository.GetSalesOrderDetailList(monthValue, yearValue).ToDataSourceResult(request));
        }

        public IActionResult KendoReadDettail([DataSourceRequest] DataSourceRequest request,long orderId, short productId)
        {
            return Json(CustomRepository.GetSalesOrderDetailByOrderId(orderId,productId).ToDataSourceResult(request));
        }
    }
}