using Noba.CarRental.Domain.Enums;

namespace Noba.CarRental.Domain.Entities
{
    public class CarCategory : BaseEntity
    {
        public CarCategoryType CategoryType { get; private set; }
        public decimal BaseDayRental { get; private set; }
        public decimal BaseKmPrice { get; private set; }

        private CarCategory() { }

        public static CarCategory Create(Guid Id, CarCategoryType categoryType, decimal baseDayRental, decimal baseKmPrice)
        {
            if (baseDayRental <= 0) throw new ArgumentException("Base day rental must be greater than zero.");
            if (baseKmPrice < 0) throw new ArgumentException("Base km price cannot be negative.");

            return new CarCategory
            {
                Id = Id,
                CategoryType = categoryType,
                BaseDayRental = baseDayRental,
                BaseKmPrice = baseKmPrice
            };
        }
    }
}
