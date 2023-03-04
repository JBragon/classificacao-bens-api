using Models.Enums;
using Models.Infrastructure;

namespace Models.Business
{
    public class EngelsCurve : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public double Income { get; set; }
        public double Amount { get; set; }
        public double AngularCoefficient { get; set; }
        public ProductClassification Classification { get; set; }

        public virtual Product Product { get; set; }

    }
}
