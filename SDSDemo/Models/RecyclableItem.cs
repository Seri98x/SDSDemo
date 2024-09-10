namespace SDSDemo.Models
{
    public class RecyclableItem
    {
        public int Id { get; set; }
        public int RecyclableTypeId { get; set; }
        public decimal Weight { get; set; }
        public decimal ComputedRate { get; set; }
        public string ItemDescription { get; set; }
        public virtual RecyclableType RecyclableType { get; set; }
    }
}