using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Common;
using PolyFilms.Data.Models;
using PolyFilms.Services.Slitting;
using PolyFilms.Data;
using PolyFilms.Services.Jumbo;

namespace PolyFilms.Web.Controllers
{
    public class CreateSlittingController : BaseController
    {
        private readonly ISlittingService _service;
        private readonly IJumboService _jumboService;

        public CreateSlittingController(ISlittingService service, IJumboService jumboService)
        {
            _service = service;
            _jumboService = jumboService;
        }

        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request, DateTime slittingDate, short shiftId,int operatorId ,long? jumboId = null, long? primarySlittingId = null,int? setNo = null)
        {
            try
            {

                if (!request.Sorts.Any())
                {
                    request.Sorts.Add(new SortDescriptor("SlittingRollNo", ListSortDirection.Descending));
                }

                return Json(_service.getSlittingData(slittingDate,shiftId,operatorId,jumboId,primarySlittingId,setNo).ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public IActionResult KendoSave([DataSourceRequest] DataSourceRequest request, AdditionalSlittingDetailModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }

            string message = string.Empty;

            try
            {
               
                model.CoreWeight = (model.Width + 10) * model.CoreWeight;
                model.Netweight = model.Grossweight - model.CoreWeight;
                model.Difference = ((model.Width * (decimal)0.905 * model.Length * model.Thickness) / 1000000) - model.Netweight;

                if (model.SlittingId > 0)
                {
                    _service.UpdateOther(model);
                }
                else
                {

                    model.SlittingId = _service.InsertOther(model);
                }
            }
            catch (Exception ex)
            {
                message = CommonHelper.GetErrorMessage(ex);
            }


            ModelState.Clear();
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError("SlittingDate", message);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public IActionResult KendoDestroy([DataSourceRequest] DataSourceRequest request, AdditionalSlittingDetailModel model)
        {
            string deleteMessage = string.Empty;

            try
            {
                _service.Delete(model.SlittingId);
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

        [HttpPost]
        public string SaveAllSlitting(ExtraSlittingModel model)
        {
            try
            {
                decimal remainigWt = 0;

                if (model.JumboId != null)
                {
                    remainigWt = _jumboService.ManageJumboWeight(model.JumboId.Value, model.TotalWt, model.WasteWt);
                }

                if (remainigWt > 0 && model.JumboJointId1 != null)
                {
                    remainigWt = _jumboService.ManageJumboWeight(model.JumboJointId1.Value, remainigWt);
                }

                if (remainigWt > 0 && model.JumboJointId2 != null)
                {
                    remainigWt = _jumboService.ManageJumboWeight(model.JumboJointId2.Value, remainigWt);
                }

                if (remainigWt > 0 && model.JumboJointId3 != null)
                {
                    _jumboService.ManageJumboWeight(model.JumboJointId3.Value, remainigWt);
                }

                _service.LockAllSlitting(model.SlittingIds);

                return string.Empty;
            }
            catch (Exception ex)
            {
                return CommonHelper.GetErrorMessage(ex);
            }
        }

        [HttpPost]
        public string SaveAllSecondarySlitting(ExtraSlittingModel model)
        {
            try
            {
                //if (model.PrimarySlittingId != null)
                //{
                //    _service.ManageSlittingWateWeight(model.PrimarySlittingId.Value, model.WasteWt == null ? 0 : model.WasteWt.Value);
                //}
                _service.LockAllSlitting(model.SlittingIds);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return CommonHelper.GetErrorMessage(ex);
            }
        }

        public IActionResult GetSlittingListReport(string slittingIds)
        {
            if (slittingIds != null)
            {
                ViewBag.SlittingIdList = slittingIds;
                return PartialView("~/Views/Slitting/_SlittingAllReport.cshtml");
            }
            return View("~/Views/Slitting/Index.cshtml");
        }

    }
}