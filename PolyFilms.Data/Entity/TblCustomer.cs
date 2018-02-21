using System;
using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblCustomer
    {
        public TblCustomer()
        {
            TblConsignee = new HashSet<TblConsignee>();
            TblDispatch = new HashSet<TblDispatch>();
            TblInvoice = new HashSet<TblInvoice>();
            TblOrder = new HashSet<TblOrder>();
        }

        public long CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string DeliveryAddress { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string EmailAddress { get; set; }
        public string Gstnumber { get; set; }
        public string PanNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<TblConsignee> TblConsignee { get; set; }
        public ICollection<TblDispatch> TblDispatch { get; set; }
        public ICollection<TblInvoice> TblInvoice { get; set; }
        public ICollection<TblOrder> TblOrder { get; set; }
    }
}
