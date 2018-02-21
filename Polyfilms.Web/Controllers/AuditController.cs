using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PolyFilms.Data;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Services.Audit;

namespace PolyFilms.Web.Controllers
{
    public class AuditController : BaseController
    {
        #region Priavte Variables
        private readonly IAuditService _service;
        #endregion

        #region Contsructor
        public AuditController(IAuditService service)
        {
            _service = service;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to return main view
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            ViewBag.UserList = SelectionList.UserList().Select(x => new { x.UserId, x.Name });
            ViewBag.ScreenList = SelectionList.ScreenList().Select(x => new { x.TableName, x.ScreenName });
            ViewBag.EntityActionList = SelectionList.EntityActionList();
            return View();
        }

        /// <summary>
        /// Method to bind Audit List grid
        /// </summary>
        /// <param name="request">DataSource Request</param>
        /// <param name="fromDate">From date</param>
        /// <param name="toDate">To date</param>
        /// <param name="userId">User Id</param>
        /// <param name="screenId">Screeen Id</param>
        /// <param name="actionId">Action Id</param>
        /// <returns></returns>
        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request, DateTime? fromDate = null, DateTime? toDate = null, int? userId = null, string screenId = null, string actionId = null)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("DateTime", ListSortDirection.Descending));
            }
            return Json(_service.GetAll(fromDate, toDate, userId, screenId, actionId).ToDataSourceResult(request));
        }

        /// <summary>
        /// Method to return details of Audit History
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult AuditDetails(long id)
        {
            AuditDetailsModel model = _service.GetById(id);

            List<AuditChange> rslt = new List<AuditChange>();

            AuditChange Change = new AuditChange
            {
                DateTimeStamp = model.DateTime.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                AuditActionTypeName = model.Action
            };

            var delta = JsonConvert.DeserializeObject<List<AuditDelta>>(model.Changes);

            foreach (AuditDelta objAuditDelta in delta)
            {
                if (objAuditDelta.FieldName == "ProductId")
                {
                    objAuditDelta.FieldName = "Product";

                    List<TblProduct> productList = SelectionList.ProductList();

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueBefore))
                    {
                        objAuditDelta.ValueBefore = productList.FirstOrDefault(x => x.ProductId == Convert.ToInt16(objAuditDelta.ValueBefore)).FilmType;
                    }

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueAfter))
                    {
                        objAuditDelta.ValueAfter = productList.FirstOrDefault(x => x.ProductId == Convert.ToInt16(objAuditDelta.ValueAfter)).FilmType;
                    }
                }

                if (objAuditDelta.FieldName == "ShiftId")
                {
                    objAuditDelta.FieldName = "Shift";

                    List<TblShiftType> productList = SelectionList.ShiftTypeList();

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueBefore))
                    {
                        objAuditDelta.ValueBefore = productList.FirstOrDefault(x => x.ShiftId == Convert.ToInt16(objAuditDelta.ValueBefore)).Name;
                    }

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueAfter))
                    {
                        objAuditDelta.ValueAfter = productList.FirstOrDefault(x => x.ShiftId == Convert.ToInt16(objAuditDelta.ValueAfter)).Name;
                    }
                }

                if (objAuditDelta.FieldName == "ShiftInchargeId")
                {
                    objAuditDelta.FieldName = "Shift Incharge";

                    List<TblUser> productList = SelectionList.UserList();

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueBefore))
                    {
                        objAuditDelta.ValueBefore = productList.FirstOrDefault(x => x.UserId == Convert.ToInt16(objAuditDelta.ValueBefore)).Name;
                    }

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueAfter))
                    {
                        objAuditDelta.ValueAfter = productList.FirstOrDefault(x => x.UserId == Convert.ToInt16(objAuditDelta.ValueAfter)).Name;
                    }
                }

                if (objAuditDelta.FieldName == "StatusId")
                {
                    objAuditDelta.FieldName = "Status";

                    List<TblJumboStatus> productList = SelectionList.JumboStatusList();

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueBefore))
                    {
                        objAuditDelta.ValueBefore = productList.FirstOrDefault(x => x.StatusId == Convert.ToInt16(objAuditDelta.ValueBefore)).Name;
                    }

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueAfter))
                    {
                        objAuditDelta.ValueAfter = productList.FirstOrDefault(x => x.StatusId == Convert.ToInt16(objAuditDelta.ValueAfter)).Name;
                    }
                }


                if (objAuditDelta.FieldName == "BuyerId")
                {
                    objAuditDelta.FieldName = "Buyer";

                    List<TblCustomer> productList = SelectionList.CustomerList();

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueBefore))
                    {
                        objAuditDelta.ValueBefore = productList.FirstOrDefault(x => x.CustomerId == Convert.ToInt16(objAuditDelta.ValueBefore)).Name;
                    }

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueAfter))
                    {
                        objAuditDelta.ValueAfter = productList.FirstOrDefault(x => x.CustomerId == Convert.ToInt16(objAuditDelta.ValueAfter)).Name;
                    }
                }

                if (objAuditDelta.FieldName == "ConsigneeId")
                {
                    objAuditDelta.FieldName = "Shift";

                    List<TblConsignee> productList = SelectionList.ConsigneeList();

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueBefore))
                    {
                        objAuditDelta.ValueBefore = productList.FirstOrDefault(x => x.ConsigneeId == Convert.ToInt16(objAuditDelta.ValueBefore)).Name;
                    }

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueAfter))
                    {
                        objAuditDelta.ValueAfter = productList.FirstOrDefault(x => x.ConsigneeId == Convert.ToInt16(objAuditDelta.ValueAfter)).Name;
                    }
                }


                if (objAuditDelta.FieldName == "CoreId")
                {
                    objAuditDelta.FieldName = "Core";

                    List<TblCore> productList = SelectionList.CoreList();

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueBefore))
                    {
                        objAuditDelta.ValueBefore = productList.FirstOrDefault(x => x.CoreId == Convert.ToInt16(objAuditDelta.ValueBefore)).Name + " " +
                        productList.FirstOrDefault(x => x.CoreId == Convert.ToInt16(objAuditDelta.ValueBefore)).Thickness;
                    }

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueAfter))
                    {
                        objAuditDelta.ValueAfter = productList.FirstOrDefault(x => x.CoreId == Convert.ToInt16(objAuditDelta.ValueAfter)).Name + " " +
                                                   productList.FirstOrDefault(x => x.CoreId == Convert.ToInt16(objAuditDelta.ValueAfter)).Thickness;
                    }
                }

                if (objAuditDelta.FieldName == "TreatmentId")
                {
                    objAuditDelta.FieldName = "Treatment";

                    List<TblTreatment> productList = SelectionList.SlittingTreatmentList();

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueBefore))
                    {
                        objAuditDelta.ValueBefore = productList.FirstOrDefault(x => x.TreatmentId == Convert.ToInt16(objAuditDelta.ValueBefore)).Name;
                    }

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueAfter))
                    {
                        objAuditDelta.ValueAfter = productList.FirstOrDefault(x => x.TreatmentId == Convert.ToInt16(objAuditDelta.ValueAfter)).Name;
                    }
                }

                if (objAuditDelta.FieldName == "JumboId")
                {
                    objAuditDelta.FieldName = "Jumbo Nr";

                    List<TblJumbo> productList = SelectionList.JumboList();

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueBefore))
                    {
                        objAuditDelta.ValueBefore = productList.FirstOrDefault(x => x.JumboId == Convert.ToInt16(objAuditDelta.ValueBefore)).JumboNo;
                    }

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueAfter))
                    {
                        objAuditDelta.ValueAfter = productList.FirstOrDefault(x => x.JumboId == Convert.ToInt16(objAuditDelta.ValueAfter)).JumboNo;
                    }
                }

                if (objAuditDelta.FieldName == "OrderId")
                {
                    objAuditDelta.FieldName = "Order Nr";

                    List<TblOrder> productList = SelectionList.OrderList();

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueBefore))
                    {
                        objAuditDelta.ValueBefore = productList.FirstOrDefault(x => x.OrderId == Convert.ToInt16(objAuditDelta.ValueBefore)).OrderNo;
                    }

                    if (!string.IsNullOrEmpty(objAuditDelta.ValueAfter))
                    {
                        objAuditDelta.ValueAfter = productList.FirstOrDefault(x => x.OrderId == Convert.ToInt16(objAuditDelta.ValueAfter)).OrderNo;
                    }
                }
                

            }

            Change.Changes.AddRange(delta);
            rslt.Add(Change);

            return PartialView("_Details", rslt);
        }
        #endregion
    }
}