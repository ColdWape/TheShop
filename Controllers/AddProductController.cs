using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AddProductController : Controller
    {

        private readonly DBContext _dBContext;


        public AddProductController(DBContext dBContext)
        {

            _dBContext = dBContext;
        }

        [HttpGet]
        public IActionResult NewPrdct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewPrdct(ProductModel model)
        {
            
                ProductModel product = new ProductModel { Name = model.Name, Price = model.Price, Quantity = model.Quantity, Info = model.Info };
                product.Img = "../images/robb.jpg";
                _dBContext.Products.Add(product);

                await _dBContext.SaveChangesAsync();


                return RedirectToAction("Index", "Home");
            
            
            

        }
    }
}
