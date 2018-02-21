using PolyFilms.Data.Entity;
using PolyFilms.Data.Repositories;
using System.Linq;
using AutoMapper;
using PolyFilms.Data.Models;

namespace PolyFilms.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<TblUser> _repository;

        private IMapper _mapper { get; set; }

        public LoginService(IRepository<TblUser> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private UserModel Map(TblUser unit)
        {
            return _mapper.Map<UserModel>(unit);
        }

        public TblUser ValidateUser(string username, string password)
        {
            TblUser objUsers = _repository.GetAll().FirstOrDefault(x => x.Username == username && x.Password == password && x.IsActive);
            return objUsers;
        }

        public TblUser GetByEmail(string email)
        {
            TblUser obj = _repository.GetAll().FirstOrDefault(m => m.EmailAddress == email);
            return obj == null ? new TblUser() : obj;
        }

        public UserModel GetByToken(string token)
        {
            TblUser obj = _repository.GetAll().FirstOrDefault(m => m.Token == token);
            return obj == null ? new UserModel() : Map(obj);
        }

        public int Update(UserModel entity)
        {
            TblUser obj = _mapper.Map<UserModel, TblUser>(entity);
            _repository.Update(obj);
            return obj.UserId;
        }

        public TblUser GetById(int id)
        {
            TblUser obj = _repository.GetById(id);
            return obj == null ? new TblUser() : obj;
        }



    }
}
