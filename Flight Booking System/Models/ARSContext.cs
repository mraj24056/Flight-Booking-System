using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Flight_Booking_System.Models
{
    public class ARSContext : DbContext
    {
        public ARSContext() :base("dbcon")
        {

        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Airline> Airlines { get; set;}
        public DbSet<UserLogin> UserLogins { get; set;}
        public DbSet<Flight> Flights { get; set;}   
        public DbSet<Airport> Airports { get; set;} 
        public DbSet<Schedule> Schedules { get; set;}

    }
}