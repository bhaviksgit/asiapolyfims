using PolyFilms.Data.Models;
using System;
using System.Collections.Generic;

namespace PolyFilms.Services.Jumbo
{
    public interface IJumboService
    {
        IEnumerable<JumboModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, short? productId = null, string jumboNo = null,short? statusId = null);
        JumboModel GetById(long id);
        long Insert(JumboModel model);
        long Update(JumboModel model);
        void Delete(long id);
        bool ChnageJumboStatus(JumboStatusModel model);
        IEnumerable<JumboModel> GetJumboListById(long? jumboId);
        decimal ManageJumboWeight(long jumboId, decimal totalweight, decimal? wasteWeight = null);
        //bool ManageJumboWasteWeight(long jumboId, decimal wasteWeight);


    }
}
