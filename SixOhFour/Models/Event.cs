using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SixOhFour.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
    }
}
