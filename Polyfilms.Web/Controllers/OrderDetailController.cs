using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Data.Models;
using Kendo.Mvc.UI;
using PolyFilms.Services.OrderDetail;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using PolyFilms.Common;

namespace PolyFilms.Web.Controllers
{
    public class OrderDetailController : BaseController
    {
        private readonly IOrderDetailService _service;

        public OrderDetailController(IOrderDetailService service)
        {
            _service = service;
        }

        public IActionResult KendoDestroy([DataSourceRequest] DataSourceRequest request, OrderDetailModel model)
        {
            string deleteMessage = string.Empty;

            try
            {
                _service.Delete(model.OrderDetailId);
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

        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request,long orderId)
        {
            try
            {

                if (!request.Sorts.Any())
                {
                    request.Sorts.Add(new SortDescriptor("ProductId", ListSortDirection.Ascending));
                }

                return Json(_service.GetAll(orderId).ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public IActionResult KendoSave([DataSourceRequest] DataSourceRequest request, OrderDetailModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }

            string message = string.Empty;

            try
            {
                if (model.OrderDetailId > 0)
                {
                    _service.Update(model);
                }
                else
                {
                    model.OrderDetailId = _service.Insert(model);
                }
            }
            catch (Exception ex)
            {
                message = CommonHelper.GetErrorMessage(ex);
            }


            ModelState.Clear();
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError("ProductId", message);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}