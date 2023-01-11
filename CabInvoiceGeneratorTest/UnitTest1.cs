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
    }
}