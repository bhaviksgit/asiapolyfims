using Microsoft.EntityFrameworkCore;
using PolyFilms.Data.CustomModel;
using PolyFilms.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using PolyFilms.Common;

namespace PolyFilms.Data.Repositories
{
    public class BaseContext
    {
        public static PolyFilmsContext GetDbContext()
        {
            var dbOptions = new DbContextOptionsBuilder<PolyFilmsContext>();
            dbOptions.UseSqlServer(CommonHelper.ConnectionString);
            var dbContext = new PolyFilmsContext(dbOptions.Options);
            return dbContext;
        }
    }

    public class CustomRepository
    {
        public static List<UserAccessPermission> UserAccessPermissions(int? roleId = null, bool isSuperAdmin = false)
        {
            IList<UserAccessPermission> dataList;

            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                dataList = dbContext.LoadStoredProc("dbo.Get_UserAccessPermissions")
                    .WithSqlParam("RoleId", roleId)
                    .WithSqlParam("IsSuperAdmin", isSuperAdmin)
                    .ExecuteStoredProc<UserAccessPermission>();
            }

            return dataList.OrderBy(m => m.MenuName).ToList();
        }

        public static List<UserRightsModel> GetUserRightsList(int roleId)
        {

            IList<UserRightsModel> dataList;

            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                dataList = dbContext.LoadStoredProc("dbo.AssignRoleList")
                    .WithSqlParam("RoleId", roleId)
                    .ExecuteStoredProc<UserRightsModel>();
            }

            return dataList.ToList();
        }

        public static List<DispatchSlittingList> GetDispatchSlittingList(long? buyerId = null, long? consigneeId = null, long? DispatchId = null, bool? IsFinalize = false)
        {

            IList<DispatchSlittingList> dataList;

            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                dataList = dbContext.LoadStoredProc("dbo.GetDispatchSlittingList")
                    .WithSqlParam("BuyerId", buyerId)
                    .WithSqlParam("ConsigneeId", consigneeId)
                    .WithSqlParam("DispatchId", DispatchId)
                    .WithSqlParam("IsFinalize", IsFinalize)
                    .ExecuteStoredProc<DispatchSlittingList>();
            }

            return dataList.OrderBy(m => m.SlittingDate).ToList();
        }

        public static bool CheckProductExistsINOrder(long OrderId, long ProductId,long OrderDetailId)
        {
            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                return dbContext.TblOrderDetail.Any(m => m.OrderId == OrderId && m.ProductId == ProductId && m.OrderDetailId != OrderDetailId);
            }
        }

        public static bool CheckProductExistsOrNot(string FilmType, decimal Thickness, long ProductId)
        {
            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                return dbContext.TblProduct.Any(m => m.FilmType == FilmType && m.Thickness == Thickness && m.ProductId != ProductId);
            }
        }

        public static List<InvoiceSlittingList> GetInvoiceSlittingList(long? dispatchId = null)
        {
            IList<InvoiceSlittingList> dataList;

            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                dataList = dbContext.LoadStoredProc("dbo.GetSlittingListByDispatchId")
                    .WithSqlParam("dispatchId", dispatchId)
                    .ExecuteStoredProc<InvoiceSlittingList>();
            }

            return dataList.OrderBy(m => m.OrderNo).ToList();
        }

        public static TotalAmountModel GetTotalAmountForInvoice(string listofDispatchId)
        {
            TotalAmountModel totalAmount;

            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                totalAmount = dbContext.LoadStoredProc("dbo.GetTotalAmountForInvoice")
                    .WithSqlParam("lostOfDispatchId", listofDispatchId)
                    .ExecuteStoredProc<TotalAmountModel>().FirstOrDefault();
            }

            return totalAmount;
        }

        public static List<InvoiceDispatchList> GetInvoiceDispatchList(long? customerId = null, long? consigneeId = null, long? invoiceId = null, bool? isFinalize = false)
        {
            IList<InvoiceDispatchList> dataList;

            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                dataList = dbContext.LoadStoredProc("dbo.GetInvoiceDispatchList")
                    .WithSqlParam("CustomerId", customerId)
                    .WithSqlParam("ConsigneeId", consigneeId)
                    .WithSqlParam("InvoiceId", invoiceId)
                    .WithSqlParam("IsFinalize", isFinalize)
                    .ExecuteStoredProc<InvoiceDispatchList>();
            }

            return dataList.OrderBy(m => m.DispatchDate).ToList();
        }

        public static List<JumboRollStockModel> GetJumboRollStockList()
        {
            IList<JumboRollStockModel> dataList;

            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                dataList = dbContext.LoadStoredProc("dbo.GetJumboRollStockData").ExecuteStoredProc<JumboRollStockModel>();
            }

            return dataList.OrderBy(m => m.JumboDate).ToList();
        }


        public static List<SalesOrderDetailModel> GetSalesOrderDetailList(int? month = null,int? year = null)
        {
            IList<SalesOrderDetailModel> dataList;

            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                dataList = dbContext.LoadStoredProc("dbo.GetSalesOrderDetail")
                    .WithSqlParam("month", month)
                    .WithSqlParam("year", year)
                    .ExecuteStoredProc<SalesOrderDetailModel>();
            }

            return dataList.OrderBy(m => m.OrderDate).ToList();
        }

        public static List<PendingProductionDataModel> GetPendingProductionList()
        {
            IList<PendingProductionDataModel> dataList;

            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                dataList = dbContext.LoadStoredProc("dbo.GetPendingProductionData").ExecuteStoredProc<PendingProductionDataModel>();
            }

            return dataList.OrderByDescending(m => m.Pending).ToList();
        }

        public static List<DispatchOrderList> GetDispatchOrderList(long buyerId, long consigneeId, long productId)
        {
            IList<DispatchOrderList> dataList;

            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                dataList = dbContext.LoadStoredProc("dbo.GetOrderForDispatchSlitting")
                    .WithSqlParam("buyerId", buyerId)
                    .WithSqlParam("consigneeId", consigneeId)
                    .WithSqlParam("productId", productId)
                    .ExecuteStoredProc<DispatchOrderList>();
            }

            return dataList.OrderBy(m => m.OrderDate).ToList();
        }

        public static bool CheckCoreAlreadyExists(string Name, decimal? thickness, short CoreId)
        {
            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                return dbContext.TblCore.Any(m => m.Name == Name && m.Thickness == thickness && m.CoreId != CoreId);
            }
        }

        public static bool CheckRoleExistsOrNot(string RoleName, int RoleId)
        {
            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                return dbContext.TblRoles.Any(m => m.RoleName == RoleName && m.RoleId != RoleId);
            }
        }

        public static bool CheckCustomerExistsOrNot(string GstNumber, string Pannumber, string contactnumber, long customerId)
        {
            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                return dbContext.TblCustomer.Any(m => m.Gstnumber == GstNumber && m.PanNumber == Pannumber && m.PhoneNumber == contactnumber && m.CustomerId != customerId);
            }
        }

        public static bool CheckConsigneeExistsOrNot(string GstNumber, string Pannumber, string contactnumber, long consigneeId)
        {
            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                return dbContext.TblConsignee.Any(m => m.Gstnumber == GstNumber && m.PanNumber == Pannumber && m.PhoneNumber == contactnumber && m.ConsigneeId != consigneeId);
            }
        }


        public static void LockSlittingData(string slittingIds)
        {
            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                dbContext.LoadStoredProc("dbo.LockSlittingData").WithSqlParam("SlittingIds", slittingIds).ExecuteStoredProc<int>().FirstOrDefault();
            }
        }

        public static List<SalesOrderModel> GetSalesOrderDetailByOrderId(long orderId,short productId)
        {
            IList<SalesOrderModel> dataList;

            using (PolyFilmsContext dbContext = BaseContext.GetDbContext())
            {
                dataList = dbContext.LoadStoredProc("dbo.GetSalesOrderDetailByOrder")
                    .WithSqlParam("OrderId", orderId)
                    .WithSqlParam("ProductId", productId)
                    .ExecuteStoredProc<SalesOrderModel>();
            }

            return dataList.OrderByDescending(m=>m.DispatchDate).ToList();
        }
    }
}
