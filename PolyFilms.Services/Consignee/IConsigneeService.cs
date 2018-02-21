using PolyFilms.Data.Models;
using System.Collections.Generic;

namespace PolyFilms.Services.Consignee
{
    public interface IConsigneeService
    {
        IEnumerable<ConsigneeModel> GetAll(long? buyerId = null);
        ConsigneeModel GetById(long id);
        long Insert(ConsigneeModel model);
        long Update(ConsigneeModel model);
        void Delete(long id);
        bool ChangeStatus(long id);
    }
}
