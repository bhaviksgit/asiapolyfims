using AutoMapper;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PolyFilms.Services.OrderDetail
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IRepository<TblOrderDetail> _repository;

        private IMapper _mapper { get; }

        public OrderDetailService(IRepository<TblOrderDetail> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private OrderDetailModel Map(TblOrderDetail unit)
        {
            return _mapper.Map<OrderDetailModel>(unit);
        }

        public IEnumerable<OrderDetailModel> GetAll(long orderId)
        {
            var obj = _repository.GetAll().Where(m=>m.OrderId == orderId);
            return obj.Select(Map);
        }

        public IEnumerable<OrderDetailModel> GetAllByProduct(long orderId , long productId)
        {
            var obj = _repository.GetAll().Where(m => m.OrderId == orderId && m.ProductId == productId);
            return obj.Select(Map);
        }

        public OrderDetailModel GetById(int id)
        {
            TblOrderDetail obj = _repository.GetById(id);
            return obj == null ? new OrderDetailModel() : Map(obj);
        }

        public int Insert(OrderDetailModel entity)
        {
            TblOrderDetail obj = _mapper.Map<OrderDetailModel, TblOrderDetail>(entity);
            _repository.Insert(obj);
            return obj.OrderDetailId;
        }

        public int Update(OrderDetailModel entity)
        {
            TblOrderDetail obj = _mapper.Map<OrderDetailModel, TblOrderDetail>(entity);
            _repository.Update(obj);
            return obj.OrderDetailId;
        }

        public void Delete(int key)
        {
            _repository.Delete(key);
        }
    }
}
