using System;
using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblOrder
    {
        public TblOrder()
        {
            TblDispatch = new HashSet<TblDispatch>();
            TblOrderDetail = new HashSet<TblOrderDetail>();
            TblSlitting = new HashSet<TblSlitting>();
        }

        public long OrderId { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public long BuyerId { get; set; }
        public long ConsigneeId { get; set; }
        public string Specialinstruction { get; set; }
        public DateTime DeliverySchedule { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int SequenceNo { get; set; }
        public short OrderStatusId { get; set; }

        public TblCustomer Buyer { get; set; }
        public TblConsignee Consignee { get; set; }
        public TblOrderStatus OrderStatus { get; set; }
        public ICollection<TblDispatch> TblDispatch { get; set; }
        public ICollection<TblOrderDetail> TblOrderDetail { get; set; }
        public ICollection<TblSlitting> TblSlitting { get; set; }
    }
}
