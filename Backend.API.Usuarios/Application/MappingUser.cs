using AutoMapper;
using Backend.API.Usuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.API.Usuarios.Application
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
