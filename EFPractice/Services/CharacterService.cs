using AutoMapper;
using EFPractice.Data;
using EFPractice.Dtos;
using EFPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EFPractice.Services
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor )
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));


        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {   
            ServiceResponse<List<GetCharacterDto>> serviceresponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);

            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            await _context.Characters.AddAsync(character );
            await _context.SaveChangesAsync();
            serviceresponse.Data = _context.Characters.Where(u => u.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceresponse;
            
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceresponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbCharacters = await _context.Characters.Where(c => c.User.Id == GetUserId()).ToListAsync();
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
            Character dbcharacter = await _context.Characters
                        .FirstOrDefaultAsync(x => x.Id == id && x.User.Id == GetUserId());
            
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

                Character character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == newCHar.Id && c.User.Id == GetUserId());
                if (character == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
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

                Character character =await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if (character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _context.Characters.Where(x => x.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character doesn't exist";
                    return serviceResponse;
                }
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
