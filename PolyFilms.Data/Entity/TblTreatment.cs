using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblTreatment
    {
        public TblTreatment()
        {
            TblSlitting = new HashSet<TblSlitting>();
        }

        public short TreatmentId { get; set; }
        public string Name { get; set; }

        public ICollection<TblJumbo> TblJumbo { get; set; }
        public ICollection<TblSlitting> TblSlitting { get; set; }
    }
}
