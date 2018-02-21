using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblCore
    {
        public TblCore()
        {
            TblSlitting = new HashSet<TblSlitting>();
        }

        public short CoreId { get; set; }
        public string Name { get; set; }
        public decimal Thickness { get; set; }
        public decimal Weight { get; set; }

        public ICollection<TblSlitting> TblSlitting { get; set; }
    }
}
