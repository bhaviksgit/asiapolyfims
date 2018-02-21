using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Services.Dispatch;
using PolyFilms.Data;
using Kendo.Mvc.UI;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using PolyFilms.Data.Models;
using PolyFilms.Services.Slitting;
using PolyFilms.Common;
using PolyFilms.Data.Repositories;
using PolyFilms.Services.OrderDetail;

namespace PolyFilms.Web.Controllers
{
    public class DispatchController : BaseController
    {
        private readonly IDispatchService _service;
        private readonly IOrderDetailService _orderDetailservice;
        private readonly ISlittingService _slittingService;

        public DispatchController(IDispatchService service, ISlittingService slittingService, IOrderDetailService orderDetailservice)
        {
            _service = service;
            _orderDetailservice = orderDetailservice;
            _slittingService = slittingService;
        }


        public IActionResult Index()
        {
            ViewBag.CustomerList = SelectionList.CustomerList().Select(x => new { x.CustomerId, x.Name });
            ViewBag.OrderList = SelectionList.OrderList().Select(x => new { x.OrderId, x.OrderNo });
            ViewBag.ConsigneeList = SelectionList.ConsigneeList().Select(x => new { x.ConsigneeId, x.Name });
            return View();
        }

        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request, DateTime? fromDate = null, DateTime? toDate = null, long? buyerId = null, long? consigneeId = null, string dispatchNo = null)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("DispatchDate", ListSortDirection.Descending));
            }
            return Json(_service.GetAll(fromDate, toDate, buyerId, consigneeId, dispatchNo).ToDataSourceResult(request));
        }

        public IActionResult Create()
        {
            return View("CreateDispatch", new DispatchModel
            {
                DispatchDate = DateTime.Now

            });
        }

        public IActionResult Edit(long id)
        {
            return View("CreateDispatch", _service.GetById(id));
        }

        public IActionResult KendoReadSlitting([DataSourceRequest] DataSourceRequest request, long? dispatchId = null, long? buyerId = null, long? consigneeId = null, bool? isFinalize = false)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("SlittingRollNo", ListSortDirection.Ascending));
            }
            return Json(CustomRepository.GetDispatchSlittingList(buyerId, consigneeId, dispatchId, isFinalize).ToDataSourceResult(request));
        }

        [HttpPost]
        public IActionResult SaveDispatch(DispatchModel model, string create)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateDispatch", model);
            }
            string message = string.Empty;
            long id = 0;

            try
            {
                if (create == "Finalize Dispatch")
                {
                    model.IsFinalize = true;
                    id = _service.Update(model);
                }
                else if (model.DispatchId > 0)
                {
                    id = _service.Update(model);
                }
                else
                {
                    id = _service.Insert(model);
                }
            }
            catch (Exception ex)
            {
                message = CommonHelper.GetErrorMessage(ex);
            }
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.openPopup = CommonHelper.ShowAlertMessage(message);
                return View("CreateDispatch", model);
            }

            if (create == "Save & Continue")
            {
                return RedirectToAction("Edit", "Dispatch", new { id });
            }

            return RedirectToAction("Index");
        }

        public string KendoDestroy(long dispatchId)
        {
            string deleteMessage = string.Empty;

            try
            {
                if (dispatchId != 0)
                {
                    _service.Delete(dispatchId);
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

        public IActionResult ViewOrderDetail(long? orderId = null)
        {
            ViewBag.ProductList = SelectionList.ProductList().Select(x => new { x.ProductId, Name = x.FilmType + " " + x.Thickness, x.Thickness });
            return PartialView("_ViewOrderDetail");
        }

        public IActionResult KendoReadOrderDetail([DataSourceRequest] DataSourceRequest request, long orderId)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("ProductId", ListSortDirection.Ascending));
            }
            return Json(_orderDetailservice.GetAll(orderId).ToDataSourceResult(request));
        }

        public IActionResult GetDispatchReport(long? dispatchId = null)
        {
            if (dispatchId != null)
            {
                ViewBag.DispatchId = dispatchId;
                return PartialView("_DispatchSlipReport");
            }
            return View("Index");
        }

        public IActionResult GetDispatchReportList(long dispatchId, string dispatchNo, string dispatchDate)
        {
            ViewBag.DispatchId = dispatchId;
            ViewBag.DispatchNo = dispatchNo;
            ViewBag.DispatchDate = dispatchDate;
            return PartialView("_DispatchListReport");
        }

        public IActionResult OpenAssignOrderPopup()
        {
            return PartialView("_AssignOrderToSlit");
        }

        public IActionResult KendoReadOrder4Dispatch([DataSourceRequest] DataSourceRequest request, long buyerId, long consigneeId, long productId)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("OrderNo", ListSortDirection.Ascending));
            }
            return Json(CustomRepository.GetDispatchOrderList(buyerId, consigneeId, productId).ToDataSourceResult(request));
        }

        public string AssignOrderToSlit(long slittingId, long orderId)
        {
            try
            {
                long result = _slittingService.UpdateOrder(slittingId, orderId);
                return result != 0 ? string.Empty : "Something Went Wrong. Please Try again Later";
            }
            catch (Exception ex)
            {
                return CommonHelper.GetErrorMessage(ex);
            }
        }

        public string RemoveOrderFromSlit(long slittingId)
        {
            try
            {
                long result = _slittingService.RemoveOrder(slittingId);
                return result != 0 ? string.Empty : "Something Went Wrong. Please Try again Later";
            }
            catch (Exception ex)
            {
                return CommonHelper.GetErrorMessage(ex);
            }
        }
    }
}