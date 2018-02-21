using System;
using System.Collections.Generic;
using System.Text;

namespace PolyFilms.Data.Models
{
    public class ExtraSlittingModel
    {
        public string SlittingIds { get; set; }
        public long? JumboId { get; set; }
        public long? JumboJointId1 { get; set; }
        public long? JumboJointId2 { get; set; }
        public long? JumboJointId3 { get; set; }
        public decimal? WasteWt { get; set; }
        public decimal TotalWt { get; set; }
        //public long? PrimarySlittingId { get; set; }
    }
}
