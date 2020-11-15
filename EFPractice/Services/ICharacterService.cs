using EFPractice.Dtos;
using EFPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFPractice.Services
{
    public interface ICharacterService
    {
       Task <ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
       Task <ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
       Task <ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto newCharacter);

        Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id);
    }
}
