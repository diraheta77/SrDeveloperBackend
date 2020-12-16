using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.API.Usuarios.Models
{
    public class User
    {
        public Guid? UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public double Balance { get; set; }

        public string Rol { get; set; }
    }
}
