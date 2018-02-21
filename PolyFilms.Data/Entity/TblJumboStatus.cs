using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblJumboStatus
    {
        public TblJumboStatus()
        {
            TblJumbo = new HashSet<TblJumbo>();
        }

        public short StatusId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<TblJumbo> TblJumbo { get; set; }
    }
}
