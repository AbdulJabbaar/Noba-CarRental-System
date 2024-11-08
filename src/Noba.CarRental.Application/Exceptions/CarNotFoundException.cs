namespace Noba.CarRental.Application.Exceptions
{
    public class CarNotFoundException : Exception
    {
        public CarNotFoundException(string message) : base(message) { }
    }
}
