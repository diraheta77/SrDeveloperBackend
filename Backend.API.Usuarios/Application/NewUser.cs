using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.API.Usuarios.Persistence;
using Backend.API.Usuarios.Models;

namespace Backend.API.Usuarios.Application
{
    public class NewUser
    {

        public class Execute : IRequest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime? BirthDate { get; set; }
            public double Balance { get; set; }

            public string Rol { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Balance).NotNull();
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
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                    Balance = request.Balance,
                    Rol = request.Rol
                    //UserId = Convert.ToString(Guid.NewGuid())
                };

                _context.Users.Add(user);

                var value = await _context.SaveChangesAsync();

                if(value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se inserto el registro de usuario");
            }
        }
    }
}
