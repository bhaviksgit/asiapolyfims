using PolyFilms.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.Models
{
    public class InvoiceModel 
    {
        public long InvoiceId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "InvoiceDate")]
        [Required(ErrorMessageResourceName = "InvoiceDateRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public DateTime InvoiceDate { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "InvoiceNo")]
        public string InvoiceNo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "BuyerId")]
        [Required(ErrorMessageResourceName = "CustomerRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public long CustomerId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "ConsigneeId")]
        [Required(ErrorMessageResourceName = "ConsigneeIdRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public long ConsigneeId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Sgst")]
        //[Required(ErrorMessageResourceName = "SgstRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public Nullable<decimal> Sgst { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Cgst")]
        //[Required(ErrorMessageResourceName = "CgstRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public Nullable<decimal> Cgst { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Igst")]
        //[Required(ErrorMessageResourceName = "IgstRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public Nullable<decimal> Igst { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "TotalAmount")]
        public decimal TotalAmount { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "TaxAmount")]
        public decimal TaxAmount { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "TotalAmountWithTax")]
        public decimal TotalAmountWithTax { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "AmountInWords")]
        public string AmountInWords { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Remarks")]
        public string Remarks { get; set; }

        public bool IsFinalize { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "IsIGSTInvoice")]
        public bool IsIGSTInvoice { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int SequenceNo { get; set; }
        public string checkedDispatchId { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (IsIGSTInvoice && Igst == null )
        //    {
        //        var fieldName = new[] { "Igst" };
        //        yield return new ValidationResult("Igst is required.", fieldName);
        //    }

        //    if (!IsIGSTInvoice)
        //    {
        //        if (Sgst == null)
        //        {
        //            var fieldName = new[] { "Sgst" };
        //            yield return new ValidationResult("Sgst is required.", fieldName);
        //        }

        //        if (Cgst == null)
        //        {
        //            var fieldName = new[] { "Cgst" };
        //            yield return new ValidationResult("Cgst is required.", fieldName);
        //        }
                
        //    }
        //}
    }
}
