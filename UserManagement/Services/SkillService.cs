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
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepo;
        private readonly ITraineeRepository _traineeRepo;
        private readonly IMapper _mapper;

        public SkillService(ISkillRepository skillRepo, ITraineeRepository traineeRepo, IMapper mapper)
        {
            _skillRepo = skillRepo;
            _traineeRepo = traineeRepo;
            _mapper = mapper;
        }

        // =========================================================
        // CRUD OPERATIONS
        // =========================================================

        public async Task<IEnumerable<SkillDto>> GetAllSkillsAsync()
        {
            var skills = await _skillRepo.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<SkillDto>>(skills);
        }

        public async Task<SkillDto?> GetSkillByIdAsync(int id)
        {
            var skill = await _skillRepo.GetByIdAsync(id);
            return _mapper.Map<SkillDto>(skill);
        }

        public async Task AddSkillAsync(SkillDto dto)
        {
            var skill = _mapper.Map<Skill>(dto);
            await _skillRepo.AddAsync(skill);
            await _skillRepo.SaveAsync();
        }

        public async Task UpdateSkillAsync(SkillDto dto)
        {
            var skill = await _skillRepo.GetByIdAsync(dto.SkillId);
            if (skill == null)
                throw new Exception($"Skill ID {dto.SkillId} not found.");

            _mapper.Map(dto, skill);
            _skillRepo.Update(skill);
            await _skillRepo.SaveAsync();
        }

        public async Task DeleteSkillAsync(int id)
        {
            var skill = await _skillRepo.GetByIdAsync(id);
            if (skill == null)
                throw new Exception($"Skill ID {id} not found.");

            _skillRepo.Delete(skill);
            await _skillRepo.SaveAsync();
        }

        // =========================================================
        // TRAINEE-SKILL LINKING LOGIC
        // =========================================================

        public async Task AssignSkillToTraineeAsync(Guid traineeId, int skillId)
        {
            var trainee = await _traineeRepo.GetByIdWithSkillsAsync(traineeId);
            if (trainee == null)
                throw new Exception($"Trainee {traineeId} not found.");

            var skill = await _skillRepo.GetByIdAsync(skillId);
            if (skill == null)
                throw new Exception($"Skill {skillId} not found.");

            trainee.Skills ??= new List<Skill>();
            if (!trainee.Skills.Any(s => s.SkillId == skill.SkillId))
            {
                trainee.Skills.Add(skill);
                await _traineeRepo.SaveAsync();
            }
        }

        public async Task RemoveSkillFromTraineeAsync(Guid traineeId, int skillId)
        {
            var trainee = await _traineeRepo.GetByIdWithSkillsAsync(traineeId);
            if (trainee == null)
                throw new Exception($"Trainee {traineeId} not found.");

            var skill = trainee.Skills?.FirstOrDefault(s => s.SkillId == skillId);
            if (skill == null)
                throw new Exception($"Skill {skillId} not found for trainee {traineeId}.");

            trainee.Skills.Remove(skill);
            await _traineeRepo.SaveAsync(); // 
        }

    }
}
