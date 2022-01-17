using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BasketController : Controller
    {
        private readonly DBContext _dBContext;


        public BasketController(DBContext dBContext)
        {

            _dBContext = dBContext;
        }
        [HttpGet]
        public IActionResult ShopBasket()
        {
            ViewBag.Products = _dBContext.Products;
            ViewBag.MyBasket = _dBContext.Baskets.Include(i => i.TheProduct).Include(i => i.Buyer);
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> ShopBasket(int ProductId)
        {
            string a = User.Identity.Name;
            UserModel user = await _dBContext.Users.FirstOrDefaultAsync(u => u.Login == a);
            ProductModel productModel = await _dBContext.Products.FirstOrDefaultAsync(u => u.Id == ProductId);

            BasketModel basket = new BasketModel { TheProduct = productModel, Buyer = user };
            
            _dBContext.Baskets.Add(basket);

            await _dBContext.SaveChangesAsync();


            return RedirectToAction("Index", "Home");
        }
    }
}
