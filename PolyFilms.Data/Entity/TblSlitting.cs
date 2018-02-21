using System;
using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblSlitting
    {
        public TblSlitting()
        {
            InversePrimarySlitting = new HashSet<TblSlitting>();
            TblDispatchDetail = new HashSet<TblDispatchDetail>();
        }

        public long SlittingId { get; set; }
        public DateTime SlittingDate { get; set; }
        public string SlittingRollNo { get; set; }
        public short ShiftId { get; set; }
        public long? OrderId { get; set; }
        public long? JumboId { get; set; }
        public long? PrimarySlittingId { get; set; }
        public short ProductId { get; set; }
        public string Location { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Od { get; set; }
        public decimal Thickness { get; set; }
        public decimal Grossweight { get; set; }
        public decimal Netweight { get; set; }
        public short CoreId { get; set; }
        public decimal CoreWeight { get; set; }
        public short Quality { get; set; }
        public int? Joint { get; set; }
        public long? JumboJointId1 { get; set; }
        public long? JumboJointId2 { get; set; }
        public long? JumboJointId3 { get; set; }
        
        public string Remarks { get; set; }
        public decimal? Difference { get; set; }
        public short TreatmentId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int SequenceNo { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool IsSlittingUsed { get; set; }
        public int? OperatorId { get; set; }
        public bool IsSlittingLocked { get; set; }
        public int? SetNo { get; set; }
        
        public long? SlittingJointId1 { get; set; }
        public long? SlittingJointId2 { get; set; }
        public long? SlittingJointId3 { get; set; }
        public decimal? SlitWasteWeight { get; set; }

        public TblCore Core { get; set; }
        public TblJumbo Jumbo { get; set; }
        public TblJumbo JumboJointId1Navigation { get; set; }
        public TblJumbo JumboJointId2Navigation { get; set; }
        public TblJumbo JumboJointId3Navigation { get; set; }
        public TblUser Operator { get; set; }
        public TblOrder Order { get; set; }
        public TblSlitting PrimarySlitting { get; set; }
        public TblProduct Product { get; set; }
        public TblSlittingStatus QualityNavigation { get; set; }
        public TblShiftType Shift { get; set; }
        public TblTreatment Treatment { get; set; }
        public ICollection<TblSlitting> InversePrimarySlitting { get; set; }
        public ICollection<TblDispatchDetail> TblDispatchDetail { get; set; }
    }
}
