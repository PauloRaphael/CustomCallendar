using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataRepository.Entities;

public class Block
{

    public int Id { get; set; }

    [MaxLength(50)]
    [Required(ErrorMessage = "{0} Required")]
    [DisplayName("Event")]
    public string Title { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    [DisplayFormat(DataFormatString = "{0:ddd dd MMM yyyy HH:mm}")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    [MaxLength(100)]
    [DisplayName("Event Description")]
    public string EventText { get; set; }
}
