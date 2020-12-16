using Backend.API.Usuarios.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.API.Usuarios.Persistence
{
    public class ContextUser : DbContext
    {
        public ContextUser(DbContextOptions<ContextUser> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
