using Microsoft.AspNetCore.Mvc;
using SportsStore.Contracts;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository storeRepository;
        public int PageSize = 5;

        public HomeController(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public IActionResult Index(int productPage = 1)
        {
            return View(new ProductListViewModel
            {
                Products = storeRepository.Products
                .OrderBy(x => x.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = storeRepository.Products.Count()
                }
            });
        }
    }
}
