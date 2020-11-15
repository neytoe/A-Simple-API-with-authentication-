using AutoMapper;
using EFPractice.Data;
using EFPractice.Dtos;
using EFPractice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFPractice.Services
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {   
            ServiceResponse<List<GetCharacterDto>> serviceresponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            await _context.Characters.AddAsync(character );
            await _context.SaveChangesAsync();
            serviceresponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceresponse;
            
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceresponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbCharacters = await _context.Characters.ToListAsync();
            if (dbCharacters == null)
            {
                serviceresponse.Success = false;
                return serviceresponse;
            }
            serviceresponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceresponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceresponse = new ServiceResponse<GetCharacterDto>();
            Character dbcharacter = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            if (dbcharacter == null)
            {
                serviceresponse.Success = false;
                return serviceresponse;
            }
            serviceresponse.Data =  _mapper.Map<GetCharacterDto>(dbcharacter);
            return serviceresponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto newCHar)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {

                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == newCHar.Id);
                if (character == null)
                {
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
                character.Name = newCHar.Name;
                character.Intelligence = newCHar.Intelligence;
                character.Strength = newCHar.Strength;
                character.HitPoints = newCHar.HitPoints;
                character.Defense = newCHar.Defense;
                character.Class = newCHar.Class;

                _context.Characters.Update(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data =  _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception e)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
                
                
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {

                Character character = _context.Characters.First(c => c.Id == id);
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception e)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;


            }

            return serviceResponse;
        }

    }
}
