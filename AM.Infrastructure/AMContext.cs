﻿using AM.ApplicationCore.Domain;
using AM.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infrastructure
{
    public class AMContext : DbContext
    {
        public DbSet<Flight> Flights{ get; set; }
        public DbSet<Passenger> Passengers{ get; set; }
        public DbSet<Plane> Planes{ get; set; }
        public DbSet<Staff> Staffs{ get; set; }
        public DbSet<Traveller> Travellers{ get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(); 
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MsSqlLocaldb;initial catalog=AyoubBelhadjyahia;Integrated Security = true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FlightConfiguration());  
            modelBuilder.ApplyConfiguration(new PassengerConfiguration());
            modelBuilder.Entity<Passenger>().ToTable("Pasger");
            modelBuilder.Entity<Traveller>().ToTable("Travller");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new SeatConfiguation());


        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<String>().HaveMaxLength(120);
            configurationBuilder.Properties<DateTime>().HaveColumnType("Date");
            configurationBuilder.Properties<Double>().HavePrecision(2,3);

        }
        
    }
}