using static DO_AN.Models.VnPayment;

namespace DO_AN.Repositories
{
    public interface IVnPayRepository
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequest model);
        VnPaymentResponse PaymentExecute(IQueryCollection collections);
    }
}
