using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Services.Slitting;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using PolyFilms.Data.Models;
using PolyFilms.Data;

namespace PolyFilms.Web.Controllers
{
    public class CloseSlittingBatchController : BaseController
    {
        private readonly ISlittingService _service;

        public CloseSlittingBatchController(ISlittingService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            BasicSlittingDetailModel model = new BasicSlittingDetailModel
            {
                SlittingDate = DateTime.Now
            };
            ViewBag.CoreList = SelectionList.CoreList().Select(x => new { x.CoreId, Name = x.Name + " " + x.Thickness, x.Weight });
            ViewBag.OrderList = SelectionList.OrderList().Select(x => new { x.OrderId, x.OrderNo });
            ViewBag.TreatmentList = SelectionList.SlittingTreatmentList().Select(x => new { x.TreatmentId, x.Name });
            ViewBag.QualityList = SelectionList.SlittingStatusList().Select(x => new { x.SlittingStatusId, x.Name });
            ViewBag.JumboList = SelectionList.JumboList().Select(x => new { x.JumboId, x.JumboNo });

            return View(model);
        }

        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request, DateTime slittingDate, short shiftId, int operatorId, long? jumboId = null, long? primarySlittingId = null, int? setNo = null)
        {
            try
            {

                if (!request.Sorts.Any())
                {
                    request.Sorts.Add(new SortDescriptor("SlittingRollNo", ListSortDirection.Descending));
                }

                return Json(_service.getSlittingData(slittingDate, shiftId, operatorId, jumboId, primarySlittingId, setNo).ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
    }
}