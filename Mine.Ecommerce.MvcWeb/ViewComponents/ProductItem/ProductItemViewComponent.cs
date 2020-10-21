using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mine.Ecommerce.Web.Pages.Models;

namespace Mine.Ecommerce.MvcWeb.ViewComponents.ProductItem
{
    public class ProductItemViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ProductDto item)
        {
            return View(item);
        }
    }
}