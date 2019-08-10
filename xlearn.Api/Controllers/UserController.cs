using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xlearn.Models;
using xlearn.Repository;
using xlearn.Services;

namespace xlearn.Api.Controllers {
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private RepositoryBase<User> userServices;

        public UserController(RepositoryBase<User> userService) {
            this.userServices = userService;
        }

        /// <summary>
        /// Get all users from database.
        /// </summary>
        /// <returns>
        /// List of users.
        /// </returns>
        /// <response code="200">Returns the json with the list of users</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<User>> Get() {
            return Ok(this.userServices.GetAll());
        }

        /// <summary>
        /// Get the specifique user from database.
        /// </summary>
        /// <param name="id">The user's identification code.</param>
        /// <returns>
        /// The watershed requested.
        /// </returns>
        /// <response code="200">Returns the requested user.</response>
        /// <response code="404">If the user was not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<User> Get(UInt64 id) {
            var user = this.userServices.GetById(id);

            if (user == null) {
                return NotFound("User Not Found");

            }

            return Ok(user);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <returns>
        /// The user requested.
        /// </returns>
        /// <param name="user">The user object.</param>
        /// <response code="201">If the user was created.</response>
        /// <response code="500">If something get wrong creating the new user.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public ActionResult<User> Post([FromBody] User user) {

            this.userServices.Insert(ref user);

            if (user != null) {
                return Created(new Uri($"api/User/{user.Id}", UriKind.Relative), user);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <returns>
        /// Code 200 if success or 500 if something get wrong.
        /// </returns>
        /// <param name="id">The user id.</param>
        /// <param name="user">The user updated values.</param>
        /// <response code="200">If the user was updated.</response>
        /// <response code="500">If something get wrong updating the user.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Put(UInt64 id, [FromBody] User user) {
                user.Id = id;            

                if (this.userServices.Update(user)) {
                    return Ok();
                }
                return StatusCode(StatusCodes.Status500InternalServerError);

        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <returns>
        /// Code 200 if success or 500 if something get wrong.
        /// </returns>
        /// <param name="id">The user id.</param>
        /// <response code="200">If the user was deleted.</response>
        /// <response code="500">If something get wrong deleting the user.</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(UInt64 id) {
            if (this.userServices.Delete(id)) {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
