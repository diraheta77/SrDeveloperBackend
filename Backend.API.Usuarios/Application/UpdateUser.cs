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
    public class UpdateUser
    {
        public class Execute : IRequest
        {
            public Guid? UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime? BirthDate { get; set; }
            public string Rol { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Rol).NotEmpty();
            }
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

                if(user == null)
                {
                    throw new Exception("Usuario no encontrado en base de datos");
                }

                //update user data
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.BirthDate = request.BirthDate;
                user.Rol = request.Rol;

                var value = await _context.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se inserto el registro de usuario");
            }
        }


    }
}
