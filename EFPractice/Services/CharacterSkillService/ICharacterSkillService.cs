﻿using EFPractice.Dtos;
using EFPractice.Dtos.CharacterSkill;
using EFPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFPractice.Services.CharacterSkillService
{
    public interface ICharacterSkillService
    {
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newcharacterSkill);
    }
}
