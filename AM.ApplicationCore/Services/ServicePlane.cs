using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Services
{
    public class ServicePlane : Service<Plane>, IServicePlane
    {
        public ServicePlane(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public void DeletePlanes()
        {
            //var l= GetAll().Where(p => (DateTime.Now - p.manufactureDate).TotalDays > 3650);
            //foreach (var p in l)
            //{
            //    Delete(p);
            //}
            //Commit();
            Delete(p => (DateTime.Now - p.manufactureDate).TotalDays > 3650);
            Commit();
        }

        public IList<Flight> GetFlights(int n)
        {
            //  return GetAll().OrderBy(p=>p.planeId).TakeLast(n).SelectMany(p=>p.flights).OrderBy(p=>p.flightDate).ToList();
            return GetAll().OrderByDescending(p => p.planeId).Take(n).SelectMany(p => p.flights).OrderBy(p => p.flightDate).ToList();
        }

        public IList<Passenger> GetPassenger(Plane plane)
        {
           return plane.flights.SelectMany(p => p.tickets).Select(f => f.Passenger).Distinct().ToList();
        }

        public bool IsAvailablePlane(Flight f, int n)
        {
            //var capacity = f.plane.capacity;
            //var tickets = f.tickets.Count();
            //return capacity-tickets > n;
            //var plane = GetAll().Where(p=>p.flights.Contains(f)==true).FirstOrDefault();
            var plane = Get(p => p.flights.Contains(f) == true);
            var capacity = plane.capacity;
            var flight = plane.flights.Single(j=>j.flightId==f.flightId);
            var ticket = flight.tickets.Count();
            return capacity - ticket > n;
        }
    }
}
