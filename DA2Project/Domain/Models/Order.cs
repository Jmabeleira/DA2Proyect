using Domain.Exceptions;
using Domain.Models.PaymentMethods;

namespace Domain.Models
{
    public class Order
    {
        private User _customer;
        private IEnumerable<CartProduct> _products;
        private DateTime _date;
        private Guid _id;
        private PaymentMethod _paymentMethod;

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

        public User Customer
        {
            get { return _customer; }
            set
            {
                _customer = value == null ?
                    throw new RequestValidationException("The customer cannot be null")
                    : value;
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value == DateTime.MinValue ?
                    throw new RequestValidationException("Invalid datetime")
                    : value;
            }
        }

        public IEnumerable<CartProduct> Products
        {
            get { return _products; }
            set
            {
                if (value != null)
                {
                    if (value.Count() != 0)
                    {
                        _products = value;
                    }
                    else
                    {
                        throw new RequestValidationException("The products list cannot be empty");
                    }
                }
                else
                {
                    throw new RequestValidationException("The products list cannot be null");
                }
            }
        }

        public AppliedPromotion AppliedPromotion { get; set; }

        public PaymentMethod PaymentMethod
        {
            get { return _paymentMethod; }
            set { _paymentMethod = value ?? throw new RequestValidationException("Payment Method can not be null"); }
        }

        public double TotalPrice { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Order o)
            {
                return o.Id == Id &&
                       o.Customer.Equals(Customer) &&
                       o.Date.Equals(Date) &&
                       o.Products.SequenceEqual(Products) &&
                       (o.AppliedPromotion == null || o.AppliedPromotion.Equals(AppliedPromotion)) &&
                       o.PaymentMethod.Equals(PaymentMethod);
            }

            return false;
        }
    }
}
