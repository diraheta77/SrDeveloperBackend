using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.API.Usuarios.Persistence;
using Backend.API.Usuarios.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.API.Usuarios.Application
{
    public class DeleteUser
    {

        public class Execute : IRequest
        {
            public Guid? UserId { get; set; }
        }


        public class ExecuteHandler : IRequestHandler<Execute>
        {
            private readonly ContextUser _context;

            public ExecuteHandler(ContextUser context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Where(d => d.UserId == request.UserId).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("Usuario no existe.");
                }
                _context.Remove(user);
                var value = await _context.SaveChangesAsync();
                if (value > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se elimino el balance de usuario");
            }

        }

    }
}
