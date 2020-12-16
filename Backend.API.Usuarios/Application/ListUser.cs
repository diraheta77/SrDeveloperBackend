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
    public class ListUser
    {
        public class Execute : IRequest<List<UserDTO>> { }

        public class ExecuteHandler : IRequestHandler<Execute, List<UserDTO>>
        {
            private readonly ContextUser _context;
            private readonly IMapper _mapper;

            public ExecuteHandler(ContextUser context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<UserDTO>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var userList = await _context.Users.ToListAsync();
                var userListDTO = _mapper.Map<List<User>, List<UserDTO>>(userList);
                return userListDTO;
            }
        }

    }
}
