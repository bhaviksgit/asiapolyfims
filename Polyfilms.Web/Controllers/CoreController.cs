using System;
using System.Linq;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Data.Models;
using PolyFilms.Web.Interfaces;
using PolyFilms.Services.Core;
using PolyFilms.Common;
using Kendo.Mvc.Extensions;
using Kendo.Mvc;

namespace PolyFilms.Web.Controllers
{
    public class CoreController : BaseController, IGenericKendoController<CoreModel>
    {
        private readonly ICoreService _service;

        public CoreController(ICoreService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult KendoDestroy([DataSourceRequest] DataSourceRequest request, CoreModel model)
        {
            string deleteMessage = string.Empty;

            try
            {
                _service.Delete(model.CoreId);
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

        public IActionResult KendoSave([DataSourceRequest] DataSourceRequest request, CoreModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }

            string message = string.Empty;

            try
            {
                model.CoreId = model.CoreId > 0 ? _service.Update(model) : _service.Insert(model);
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
    }
}