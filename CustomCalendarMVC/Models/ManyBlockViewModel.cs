using DataRepository.Entities;

namespace CustomCalendarMVC.Models
{
    public class ManyBlockViewModel
    {
        public int CategoryId { get; set; }
        public Block Block { get; set; }
        public int Repetitions { get; set; }
        public string Span { get; set; }
    }

}
