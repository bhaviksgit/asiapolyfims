using System;
using System.Linq;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Web.Interfaces;
using PolyFilms.Services.Customer;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using PolyFilms.Common;
using PolyFilms.Data.Models;

namespace PolyFilms.Web.Controllers
{
    public class CustomerController : BaseController, IGenericKendoController<CustomerModel>
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult KendoDestroy([DataSourceRequest] DataSourceRequest request, CustomerModel model)
        {
            string deleteMessage = string.Empty;

            try
            {
                _service.Delete(model.CustomerId);
            }
            catch (Exception ex)
            {
                deleteMessage = CommonHelper.GetErrorMessage(ex);
            }

            ModelState.Clear();
            if (!string.IsNullOrEmpty(deleteMessage))
            {
                ModelState.AddModelError(deleteMessage, deleteMessage);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("Name", ListSortDirection.Ascending));
            }

            return Json(_service.GetAll().ToDataSourceResult(request));
        }

        public IActionResult KendoSave([DataSourceRequest] DataSourceRequest request, CustomerModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }

            string message = string.Empty;

            try
            {
                if (model.CustomerId > 0)
                {
                    model.ModifiedDate = DateTime.Now;
                    _service.Update(model);
                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    model.CustomerId = _service.Insert(model);
                }
            }
            catch (Exception ex)
            {
                message = CommonHelper.GetErrorMessage(ex);
            }


            ModelState.Clear();
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError("Name", message);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public bool ChangeStatus(int id)
        {
            return id != 0 && _service.ChangeStatus(id);
        }
    }
}