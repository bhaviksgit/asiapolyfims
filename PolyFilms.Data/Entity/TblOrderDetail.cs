namespace PolyFilms.Data.Entity
{
    public partial class TblOrderDetail
    {
        public int OrderDetailId { get; set; }
        public long OrderId { get; set; }
        public short ProductId { get; set; }
        public decimal Thickness { get; set; }
        public decimal Width { get; set; }
        public decimal Od { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalSlittingDone { get; set; }
        public decimal? RemainingSlitting { get; set; }

        public TblOrder Order { get; set; }
        public TblProduct Product { get; set; }
    }
}
