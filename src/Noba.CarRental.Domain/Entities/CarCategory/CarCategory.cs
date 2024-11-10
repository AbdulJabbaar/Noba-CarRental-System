using Noba.CarRental.Domain.Enums;

namespace Noba.CarRental.Domain.Entities.CarCategory
{
    public class CarCategory : BaseEntity
    {
        public CarCategoryType CategoryType { get; private set; }
        public Pricing Pricing { get; private set; }

        private CarCategory() { }

        public static CarCategory Create(Guid Id, CarCategoryType categoryType, Pricing pricing)
        {
            if (pricing == null) throw new ArgumentNullException(nameof(pricing));

            return new CarCategory
            {
                Id = Id,
                CategoryType = categoryType,
                Pricing = pricing
            };
        }
    }
}
