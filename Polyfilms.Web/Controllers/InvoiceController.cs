using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Services.Invoice;
using Kendo.Mvc.UI;
using Kendo.Mvc;
using PolyFilms.Data.Models;
using Kendo.Mvc.Extensions;
using PolyFilms.Services.Slitting;
using PolyFilms.Data.Repositories;
using Newtonsoft.Json;
using PolyFilms.Data.CustomModel;
using PolyFilms.Common;
using PolyFilms.Data;

namespace PolyFilms.Web.Controllers
{
    public class InvoiceController : BaseController
    {
        #region Priavte Variables
        private readonly IInvoiceService _service;

        private readonly ISlittingService _serviceSlitting;
        #endregion

        #region Contsructor
        public InvoiceController(IInvoiceService service, ISlittingService slittingService)
        {
            _service = service;
            _serviceSlitting = slittingService;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to return main view
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            ViewBag.CustomerList = SelectionList.CustomerList().Select(x => new { x.CustomerId, x.Name });
            ViewBag.ConsigneeList = SelectionList.ConsigneeList().Select(x => new { x.ConsigneeId, x.Name });
            return View();
        }

        /// <summary>
        /// Method to bind Invoice List grid
        /// </summary>
        /// <param name="request">DataSource Request</param>
        /// <param name="fromDate">From date</param>
        /// <param name="toDate">To date</param>
        /// <param name="customerId">Customer Id</param>
        /// <param name="consigneeId">Consignee Id</param>
        /// <param name="invoiceNo">Invoice Nr</param>
        /// <returns></returns>
        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request, DateTime? fromDate = null, DateTime? toDate = null, long? customerId = null,long? consigneeId = null, string invoiceNo = null)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("InvoiceDate", ListSortDirection.Descending));
            }
            return Json(_service.GetAll(fromDate, toDate, customerId,consigneeId, invoiceNo).ToDataSourceResult(request));
        }

        /// <summary>
        /// Method to create new invoice
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            InvoiceModel model = new InvoiceModel { InvoiceDate = DateTime.Now };
            return View("CreateInvoice", model);
        }

        /// <summary>
        /// Method to edit invoice
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <returns></returns>
        public IActionResult Edit(long id)
        {
            return View("CreateInvoice", _service.GetById(id));
        }

        /// <summary>
        /// Method to bind the dispatch grid
        /// </summary>
        /// <param name="request">DataSource Request</param>
        /// <param name="customerId">Customer Id</param>
        /// <param name="consigneeId">Consignee Id</param>
        /// <param name="invoiceId">Invoice Id</param>
        /// <param name="isFinalize">Is finalized</param>
        /// <returns></returns>
        public IActionResult KendoReadDispatch([DataSourceRequest] DataSourceRequest request, long? customerId = null, long? consigneeId = null, long? invoiceId = null, bool? isFinalize = false)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("DispatchDate", ListSortDirection.Ascending));
            }
            return Json(CustomRepository.GetInvoiceDispatchList(customerId, consigneeId, invoiceId, isFinalize).ToDataSourceResult(request));
        }

        /// <summary>
        /// Method used to retun Slitting partial view
        /// </summary>
        /// <param name="IsFinalize">Is finalized</param>
        /// <returns></returns>
        public IActionResult GetSlittingByDispatchId(bool IsFinalize)
        {
            ViewBag.IsFinalize = IsFinalize;
            return PartialView("_SlittingList");
        }

        /// <summary>
        /// Method to bind the slitting grid
        /// </summary>
        /// <param name="request">DataSource Request</param>
        /// <param name="dispatchId">DispatchId</param>
        /// <returns></returns>
        public IActionResult KendoReadSlittingByDispatchId([DataSourceRequest] DataSourceRequest request, long? dispatchId = null)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("SlittingRollNo", ListSortDirection.Ascending));
            }
            return Json(CustomRepository.GetInvoiceSlittingList(dispatchId).ToDataSourceResult(request));
        }

        /// <summary>
        /// Method to save slitting rates
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string SaveSlittingRate(string model)
        {
            var slittingRate = JsonConvert.DeserializeObject<IEnumerable<InvoiceSlittingList>>(model);
            return _serviceSlitting.UpdateRate(slittingRate);
        }

        /// <summary>
        /// Method to save invoice
        /// </summary>
        /// <param name="model"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public IActionResult SaveInvoice(InvoiceModel model, string create)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateInvoice", model);
            }
            string message = string.Empty;
            long id = 0;

            try
            {
                if (create == "Finalize Invoice")
                {
                    model.IsFinalize = true;
                    id = _service.Update(model);
                }
                else if (model.InvoiceId > 0)
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
                message = CommonHelper.GetErrorMessage(ex, false);
            }

            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.openPopup = CommonHelper.ShowAlertMessage(CommonHelper.CommonErrorMsg);
                return View("CreateInvoice", model);
            }

            if (create == "Save & Continue")
            {
                return RedirectToAction("Edit", "Invoice", new { id });
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Method to delete invoice
        /// </summary>
        /// <param name="invoiceId">Invoice Id</param>
        /// <returns></returns>
        public string KendoDestroy(long invoiceId)
        {
            string deleteMessage = string.Empty;

            try
            {
                if (invoiceId != 0)
                {
                    _service.Delete(invoiceId);
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

        /// <summary>
        /// Method to return Invoice Report View
        /// </summary>
        /// <param name="invoiceId">Invoice Id</param>
        /// <returns></returns>
        public IActionResult GetInvoiceReport(long? invoiceId = null)
        {
            if (invoiceId != null)
            {
                ViewBag.InvoiceId = invoiceId;
                return PartialView("_InvoiceSlipReport");
            }
            return View("Index");
        }

        /// <summary>
        /// Method to bind Dispatch grid for invoice
        /// </summary>
        /// <param name="request"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public IActionResult KendoReadDispatchByInvoice([DataSourceRequest] DataSourceRequest request, long? invoiceId = null)
        {
            if (invoiceId != null)
            {
                return Json(SelectionList.DispatchListByInvoice(invoiceId).Select(x => new { x.DispatchId, x.DispatchNo, x.DispatchDate }).ToDataSourceResult(request));
            }
            return Json(string.Empty);
        }
        #endregion
    }
}