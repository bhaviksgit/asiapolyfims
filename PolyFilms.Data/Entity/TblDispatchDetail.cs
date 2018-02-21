namespace PolyFilms.Data.Entity
{
    public partial class TblDispatchDetail
    {
        public long DispatchDetailId { get; set; }
        public long DispatchId { get; set; }
        public long SlittingId { get; set; }

        public TblDispatch Dispatch { get; set; }
        public TblSlitting Slitting { get; set; }
    }
}
