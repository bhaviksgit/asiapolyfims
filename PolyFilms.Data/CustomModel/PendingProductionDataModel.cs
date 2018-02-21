namespace PolyFilms.Data.CustomModel
{
    public class PendingProductionDataModel
    {
        //public short ProductId { get; set; }
        public string FilmType { get; set; }
        public decimal Thickness { get; set; }
        public decimal TotalQty { get; set; }
        public decimal RemainingJumbo { get; set; }
        public decimal TotalSlittStock { get; set; }
        public decimal OrderdQty { get; set; }
        public decimal FullFilledQty { get; set; }
        public decimal OrderedToFullFilled { get; set; }
        public decimal Pending { get; set; }
        
    }
}
