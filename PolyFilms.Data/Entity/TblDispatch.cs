using System;
using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblDispatch
    {
        public TblDispatch()
        {
            TblDispatchDetail = new HashSet<TblDispatchDetail>();
            TblInvoiceDetail = new HashSet<TblInvoiceDetail>();
        }

        public long DispatchId { get; set; }
        public string DispatchNo { get; set; }
        public long? BuyerId { get; set; }
        public long ConsigneeId { get; set; }
        public DateTime DispatchDate { get; set; }
        public bool IsInvoiceGenerated { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int SequenceNo { get; set; }
        public bool IsFinalize { get; set; }
        public string ModeOfTransport { get; set; }
        public string NameOfTransporter { get; set; }
        public string VehicleNo { get; set; }
        public string Lrno { get; set; }
        public decimal AppGrossWeight { get; set; }
        public short TotalRoll { get; set; }
        public string Remarks { get; set; }

        public TblCustomer Buyer { get; set; }
        public TblOrder BuyerNavigation { get; set; }
        public TblConsignee Consignee { get; set; }
        public ICollection<TblDispatchDetail> TblDispatchDetail { get; set; }
        public ICollection<TblInvoiceDetail> TblInvoiceDetail { get; set; }
    }
}
