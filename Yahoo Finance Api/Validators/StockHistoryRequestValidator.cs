using FluentValidation;
using Yahoo_Finance_Api.Models.Requests;

namespace Yahoo_Finance_Api.Validators
{
    public class StockHistoryRequestValidator : AbstractValidator<StockHistoryRequest>
    {
        public StockHistoryRequestValidator()
        {
            RuleFor(x => x.Symbol).NotEmpty();
        }
    }
}