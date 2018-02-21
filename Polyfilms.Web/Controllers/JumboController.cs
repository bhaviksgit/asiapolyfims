using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc;
using PolyFilms.Services.Jumbo;
using Kendo.Mvc.Extensions;
using PolyFilms.Data;
using PolyFilms.Data.Models;
using PolyFilms.Common;

namespace PolyFilms.Web.Controllers
{
    public class JumboController : BaseController
    {
        private readonly IJumboService _service;

        public JumboController(IJumboService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            ViewBag.ProductList = SelectionList.ProductList().Select(x => new { x.ProductId, Name = x.FilmType + " " + x.Thickness });
            ViewBag.StatusList = SelectionList.JumboStatusList().Select(x => new { x.StatusId, x.Name });
            return View();
        }

        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request, DateTime? fromDate = null, DateTime? toDate = null, short? productId = null, string jumboNo = null, short? statusId = null)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("JumboDate", ListSortDirection.Descending));
            }

            return Json(_service.GetAll(fromDate, toDate, productId, jumboNo, statusId).ToDataSourceResult(request));
        }

        public IActionResult Create()
        {
            return View("CreateJumbo", new JumboModel
            {
                JumboDate = DateTime.Now

            });
        }

        public IActionResult Edit(long id)
        {
            return View("CreateJumbo", _service.GetById(id));
        }

        [HttpPost]
        public IActionResult SaveJumbo(JumboModel model, string create)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateJumbo", model);
            }

            string message = string.Empty;
            long id = 0;

            try
            {
                model.WasteWeight = 0;
                if (create == "Ready For Test")
                {
                    model.StatusId = (short) Enums.JumboStatus.ReadyForTest; //ready for test status change ststically
                    id = _service.Update(model);
                }
                else if (model.JumboId > 0)
                {
                    id = _service.Update(model);
                }
                else
                {
                    model.StatusId = (short)Enums.JumboStatus.NEW;
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
                return View("CreateJumbo", model);
            }

            if (create == "Save & Continue")
            {
                return RedirectToAction("Edit", "Jumbo", new { id });
            }

            return RedirectToAction("Index");

        }

        public string KendoDestroy(long jumboId)
        {
            string deleteMessage = string.Empty;

            try
            {
                if (jumboId != 0)
                {
                    _service.Delete(jumboId);
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

        public IActionResult ChangeJumboStatus(JumboStatusModel model)
        {
            ViewBag.StatusList = SelectionList.JumboStatusList().Select(x => new { x.StatusId, x.Name });
            return PartialView("_StatusChange", model);
        }

        [HttpPost]
        public string SaveChangeStatus(JumboStatusModel model)
        {
            if (model == null) return "Something went wrong. Please try again";
            _service.ChnageJumboStatus(model);
            return string.Empty;
        }
    }
}