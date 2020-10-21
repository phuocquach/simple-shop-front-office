using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine.Commerce.V1;
using Mine.Ecommerce.Web.Pages.Models;
using ProductClient = Mine.Commerce.V1.Products.ProductsClient ;
namespace Mine.Ecommerce.MvcWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductClient _productClient;
        public ProductService(ProductClient productClient)
        {
            _productClient = productClient;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrand()
        {
            var response = await _productClient.GetAllBrandsAsync(new Google.Protobuf.WellKnownTypes.Empty());
            return response.ResponseData.ListBrand
            .Select(x => new BrandDto
            {
                Id = new Guid(x.Id),
                Name = x.Name
            });
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            var response = await _productClient.GetAllCategoriesAsync(new Google.Protobuf.WellKnownTypes.Empty());
            return response.ResponseData.ListCategory
            .Select(x => new CategoryDto
            {
                Id = new Guid(x.Id),
                Name = x.Name
            });
        }

        public async Task<IEnumerable<ProductDto>> GetAllProduct()
        {
            
            var response = await _productClient.GetProductListAsync(new GetProductListRequest());
            return response.ResponseData.ListProduct.Select(x => new ProductDto 
            {
                Name = x.Name,
                Price = x.Price,
                InStock = x.InStock,
                Image = x.Description
            });
        }
    }
}