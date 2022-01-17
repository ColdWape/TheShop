using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DBContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<BasketModel> Baskets { get; set; }


        public DBContext(DbContextOptions<DBContext> dbContextOptions)
            : base(dbContextOptions)
        {
            Database.EnsureCreated();
            if (!Products.Any())
            {
                Products.Add(new ProductModel { Name = "Snickers" ,
                                                Price = 50,
                                                Quantity = 124,
                                                Info = "Сладкий, шоколадный батончик сникерс. Подкрепись, когда голодный!",
                                                Img = "../images/snickers.jpg"
                                               });
                Products.Add(new ProductModel
                {
                    Name = "Coca-Cola",
                    Price = 120,
                    Quantity = 453,
                    Info = "Отмечай Новый год вместе с Coca-Cola",
                    Img = "../images/scale_1200.jpg"
                });
                Products.Add(new ProductModel
                {
                    Name = "Raffaello",
                    Price = 200,
                    Quantity = 52,
                    Info = "Почувсвуй блаженное удовольствие вместе с рафаелло",
                    Img = "../images/raffaello.jpg"
                });
                Products.Add(new ProductModel
                {
                    Name = "Халва",
                    Price = 400,
                    Quantity = 12,
                    Info = "Вспомни вкус детства со свежей халвой",
                    Img = "../images/halva.jpg"
                });
                Products.Add(new ProductModel
                {
                    Name = "Тушка цыпленка",
                    Price = 50,
                    Quantity = 124,
                    Info = "Свежая тушка! Зарежем, ощипаем при Вашем присутвии!!!",
                    Img = "../images/3206253.jpg"
                });






            }
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";
            string sellerRoleName = "seller";

            string adminLogin = "emperor";
            string adminPassword = "kingdom";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            Role sellerRole = new Role { Id = 3, Name = sellerRoleName };

            UserModel adminUser = new UserModel { Id = 1, Login = adminLogin, Pass = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole, sellerRole });
            modelBuilder.Entity<UserModel>().HasData(new UserModel[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
