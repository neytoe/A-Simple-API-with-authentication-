using AutoMapper;
using EFPractice.Data;
using EFPractice.Dtos;
using EFPractice.Dtos.Weapon;
using EFPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EFPractice.WeaponServices
{
    public class WeaponService : IWeaponService
    {
       
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WeaponService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User.Id == GetUserId());
                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character doesn't exist";
                    return response;
                }
                Weapon weapon = _mapper.Map<Weapon>(newWeapon);
                weapon.Character = character;
                await _context.Weapons.AddAsync(weapon);
                await _context.SaveChangesAsync();

                //return response
                response.Message = "Successfully added";
                response.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
