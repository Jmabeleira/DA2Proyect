using Domain.Models;

namespace Domain.WebModels.In
{
    public class OrderIn
    {
        private Guid _id = Guid.Empty;
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

        public User User { get; set; }
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
        public AppliedPromotion? Promotion { get; set; }
        public double TotalPrice { get; set; }

        public Order ToEntity()
        {
            return new Order
            {
                Id = Id,
                Customer = User,
                Products = Products,
                AppliedPromotion = Promotion,
                TotalPrice = TotalPrice,
                Date = DateTime.Now
            };
        }
    }
}
