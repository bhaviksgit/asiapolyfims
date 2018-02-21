using System;
using System.Linq;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Data.Models;
using PolyFilms.Web.Interfaces;
using PolyFilms.Services.Product;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using PolyFilms.Common;

namespace PolyFilms.Web.Controllers
{
    public class ProductController : BaseController, IGenericKendoController<ProductModel>
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult KendoDestroy([DataSourceRequest] DataSourceRequest request, ProductModel model)
        {
            string deleteMessage = string.Empty;

            try
            {
                _service.Delete(model.ProductId);
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
            try
            {

                if (!request.Sorts.Any())
                {
                    request.Sorts.Add(new SortDescriptor("FilmType", ListSortDirection.Ascending));
                }

                return Json(_service.GetAll().ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public IActionResult KendoSave([DataSourceRequest] DataSourceRequest request, ProductModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }

            string message = string.Empty;

            try
            {
                if (model.ProductId > 0)
                {
                    _service.Update(model);
                }
                else
                {
                    model.ProductId = _service.Insert(model);
                }
            }
            catch (Exception ex)
            {
                message = CommonHelper.GetErrorMessage(ex);
            }


            ModelState.Clear();
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError("FilmType", message);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public bool ChangeStatus(short id)
        {
            return id != 0 && _service.ChangeStatus(id);
        }
    }
}