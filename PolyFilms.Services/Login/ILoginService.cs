using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;

namespace PolyFilms.Services.Login
{
    public interface ILoginService 
    {
        TblUser ValidateUser(string username, string password);
        TblUser GetByEmail(string email);
        int Update(UserModel entity);
        TblUser GetById(int id);
        UserModel GetByToken(string token);
    }
}
