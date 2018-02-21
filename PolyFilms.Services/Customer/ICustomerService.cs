using PolyFilms.Data.Models;
using System.Collections.Generic;

namespace PolyFilms.Services.Customer
{
    public interface ICustomerService
    {
        IEnumerable<CustomerModel> GetAll();
        CustomerModel GetById(long id);
        long Insert(CustomerModel model);
        long Update(CustomerModel model);
        void Delete(long id);
        bool ChangeStatus(long id);
    }
}
