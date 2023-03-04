using FluentValidation;
using Models.Infrastructure;

namespace Models.Mapper.Request
{
    public class ProductPost
    {
        public string Name { get; set; }

        public List<EngelsCurvePost> EngelsCurvesPost { get; set; }
    }

    public class ProductPostValidation : AbstractValidator<ProductPost>
    {
        public ProductPostValidation()
        {
            RuleFor(v => v.Name)
              .NotEmpty()
              .WithMessage(RuleMessage.Informed("{PropertyName}"))
              .MaximumLength(50);

            RuleFor(v => v.EngelsCurvesPost)
              .NotEmpty()
              .WithMessage(RuleMessage.Informed("{PropertyName}"))
              .Must(list => list.Count >= 2)
              .WithMessage("Deve possuir no mínimo duas coordenadas!")
              .Custom((list, context) => {
                  if (list.GroupBy(p => new { p.Income, p.Amount }).Any(x => x.Count() > 1))
                  {
                      context.AddFailure("Não podem possuir coordenadas duplicadas!");
                  }

                  if (list.Any(e => e.Income < 0) || list.Any(e => e.Amount < 0))
                  {
                      context.AddFailure("Não podem possuir coordenadas negativas!");
                  }
              });

        }
    }
}
