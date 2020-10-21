using System.Collections.Generic;
using Mine.Ecommerce.Web.Pages.Models;

namespace Mine.Ecommerce.MvcWeb.ViewComponents.ProductList
{
    public class ProductListViewModel
    {
        public ICollection<ProductDto> ListProducts {get;set;}
        public int ProductPerRow { get; set; }
    }
}