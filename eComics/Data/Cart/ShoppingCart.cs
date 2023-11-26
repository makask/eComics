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

        public void AddItemToCart(Book book)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Book.Id == book.Id &&
                n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Book = book,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Book book)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Book.Id == book.Id &&
                n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                { 
                    _context.ShoppingCartItems.Remove(shoppingCartItem);                      
                }
            }
            _context.SaveChanges();
        }

        public double GetShoppingCartTotal()
        { 
            var total = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(p => p.Book.Price * p.Amount).Sum();
            return total;
        }
    }
}
