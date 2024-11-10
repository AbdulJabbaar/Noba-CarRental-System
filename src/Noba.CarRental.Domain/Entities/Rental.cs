using Noba.CarRental.Domain.Pricing;

namespace Noba.CarRental.Domain.Entities
{
    public class Rental : BaseEntity
    {
        public string BookingNumber { get; private set; }
        public string CustomerSSN { get; private set; }
        public DateTime PickUpDate { get; private set; }
        public decimal PickUpKm { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public decimal? ReturnKm { get; private set; }
        public Guid CarId { get; private set; }
        public Car Car { get; private set; }

        private Rental() { }

        public static Rental Create(Guid Id, string bookingNumber, string customerSSN, DateTime pickUpDate, decimal pickUpKm, Car car)
        {
            if (string.IsNullOrWhiteSpace(bookingNumber)) throw new ArgumentException("Booking number cannot be empty.");
            if (string.IsNullOrWhiteSpace(customerSSN)) throw new ArgumentException("Customer SSN cannot be empty.");
            if (pickUpKm < 0) throw new ArgumentException("Pick-up kilometers cannot be negative.");

            return new Rental
            {
                Id = Id,
                BookingNumber = bookingNumber,
                CustomerSSN = customerSSN,
                PickUpDate = pickUpDate,
                PickUpKm = pickUpKm,
                CarId = car.Id,
                Car = car
            };
        }

        public void ReturnCar(DateTime returnDate, decimal returnKm)
        {
            if (returnKm < PickUpKm) throw new ArgumentException("Return kilometers cannot be less than pick-up kilometers.");
            ReturnDate = returnDate;
            ReturnKm = returnKm;
        }

        public decimal CalculateRent()
        {
            if (ReturnDate == null || ReturnKm == null || ReturnKm < 0)
            {
                return 0;
            }

            var pricingStrategyFactory = new PricingStrategyFactory();
            var pricingStrategy = pricingStrategyFactory.GetStrategy(Car.CarCategory.CategoryType);

            return pricingStrategy.CalculatePrice(Car.CarCategory.Pricing.BaseDayRental, Car.CarCategory.Pricing.BaseKmPrice, NumberOfDays(), NumberOfKms());
        }

        private int NumberOfDays()
        {
            var totalDays = (ReturnDate!.Value - PickUpDate).TotalDays;
            return (int)Math.Ceiling(totalDays);
        }

        private decimal NumberOfKms()
        {
            return ReturnKm!.Value - PickUpKm;
        }
    }
}

// Each class has a static factory method (Create) to ensure that objects are only created with valid values,
// encapsulating validation and improving readabiligy

// Properties as private set to ensure they cannot be modified from outside the class
// enforcing immutability after object creating. This is important part of the DDD to ensure the integrity of domain objects

// validation logic is placed in the factory method to ensure that all invariats are respected