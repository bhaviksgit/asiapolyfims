using AutoMapper;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyFilms.Services.Jumbo
{
    public class JumboService : IJumboService
    {
        private readonly IRepository<TblJumbo> _repository;

        private IMapper _mapper { get; }

        public JumboService(IRepository<TblJumbo> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private JumboModel Map(TblJumbo unit)
        {
            return _mapper.Map<JumboModel>(unit);
        }

        public IEnumerable<JumboModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, short? productId = null, string jumboNo = null, short? statusId = null)
        {
            var obj = _repository.GetAll()
                            .Where(x => (x.ProductId == productId || productId == null)
                                    && (x.JumboDate.Date >= fromDate || fromDate == null)
                                    && (x.JumboDate.Date <= toDate || toDate == null)
                                    && ((x.JumboNo.Contains(jumboNo) || jumboNo == null))
                                    && (x.StatusId == statusId || statusId == null)
                                    );
            //var obj = _repository.GetAll();
            return obj.Select(Map);
        }

        public JumboModel GetById(long id)
        {
            TblJumbo obj = _repository.GetById(id);
            return obj == null ? new JumboModel() : Map(obj);
        }

        public long Insert(JumboModel model)
        {
            TblJumbo obj = _mapper.Map<JumboModel, TblJumbo>(model);
            obj.CreatedDate = DateTime.Now;
            obj.Day = model.JumboDate.Day;
            obj.Month = model.JumboDate.Month;
            obj.Year = model.JumboDate.Year;
            obj.JumboNo = "";
            obj.SequenceNo = 0;
            _repository.Insert(obj);
            return obj.JumboId;
        }

        public long Update(JumboModel model)
        {
            TblJumbo obj = _mapper.Map<JumboModel, TblJumbo>(model);
            _repository.Update(obj);
            return obj.JumboId;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
        
        public bool ChnageJumboStatus(JumboStatusModel model)
        {
            TblJumbo jumboModel = _repository.GetById(model.JumboId);
            jumboModel.StatusId = model.StatusId;
            jumboModel.JumboStatusRemarks = model.JumboStatusRemarks;
            _repository.Update(jumboModel);
            return true;
        }

        public IEnumerable<JumboModel> GetJumboListById(long? jumboId)
        {
            var obj = _repository.GetAll()
                .Where(x => x.JumboId == jumboId || jumboId == null);
            return obj.Select(Map);
        }

        public decimal ManageJumboWeight(long jumboId, decimal totalSlitweight,decimal? wasteWeight = null)
        {
            TblJumbo jumboModel = _repository.GetById(jumboId);
            jumboModel.IsJumboUsed = true;

            decimal remainingWt = 0;
            decimal wasteW = 0;

            if (wasteWeight.HasValue)
            {
                wasteW = wasteWeight.Value;
            }

            if (jumboModel.RemainingJumbo <= totalSlitweight)
            {
                jumboModel.TotalSlitJumbo = jumboModel.TotalSlitJumbo + jumboModel.RemainingJumbo.Value - wasteW;
                jumboModel.WasteWeight = jumboModel.WasteWeight + wasteW;
                remainingWt = totalSlitweight - jumboModel.RemainingJumbo.Value + wasteW;
            }

            if (jumboModel.RemainingJumbo >= totalSlitweight)
            {
                jumboModel.TotalSlitJumbo = jumboModel.TotalSlitJumbo + totalSlitweight;
                jumboModel.WasteWeight = jumboModel.WasteWeight + wasteW;
            }


            _repository.Update(jumboModel);

            return remainingWt;
        }

        //public bool ManageJumboWasteWeight(long jumboId, decimal wasteWeight)
        //{
        //    TblJumbo jumboModel = _repository.GetById(jumboId);
        //    jumboModel.WasteWeight = jumboModel.WasteWeight + wasteWeight;
            
        //    _repository.Update(jumboModel);
        //    return true;
        //}

    }
}
