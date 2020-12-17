using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.API.Usuarios.Persistence;
using Microsoft.EntityFrameworkCore;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Backend.API.Usuarios.Application
{
    public class RetrieveToken
    {
        public class Execute : IRequest<string>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        /// <summary>
        /// valdiaciones
        /// </summary>
        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
        }


        public class ExecuteHandler : IRequestHandler<Execute, string>
        {
            private readonly ContextUser _context;
            public IConfiguration _configuration;

            public ExecuteHandler(ContextUser context, IConfiguration config)
            {
                _context = context;
                _configuration = config;
            }
            public async Task<string> Handle(Execute request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Where(d => d.FirstName.ToLower() == request.FirstName.ToLower() && d.LastName.ToLower() == request.LastName.ToLower()).FirstOrDefaultAsync();
                if(user == null)
                {
                    throw new Exception("Usuario no existe o datos incorrectos.");
                }

                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("Role", user.Rol)
                   };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                var _token = new JwtSecurityTokenHandler().WriteToken(token);

                return _token;

            }
        }



    }
}
