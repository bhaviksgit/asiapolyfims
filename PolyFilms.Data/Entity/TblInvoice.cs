using System;
using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblInvoice
    {
        public TblInvoice()
        {
            TblInvoiceDetail = new HashSet<TblInvoiceDetail>();
        }

        public long InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public long CustomerId { get; set; }
        public long ConsigneeId { get; set; }
        public decimal? Sgst { get; set; }
        public decimal? Cgst { get; set; }
        public decimal? Igst { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmountWithTax { get; set; }
        public string AmountInWords { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int SequenceNo { get; set; }
        public bool IsFinalize { get; set; }
        public string Remarks { get; set; }
        public bool IsIGSTInvoice { get; set; }

        public TblCustomer Customer { get; set; }
        public TblConsignee Consignee { get; set; }
        public ICollection<TblInvoiceDetail> TblInvoiceDetail { get; set; }
    }
}
