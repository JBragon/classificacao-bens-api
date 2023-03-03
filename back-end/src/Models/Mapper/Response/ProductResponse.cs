namespace Models.Mapper.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EngelsCurveResponse> EngelsCurvesResponse { get; set; }
    }
}
