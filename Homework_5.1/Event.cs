using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_5._1
{
    
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Guest> Guests { get; set; }

        public ICollection<EventGuestRelation> EventGuestRelations { get; set; }

        public Event()
        {
            Guests = new List<Guest>();
        }
    }
}
