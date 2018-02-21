using System;
using System.Collections.Generic;
using PolyFilms.Data.Entity;

namespace PolyFilms.Data.Models
{
    public class AuditModel
    {
        public long Id { get; set; }
        public string TableName { get; set; }
        public DateTime? DateTime { get; set; }
        public string Action { get; set; }
        public int? UserId { get; set; }
    }

    public class AuditDetailsModel
    {
        public DateTime? DateTime { get; set; }
        public string Action { get; set; }
        public string TableName { get; set; }
        public string Changes { get; set; }
    }

    public class AuditChange
    {
        public string DateTimeStamp { get; set; }
        public string AuditActionTypeName { get; set; }
        public List<AuditDelta> Changes { get; set; }
        public AuditChange()
        {
            Changes = new List<AuditDelta>();
        }
    }

    public class AuditDelta
    {
        public string FieldName { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
    }
}
