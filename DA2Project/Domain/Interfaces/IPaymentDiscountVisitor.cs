namespace Domain.Interfaces
{
    public interface IPaymentDiscountVisitor : IPaymentMethodVisitor
    {
        double GetDiscount();
    }
}
