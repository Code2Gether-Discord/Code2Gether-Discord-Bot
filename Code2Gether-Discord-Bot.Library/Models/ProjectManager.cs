using System;
using System.Linq;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models.Repositories;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class ProjectManager : IProjectManager
    {
        private IProjectRepository _projectRepository;

        public ProjectManager(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> DoesProjectExistAsync(string projectName)
        {
            return (await _projectRepository.ReadAsync(projectName)) is not null;
        }

        public Task<Project> GetProjectAsync(string projectName)
        {
            return _projectRepository.ReadAsync(projectName);
        }

        public async Task<Project> CreateProjectAsync(string projectName, Member author)
        {
            var newProject = new Project(0, projectName, author);
            if (await _projectRepository.CreateAsync(newProject))
                if (await JoinProjectAsync(projectName, author))    // Author joins new projects by default
                    return newProject;
            throw new Exception($"Failed to create new project: {newProject}!");
        }

        public async Task<bool> JoinProjectAsync(string projectName, Member user)
        {
            var project = (await _projectRepository
                .ReadAllAsync())
                .FirstOrDefault(x => x.Name == projectName);
            
            project.Members.Add(user);

            var result = await _projectRepository.UpdateAsync(project);
            project = await _projectRepository.ReadAsync(projectName);

            return result && project.Members.Contains(user);
        }
    }
}
