using AutoMapper;
using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepo;
        private readonly IMapper _mapper;

        public SkillService(ISkillRepository skillRepo, IMapper mapper)
        {
            _skillRepo = skillRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SkillDto>> GetAllSkillsAsync()
        {
            var skills = await _skillRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<SkillDto>>(skills);
        }

        public async Task<SkillDto?> GetSkillByIdAsync(int id)
        {
            var skill = await _skillRepo.GetByIdAsync(id);
            return _mapper.Map<SkillDto>(skill);
        }

        public async Task AddSkillAsync(SkillDto skillDto)
        {
            var skill = _mapper.Map<Skill>(skillDto);
            await _skillRepo.AddAsync(skill);
        }

        public async Task UpdateSkillAsync(SkillDto skillDto)
        {
            var skill = _mapper.Map<Skill>(skillDto);
            _skillRepo.Update(skill);
        }

        public async Task DeleteSkillAsync(int id)
        {
            var skill = await _skillRepo.GetByIdAsync(id);
            if (skill != null)
            {
                _skillRepo.Delete(skill);
            }
        }

        public async Task AssignSkillToTraineeAsync(int traineeId, int skillId)
        {
            await _skillRepo.AssignSkillToTraineeAsync(traineeId, skillId);
        }
    }
}
