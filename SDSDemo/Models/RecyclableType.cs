using System.Collections.Generic;

namespace SDSDemo.Models
{
    public class RecyclableType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Rate { get; set; }
        public decimal MinKg { get; set; }
        public decimal MaxKg { get; set; }
        public virtual ICollection<RecyclableItem> RecyclableItems { get; set; }

    }
}