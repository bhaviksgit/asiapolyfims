using AutoMapper;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyFilms.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<TblCustomer> _repository;

        private IMapper _mapper { get; }

        public CustomerService(IRepository<TblCustomer> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private CustomerModel Map(TblCustomer unit)
        {
            return _mapper.Map<CustomerModel>(unit);
        }

        public IEnumerable<CustomerModel> GetAll()
        {
            var obj = _repository.GetAll();
            return obj.Select(Map);
        }

        public CustomerModel GetById(long id)
        {
            TblCustomer obj = _repository.GetById(id);
            return obj == null ? new CustomerModel() : Map(obj);
        }

        public long Insert(CustomerModel model)
        {
            TblCustomer obj = _mapper.Map<CustomerModel, TblCustomer>(model);
            _repository.Insert(obj);
            return obj.CustomerId;
        }

        public long Update(CustomerModel model)
        {
            TblCustomer obj = _mapper.Map<CustomerModel, TblCustomer>(model);
            _repository.Update(obj);
            return obj.CustomerId;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public bool ChangeStatus(long id)
        {
            TblCustomer obj = _repository.GetById(id);
            obj.IsActive = !obj.IsActive;
            obj.ModifiedDate = DateTime.Now;
            _repository.Update(obj);
            return true;
        }
    }
}
