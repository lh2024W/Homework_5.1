using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_5._1
{
    public enum GuestRole { Attendee, Vip, Organizer, Speaker }
    public class ApplicationContext : DbContext
    {
        
        public DbSet<Guest> Guests { get; set; } = null;
        public DbSet<Event> Events { get; set; } = null;
        public DbSet<EventGuestRelation> EventGuestRElations { get; set; } = null;

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasMany(e => e.Guests).WithMany(e => e.Events).UsingEntity<EventGuestRelation>
                (
                g => g.HasOne(e => e.Guest).WithMany(e => e.EventGuestRelations).HasForeignKey(e => e.GuestId),
                g => g.HasOne(e => e.Event).WithMany(e => e.EventGuestRelations).HasForeignKey(e => e.EventId),
                egr =>
                {
                    egr.Property(e => e.Role).HasDefaultValue(GuestRole.Attendee);
                    egr.HasKey(key => new {key.EventId, key.GuestId});
                    egr.ToTable("EventGuestRelations");
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
