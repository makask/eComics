using eComics.Data.Cart;
using eComics.Data.Services;
using eComics.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eComics.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly ShoppingCart _shoppingCart;
        public OrdersController(IBooksService booksService, ShoppingCart shoppingCart)
        {
            _booksService = booksService;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;    

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }
    }
}
