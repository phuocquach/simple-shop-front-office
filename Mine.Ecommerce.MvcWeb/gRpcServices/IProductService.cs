using Mine.Ecommerce.Web.Pages.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mine.Ecommerce.MvcWeb.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProduct();
        Task<IEnumerable<CategoryDto>> GetAllCategory();
        Task<IEnumerable<BrandDto>> GetAllBrand();
    }
}