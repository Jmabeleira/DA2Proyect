using Domain.Exceptions;

namespace Domain.Models
{
    public class Address
    {
        private string _country;
        private string _city;
        private string _street;
        private string _zipCode;
        private int _doorNumber;

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Country
        {
            get { return _country; }
            set
            {
                _country = (string.IsNullOrWhiteSpace(value)) ?
                    throw new RequestValidationException("Country cannot be null or empty")
                        : value.Trim();
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                _city = string.IsNullOrWhiteSpace(value) ?
                    throw new RequestValidationException("City cannot be null or empty") :
                        value.Trim();
            }
        }

        public string Street
        {
            get { return _street; }
            set
            {
                _street = string.IsNullOrWhiteSpace(value) ?
                    throw new RequestValidationException("Street cannot be null or empty")
                        : value.Trim();
            }
        }

        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                _zipCode = string.IsNullOrWhiteSpace(value) ?
                    throw new RequestValidationException("ZipCode cannot be null or empty")
                        : value.Trim();
            }
        }

        public int DoorNumber
        {
            get { return _doorNumber; }
            set
            {
                _doorNumber = value <= 0 ?
                    throw new RequestValidationException("DoorNumber must be a positive integer")
                        : value;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is Address a)
            {
                return a.City == City &&
                       a.Country == Country &&
                       a.DoorNumber == DoorNumber &&
                       a.Street == Street &&
                       a.ZipCode == ZipCode;
            }
            return false;
        }
    }
}
