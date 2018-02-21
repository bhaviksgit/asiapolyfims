using AutoMapper;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyFilms.Services.Dispatch
{
    public class DispatchService : IDispatchService
    {
        private readonly IRepository<TblDispatch> _repository;
        private readonly IRepository<TblDispatchDetail> _repositoryDetail;

        private IMapper _mapper { get; }

        public DispatchService(IRepository<TblDispatch> repository, IRepository<TblDispatchDetail> repositoryDetail, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _repositoryDetail = repositoryDetail;
        }

        private DispatchModel Map(TblDispatch unit)
        {
            return _mapper.Map<DispatchModel>(unit);
        }

        public IEnumerable<DispatchModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, long? buyerId = null, long? consigneeId = null, string dispatchNo = null)
        {
            //var obj = _repository.GetAll();
            var obj = _repository.GetAll()
                    .Where(x => (x.BuyerId == buyerId || buyerId == null)
                                    && (x.ConsigneeId == consigneeId || consigneeId == null)
                                    && (x.DispatchDate.Date >= fromDate || fromDate == null)
                                    && (x.DispatchDate.Date <= toDate || toDate == null)
                                    && ((x.DispatchNo.Contains(dispatchNo) || dispatchNo == null))
                                    );
            return obj.Select(Map);
        }

        public DispatchModel GetById(long id)
        {
            TblDispatch obj = _repository.GetById(id);
            return obj == null ? new DispatchModel() : Map(obj);
        }

        public long Insert(DispatchModel model)
        {
            TblDispatch obj = _mapper.Map<DispatchModel, TblDispatch>(model);
            obj.IsInvoiceGenerated = false;
            obj.IsFinalize = false;
            obj.Day = model.DispatchDate.Day;
            obj.Month = model.DispatchDate.Month;
            obj.Year = model.DispatchDate.Year;
            obj.DispatchNo = "";
            obj.SequenceNo = 0;

            List<long> slittingIdList = model.checkedSlittingId.Split(',').Select(long.Parse).ToList();

            foreach (long slittingId in slittingIdList)
            {
                obj.TblDispatchDetail.Add(new TblDispatchDetail
                {
                    SlittingId = slittingId
                });
            }

            _repository.Insert(obj);
            return obj.DispatchId;
        }

        public long Update(DispatchModel model)
        {
            TblDispatch obj = _mapper.Map<DispatchModel, TblDispatch>(model);
            List<TblDispatchDetail> dispatchDetailModel = _repositoryDetail.GetAll().Where(m => m.DispatchId == obj.DispatchId).ToList();

            List<long> originalSlittingId = dispatchDetailModel.Select(m => m.SlittingId).ToList();
            List<long> currentSlitingId = model.checkedSlittingId.Split(',').Select(long.Parse).ToList();
            List<long> deleteSlittingId = originalSlittingId.Except(currentSlitingId).ToList();
            List<long> insertSlittingId = currentSlitingId.Except(originalSlittingId).ToList();

            foreach (long deleteId in deleteSlittingId)
            {
                TblDispatchDetail objModel = dispatchDetailModel.FirstOrDefault(m => m.SlittingId == deleteId);
                if (objModel != null) _repositoryDetail.Delete(objModel.DispatchDetailId);
            }

            foreach (long insertingId in insertSlittingId)
            {
                TblDispatchDetail objInsertModel = new TblDispatchDetail
                {
                    DispatchId = obj.DispatchId,
                    SlittingId = insertingId
                };
                _repositoryDetail.Insert(objInsertModel);
            }
            _repository.Update(obj);
            return obj.DispatchId;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

    }
}
