using Domain.Exceptions;

namespace Domain.Models
{
    public class CartProduct
    {
        private Guid _id;
        private Product _product = null;
        private int quantity = 1;

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

        public Product Product
        {
            get { return _product; }
            set { _product = value != null ? value : throw new RequestValidationException("Product can't be null"); }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = (value > 0) ? value :
                    throw new RequestValidationException("Quantity must be greater than 0");
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is CartProduct cp)
            {
                return cp.Id == Id &&
                       cp.Product.Equals(Product) &&
                       cp.Quantity == Quantity;
            }
            else
            {
                return false;
            }
        }
    }
}
