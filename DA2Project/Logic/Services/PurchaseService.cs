using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.PaymentMethods;
using Logic.Exceptions;

namespace Logic.Services
{
    public class PurchaseService : IPurchaseService
    {
        private IProductService _productService;

        public PurchaseService(IProductService productService)
        {
            _productService = productService;
        }

        public Order Purchase(Order order, string paymentMethod)
        {
            try
            {
                var paymentMethodInstance = PaymentHelper.GetPaymentMethodInstance(paymentMethod);
                order.PaymentMethod = paymentMethodInstance;
                order.TotalPrice = CalculateTotalPrice(order.TotalPrice, paymentMethodInstance);

                UpdateProductsStock(order);

                order.Id = Guid.NewGuid();

                return order;
            }
            catch (OutOfStockException ex)
            {
                throw new NoStockException("There are not enough products in stock", ex);
            }
            catch (PaymentMethodNotFoundException ex)
            {
                throw new PaymentMethodException("Payment method was rejected", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        private void UpdateProductsStock(Order order)
        {
            if (order.Products.Any(p => p.Product.Stock < p.Quantity))
            {
                var productsWithNoStock = order.Products.Where(p => p.Product.Stock < p.Quantity).Select(p => p.Product.Name).ToList();

                string productWithNoStockNames = string.Join(", ", productsWithNoStock);

                throw new OutOfStockException($"The following product's stock have been modified {productWithNoStockNames}");
            }

            foreach (var item in order.Products)
            {
                item.Product.Stock = item.Product.Stock - item.Quantity;
                _productService.UpdateProduct(item.Product);
            }
        }

        private double CalculateTotalPrice(double totalPrice, PaymentMethod paymentMethod)
        {
            IPaymentDiscountVisitor paymentDiscountVisitor = new PaymentDiscountVisitor();
            paymentMethod.Accept(paymentDiscountVisitor);

            double paymentMethodDiscount = paymentDiscountVisitor.GetDiscount();

            double finalPrice = totalPrice - (totalPrice * paymentMethodDiscount);

            return finalPrice;
        }
    }
}
