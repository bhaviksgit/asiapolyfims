using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblOrderStatus
    {
        public TblOrderStatus()
        {
            TblOrder = new HashSet<TblOrder>();
        }

        public short OrderStatusId { get; set; }
        public string Name { get; set; }

        public ICollection<TblOrder> TblOrder { get; set; }
    }
}
