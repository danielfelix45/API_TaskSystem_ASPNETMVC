using Microsoft.AspNetCore.Mvc;
using TaskSystem.Models;
using TaskSystem.Repositories.Interfaces;

namespace TaskSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAll()
        {
            List<UserModel> users = await _userRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserModel>>> GetById(int id)
        {
            UserModel user = await _userRepository.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<UserModel>>> Register([FromBody] UserModel userModel)
        {
            UserModel user = await _userRepository.ToAdd(userModel);
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<List<UserModel>>> Update([FromBody] UserModel userModel, int id)
        {
            userModel.Id = id;
            UserModel user = await _userRepository.ToUpdate(userModel, id);
            return Ok(user);
        }

        [HttpDelete]
        public async Task<ActionResult<List<UserModel>>> Delete(int id)
        {
            bool deleted = await _userRepository.ToDelete(id);
            return Ok(deleted);
        }
    }
}
