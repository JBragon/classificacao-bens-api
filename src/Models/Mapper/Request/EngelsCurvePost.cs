using FluentValidation;
using Models.Infrastructure;

namespace Models.Mapper.Request
{
    public class EngelsCurvePost
    {
        public double Income { get; set; }
        public double Amount { get; set; }

    }

    public class EngelsCurvePostValidation : AbstractValidator<EngelsCurvePost>
    {
        public EngelsCurvePostValidation()
        {
            RuleFor(v => v.Income)
              .NotEmpty()
              .WithMessage(RuleMessage.Informed("{PropertyName}"))
              .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Amount)
              .NotEmpty()
              .WithMessage(RuleMessage.Informed("{PropertyName}"))
              .GreaterThanOrEqualTo(0);
        }
    }
}
