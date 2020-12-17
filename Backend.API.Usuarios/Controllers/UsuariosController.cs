using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.API.Usuarios.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get one user data, include balance.
        /// </summary>
        /// <param name="id">Type Guid</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetBalanceUser(Guid? id)
        {
            return await _mediator.Send(new QueryUser.Execute { UserId = id });
        }

        /// <summary>
        /// Execute transfer user to user.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Transfer")]
        public async Task<ActionResult<Unit>> Transferir(TransferBalance.Execute data)
        {
            return await _mediator.Send(data);
        }

    }
}
