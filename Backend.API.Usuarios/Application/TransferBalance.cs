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
    public class TransferBalance
    {
        public class Execute : IRequest
        {
            public string FirstNamePrincipal { get; set; }
            public string LastNamePrincipal { get; set; }
            public string FirstNameBeneficiary { get; set; }
            public string LastNameBeneficiary { get; set; }
            public double Payment { get; set; }
        }


        /// <summary>
        /// Validaciones
        /// </summary>
        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.FirstNamePrincipal).NotEmpty();
                RuleFor(x => x.LastNamePrincipal).NotEmpty();
                RuleFor(x => x.FirstNameBeneficiary).NotEmpty();
                RuleFor(x => x.LastNameBeneficiary).NotEmpty();
                RuleFor(x => x.Payment).NotEmpty();
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
                var pagador = await _context.Users.Where(d => d.FirstName == request.FirstNamePrincipal && d.LastName == request.LastNamePrincipal).FirstOrDefaultAsync();
                var benef = await _context.Users.Where(d => d.FirstName == request.FirstNameBeneficiary && d.LastName == request.LastNameBeneficiary).FirstOrDefaultAsync();

                if(pagador == null)
                {
                    throw new Exception("No se encontro el registro de usuario que transfiere");
                }

                if (benef == null)
                {
                    throw new Exception("No se encontro el registro de usuario que recibe");
                }

                pagador.Balance = pagador.Balance - request.Payment;
                benef.Balance = benef.Balance + request.Payment;

                var value = await _context.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se actualizo el balance de los usuarios");
            }
        }



    }
}
