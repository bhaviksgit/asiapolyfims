using AutoMapper;
using PolyFilms.Common;
using PolyFilms.Data.CustomModel;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyFilms.Services.Slitting
{
    public class SlittingService : ISlittingService
    {
        private readonly IRepository<TblSlitting> _repository;

        private IMapper _mapper { get; }

        public SlittingService(IRepository<TblSlitting> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private SlittingModel Map(TblSlitting unit)
        {
            return _mapper.Map<SlittingModel>(unit);
        }

        private AdditionalSlittingDetailModel MapOtherModel(TblSlitting unit)
        {
            return _mapper.Map<AdditionalSlittingDetailModel>(unit);
        }

        public IEnumerable<SlittingModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, short? productId = null, string slittingRollno = null, string orderNo = null, short? statusId = null)
        {
            //var obj = _repository.GetAll();
            var obj = _repository.GetAll()
                                 .Where(x => (x.ProductId == productId || productId == null)
                                    && (x.SlittingDate.Date >= fromDate || fromDate == null)
                                    && (x.SlittingDate.Date <= toDate || toDate == null)
                                    && ((x.SlittingRollNo.Contains(slittingRollno) || slittingRollno == null))
                                    && ((x.Order.OrderNo.Contains(orderNo) || orderNo == null))
                                    && (x.Quality == statusId || statusId == null)
                                    );
            return obj.Select(Map);
        }

        public SlittingModel GetById(long id)
        {
            TblSlitting obj = _repository.GetById(id);
            return obj == null ? new SlittingModel() : Map(obj);
        }

        public long Insert(SlittingModel model)
        {
            TblSlitting obj = _mapper.Map<SlittingModel, TblSlitting>(model);
            obj.Day = model.SlittingDate.Day;
            obj.Month = model.SlittingDate.Month;
            obj.Year = model.SlittingDate.Year;
            obj.SlittingRollNo = "";
            obj.SequenceNo = 0;
            _repository.Insert(obj);
            return obj.SlittingId;
        }

        public long Update(SlittingModel model)
        {
            TblSlitting obj = _mapper.Map<SlittingModel, TblSlitting>(model);
            _repository.Update(obj);
            return obj.SlittingId;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public string UpdateRate(IEnumerable<InvoiceSlittingList> slittingList)
        {
            try
            {
                using (PolyFilmsContext context = BaseContext.GetDbContext())
                {
                    foreach (InvoiceSlittingList rate in slittingList)
                    {
                        TblSlitting currentModel = context.Set<TblSlitting>().Find(rate.SlittingId);
                        currentModel.UnitPrice = rate.UnitPrice;
                        currentModel.TotalPrice = rate.UnitPrice * currentModel.Netweight;

                        _repository.Update(currentModel, context);
                    }


                    context.SaveChanges();
                }
                return string.Empty;
            }
            catch(Exception ex)
            {
                return CommonHelper.GetErrorMessage(ex);
            }
        }

        public long UpdateOrder(long slittingId , long orderId)
        {
            TblSlitting model = _repository.GetById(slittingId);
            model.OrderId = orderId;
            _repository.Update(model);
            return model.SlittingId;
        }

        public long RemoveOrder(long slittingId)
        {
            TblSlitting model = _repository.GetById(slittingId);
            model.OrderId = null;
            _repository.Update(model);
            return model.SlittingId;
        }

        public IEnumerable<SlittingModel> getSlittingData(DateTime slittingDate, short shiftId,int operatorId, long? jumboId = null, long? primarySlittingId = null, int? setNo = null)
        {
            var obj = _repository.GetAll()
                .Where(x => (x.SlittingDate.Date == slittingDate.Date) 
                            && (x.JumboId == jumboId || jumboId == null)
                            && (x.PrimarySlittingId == primarySlittingId || primarySlittingId == null)
                            && (x.ShiftId == shiftId)
                            && (x.OperatorId == operatorId)
                            &&(x.SetNo == setNo)
                            && (x.IsSlittingLocked == false)
                );
            return obj.Select(Map);
        }

        public long InsertOther(AdditionalSlittingDetailModel model)
        {
            TblSlitting obj = _mapper.Map<AdditionalSlittingDetailModel, TblSlitting>(model);
            obj.Day = model.SlittingDate.Day;
            obj.Month = model.SlittingDate.Month;
            obj.Year = model.SlittingDate.Year;
            obj.SlittingRollNo = "";
            obj.SequenceNo = 0;
            obj.TotalPrice = 0;
            obj.UnitPrice = 0;
            obj.IsSlittingUsed = false;
            obj.Od = 0;
            obj.SlitWasteWeight = 0;
            

            _repository.Insert(obj);
            return obj.SlittingId;
        }

        public long UpdateOther(AdditionalSlittingDetailModel model)
        {
            TblSlitting obj = _mapper.Map<AdditionalSlittingDetailModel, TblSlitting>(model);
            _repository.Update(obj);
            return obj.SlittingId;
        }

        public void LockAllSlitting(string slittingIds)
        {
            CustomRepository.LockSlittingData(slittingIds);

            //List<long> slittingIdList = slittingIds.Split(',').Select(long.Parse).ToList();
            //foreach (long id in slittingIdList)
            //{
            //    TblSlitting model = _repository.GetById(id);
            //    model.IsSlittingLocked = true;
            //    _repository.Update(model);
               
            //}
        }

        public bool ManageSlittingWateWeight(long slittingId, decimal wasteWeight)
        {
            TblSlitting Model = _repository.GetById(slittingId);
            Model.SlitWasteWeight = Model.SlitWasteWeight + wasteWeight;
            _repository.Update(Model);
            return true;
        }


    }
}
