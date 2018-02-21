using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PolyFilms.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using PolyFilms.Common;

namespace PolyFilms.Web.Controllers
{
    /// <summary>
    /// Common controller having all the common methods
    /// </summary>
    public class CommonController : BaseController
    {
        /// <summary>
        /// Method to return all the Active Product List 
        /// </summary>
        /// <returns></returns>
        public IActionResult GetProductList()
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblProduct.Where(m => m.IsActive).Select(m => new { m.ProductId, Name = m.FilmType + "  " + m.Thickness.ToString("n0"), m.Thickness }).ToList();
                return Json(list);
            }

        }

        /// <summary>
        /// Method to return All the active User List
        /// </summary>
        /// <returns></returns>
        public IActionResult GetUserList()
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblUser.Where(m => m.IsActive).Select(m => new { m.UserId, m.Username }).ToList();
                return Json(list);
            }

        }

        /// <summary>
        /// Method to return All Shift List
        /// </summary>
        /// <returns></returns>
        public IActionResult GetShiftList()
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblShiftType.Select(m => new { m.ShiftId, m.Name }).ToList();
                return Json(list);
            }

        }

        /// <summary>
        /// Methos to return all active Customer List
        /// </summary>
        /// <returns></returns>
        public IActionResult GetCustomerList()
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblCustomer.Where(x => x.IsActive).Select(m => new { m.CustomerId, m.Name, m.PhoneNumber, m.DeliveryAddress }).OrderBy(m => m.Name).ToList();
                return Json(list);
            }
        }

        /// <summary>
        /// Method to return all Order filter by Product
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns></returns>
        public IActionResult GetFilterOrderList(short? productId)
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblOrder.Include(m => m.TblOrderDetail).Where(p => p.TblOrderDetail.Any(c => c.ProductId == productId))
                                  .Select(m => new { m.OrderId, m.OrderNo }).OrderBy(m => m.OrderNo).ToList();
                return Json(list);
            }

        }

        /// <summary>
        /// Method to return all Order List Filtered by Buyer
        /// </summary>
        /// <param name="customerId">Buyer Id</param>
        /// <returns></returns>
        public IActionResult GetFilterOrderByCustomerList(long? customerId)
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblOrder.Where(x => x.ConsigneeId == customerId).Select(m => new { m.OrderId, m.OrderNo }).OrderBy(m => m.OrderNo).ToList();

                return Json(list);
            }

        }

        /// <summary>
        /// Method to return Jumbo List Whose Status = Q1 /Q2
        /// </summary>
        /// <returns></returns>
        public IActionResult GetJumboList()
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblJumbo
                            .Include(m => m.Product)
                            .Where(m => (m.StatusId == (short)Enums.JumboStatus.Q1 || m.StatusId == (short)Enums.JumboStatus.Q2) && (m.RemainingJumbo != 0)).OrderByDescending(m => m.JumboDate)
                            .Select(m => new { m.JumboId, m.JumboNo, m.ProductId, m.Thickness, m.RemainingJumbo }).ToList();
                return Json(list);
            }

        }

        /// <summary>
        /// Method to return all the Primary Slitting (Slittinf Whose Status = SC)
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPrimarySlittingList()
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblSlitting
                            .Include(m => m.Product)
                            .Where(m => m.JumboId != null && m.PrimarySlittingId == null ).OrderByDescending(m => m.SlittingDate)
                            .Select(m => new { m.SlittingId, m.SlittingRollNo, m.ProductId, m.Thickness, m.Netweight }).ToList();
                return Json(list);
            }

        }

        /// <summary>
        /// Method to return Core List
        /// </summary>
        /// <returns></returns>
        public IActionResult GetCoreList()
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblCore.Select(m => new { m.CoreId, Name = m.Name + "-" + m.Thickness, m.Weight }).ToList();
                return Json(list);
            }

        }

        /// <summary>
        /// Method to return Slitting Status / Quality List
        /// </summary>
        /// <returns></returns>
        public IActionResult GetQualityList()
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblSlittingStatus.Select(m => new { m.SlittingStatusId, m.Name }).OrderBy(m => m.Name).ToList();
                return Json(list);
            }

        }

        /// <summary>
        /// Method to return all Treatment for Slitting
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreatmentList()
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblTreatment.Select(m => new { m.TreatmentId, m.Name }).OrderBy(m => m.Name).ToList();
                return Json(list);
            }
        }

        /// <summary>
        /// Method to return all Consignee For Particular Buyer
        /// </summary>
        /// <param name="buyerId">Buyer Id</param>
        /// <returns></returns>
        public IActionResult GetConsigneeListFromBuyer(long? buyerId = null)
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblConsignee.Where(m => m.BuyerId == buyerId && m.IsActive).Select(m => new { m.ConsigneeId, m.Name, m.DeliveryAddress, m.PhoneNumber }).OrderBy(m => m.Name).ToList();
                return Json(list);
            }
        }

        public IActionResult GetJumboListForJoint(short productId, long jumboId)
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblJumbo
                    .Include(m => m.Product)
                    .Where(m =>
                            (m.StatusId == (short)Enums.JumboStatus.Q1 || m.StatusId == (short)Enums.JumboStatus.Q2)
                                && (m.ProductId == productId)
                                && (m.JumboId != jumboId)
                                && (m.RemainingJumbo != 0)
                            )
                    .Select(m => new { m.JumboId, m.JumboNo, m.ProductId, m.Thickness,m.RemainingJumbo }).OrderBy(m => m.JumboNo).ToList();
                return Json(list);
            }
        }

        public IActionResult GetSlittingListForJoint(short productId, long slittingId)
        {
            using (var context = BaseContext.GetDbContext())
            {
                var list = context.TblSlitting
                    .Include(m => m.Product)
                    .Where(m => (m.ProductId == productId) && (m.SlittingId != slittingId))
                    .Select(m => new { m.SlittingId,m.SlittingRollNo, m.ProductId, m.Thickness }).OrderBy(m => m.SlittingRollNo).ToList();
                return Json(list);
            }
        }

    }
}