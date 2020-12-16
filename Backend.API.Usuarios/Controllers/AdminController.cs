using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.API.Usuarios.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IMediator _mediator;
        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new User.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(NewUser.Execute data)
        {
            return await _mediator.Send(data);
        }

        /// <summary>
        /// Update user data in database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Unit>> Actualizar(UpdateUser.Execute data)
        {
            return await _mediator.Send(data);
        }

        /// <summary>
        /// Delete user from database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid? id)
        {
            return await _mediator.Send(new DeleteUser.Execute { UserId = id });
        }

        /// <summary>
        /// Get All users, no filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            return await _mediator.Send(new ListUser.Execute());
        }

        /// <summary>
        /// Add amount to balance for user.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Balance/Add")]
        public async Task<ActionResult<Unit>> AddToBalance(AddBalance.Execute data)
        {
            return await _mediator.Send(data);
        }

        /// <summary>
        /// Less amount to balance for user.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Balance/Less")]
        public async Task<ActionResult<Unit>> LessToBalance(LessBalance.Execute data)
        {
            return await _mediator.Send(data);
        }

    }
}
