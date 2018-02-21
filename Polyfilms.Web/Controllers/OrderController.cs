using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Services.Order;
using PolyFilms.Data;
using Kendo.Mvc.UI;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PolyFilms.Data.Models;
using PolyFilms.Common;
using PolyFilms.Services.Customer;
using PolyFilms.Services.Consignee;

namespace PolyFilms.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _service;
        private readonly ICustomerService _customerService;
        private readonly IConsigneeService _consigneeService;

        public OrderController(IOrderService service, ICustomerService customerService, IConsigneeService consigneeService)
        {
            _service = service;
            _customerService = customerService;
            _consigneeService = consigneeService;
        }

        public IActionResult Index()
        {
            ViewBag.CustomerList = SelectionList.CustomerList().Select(x => new { x.CustomerId, x.Name });
            ViewBag.ConsigneeList = SelectionList.ConsigneeList().Select(x => new { x.ConsigneeId, x.Name });
            ViewBag.OrderStatusList = SelectionList.OrderStatusList().Select(x => new { x.OrderStatusId, x.Name });
            return View();
        }

        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request, DateTime? fromDate = null, DateTime? toDate = null, long? buyerId = null, long? consigneeId = null, string orderNo = null,short? statusId = null)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("OrderDate", ListSortDirection.Descending));
            }
            return Json(_service.GetAll(fromDate, toDate, buyerId, consigneeId, orderNo,statusId).ToDataSourceResult(request));
        }

        public IActionResult Create()
        {
            OrderModel model = new OrderModel
            {
                OrderDate = DateTime.Now,
                DeliverySchedule = DateTime.Now
            };
            return View("CreateOrder", model);
        }

        public IActionResult Edit(long id)
        {
            ViewBag.ProductList = SelectionList.ProductList().Select(x => new { x.ProductId, Name = x.FilmType + " " + x.Thickness, x.Thickness });
            OrderModel model = _service.GetById(id);
            ConsigneeModel customerModel = _consigneeService.GetById(model.ConsigneeId);
            model.PhoneNumber = customerModel.PhoneNumber;
            model.DeliveryAddress = customerModel.DeliveryAddress;
            return View("CreateOrder", model);
        }

        [HttpPost]
        public IActionResult SaveOrder(OrderModel model, string create)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateOrder", model);
            }

            string message = string.Empty;
            long id = 0;

            try
            {
                id = model.OrderId > 0 ? _service.Update(model) : _service.Insert(model);
            }
            catch (Exception ex)
            {
                message = CommonHelper.GetErrorMessage(ex);
            }


            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.openPopup = CommonHelper.ShowAlertMessage(message);
                return View("CreateOrder", model);
            }

            if (create == "Save & Continue")
            {
                return RedirectToAction("Edit", "Order", new {id });
            }

            return RedirectToAction("Index");

        }



        public string KendoDestroy(long orderId)
        {
            string deleteMessage = string.Empty;

            try
            {
                if (orderId != 0)
                {
                    _service.Delete(orderId);
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

        public IActionResult GetConsigneeData(long consigneeId)
        {
            string erroeMessage = string.Empty;

            try
            {
                if (consigneeId == 0) return Json(erroeMessage);

                CustomerModel model = _customerService.GetById(consigneeId);
                return Json(new { phone = model.PhoneNumber, address = model.DeliveryAddress });
            }
            catch (Exception ex)
            {
                erroeMessage = CommonHelper.GetErrorMessage(ex);
            }
            return Json(erroeMessage);
        }

        public IActionResult AddCustomer()
        {
            CustomerModel model = new CustomerModel();
            model.IsActive = true;
            return PartialView("_AddCustomer", model);
        }

        public IActionResult AddConsignee(long buyerId)
        {
            ConsigneeModel model = new ConsigneeModel();
            model.IsActive = true;
            return PartialView("_AddConsignee", model);
        }

        [HttpPost]
        public long SaveCustomer(CustomerModel model)
        {
            try
            {
                if (model != null)
                {
                    model.IsActive = true;
                    return _customerService.Insert(model);
                }
                
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPost]
        public long SaveConsignee(ConsigneeModel model)
        {
            try
            {
                if (model != null)
                {
                    model.IsActive = true;
                    return _consigneeService.Insert(model);
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}