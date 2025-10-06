using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    /// <summary>
    /// Service layer for Skill management and trainee-skill linking.
    /// </summary>
    public class InstructorSkillService : IInstructorSkillService
    {
        private readonly IInstructorSkillRepository _InsskillRepo;
        private readonly ITraineeRepository _traineeRepo;
        private readonly IMapper _mapper;

        public InstructorSkillService(IInstructorSkillRepository InsskillRepo, ITraineeRepository traineeRepo, IMapper mapper)
        {
            _InsskillRepo = InsskillRepo;
            _traineeRepo = traineeRepo;
            _mapper = mapper;
        }

        // =========================================================
        // CRUD OPERATIONS
        // =========================================================

        public async Task<IEnumerable<InsSkillDto>> GetAllSkillsAsync()
        {
            var skills = await _InsskillRepo.GetAllSkillsAsync();
            return _mapper.Map<IEnumerable<InsSkillDto>>(skills);
        }


        public async Task<InsSkillDto?> GetSkillByIdAsync(int id)
        {
            var skill = await _InsskillRepo.GetSkillsByInstructorAsync(id);
            return _mapper.Map<InsSkillDto>(skill);
        }

        public async Task AddSkillAsync(InsSkillDto dto)
        {
            var skill = _mapper.Map<InstructorSkill>(dto);
            await _InsskillRepo.AddSkillAsync(skill);
            await _InsskillRepo.SaveAsync();
        }

        public async Task UpdateSkillAsync(InsSkillDto dto)
        {
            var skill = await _InsskillRepo.GetSkillByIdAsync(dto.InstructorSkillId);
            if (skill == null)
                throw new Exception($"Skill ID {dto.InstructorSkillId} not found.");

            _mapper.Map(dto, skill);
            _InsskillRepo.UpdateSkill(skill);
            await _InsskillRepo.SaveAsync();
        }

        public async Task DeleteSkillAsync(int id)
        {
            var skill = await _InsskillRepo.GetSkillByIdAsync(id);
            if (skill == null)
                throw new Exception($"Skill ID {id} not found.");

            _InsskillRepo.RemoveSkillFromInstructorAsync(id, skill.InstructorSkillId);
            await _InsskillRepo.SaveAsync();
        }

        // =========================================================
        // TRAINEE-SKILL LINKING LOGIC
        // =========================================================

        //public async Task AssignSkillToTraineeAsync(Guid traineeId, int skillId)
        //{
        //    var trainee = await _traineeRepo.GetByIdWithSkillsAsync(traineeId);
        //    if (trainee == null)
        //        throw new Exception($"Trainee {traineeId} not found.");

        //    var skill = await _InsskillRepo.GetByIdAsync(skillId);
        //    if (skill == null)
        //        throw new Exception($"Skill {skillId} not found.");

        //    trainee.Skills ??= new List<Skill>();
        //    if (!trainee.Skills.Any(s => s.SkillId == skill.SkillId))
        //    {
        //        trainee.Skills.Add(skill);
        //        await _traineeRepo.SaveAsync();
        //    }
        //}

        //public async Task RemoveSkillFromTraineeAsync(Guid traineeId, int skillId)
        //{
        //    var trainee = await _traineeRepo.GetByIdWithSkillsAsync(traineeId);
        //    if (trainee == null)
        //        throw new Exception($"Trainee {traineeId} not found.");

        //    var skill = trainee.Skills?.FirstOrDefault(s => s.SkillId == skillId);
        //    if (skill == null)
        //        throw new Exception($"Skill {skillId} not found for trainee {traineeId}.");

        //    trainee.Skills.Remove(skill);
        //    await _traineeRepo.SaveAsync(); // 
        //}

    }
}
