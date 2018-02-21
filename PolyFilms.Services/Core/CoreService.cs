using AutoMapper;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PolyFilms.Services.Core
{
    public class CoreService : ICoreService
    {
        private readonly IRepository<TblCore> _repository;

        private IMapper _mapper { get; }

        public CoreService(IRepository<TblCore> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private CoreModel Map(TblCore unit)
        {
            return _mapper.Map<CoreModel>(unit);
        }

        public IEnumerable<CoreModel> GetAll()
        {
            var obj = _repository.GetAll();
            return obj.Select(Map);
        }

        public CoreModel GetById(int id)
        {
            TblCore obj = _repository.GetById(id);
            return obj == null ? new CoreModel() : Map(obj);
        }

        public short Insert(CoreModel entity)
        {
            TblCore obj = _mapper.Map<CoreModel, TblCore>(entity);
            _repository.Insert(obj);
            return obj.CoreId;
        }

        public short Update(CoreModel entity)
        {
            TblCore obj = _mapper.Map<CoreModel, TblCore>(entity);
            _repository.Update(obj);
            return obj.CoreId;
        }

        public void Delete(short key)
        {
            _repository.Delete(key);
        }
    }
}
