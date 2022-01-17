using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        private readonly DBContext _dBContext;


        public AuthController(DBContext dBContext)
        {

            _dBContext = dBContext;
        }


        [HttpGet]
        public IActionResult UserInfo()
        {
            ViewBag.Userr = _dBContext.Users.First(i => i.Login == User.Identity.Name);
            
            return View();
        }









        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = await _dBContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Login == model.Login && u.Pass == model.Pass);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        private async Task Authenticate(UserModel person)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),

                new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationModel model, string ChooseRole)
        {
            if (ModelState.IsValid)
            {
                UserModel person = await _dBContext.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (person == null)
                {
                    // добавляем пользователя в бд
                    person = new UserModel { Login = model.Login, Pass = model.Pass };

                    Role userRole = await _dBContext.Roles.FirstOrDefaultAsync(r => r.Name == ChooseRole);
                    if (userRole != null)
                        person.Role = userRole;

                    _dBContext.Users.Add(person);
                    await _dBContext.SaveChangesAsync();

                    await Authenticate(person); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким именем уже есть!");
                }
            }
            return View(model);
        }
    }
}
    

