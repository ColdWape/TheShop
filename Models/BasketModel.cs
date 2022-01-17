using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class BasketModel
    {
        public int Id { get; set; }
        public ProductModel TheProduct { get; set; }
        public UserModel Buyer { get; set; }

    }
}
