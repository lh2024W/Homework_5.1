using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_5._1
{
    public class DatabaseService
    {        

        DbContextOptions<ApplicationContext> options;
        public void EnsurePopulated()
        {

            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var events = new List<Event>
                {
                    new Event{Name = "Party"},
                    new Event{Name = "Conference"}
                };

                var guests = new List<Guest>
                {
                    new Guest{Name = "John Doe"},
                    new Guest{Name = "Mark Spin"},
                    new Guest{Name = "Tom Perl"}
                };

                var eventGuestRelations = new List<EventGuestRelation>
                {
                    new EventGuestRelation { Event = events[0], Guest = guests[0], Role = GuestRole.Attendee},
                    new EventGuestRelation { Event = events[0], Guest = guests[1], Role = GuestRole.Organizer},
                    new EventGuestRelation { Event = events[1], Guest = guests[1], Role = GuestRole.Speaker},
                    new EventGuestRelation { Event = events[0], Guest = guests[2], Role = GuestRole.Vip},
                };
                db.Events.AddRange(events);
                db.Guests.AddRange(guests);
                db.EventGuestRElations.AddRange(eventGuestRelations);
                db.SaveChanges();
            }

        }
        public void EditEvent(int idGuest, int idEvent)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var currentGuest = db.Guests.FirstOrDefault(e => e.Id == idGuest);
                if (currentGuest != null)
                {
                    var currentEvent = db.Events.FirstOrDefault(e => e.Id == idEvent);
                    if (currentEvent != null)
                    {
                        currentEvent.Guests.Add(currentGuest);
                        db.SaveChanges();
                    }
                }
            }
        }

        public void GetGuestsFromEvent(int id) 
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var currentEvent = db.Events.Include(e => e.Guests).FirstOrDefault(e => e.Id == id);
            }
        }

        public void EditGuestRoleToVip(int idGuest, int idEvent)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var eventGuestRelationToUpdate = db.EventGuestRElations.
                    FirstOrDefault(e => e.EventId == idEvent && e.GuestId == idGuest);
                if (eventGuestRelationToUpdate != null)
                {
                    eventGuestRelationToUpdate.Role = GuestRole.Vip;
                    db.SaveChanges();
                }
            }
        }

        public void GetEventsFromGuest(int id)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var currentEvent = db.Guests.Include(e => e.Events).FirstOrDefault(e => e.Id == id);
            }
        }

        public void RemoveGuest(int idGuest, int idEvent)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var eventGuestRelationToRemove = db.EventGuestRElations.
                    FirstOrDefault(e => e.EventId == idEvent && e.GuestId == idGuest);
                if (eventGuestRelationToRemove != null)
                {
                    db.EventGuestRElations.Remove(eventGuestRelationToRemove);
                    db.SaveChanges();
                }
            }
        }

        public void PrintEventsWhereGuestWasSpeaker(int id)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var eventsWhereGuestWasSpeaker = db.EventGuestRElations.
                    Where(e => e.GuestId == id && e.Role == GuestRole.Speaker).
                    Select(e => e.Event).ToList();
            }
        }
    }
}
