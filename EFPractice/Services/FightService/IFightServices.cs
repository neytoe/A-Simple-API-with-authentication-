using EFPractice.Dtos.Fight;
using EFPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFPractice.Services.FightService
{
    public interface IFightServices
    {
        Task<ServiceResponse<AttackResultDto>> weaponAttack(WeaponAttackDto request);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);

    }
}
