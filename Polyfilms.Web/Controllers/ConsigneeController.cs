using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Services.Consignee;
using Kendo.Mvc.UI;
using PolyFilms.Common;
using Kendo.Mvc.Extensions;
using Kendo.Mvc;
using PolyFilms.Data.Models;

namespace PolyFilms.Web.Controllers
{
    public class ConsigneeController : BaseController
    {
        private readonly IConsigneeService _service;

        public ConsigneeController(IConsigneeService service)
        {
            _service = service;
        }

        public IActionResult KendoDestroy([DataSourceRequest] DataSourceRequest request, ConsigneeModel model)
        {
            string deleteMessage = string.Empty;

            try
            {
                _service.Delete(model.ConsigneeId);
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

        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request,long? buyerId = null)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("Name", ListSortDirection.Ascending));
            }

            return Json(_service.GetAll(buyerId).ToDataSourceResult(request));
        }

        public IActionResult KendoSave([DataSourceRequest] DataSourceRequest request, ConsigneeModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }

            string message = string.Empty;

            try
            {
                if (model.ConsigneeId > 0)
                {                     
                    _service.Update(model);
                }
                else
                {
                    model.ConsigneeId = _service.Insert(model);
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