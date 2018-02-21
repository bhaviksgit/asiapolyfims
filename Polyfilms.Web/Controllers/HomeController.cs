using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc;
using PolyFilms.Data.Repositories;
using Kendo.Mvc.Extensions;
using PolyFilms.Common;
using PolyFilms.Data.Entity;
using PolyFilms.Data;

namespace PolyFilms.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            ViewBag.ProductList = SelectionList.ProductList().Select(x => new { x.ProductId, Name = x.FilmType + " " + x.Thickness, x.Thickness });
            return View();
        }

        public IActionResult KendoReadJumboRollStock([DataSourceRequest] DataSourceRequest request)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("JumboNo", ListSortDirection.Ascending));
            }
            return Json(CustomRepository.GetJumboRollStockList().ToDataSourceResult(request));
        }

        //public IActionResult KendoReadSalesOrderDetail([DataSourceRequest] DataSourceRequest request)
        //{
        //    if (!request.Sorts.Any())
        //    {
        //        request.Sorts.Add(new SortDescriptor("OrderNo", ListSortDirection.Ascending));
        //    }
        //    return Json(CustomRepository.GetSalesOrderDetailList().ToDataSourceResult(request));
        //}

        public IActionResult KendoReadPendingProduction([DataSourceRequest] DataSourceRequest request)
        {
            return Json(CustomRepository.GetPendingProductionList().ToDataSourceResult(request));
        }

        public IActionResult KendoReadSlittingStock([DataSourceRequest] DataSourceRequest request)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("SlittingDate", ListSortDirection.Descending));
            }

            using (var context = BaseContext.GetDbContext())
            {
                return Json(context.TblSlitting.Where(m => m.OrderId == null && m.Quality == (short)Enums.SlitingStatus.Q1).Select(x => new
                {
                    x.SlittingDate,
                    x.SlittingRollNo,
                    x.Location,
                    x.ProductId,
                    x.Length,
                    x.Width,
                    x.Od,
                    x.Grossweight,
                    x.Netweight
                }).ToDataSourceResult(request));
            }
        }

        public IActionResult KendoReadSlittingStockQ2([DataSourceRequest] DataSourceRequest request)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("SlittingDate", ListSortDirection.Descending));
            }

            using (var context = BaseContext.GetDbContext())
            {
                return Json(context.TblSlitting.Where(m => m.OrderId == null && m.Quality == (short)Enums.SlitingStatus.Q2).Select(x => new
                {
                    x.SlittingDate,
                    x.SlittingRollNo,
                    x.Location,
                    x.ProductId,
                    x.Length,
                    x.Width,
                    x.Od,
                    x.Grossweight,
                    x.Netweight
                }).ToDataSourceResult(request));
            }
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

    }
}
