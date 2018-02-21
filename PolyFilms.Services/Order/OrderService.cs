using AutoMapper;
using PolyFilms.Common;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PolyFilms.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<TblOrder> _repository;
        private readonly PolyFilmsContext _context;

        private IMapper _mapper { get; }

        public OrderService(IRepository<TblOrder> repository, IMapper mapper,PolyFilmsContext context)
        {
            _mapper = mapper;
            _repository = repository;
            _context = context;
        }

        private OrderModel Map(TblOrder unit)
        {
            return _mapper.Map<OrderModel>(unit);
        }

        public IEnumerable<OrderModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, long? buyerId = null, long? consigneeId = null, string orderNo = null, short? statusId = null)
        {
            var obj = _repository.GetAll()
                    .Where(x => (x.BuyerId == buyerId || buyerId == null)
                                && (x.ConsigneeId == consigneeId || consigneeId == null)
                                   && (x.OrderDate.Date >= fromDate || fromDate == null)
                                   && (x.OrderDate.Date <= toDate || toDate == null)
                                   && (x.OrderNo.Contains(orderNo) || string.IsNullOrEmpty(orderNo))
                                    &&(x.OrderStatusId == statusId || statusId == null)); 
            return obj.Select(Map);
        }

        public OrderModel GetById(long id)
        {
            TblOrder obj = _context.TblOrder.Include(m=>m.Consignee).FirstOrDefault(m=>m.OrderId == id);
            return obj == null ? new OrderModel() : Map(obj);
        }

        public long Insert(OrderModel entity)
        {
            TblOrder obj = _mapper.Map<OrderModel, TblOrder>(entity);
            obj.OrderNo = "";
            obj.Day = entity.OrderDate.Day;
            obj.Month = entity.OrderDate.Month;
            obj.Year = entity.OrderDate.Year;
            obj.SequenceNo = 0;
            obj.OrderStatusId = (short)Enums.OrderStatus.Pending;
            _repository.Insert(obj);
            return obj.OrderId;
        }

        public long Update(OrderModel entity)
        {
            TblOrder obj = _mapper.Map<OrderModel, TblOrder>(entity);
            _repository.Update(obj);
            return obj.OrderId;
        }

        public void Delete(long key)
        {
            _repository.Delete(key);
        }

        

    }
}
