using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public string Pass { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }

    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserModel> Users { get; set; }
        public Role()
        {
            Users = new List<UserModel>();
        }
    }

    public class LoginModel
    {
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Pass { get; set; }
    }

    public class RegistrationModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [DataType(DataType.Password)]
        [Compare("Pass", ErrorMessage = "Пароли не совпадают")]
        public string OneMoreTimePass { get; set; }

    }
}
