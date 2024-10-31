using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomCalendarMVC.Models
{
    public class BlockViewModel
    {
        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [DisplayName("From")]
        [DisplayFormat(DataFormatString = "{0:ddd dd MMM yyyy}")]
        public DateTime? From { get; set; }

        [DisplayName("To")]
        [DisplayFormat(DataFormatString = "{0:ddd dd MMM yyyy}")]
        public DateTime? To { get; set; }
        public IEnumerable<DataRepository.Entities.Block> Block { get; set; }
    }

}
