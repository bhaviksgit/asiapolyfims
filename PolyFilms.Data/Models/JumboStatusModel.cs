using System.ComponentModel.DataAnnotations;
using PolyFilms.Common;

namespace PolyFilms.Data.Models
{
    public class JumboStatusModel
    {
        public long JumboId { get; set; }
        public string JumboNo { get; set; }

        [Required(ErrorMessageResourceName = "StatusRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short StatusId { get; set; }

        //[Display(ResourceType = typeof(resLabels), Name = "JumboStatusRemarks")]
        public string JumboStatusRemarks { get; set; }
    }
}
