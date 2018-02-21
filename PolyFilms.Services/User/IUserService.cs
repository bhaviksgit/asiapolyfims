using PolyFilms.Data.Models;
using System.Collections.Generic;

namespace PolyFilms.Services.User
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAll();

        UserModel GetById(int id);

        int Insert(UserModel entity);

        int Update(UserModel entity);

        void Delete(int key);

        bool ChangeStatus(int id);

       
    }
}
