using System.ComponentModel.DataAnnotations;

namespace DataRepository.Entities;

public class Block
{
    
    public int Id { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    public string EventText { get; set; }
}
