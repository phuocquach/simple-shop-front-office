using Microsoft.AspNetCore.Mvc;
using Mine.Ecommerce.MvcWeb.Services;
using Mine.Ecommerce.Web.Pages.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mine.Ecommerce.MvcWeb.ViewComponents.ProductList
{
    public class ProductListViewComponent : ViewComponent
    {
        public ProductListViewModel model {get;set;}
        public ProductListViewComponent()
        {
            model = new ProductListViewModel();
        }
        public async Task<IViewComponentResult> InvokeAsync(ICollection<ProductDto> listProducts,
                                                            int numOfProductOnRow)
        {
            model.ListProducts = listProducts;
            model.ProductPerRow = numOfProductOnRow;
            return View(model);
        }
    }
}
