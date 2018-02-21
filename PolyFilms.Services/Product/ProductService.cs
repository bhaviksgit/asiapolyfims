using AutoMapper;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PolyFilms.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IRepository<TblProduct> _repository;

        private IMapper _mapper { get; }

        public ProductService(IRepository<TblProduct> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private ProductModel Map(TblProduct unit)
        {
            return _mapper.Map<ProductModel>(unit);
        }

        public IEnumerable<ProductModel> GetAll()
        {
            var obj = _repository.GetAll();
            return obj.Select(Map);
        }

        public ProductModel GetById(short id)
        {
            TblProduct obj = _repository.GetById(id);
            return obj == null ? new ProductModel() : Map(obj);
        }

        public short Insert(ProductModel entity)
        {
            TblProduct obj = _mapper.Map<ProductModel, TblProduct>(entity);
            _repository.Insert(obj);
            return obj.ProductId;
        }

        public short Update(ProductModel entity)
        {
            TblProduct obj = _mapper.Map<ProductModel, TblProduct>(entity);
            _repository.Update(obj);
            return obj.ProductId;
        }

        public void Delete(short key)
        {
            _repository.Delete(key);
        }

        public bool ChangeStatus(short id)
        {
            TblProduct obj = _repository.GetById(id);
            obj.IsActive = !obj.IsActive;
            _repository.Update(obj);
            return true;
        }
    }
}
