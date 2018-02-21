using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblConsignee
    {
        public TblConsignee()
        {
            TblDispatch = new HashSet<TblDispatch>();
            TblOrder = new HashSet<TblOrder>();
        }

        public long ConsigneeId { get; set; }
        public long BuyerId { get; set; }
        public string Name { get; set; }
        public string DeliveryAddress { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Gstnumber { get; set; }
        public string PanNumber { get; set; }
        public bool IsActive { get; set; }

        public TblCustomer Buyer { get; set; }
        public ICollection<TblDispatch> TblDispatch { get; set; }
        public ICollection<TblOrder> TblOrder { get; set; }
        public ICollection<TblInvoice> TblInvoice { get; set; }
    }
}
