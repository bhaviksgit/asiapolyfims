using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Data.Models;
using PolyFilms.Web.Interfaces;
using PolyFilms.Services.Roles;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using PolyFilms.Common;
using PolyFilms.Data.CustomModel;
using PolyFilms.Data.Repositories;
using Newtonsoft.Json;
using PolyFilms.Data.Entity;
using PolyFilms.Services.RoleMenuMap;

namespace PolyFilms.Web.Controllers
{
    public class RolesController : BaseController, IGenericKendoController<RolesModel>
    {
        private readonly IRolesService _service;
        private readonly IRoleMenuMapService _tblRoleMenuservice;


        public RolesController(IRolesService service, IRoleMenuMapService roleMapService)
        {
            _service = service;
            _tblRoleMenuservice = roleMapService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request)
        {
            if (!request.Sorts.Any())
            {
                request.Sorts.Add(new SortDescriptor("RoleName", ListSortDirection.Ascending));
            }

            return Json(_service.GetAll().ToDataSourceResult(request));
        }

        public IActionResult KendoSave([DataSourceRequest] DataSourceRequest request, RolesModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }

            string message = string.Empty;

            try
            {
                if (model.RoleId > 0)
                {
                    _service.Update(model);
                }
                else
                {
                    model.RoleId = _service.Insert(model);
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

        public IActionResult KendoDestroy([DataSourceRequest] DataSourceRequest request, RolesModel model)
        {
            string deleteMessage = string.Empty;

            try
            {
                _service.Delete(model.RoleId);
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

        public bool ChangeStatus(int id)
        {
            return id != 0 && _service.ChangeStatus(id);
        }

        public IActionResult RoleMenusView()
        {
            return PartialView("_RoleMenuAssignment");
        }


        public IActionResult GetUserRightsList(DataSourceRequest request, int RoleId)
        {
            if (RoleId != 0)
            {
                List<UserRightsModel> list = CustomRepository.GetUserRightsList(RoleId).ToList();
                return Json(list.ToDataSourceResult(request));
            }
            return Json(new List<UserRightsModel>().ToDataSourceResult(request));
        }

        [HttpPost]
        public string SaveUserRights(string model)
        {
            try
            {
                var roleActions = JsonConvert.DeserializeObject<IEnumerable<UserRightsModel>>(model);

                foreach (UserRightsModel roleAction in roleActions)
                {
                    var roleActionEntity = new TblRoleMenuMap
                    {
                        IsInsert = roleAction.IsInsert,
                        IsView = roleAction.IsView,
                        IsEdit = roleAction.IsEdit,
                        IsDelete = roleAction.IsDelete,
                        IsChangeStatus = roleAction.IsChangeStatus,
                        RoleMenuPk = roleAction.RoleMenuPk,
                        MenuId = roleAction.MenuId,
                        RoleId = roleAction.RoleId
                    };

                    _tblRoleMenuservice.Update(roleActionEntity);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return CommonHelper.GetErrorMessage(ex, false);
            }
        }
    }
}