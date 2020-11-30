using EFPractice.Data;
using EFPractice.Dtos.Fight;
using EFPractice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFPractice.Services.FightService
{
    public class FightServices : IFightServices
    {
        private readonly DataContext _context;

        public FightServices(DataContext context )
        {
            _context = context;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            ServiceResponse<AttackResultDto> response = new ServiceResponse<AttackResultDto>();
            try
            {
                Character attacker = await _context.Characters
                                    .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill)
                                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                Character Opponent = await _context.Characters.FirstOrDefaultAsync(c => c.Id == request.OpponentId);
                CharacterSkill skill = await _context.CharacterSkills.FirstOrDefaultAsync();

                int damage = attacker.weapon.Damage + (new Random().Next(attacker.Strength));
                damage -= new Random().Next(Opponent.Defense);
                if (damage > 0)
                {
                    Opponent.HitPoints -= damage;
                }
                if (Opponent.HitPoints <= 0)
                {
                    response.Message = $"{Opponent.Name} has been defeated";
                }
                _context.Characters.Update(Opponent);
                await _context.SaveChangesAsync();
                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = Opponent.Name,
                    OpponentHP = Opponent.HitPoints,
                    Damage = damage

                };


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> weaponAttack(WeaponAttackDto request)
        {
            ServiceResponse<AttackResultDto> response = new ServiceResponse<AttackResultDto>();
            try
            {
                Character attacker = await _context.Characters
                                    .Include(c => c.weapon)
                                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                Character Opponent = await _context.Characters.FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                int damage = attacker.weapon.Damage + (new Random().Next(attacker.Strength));
                damage -= new Random().Next(Opponent.Defense);
                if (damage > 0)
                {
                    Opponent.HitPoints -= damage;
                }
                if (Opponent.HitPoints <= 0)
                {
                    response.Message = $"{Opponent.Name} has been defeated";
                }
                _context.Characters.Update(Opponent);
                await _context.SaveChangesAsync();
                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = Opponent.Name,
                    OpponentHP = Opponent.HitPoints,
                    Damage = damage    

                };
                
                                    
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
