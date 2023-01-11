using System;

namespace CabInvoiceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Cab Invoice Generator!\n");
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double fare = invoiceGenerator.CalculateFare(2.0, 5);
            Console.WriteLine("Normal Ride Fare: " + fare);

            InvoiceGenerator invoiceGenerator1 = new InvoiceGenerator(RideType.PREMIUM);
            double premiumfare = invoiceGenerator1.CalculateFare(2.5, 5);
            Console.WriteLine("Premium Ride Fare :" + premiumfare);
        }
    }
}