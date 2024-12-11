using Domain.Exceptions;

namespace Domain.Models
{
    public class Product
    {
        private string _name;
        private double _price;
        private string _description;
        private string _brand;
        private IEnumerable<string> _colors;
        private string _category;
        private int _stock;
        private bool _isPromotional = true;

        private Guid _id;

        public Guid Id
        {
            get
            {
                return GetId();
            }
            set { _id = value; }
        }

        private Guid GetId()
        {
            if (_id == Guid.Empty)
            {
                _id = Guid.NewGuid();
            }
            return _id;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = string.IsNullOrWhiteSpace(value) ?
                    throw new RequestValidationException("The product's name cannot be empty")
                        : value;
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                _price = (value <= 0) ?
                    throw new RequestValidationException("The product's price must be greater or equal to 0")
                    : value;
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = string.IsNullOrWhiteSpace(value) ?
                    throw new RequestValidationException("The product's description cannot be empty")
                        : value;
            }
        }

        public string Brand
        {
            get { return _brand; }
            set
            {
                _brand = string.IsNullOrWhiteSpace(value) ?
                    throw new RequestValidationException("The product's brand cannot be empty")
                        : value;
            }
        }

        public IEnumerable<string> Colors
        {
            get { return _colors; }
            set
            {
                _colors = (value == null || !value.Any()) ?
                    throw new RequestValidationException("Colors cannot be null or empty")
                        : value;
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                _category = string.IsNullOrWhiteSpace(value) ?
                    throw new RequestValidationException("Category cannot be null or empty")
                        : value;
            }
        }

        public int Stock
        {
            get { return _stock; }
            set
            {
                _stock = value != null ? (value >= 0 ? value : throw new RequestValidationException("Stock cannot be lower than 0")) :
                        throw new RequestValidationException("Stock cannot be null");
            }
        }

        public bool IsPromotional
        {
            get { return _isPromotional; }
            set { _isPromotional = value; }
        }

        public override bool Equals(object? obj)
        {
            if (obj is Product p)
            {
                return p.Id == Id &&
                       p.Name == Name &&
                       p.Price == Price &&
                       p.Description == Description &&
                       p.Brand == Brand &&
                       p.Colors.SequenceEqual(Colors) &&
                       p.Category == Category &&
                       p.IsPromotional == IsPromotional;
            }
            return false;
        }
    }
}
