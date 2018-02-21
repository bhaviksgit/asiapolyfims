using PolyFilms.Data.Models;
using System.Collections.Generic;

namespace PolyFilms.Services.Product
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAll();
        ProductModel GetById(short id);
        short Insert(ProductModel entity);
        short Update(ProductModel entity);
        void Delete(short key);
        bool ChangeStatus(short id);
    }
}
