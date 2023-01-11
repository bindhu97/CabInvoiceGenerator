using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        RideType rideType;
        private RideRepository rideRepository;
        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PER_TIME;
        private readonly double MINIMUM_FARE;
        //UC1
        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepository();
            try
            {
                if (rideType.Equals(RideType.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUM_FARE = 20;
                }
                else if (rideType.Equals(RideType.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUM_FARE = 5;
                }
            }
            catch (CabInvoicException)
            {
                throw new CabInvoicException(CabInvoicException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
            }
        }
        /// Default Constructor.
        public InvoiceGenerator()
        {
        }
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            try
            {
                //Calculsting Total Fare.
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            }
            catch (CabInvoicException)
            {
                if (rideType.Equals(null))
                {
                    throw new CabInvoicException(CabInvoicException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
                }
                if (distance <= 0)
                {
                    throw new CabInvoicException(CabInvoicException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                }
                if (time < 0)
                {
                    throw new CabInvoicException(CabInvoicException.ExceptionType.INVALID_TIME, "Invalid Time");
                }
            }
            return Math.Max(totalFare, MINIMUM_FARE);
        }
        //UC2
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch (CabInvoicException)
            {
                if (rides == null)
                {
                    throw new CabInvoicException(CabInvoicException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }
        public void AddRides(string userId, Ride[] rides)
        {
            try
            {
                rideRepository.AddRide(userId, rides);
            }
            catch (CabInvoicException)
            {
                if (rides == null)
                {
                    throw new CabInvoicException(CabInvoicException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
        }
        public InvoiceSummary GetInvoiceSummary(string userId)
        {
            try
            {
                return this.CalculateFare(rideRepository.GetRides(userId));
            }
            catch (CabInvoicException)
            {
                throw new CabInvoicException(CabInvoicException.ExceptionType.INVALID_USER_ID, "Invalid UserID");
            }
        }
    }
}
