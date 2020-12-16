using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.API.Usuarios.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Usuarios.Application
{
    public class AddBalance
    {

        public class Execute : IRequest
        {
            public Guid? UserId { get; set; }
            public double BalanceAmount { get; set; }
        }

        /// <summary>
        /// Validaciones. logica de negocio
        /// </summary>
        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.BalanceAmount).NotEmpty();
                RuleFor(x => x.BalanceAmount).GreaterThan(0);
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
                    throw new Exception("Usuario no existe.");
                }

                user.Balance += request.BalanceAmount;
                var value = await _context.SaveChangesAsync();
                if (value > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se actualizo el balance de usuario");
            }
        }


    }
}
