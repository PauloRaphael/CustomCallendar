namespace CustomCalendarMVC.Models
{
    public class BlockViewModel
    {
        public int CategoryId { get; set; }
        public IEnumerable<DataRepository.Entities.Block> Block { get; set; }
    }

}
