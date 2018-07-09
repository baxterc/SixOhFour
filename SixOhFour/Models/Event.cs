using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SixOhFour.Models
{
    public class Event
    {
        private int EventId { get; set; }
        private string Name { get; set; }
        private DateTime CreatedDate { get; set; }
        private DateTime EventDate { get; set; }
        private string Description { get; set; }
    }
}
