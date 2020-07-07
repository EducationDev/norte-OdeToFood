using OdeToFood.Data.Model;
using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.WebSite.Controllers
{
    public class ShopController : BaseController
    {
        // GET: Shop
        private readonly BaseDataService<Product> MyContext = new BaseDataService<Product>();
        private readonly RestoDbContext db = new RestoDbContext();
        public ActionResult Index()
        {
            var products = MyContext.Get(null, null, "Artist");
            return View(products);
        }

        public ActionResult Buy(int id)
        {
            var cookie = "shop-art-cookie";

            var cart = new Cart
            {
                CartDate = DateTime.Now,
                Cookie = cookie,
                ItemCount =1,                
            };
            this.CheckAuditPattern(cart, true);

            var cartitem = new CartItem
            {
                Price = 100,
                ProductId = id,
                Quantity = 2,
            };
            this.CheckAuditPattern(cartitem, true);

            cart.CartItem = new List<CartItem>() { cartitem };
            db.Cart.Add(cart);
            db.SaveChanges();

            return RedirectToAction("Index", "CartItem");

        }


        
    }
}