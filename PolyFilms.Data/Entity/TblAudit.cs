using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using PolyFilms.Data.Models;

namespace PolyFilms.Data.Entity
{
    public partial class TblAudit
    {
        public long Id { get; set; }
        public string TableName { get; set; }
        public DateTime? DateTime { get; set; }
        public string Action { get; set; }
        public int? UserId { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string Changes { get; set; }
    }

    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
        public string Action { get; set; }
        public bool HasTemporaryProperties => TemporaryProperties.Any();
        public int? UserId { get; set; }

        public TblAudit ToAudit()
        {
            var audit = new TblAudit
            {
                TableName = TableName,
                DateTime = DateTime.UtcNow,
                Action = Action,
                UserId = UserId,
                KeyValues = KeyValues.First().Value.ToString(),
                OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),

            };


            List<AuditDelta> DeltaList = new List<AuditDelta>();

            JObject sourceJObject = JsonConvert.DeserializeObject<JObject>(audit.OldValues);

            if (audit.Action == "Modified")
            {
                JObject targetJObject = JsonConvert.DeserializeObject<JObject>(audit.NewValues);

                foreach (KeyValuePair<string, JToken> sourceProperty in sourceJObject)
                {
                    JProperty targetProp = targetJObject.Property(sourceProperty.Key);

                    if (!JToken.DeepEquals(sourceProperty.Value, targetProp.Value))
                    {
                        AuditDelta delta = new AuditDelta
                        {
                            FieldName = sourceProperty.Key,
                            ValueBefore = Convert.ToString(sourceProperty.Value),
                            ValueAfter = Convert.ToString(targetProp.Value)
                        };
                        DeltaList.Add(delta);
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<string, JToken> sourceProperty in sourceJObject)
                {
                    AuditDelta delta = new AuditDelta
                    {
                        FieldName = sourceProperty.Key,
                        ValueBefore = Convert.ToString(sourceProperty.Value),
                        ValueAfter = string.Empty
                    };
                    DeltaList.Add(delta);
                }
            }



            audit.Changes = JsonConvert.SerializeObject(DeltaList);

            return audit;
        }
    }

    
}
