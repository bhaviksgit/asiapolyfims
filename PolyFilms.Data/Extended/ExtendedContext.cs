﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PolyFilms.Common;
using PolyFilms.Data.CustomModel;

namespace PolyFilms.Data.Entity
{
    public partial class PolyFilmsContext
    {
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (IsAuditDisabled(entry)) continue;

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.Relational().TableName
                };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        //case EntityState.Added:
                        //    auditEntry.NewValues[propertyName] = property.CurrentValue;
                        //    break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.Action = "Deleted";
                            auditEntry.UserId = CommonHelper.UserId;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.Action = "Modified";
                                auditEntry.OldValues[propertyName] = entry.GetDatabaseValues().GetValue<object>(propertyName);
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.UserId = CommonHelper.UserId;
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                TblAudit.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private bool IsAuditDisabled(EntityEntry entry)
        {
            if (entry.Entity is TblAudit)
            {
                return true;
            }

            if (SettingConfig.AuditSettings.Any(x => !x.IsEnabled && x.TableName == entry.Metadata.Relational().TableName))
            {
                return true;
            }

            switch (entry.State)
            {
                case EntityState.Detached:
                    return true;
                case EntityState.Unchanged:
                    return true;
                case EntityState.Added:
                    return true;
            }
            return false;
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                TblAudit.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }
    }
}
