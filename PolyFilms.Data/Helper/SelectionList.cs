using PolyFilms.Data.Entity;
using PolyFilms.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using PolyFilms.Data.CustomModel;
using Microsoft.EntityFrameworkCore;
using PolyFilms.Data.Models;

namespace PolyFilms.Data
{
    public class SelectionList
    {
        public static List<TblRoles> RoleList()
        {
            using(PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblRoles.OrderBy(x => x.RoleName).ToList();
            }
        }

        public static List<TblProduct> ProductList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblProduct.OrderBy(x => x.FilmType).ToList();
            }
        }

        public static List<TblCustomer> CustomerList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblCustomer.OrderBy(x => x.Name).ToList();
            }
        }

        public static List<TblConsignee> ConsigneeList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblConsignee.OrderBy(x => x.Name).ToList();
            }
        }

        public static List<TblJumboStatus> JumboStatusList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblJumboStatus.OrderBy(x => x.Name).ToList();
            }
        }

        public static List<TblSlittingStatus> SlittingStatusList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblSlittingStatus.OrderBy(x => x.Name).ToList();
            }
        }

        public static List<TblOrderStatus> OrderStatusList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblOrderStatus.OrderBy(x => x.Name).ToList();
            }
        }

        public static List<TblShiftType> ShiftTypeList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblShiftType.OrderBy(x => x.Name).ToList();
            }
        }

        public static List<TblOrder> OrderList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblOrder.OrderBy(x => x.OrderDate).ToList();
            }
        }

        public static List<TblSlitting> PrimarySlittingList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblSlitting.OrderBy(x => x.SlittingDate).ToList();
            }
        }

        public static List<TblJumbo> JumboList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblJumbo.OrderBy(x => x.JumboDate).ToList();
            }
        }

        public static List<TblTreatment> SlittingTreatmentList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblTreatment.OrderBy(x => x.Name).ToList();
            }
        }

        public static List<TblCore> CoreList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblCore.OrderBy(x => x.Name).ToList();
            }
        }

        public static List<TblUser> UserList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblUser.OrderBy(x => x.Name).ToList();
            }
        }

        public static List<TblAuditSettings> ScreenList()
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblAuditSettings.OrderBy(x => x.ScreenName).ToList();
            }
        }

        public static List<EntityAction> EntityActionList()
        {
            List<EntityAction> entityList = new List<EntityAction>
            {
                new EntityAction
                {
                    Name = "Modified"
                },
                new EntityAction
                {
                    Name = "Deleted"
                }
            };

            return entityList;
        }

        public static List<TblDispatch> DispatchListByInvoice(long? invoiceId)
        {
            using (PolyFilmsContext _dbContext = BaseContext.GetDbContext())
            {
                return _dbContext.TblDispatch.Include(m=>m.TblInvoiceDetail).Where(m=>m.TblInvoiceDetail.Any(y=>y.InvoiceId == invoiceId)).OrderBy(x => x.DispatchNo).ToList();
            }
        }

        
    }
}
