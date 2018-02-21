using AutoMapper;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PolyFilms.Services.Roles
{
    public class RolesService : IRolesService
    {
        private readonly IRepository<TblRoles> _repository;

        private IMapper _mapper { get; }

        public RolesService(IRepository<TblRoles> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private RolesModel Map(TblRoles unit)
        {
            return _mapper.Map<RolesModel>(unit);
        }

        public IEnumerable<RolesModel> GetAll()
        {
            var obj = _repository.GetAll();
            return obj.Select(Map);
        }

        public RolesModel GetById(int id)
        {
            TblRoles obj = _repository.GetById(id);
            return obj == null ? new RolesModel() : Map(obj);
        }

        public int Insert(RolesModel entity)
        {
            TblRoles obj = _mapper.Map<RolesModel, TblRoles>(entity);
            _repository.Insert(obj);
            return obj.RoleId;
        }

        public int Update(RolesModel entity)
        {
            TblRoles obj = _mapper.Map<RolesModel, TblRoles>(entity);
            _repository.Update(obj);
            return obj.RoleId;
        }

        public void Delete(int key)
        {
            _repository.Delete(key);
        }

        public bool ChangeStatus(int id)
        {
            TblRoles obj = _repository.GetById(id);
            obj.IsActive = !obj.IsActive;
            _repository.Update(obj);
            return true;
        }

    }
}
