using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Mine.Ecommerce.MvcWeb.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Mine.Ecommerce.Web.Pages.Models;
using System;

namespace Mine.Ecommerce.Web.Pages
{ 
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;
        public List<ProductDto> Products;
        public int ProductPerRow = 1;
        public int NumOfRow = 1;
        public IndexModel(ILogger<IndexModel> logger,
                        IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task OnGet()
        {
            try
            {
                Products = (await _productService.GetAllProduct()).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                Products = new List<ProductDto>
                {
                    new ProductDto
                    {
                        Name = "Temp",
                        Price = 0.000,
                        InStock = false,
                        Desciption = "https://via.placeholder.com/550x350/eeeeee/000000"
                    }
                };
            }
            
        }
    }
}
