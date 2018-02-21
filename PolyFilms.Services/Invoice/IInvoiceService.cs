using PolyFilms.Data.Models;
using System;
using System.Collections.Generic;

namespace PolyFilms.Services.Invoice
{
    public interface IInvoiceService
    {
        IEnumerable<InvoiceModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, long? customerId = null, long? consigneeId = null, string invoiceNo = null);
        InvoiceModel GetById(long id);
        long Insert(InvoiceModel model);
        void Delete(long id);
        long Update(InvoiceModel model);
    }
}
