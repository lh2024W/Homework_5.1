using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_5._1
{
    public class EventGuestRelation
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int GuestId { get; set; }
        public Guest Guest { get; set; }
        public GuestRole Role { get; set; }

    }
}
