using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblJumbo = new HashSet<TblJumbo>();
            TblOrderDetail = new HashSet<TblOrderDetail>();
            TblSlitting = new HashSet<TblSlitting>();
        }

        public short ProductId { get; set; }
        public string FilmType { get; set; }
        public string MainFeatures { get; set; }
        public decimal Thickness { get; set; }
        public string PreTreatment { get; set; }
        public string MainApplication { get; set; }
        public bool IsActive { get; set; }

        public ICollection<TblJumbo> TblJumbo { get; set; }
        public ICollection<TblOrderDetail> TblOrderDetail { get; set; }
        public ICollection<TblSlitting> TblSlitting { get; set; }
    }
}
