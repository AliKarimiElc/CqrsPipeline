using FluentValidation;

namespace CqrsPipeline.Samples.BasicConsoleValidation;

public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Code).MinimumLength(3).WithMessage("Product code minimum length is 3");
    }
}