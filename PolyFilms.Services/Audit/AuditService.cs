using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;

namespace PolyFilms.Services.Audit
{
    public class AuditService : IAuditService
    {
        private readonly IRepository<TblAudit> _repository;

        private IMapper _mapper { get; }

        public AuditService(IRepository<TblAudit> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private AuditModel Map(TblAudit unit)
        {
            return _mapper.Map<AuditModel>(unit);
        }

        private AuditDetailsModel MapDetails(TblAudit unit)
        {
            return _mapper.Map<AuditDetailsModel>(unit);
        }

        public IEnumerable<AuditModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, int? userId = null, string screenId = null, string actionId = null)
        {
            var obj = _repository.GetAll()
                .Where(x => (x.UserId == userId || userId == null)
                            && (x.TableName.Contains(screenId) || screenId == null)
                            && (x.DateTime.Value.Date >= fromDate || fromDate == null)
                            && (x.DateTime.Value.Date <= toDate || toDate == null)
                            && ((x.Action.Contains(actionId) || actionId == null))
                );

         
            return obj.AsEnumerable().Select(Map);
        }

        public AuditDetailsModel GetById(long id)
        {
            TblAudit obj = _repository.GetById(id);
            return obj == null ? new AuditDetailsModel() : MapDetails(obj);
        }
    }
}
