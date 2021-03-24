using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator:AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.DailyPrice).GreaterThan(0);
            RuleFor(c => c.BrandId).NotNull();
            RuleFor(c => c.ColorId).NotNull();
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.ModelYear).GreaterThan(1900);
        }
    }
}