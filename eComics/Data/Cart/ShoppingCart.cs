using eComics.Models;
using Microsoft.EntityFrameworkCore;

namespace eComics.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public List<ShoppingCartItem> GetShoppingCartItems() 
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId ==
                ShoppingCartId).Include(b => b.Book).ToList());
        }

        public double GetShoppingCartTotal()
        { 
            var total = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(p => p.Book.Price * p.Amount).Sum();
            return total;
        }
    }
}
