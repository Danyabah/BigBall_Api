using BigBall.API.Models.CreateRequest;

namespace BigBall.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания способа оплаты
    /// </summary>
    public class PaymentRequest : CreatePaymentRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        public object TestValidate(PaymentRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
