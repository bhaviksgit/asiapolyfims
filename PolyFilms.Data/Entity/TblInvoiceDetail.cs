namespace PolyFilms.Data.Entity
{
    public partial class TblInvoiceDetail
    {
        public long InvoiceDetailId { get; set; }
        public long InvoiceId { get; set; }
        public long DispatchId { get; set; }

        public TblDispatch Dispatch { get; set; }
        public TblInvoice Invoice { get; set; }
    }
}
