using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Business.Interface;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Models.Business;
using Models.Enums;
using Models.Filters;
using Models.Infrastructure;
using Models.Mapper.Request;
using Models.Mapper.Response;

namespace Business.Services
{
    public class ProductService : BaseService<Product, int>, IProductService
    {

        private IEngelsCurveService _engelsCurveService;

        public ProductService(IUnitOfWork<DBContext> unitOfWork, IMapper mapper, IEngelsCurveService engelsCurveService) : base(unitOfWork, mapper)
        {
            _engelsCurveService = engelsCurveService;
        }

        public SearchResponse<TOutputModel> Search<TOutputModel>(ProductFilter filter)
        {
            var response = base.Search<TOutputModel>(
               filter.GetFilter(),
               include: source => source.Include(i => i.EngelsCurves),
               orderBy: null,
               filter.Page,
               filter.RowsPerPage
            );

            return response;
        }

        public override TOutputModel GetById<TOutputModel>(int Id)
        {
            return base.GetById<TOutputModel>(Id,
                include: source => source.Include(i => i.EngelsCurves));
        }

        public ProductResponse InsertAndClassificateProduct(ProductPost inputProduct)
        {
            var insertedProduct = base.Create<ProductResponse>(inputProduct);

            EngelsCurveResponse lastCurve = null;

            inputProduct.EngelsCurvesPost.ForEach(i =>
            {
                double angularCoefficient = 0;
                if (lastCurve != null)
                {
                    double deltaAmount = i.Amount - lastCurve.Amount;
                    double deltaIncome = i.Income - lastCurve.Income;
                    angularCoefficient = deltaAmount > 0 ? deltaIncome / deltaAmount : 0;
                }

                EngelsCurve engelsCurve = new EngelsCurve
                {
                    ProductId = insertedProduct.Id,
                    Income = i.Income,
                    Amount = i.Amount,
                    AngularCoefficient = angularCoefficient,
                    Classification = angularCoefficient >= 0 ? ProductClassification.StandardOrSuperior : ProductClassification.Inferior
                };

                lastCurve = _engelsCurveService.Create<EngelsCurveResponse>(engelsCurve);

                insertedProduct.EngelsCurvesResponse.Add(lastCurve);

            });

            return insertedProduct;
        }
    }
}
