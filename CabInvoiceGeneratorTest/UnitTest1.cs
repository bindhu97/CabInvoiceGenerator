using CabInvoiceGenerator;
using NUnit.Framework;
using System.IO;

namespace CabInvoiceGeneratorTest
{
    public class Tests
    {
        InvoiceGenerator invoiceGenerator = null;
        [Test]
        public void GiveDistanceAndTimeSouldReturnTotalFare()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double distance = 2.0;
            int time = 5;

            double fare = invoiceGenerator.CalculateFare(distance, time);
            double excepted = 25;
            Assert.AreEqual(excepted, fare);
        }
        public void GivenUserIdWith5Rides_ShouldReturnTotalFare()
        {
            Ride[] rides =
            {
                new Ride(1.0, 1),
                new Ride(2.0, 2),
                new Ride(2.0, 2),
                new Ride(4.0, 4),
                new Ride(3.0, 3)
            };
            double expected = 132;
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            Assert.AreEqual(expected, summary.totalFare);
        }
    }
}