namespace Noba.CarRental.Domain.Entities
{
    public class Car : BaseEntity
    {
        public string RegistrationNumber { get; private set; }
        public Guid CarCategoryId { get; private set; }
        public CarCategory CarCategory { get; private set; }
        public decimal CurrentKm { get; private set; }


        private Car() { }

        public static Car Create(Guid Id, string registrationNumber, CarCategory carCategory, decimal currentKm)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber)) throw new ArgumentException("Registration number cannot be empty.");
            if (currentKm < 0) throw new ArgumentException("Current kilometers cannot be negative.");

            return new Car
            {
                Id = Id,
                RegistrationNumber = registrationNumber,
                CarCategoryId = carCategory.Id,
                CarCategory = carCategory,
                CurrentKm = currentKm
            };
        }

        public void UpdateKm(decimal newKm)
        {
            if (newKm < CurrentKm) throw new ArgumentException("New kilometers must be greater than current kilometers.");
            CurrentKm = newKm;
        }
    }
}
