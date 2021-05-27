using ddd.Domain.Entities;
using ddd.Domain.Interfaces;
using ddd.Service.Validators;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ddd.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBaseService<User> _baseUserService;
        public UserController(IBaseService<User> baseUserService)
        {
            _baseUserService = baseUserService;
        }
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
                return NotFound();
            return Execute(() => _baseUserService.Add<UserValidator>(user).Id);
        }
        [HttpPut]
        public IActionResult Update([FromBody] User user)
        {
            if (user == null)
                return NotFound();
            return Execute(() => _baseUserService.Update<UserValidator>(user).Id);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();
            Execute(() =>
            {
                _baseUserService.Delete(id);
                return true;
            });
            return new NoContentResult();
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseUserService.Get());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();
            return Execute(() => _baseUserService.GetById(id));
        }
        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
