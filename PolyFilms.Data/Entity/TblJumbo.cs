using System;
using System.Collections.Generic;

namespace PolyFilms.Data.Entity
{
    public partial class TblJumbo
    {
        public TblJumbo()
        {
            TblSlittingJumbo = new HashSet<TblSlitting>();
            TblSlittingJumboJointId1Navigation = new HashSet<TblSlitting>();
            TblSlittingJumboJointId2Navigation = new HashSet<TblSlitting>();
            TblSlittingJumboJointId3Navigation = new HashSet<TblSlitting>();
        }

        public long JumboId { get; set; }
        public DateTime JumboDate { get; set; }
        public short ProductId { get; set; }
        public string JumboNo { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Weight { get; set; }
        public decimal Thickness { get; set; }
        public string LineNo { get; set; }
        public string CoreNo { get; set; }
        public short ShiftId { get; set; }
        public decimal? Speed { get; set; }
        public int ShiftInchargeId { get; set; }
        public short? StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public short SequenceNo { get; set; }
        public string Remarks { get; set; }
        public bool IsJumboUsed { get; set; }
        public decimal TotalSlitJumbo { get; set; }
        public decimal? RemainingJumbo { get; set; }
        public string JumboStatusRemarks { get; set; }
        public string Winder { get; set; }
        public short TreatmentId { get; set; }
        public decimal WasteWeight { get; set; }

        public TblProduct Product { get; set; }
        public TblShiftType Shift { get; set; }
        public TblUser ShiftIncharge { get; set; }
        public TblJumboStatus Status { get; set; }
        public TblTreatment Treatment { get; set; }
        public ICollection<TblSlitting> TblSlittingJumbo { get; set; }
        public ICollection<TblSlitting> TblSlittingJumboJointId1Navigation { get; set; }
        public ICollection<TblSlitting> TblSlittingJumboJointId2Navigation { get; set; }
        public ICollection<TblSlitting> TblSlittingJumboJointId3Navigation { get; set; }
    }
}
