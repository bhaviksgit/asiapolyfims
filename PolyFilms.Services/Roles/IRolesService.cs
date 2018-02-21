using PolyFilms.Data.Models;
using System.Collections.Generic;

namespace PolyFilms.Services.Roles
{
    public interface IRolesService
    {
        IEnumerable<RolesModel> GetAll();

        RolesModel GetById(int id);

        int Insert(RolesModel entity);

        int Update(RolesModel entity);

        void Delete(int key);

        bool ChangeStatus(int id);
    }
}
