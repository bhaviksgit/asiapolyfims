using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Services.Slitting;
using Kendo.Mvc.UI;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using PolyFilms.Data;
using PolyFilms.Data.Models;
using PolyFilms.Common;
using PolyFilms.Services.Jumbo;
using PolyFilms.Services.OrderDetail;

namespace PolyFilms.Web.Controllers
{
    public class SlittingController : BaseController
    {
        private readonly ISlittingService _service;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IJumboService _jumboService;

        public SlittingController(ISlittingService service, IOrderDetailService orderDetailService, IJumboService jumboService)
        {
            _service = service;
            _orderDetailService = orderDetailService;
            _jumboService = jumboService;
        }

        public IActionResult Index()
        {
            ViewBag.ProductList = SelectionList.ProductList().Select(x => new { x.ProductId, Name = x.FilmType + " " + x.Thickness });
            ViewBag.OrderList = SelectionList.OrderList().Select(x => new { x.OrderId, x.OrderNo });
            ViewBag.QualityList = SelectionList.SlittingStatusList().Select(x => new { x.SlittingStatusId, x.Name });
            ViewBag.JumboList = SelectionList.JumboList().Select(x => new { x.JumboId, x.JumboNo });

            return View();
        }

        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request, DateTime? fromDate = null, DateTime? toDate = null, short? productId = null, string slittingRollno = null, string orderNo = null, short? statusId = null)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("SlittingDate", ListSortDirection.Descending));
            }
            return Json(_service.GetAll(fromDate, toDate, productId, slittingRollno, orderNo, statusId).ToDataSourceResult(request));
        }

        public IActionResult Create(string type)
        {
            BasicSlittingDetailModel model = new BasicSlittingDetailModel
            {
                IsPrimarySlitting = type == "P",
                SlittingDate = DateTime.Now
            };
            ViewBag.CoreList = SelectionList.CoreList().Select(x => new { x.CoreId, Name = x.Name + " " + x.Thickness, x.Weight });
            ViewBag.OrderList = SelectionList.OrderList().Select(x => new { x.OrderId, x.OrderNo });
            ViewBag.TreatmentList = SelectionList.SlittingTreatmentList().Select(x => new { x.TreatmentId, x.Name });
            ViewBag.QualityList = SelectionList.SlittingStatusList().Select(x => new { x.SlittingStatusId, x.Name });

            if (type == "P")
            {
                return View("NewCreateSlitting", model);
            }
            else
            {
                return View("CreateSecondarySlitting", model);
            }
        }



        public IActionResult Edit(long id)
        {
            SlittingModel model = _service.GetById(id);
            model.IsPrimarySlitting = model.JumboId != null;

            return View("CreateSlitting", model);
        }

        //public IActionResult SaveAndNew()
        //{
        //    SlittingModel newModel = new SlittingModel();

        //    if (TempData["Slitting"] is string tempModel)
        //    {
        //        SlittingModel model = JsonConvert.DeserializeObject<SlittingModel>(tempModel);
        //        newModel.SlittingDate = model.SlittingDate;
        //        newModel.ShiftId = model.ShiftId;
        //        newModel.JumboId = model.JumboId;
        //        newModel.OperatorId = model.OperatorId;
        //        newModel.IsPrimarySlitting = model.JumboId != null;
        //    }

        //    return View("CreateSlitting", newModel);
        //}

        public IActionResult SaveSlitting(SlittingModel model, string create)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateSlitting", model);
            }
            string message = string.Empty;
            long id = 0;

            try
            {
                
                model.Difference = (model.Width.Value * (decimal)0.905 * model.Length * model.Thickness) / 1000000 - model.Netweight;
                id = model.SlittingId > 0 ? _service.Update(model) : _service.Insert(model);
            }
            catch (Exception ex)
            {
                message = CommonHelper.GetErrorMessage(ex);
            }


            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.openPopup = CommonHelper.ShowAlertMessage(message);
                return View("CreateSlitting", model);
            }

            //if (create == "Save & Continue")
            //{
            //    return RedirectToAction("Edit", "Slitting", new { id });
            //}

            //if (create == "Save & New")
            //{
            //    var tempModel = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            //    TempData["Slitting"] = tempModel;
            //    return RedirectToAction("SaveAndNew", "Slitting");
            //}

            return RedirectToAction("Index");
        }

        public string KendoDestroy(long slittingId)
        {
            string deleteMessage = string.Empty;

            try
            {
                if (slittingId != 0)
                {
                    _service.Delete(slittingId);
                }
                else
                {
                    deleteMessage = "Something went wrong.";
                }
            }
            catch (Exception ex)
            {
                deleteMessage = CommonHelper.GetDeleteException(ex);
            }

            return deleteMessage;
        }

        public IActionResult ViewOrderDetail()
        {
            ViewBag.ProductList = SelectionList.ProductList().Select(x => new { x.ProductId, Name = x.FilmType + " " + x.Thickness, x.Thickness });
            return PartialView("_ViewOrderDetail");
        }


        public IActionResult KendoReadOrderDetail([DataSourceRequest] DataSourceRequest request, long? orderId = null, long? productId = null)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("ProductId", ListSortDirection.Ascending));
            }
            return Json(_orderDetailService.GetAllByProduct(orderId.Value, productId.Value).ToDataSourceResult(request));
        }

        public IActionResult ViewJumboDetail()
        {
            return PartialView("_ViewJumboDetail");
        }

        public IActionResult KendoReadJumboDetail([DataSourceRequest] DataSourceRequest request, long? jumboId = null)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("ProductId", ListSortDirection.Ascending));
            }
            return Json(_jumboService.GetJumboListById(jumboId).ToDataSourceResult(request));
        }

        public IActionResult GetSlittingReport(long? slittingId = null)
        {
            if (slittingId != null)
            {
                ViewBag.SlittingId = slittingId;
                return PartialView("_SlittingReport");
            }
            return View("Index");
        }
    }
}