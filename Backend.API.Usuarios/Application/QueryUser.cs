using AutoMapper;
using Backend.API.Usuarios.Models;
using Backend.API.Usuarios.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.API.Usuarios.Application
{
    public class QueryUser
    {

        public class Execute : IRequest<UserDTO> 
        {
            public Guid? UserId { get; set; }
        }

        public class ExecuteHandler : IRequestHandler<Execute, UserDTO>
        {
            private readonly ContextUser _context;
            private readonly IMapper _mapper;

            public ExecuteHandler(ContextUser context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserDTO> Handle(Execute request, CancellationToken cancellationToken)
            {
                var userFind = await _context.Users.Where(d => d.UserId == request.UserId).FirstOrDefaultAsync();
                if(userFind == null)
                {
                    throw new Exception("No se encontro el usuario");
                }
                var userDTO = _mapper.Map<User, UserDTO>(userFind);
                return userDTO;
            }
        }
    }
}
