using PolyFilms.Data.Models;
using System;
using System.Collections.Generic;

namespace PolyFilms.Services.Order
{
    public interface IOrderService
    {
        IEnumerable<OrderModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, long? buyerId = null, long? consigneeId = null, string orderNo = null, short? statusId = null);
        OrderModel GetById(long id);
        long Insert(OrderModel entity);
        long Update(OrderModel entity);
        void Delete(long key);
        
    }
}
