using PolyFilms.Data.CustomModel;
using PolyFilms.Data.Models;
using System;
using System.Collections.Generic;

namespace PolyFilms.Services.Slitting
{
    public interface ISlittingService
    {
        IEnumerable<SlittingModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, short? productId = null, string slittingRollno = null, string orderNo = null, short? statusId = null);
        SlittingModel GetById(long id);
        long Insert(SlittingModel model);
        long Update(SlittingModel model);
        void Delete(long id);
        string UpdateRate(IEnumerable<InvoiceSlittingList> slittingList);
        long UpdateOrder(long slittingId, long orderId);
        long RemoveOrder(long slittingId);
        IEnumerable<SlittingModel> getSlittingData(DateTime slittingDate, short shiftId, int operatorId, long? jumboId = null, long? primarySlittingId = null, int? setNo = null);
        long InsertOther(AdditionalSlittingDetailModel model);
        long UpdateOther(AdditionalSlittingDetailModel model);
        void LockAllSlitting(string slittingIds);
        bool ManageSlittingWateWeight(long slittingId, decimal wasteWeight);
    }
}
