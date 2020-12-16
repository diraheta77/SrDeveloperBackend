using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.API.Usuarios.Application
{
    public class UserDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Balance { get; set; }

        public string Rol { get; set; }
    }
}
