using System;
using System.Collections.Generic;

namespace WebExampleCrudNetCore.Models
{
    public partial class Usuario
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool activo { get; set; }        
    }
}
