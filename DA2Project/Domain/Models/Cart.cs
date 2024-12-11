using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Cart
    {
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

        public User User { get; set; }
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
        public Promotion Promotion { get; set; }

        public void AddProduct(Product product)
        {
            try
            {
                var cartProduct = GetProduct(product.Id);
                if (cartProduct != null)
                {
                    Products = Products.Select(x =>
                    {
                        if (x.Product.Id == product.Id)
                        {
                            x.Quantity++;
                        }
                        return x;
                    }).ToList();
                }
            }
            catch (Exception)
            {
                Products.Add(new CartProduct { Product = product, Quantity = 1 });
            }
        }

        public bool RemoveProduct(Product product)
        {
            CartProduct cartProduct = Products.FirstOrDefault(p => p.Product.Id == product.Id);
            if (cartProduct != null)
            {
                return Products.Remove(cartProduct);
            }
            throw new ProductNotFoundException("Product not found");
        }

        public void RemoveAllProducts()
        {
            Products.Clear();
        }

        public void AddProducts(List<Product> products)
        {
            products.ForEach(product => AddProduct(product));
        }

        public CartProduct GetProduct(Guid Id)
        {
            try
            {
                var product = Products.FirstOrDefault(p => p.Product.Id == Id);
                if (product != null)
                {
                    return product;
                }
                throw new ProductNotFoundException("Product not found");
            }
            catch (Exception)
            {
                throw new ProductNotFoundException("Product not found");
            }
        }
    }
}

