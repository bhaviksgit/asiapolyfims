using AutoMapper;
using PolyFilms.Common;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Models;
using PolyFilms.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyFilms.Services.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<TblInvoice> _repository;
        private readonly IRepository<TblInvoiceDetail> _repositoryDetail;

        private IMapper _mapper { get; }

        public InvoiceService(IRepository<TblInvoice> repository, IRepository<TblInvoiceDetail> repositoryDetail, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _repositoryDetail = repositoryDetail;
        }

        private InvoiceModel Map(TblInvoice unit)
        {
            return _mapper.Map<InvoiceModel>(unit);
        }




        public IEnumerable<InvoiceModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, long? customerId = null, long? consigneeId = null, string invoiceNo = null)
        {
            var obj = _repository.GetAll()
                .Where(x => (x.CustomerId == customerId || customerId == null)
                                    &&(x.ConsigneeId == consigneeId || consigneeId == null)
                                    && (x.InvoiceDate.Date >= fromDate || fromDate == null)
                                    && (x.InvoiceDate.Date <= toDate || toDate == null)
                                    && (x.InvoiceNo.Contains(invoiceNo) || invoiceNo == null)
                                    );
            return obj.Select(Map);
        }

        public InvoiceModel GetById(long id)
        {
            TblInvoice obj = _repository.GetById(id);
            return obj == null ? new InvoiceModel() : Map(obj);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public long Insert(InvoiceModel model)
        {

            TblInvoice obj = _mapper.Map<InvoiceModel, TblInvoice>(model);
            obj.Day = model.InvoiceDate.Day;
            obj.Month = model.InvoiceDate.Month;
            obj.Year = model.InvoiceDate.Year;
            obj.InvoiceNo = "";
            obj.SequenceNo = 0;
            obj.TotalAmount = CustomRepository.GetTotalAmountForInvoice(model.checkedDispatchId).TotalAmount;

            decimal SgstAmount = Math.Round(obj.TotalAmount * (model.Sgst ?? 0) / 100, 2);
            decimal IgstAmount = Math.Round(obj.TotalAmount * (model.Igst ?? 0) / 100, 2);
            decimal CgstAmount = Math.Round(obj.TotalAmount * (model.Cgst ?? 0)  / 100, 2);

            obj.TaxAmount = SgstAmount + IgstAmount + CgstAmount;
            obj.TotalAmountWithTax = obj.TotalAmount + obj.TaxAmount;
            obj.AmountInWords = CurrencyHelper.changeCurrencyToWords(obj.TotalAmountWithTax);

            List<long> dispatchIdList = model.checkedDispatchId.Split(',').Select(long.Parse).ToList();
            foreach (long dispatchId in dispatchIdList)
            {
                obj.TblInvoiceDetail.Add(new TblInvoiceDetail
                {
                    DispatchId = dispatchId
                });
            }

            _repository.Insert(obj);
            return obj.InvoiceId;
        }

        public long Update(InvoiceModel model)
        {
            TblInvoice obj = _mapper.Map<InvoiceModel, TblInvoice>(model);
            obj.TotalAmount = CustomRepository.GetTotalAmountForInvoice(model.checkedDispatchId).TotalAmount;

            decimal SgstAmount = Math.Round(obj.TotalAmount * (model.Sgst ?? 0) / 100, 2);
            decimal IgstAmount = Math.Round(obj.TotalAmount * (model.Igst ?? 0) / 100, 2);
            decimal CgstAmount = Math.Round(obj.TotalAmount * (model.Cgst ?? 0) / 100, 2);

            obj.TaxAmount = SgstAmount + IgstAmount + CgstAmount;
            obj.TotalAmountWithTax = obj.TotalAmount + obj.TaxAmount;
            obj.AmountInWords = CurrencyHelper.changeCurrencyToWords(obj.TotalAmountWithTax);
            List<TblInvoiceDetail> invoiceDetailModel = _repositoryDetail.GetAll().Where(m => m.InvoiceId == obj.InvoiceId).ToList();

            List<long> originalDispatchId = invoiceDetailModel.Select(m => m.DispatchId).ToList();
            List<long> currentDispatchId = model.checkedDispatchId.Split(',').Select(long.Parse).ToList();
            List<long> deleteDispatchId = originalDispatchId.Except(currentDispatchId).ToList();
            List<long> insertDispatchId = currentDispatchId.Except(originalDispatchId).ToList();

            foreach (long deleteId in deleteDispatchId)
            {
                TblInvoiceDetail objModel = invoiceDetailModel.FirstOrDefault(m => m.DispatchId == deleteId);
                if (objModel != null) _repositoryDetail.Delete(objModel.InvoiceDetailId);
            }

            foreach (long insertingId in insertDispatchId)
            {
                TblInvoiceDetail objInsertModel = new TblInvoiceDetail
                {
                    InvoiceId = obj.InvoiceId,
                    DispatchId = insertingId
                };
                _repositoryDetail.Insert(objInsertModel);
            }
            _repository.Update(obj);
            return obj.InvoiceId;
        }





    }
}
