using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    /// <summary>
    /// Service for trainee business logic.
    /// </summary>
    public class TraineeService : ITraineeService
    {
        private readonly ITraineeRepository _repository;

        public TraineeService(ITraineeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TraineeDTO>> GetAllTraineesAsync()
        {
            var trainees = await _repository.GetAllAsync();
            return trainees.Select(t => new TraineeDTO
            {
                TraineeId = t.TraineeId,
                GithubUsername = t.GithubUsername,
                ProfileImage = t.ProfileImage,
                EducationalBackground = t.EducationalBackground,
                TraineeCV = t.TraineeCV,
                LearningObjectives = t.LearningObjectives,
                ExperienceLevel = t.ExperienceLevel
            });
        }

        public async Task<TraineeDTO?> GetTraineeByIdAsync(Guid id)
        {
            var t = await _repository.GetByIdAsync(id);
            if (t == null) return null;

            return new TraineeDTO
            {
                TraineeId = t.TraineeId,
                GithubUsername = t.GithubUsername,
                ProfileImage = t.ProfileImage,
                EducationalBackground = t.EducationalBackground,
                TraineeCV = t.TraineeCV,
                LearningObjectives = t.LearningObjectives,
                ExperienceLevel = t.ExperienceLevel
            };
        }

        public async Task<TraineeDTO> CreateTraineeAsync(TraineeDTO dto)
        {
            var trainee = new Trainee
            {
                TraineeId = Guid.NewGuid(),
                GithubUsername = dto.GithubUsername,
                ProfileImage = dto.ProfileImage,
                EducationalBackground = dto.EducationalBackground,
                TraineeCV = dto.TraineeCV,
                LearningObjectives = dto.LearningObjectives,
                ExperienceLevel = dto.ExperienceLevel
            };

            await _repository.AddAsync(trainee);
            dto.TraineeId = trainee.TraineeId;
            return dto;
        }

        public async Task<TraineeDTO> UpdateTraineeAsync(TraineeDTO dto)
        {
            var trainee = await _repository.GetByIdAsync(dto.TraineeId);
            if (trainee == null) throw new Exception("Trainee not found");

            trainee.GithubUsername = dto.GithubUsername;
            trainee.ProfileImage = dto.ProfileImage;
            trainee.EducationalBackground = dto.EducationalBackground;
            trainee.TraineeCV = dto.TraineeCV;
            trainee.LearningObjectives = dto.LearningObjectives;
            trainee.ExperienceLevel = dto.ExperienceLevel;

            await _repository.UpdateAsync(trainee);

            return dto;
        }

        public async Task DeleteTraineeAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
