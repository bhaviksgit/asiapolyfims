using System;
using System.Collections.Generic;
using PolyFilms.Data.Models;

namespace PolyFilms.Services.Audit
{
    public interface IAuditService
    {
        IEnumerable<AuditModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, int? userId = null, string screenId = null, string actionId = null);

        AuditDetailsModel GetById(long id);
    }
}
