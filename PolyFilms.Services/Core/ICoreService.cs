using PolyFilms.Data.Models;
using System.Collections.Generic;

namespace PolyFilms.Services.Core
{
    public interface ICoreService 
    {
        IEnumerable<CoreModel> GetAll();
        CoreModel GetById(int id);
        short Insert(CoreModel entity);
        short Update(CoreModel entity);
        void Delete(short key);
    }
}
