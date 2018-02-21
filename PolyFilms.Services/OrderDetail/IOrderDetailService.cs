using PolyFilms.Data.Models;
using System.Collections.Generic;

namespace PolyFilms.Services.OrderDetail
{
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetailModel> GetAll(long orderId);
        OrderDetailModel GetById(int id);
        int Insert(OrderDetailModel entity);
        int Update(OrderDetailModel entity);
        void Delete(int key);
        IEnumerable<OrderDetailModel> GetAllByProduct(long orderId, long productId);
    }
}
