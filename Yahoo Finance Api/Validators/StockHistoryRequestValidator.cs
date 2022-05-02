using FluentValidation;
using Yahoo_Finance_Api.Handlers;

namespace Yahoo_Finance_Api.Validators;

public class StockHistoryRequestValidator : AbstractValidator<StockHistoryRequest>
{
    public StockHistoryRequestValidator()
    {
        RuleFor(x => x.Symbol).NotEmpty();
    }
}
