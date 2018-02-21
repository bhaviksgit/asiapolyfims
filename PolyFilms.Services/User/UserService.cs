using AutoMapper;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PolyFilms.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<TblUser> _repository;

        private IMapper _mapper { get; }

        public UserService(IRepository<TblUser> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private UserModel Map(TblUser unit)
        {
            return _mapper.Map<UserModel>(unit);
        }

        public IEnumerable<UserModel> GetAll()
        {
            var obj = _repository.GetAll().Where(m=>m.IsSuperAdmin == false).ToList();
            return obj.Select(Map);
        }

        public UserModel GetById(int id)
        {
            TblUser obj = _repository.GetById(id);
            return obj == null ? new UserModel() : Map(obj);
        }

        public int Insert(UserModel entity)
        {
            TblUser obj = _mapper.Map<UserModel, TblUser>(entity);
            obj.IsSuperAdmin = false;
            _repository.Insert(obj);
            return obj.UserId;
        }

        public int Update(UserModel entity)
        {
            TblUser obj = _mapper.Map<UserModel, TblUser>(entity);
            _repository.Update(obj);
            return obj.UserId;
        }

        public void Delete(int key)
        {
            _repository.Delete(key);
        }

        public bool ChangeStatus(int id)
        {
            TblUser obj = _repository.GetById(id);
            obj.IsActive = !obj.IsActive;
            _repository.Update(obj);
            return true;
        }

        

    }
}
