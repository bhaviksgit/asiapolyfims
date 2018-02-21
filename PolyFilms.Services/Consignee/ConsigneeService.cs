using AutoMapper;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PolyFilms.Services.Consignee
{
    public class ConsigneeService : IConsigneeService
    {
        private readonly IRepository<TblConsignee> _repository;

        private IMapper _mapper { get; }

        public ConsigneeService(IRepository<TblConsignee> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private ConsigneeModel Map(TblConsignee unit)
        {
            return _mapper.Map<ConsigneeModel>(unit);
        }

        public IEnumerable<ConsigneeModel> GetAll(long? buyerId = null)
        {
            var obj = _repository.GetAll().Where(m=>m.BuyerId == buyerId);
            return obj.AsEnumerable().Select(Map);
        }

        public ConsigneeModel GetById(long id)
        {
            TblConsignee obj = _repository.GetById(id);
            return obj == null ? new ConsigneeModel() : Map(obj);
        }

        public long Insert(ConsigneeModel model)
        {
            TblConsignee obj = _mapper.Map<ConsigneeModel, TblConsignee>(model);
            _repository.Insert(obj);
            return obj.ConsigneeId;
        }

        public long Update(ConsigneeModel model)
        {
            TblConsignee obj = _mapper.Map<ConsigneeModel, TblConsignee>(model);
            _repository.Update(obj);
            return obj.ConsigneeId;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public bool ChangeStatus(long id)
        {
            TblConsignee obj = _repository.GetById(id);
            obj.IsActive = !obj.IsActive;
            _repository.Update(obj);
            return true;
        }
    }
}
