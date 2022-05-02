using FluentValidation;
using Yahoo_Finance_Api.Handlers;

namespace Yahoo_Finance_Api.Validators;

public class StockSummaryRequestValidator : AbstractValidator<StockSummaryRequest>
{
    public StockSummaryRequestValidator()
    {
        RuleFor(x => x.Symbol).NotEmpty();
    }
}