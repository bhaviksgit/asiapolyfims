using PolyFilms.Data.Models;
using System;
using System.Collections.Generic;

namespace PolyFilms.Services.Dispatch
{
    public interface IDispatchService
    {
        IEnumerable<DispatchModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, long? buyerId = null, long? consigneeId = null, string dispatchNo = null);
        DispatchModel GetById(long id);
        long Insert(DispatchModel model);
        long Update(DispatchModel model);
        void Delete(long id);
    }
}
