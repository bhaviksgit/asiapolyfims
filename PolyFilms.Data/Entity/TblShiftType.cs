using System;
using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblShiftType
    {
        public TblShiftType()
        {
            TblJumbo = new HashSet<TblJumbo>();
            TblSlitting = new HashSet<TblSlitting>();
        }

        public short ShiftId { get; set; }
        public string Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsActive { get; set; }

        public ICollection<TblJumbo> TblJumbo { get; set; }
        public ICollection<TblSlitting> TblSlitting { get; set; }
    }
}
