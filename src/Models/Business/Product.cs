using Models.Infrastructure;

namespace Models.Business
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Registration { get; set; }
        public string Observation { get; set; }


        public virtual ICollection<EngelsCurve> EngelsCurves { get; set; }
    }
}
