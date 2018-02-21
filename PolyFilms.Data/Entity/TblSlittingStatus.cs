using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblSlittingStatus
    {
        public TblSlittingStatus()
        {
            TblSlitting = new HashSet<TblSlitting>();
        }

        public short SlittingStatusId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<TblSlitting> TblSlitting { get; set; }
    }
}
