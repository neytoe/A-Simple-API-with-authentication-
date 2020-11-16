
using EFPractice.Dtos;
using EFPractice.Models;
using EFPractice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFPractice.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [Route("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok( await _characterService.GetAllCharacters());
        }
        
        [Route("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult>  AddCharacter (AddCharacterDto chars)
        {
            
            return Ok(await _characterService.AddCharacter(chars));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateCharacterDto chars)
        {
            ServiceResponse<GetCharacterDto> response = await _characterService.Update(chars);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = await _characterService.Delete(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
