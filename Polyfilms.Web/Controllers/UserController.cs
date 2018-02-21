using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Web.Interfaces;
using PolyFilms.Data;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using PolyFilms.Services.User;
using PolyFilms.Data.Models;
using PolyFilms.Common;

namespace PolyFilms.Web.Controllers
{
    public class UserController : BaseController , IGenericKendoController<UserModel>
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            ViewBag.RoleList = SelectionList.RoleList().Select(x => new { x.RoleId, x.RoleName });
            return View();
        }

        public IActionResult KendoRead([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                if (!request.Sorts.Any())
                {
                    request.Sorts.Add(new SortDescriptor("Name", ListSortDirection.Ascending));
                }

                List<UserModel> result = new List<UserModel>();

                IEnumerable<UserModel> modelResult = _service.GetAll();


                foreach (UserModel entity in modelResult)
                {
                    entity.Password = EncryptionDecryption.GetDecrypt(entity.Password);

                    result.Add(entity);
                }

                return Json(result.ToDataSourceResult(request));
            }
            catch(Exception ex)
            {
                return Json(ex);
            }
            
        }

        public IActionResult KendoSave([DataSourceRequest] DataSourceRequest request, UserModel model)
        {

            if (model == null || !ModelState.IsValid)
            {
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }

            string message = string.Empty;

            try
            {
                if (model.UserId > 0)
                {
                    model.Password = EncryptionDecryption.GetEncrypt(model.Password);
                    _service.Update(model);
                }
                else
                {
                    model.Password = EncryptionDecryption.GetEncrypt(model.Password);
                    model.UserId = _service.Insert(model);
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

        public IActionResult KendoDestroy([DataSourceRequest] DataSourceRequest request, UserModel model)
        {
            string deleteMessage = string.Empty;

            try
            {
                _service.Delete(model.UserId);
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


    }
}