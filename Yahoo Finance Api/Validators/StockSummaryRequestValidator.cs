using FluentValidation;
using Yahoo_Finance_Api.Models.Requests;

namespace Yahoo_Finance_Api.Validators
{
    public class StockSummaryRequestValidator : AbstractValidator<StockSummaryRequest>
    {
        public StockSummaryRequestValidator()
        {
            RuleFor(x => x.Symbol).NotEmpty();
        }
    }
}