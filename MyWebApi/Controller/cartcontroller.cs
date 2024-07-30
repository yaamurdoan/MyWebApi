using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private static List<Cart> Carts = new List<Cart>();

        // GET: api/cart
        [HttpGet]
        public ActionResult<IEnumerable<Cart>> GetCarts()
        {
            return Ok(Carts);
        }

        // GET: api/cart/{id}
        [HttpGet("{id}")]
        public ActionResult<Cart> GetCart(int id)
        {
            var cart = Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // POST: api/cart
        [HttpPost]
        public ActionResult<Cart> CreateCart([FromBody] Cart cart)
        {
            cart.Id = Carts.Count > 0 ? Carts.Max(c => c.Id) + 1 : 1;
            Carts.Add(cart);
            return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
        }

        // PUT: api/cart/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCart(int id, [FromBody] Cart updatedCart)
        {
            var cart = Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            cart.CartItems = updatedCart.CartItems;
            return NoContent();
        }

        // DELETE: api/cart/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCart(int id)
        {
            var cart = Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            Carts.Remove(cart);
            return NoContent();
        }

        // POST: api/cart/{id}/items
        [HttpPost("{id}/items")]
        public ActionResult AddCartItem(int id, [FromBody] CartItem cartItem)
        {
            var cart = Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            cart.CartItems.Add(cartItem);
            return Ok(cart);
        }

        // DELETE: api/cart/{cartId}/items/{itemId}
        [HttpDelete("{cartId}/items/{itemId}")]
        public ActionResult RemoveCartItem(int cartId, int itemId)
        {
            var cart = Carts.FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
            {
                return NotFound();
            }

            var cartItem = cart.CartItems.FirstOrDefault(item => item.Id == itemId);
            if (cartItem == null)
            {
                return NotFound();
            }

            cart.CartItems.Remove(cartItem);
            return NoContent();
        }
    }
}
