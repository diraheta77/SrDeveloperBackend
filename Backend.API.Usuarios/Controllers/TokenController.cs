using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.API.Usuarios.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Backend.API.Usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _configuration = config;
        }

        [HttpPost]
        public async Task<ActionResult<string>> GetToken(RetrieveToken.Execute data)
        {
            return await _mediator.Send(data);
        }


    }
}
