using System;
using System.Linq;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models.Repositories;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class ProjectManager : IProjectManager
    {
        private IMemberRepository _memberRepository;
        private IProjectRepository _projectRepository;

        public ProjectManager(IMemberRepository memberRepository, IProjectRepository projectRepository)
        {
            _memberRepository = memberRepository;
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Checks if a project exists by a given <see cref="projectName"/>.
        /// </summary>
        /// <param name="projectName">Project name to check for</param>
        /// <returns>true if the project exists. false if the project does not exist.</returns>
        public async Task<bool> DoesProjectExistAsync(string projectName)
        {
            return await _projectRepository.ReadAsync(projectName) is not null;
        }

        /// <summary>
        /// Get a project by a given <see cref="projectName"/>
        /// </summary>
        /// <param name="projectName">Project name to search for</param>
        /// <returns>a project if it is found or otherwise returns null</returns>
        public Task<Project> GetProjectAsync(string projectName)
        {
            return _projectRepository.ReadAsync(projectName);
        }

        /// <summary>
        /// Creates a new project with a given name and an given author.
        /// The author is automatically added to the project.
        /// </summary>
        /// <param name="projectName">Name for new project</param>
        /// <param name="author">Member that is requesting project be made</param>
        /// <returns>A new project instance 
        /// or throws an exception if project was not created
        /// or author failed to join the project.</returns>
        public async Task<Project> CreateProjectAsync(string projectName, Member author)
        {
            var retrievedAuthor = await _memberRepository.ReadFromSnowflakeAsync(author.SnowflakeId);

            // If author doesn't exist
            if (retrievedAuthor == null)
            {
                // Create author member
                if (await _memberRepository.CreateAsync(author))
                    author = await _memberRepository.ReadFromSnowflakeAsync(author.SnowflakeId); // Update author
                else
                    throw new Exception($"Failed to create new member: {author}!");

            }
            else // Author exists
            {
                author = retrievedAuthor;     // Update local object for author
            }

            var newProject = new Project(projectName, author);

            newProject.Members.Add(author);

            if (await _projectRepository.CreateAsync(newProject)) // Create project with reference to author member
            {
                newProject = await _projectRepository.ReadAsync(newProject.Name);
                await _projectRepository.AddMemberAsync(newProject, author);
                newProject = await _projectRepository.ReadAsync(newProject.Name);

                return newProject;
            }

            throw new Exception($"Failed to create new project: {projectName}!");
        }

        /// <summary>
        /// Attempt to join a project by a given name with a given member.
        /// </summary>
        /// <param name="projectName">Project name to join</param>
        /// <param name="member">Member to join a project</param>
        /// <returns>true if update was successful and the new member is apart of the project
        /// or false if the user is already in the project.</returns>
        public async Task<bool> JoinProjectAsync(string projectName, Member member)
        {
            var retreivedMember = await _memberRepository.ReadFromSnowflakeAsync(member.SnowflakeId);

            // Get all projects and store matching one matching projectName
            var project = await _projectRepository.ReadAsync(projectName);

            // If the given member by SnowflakeId does not exist in the project as a member
            if (!project.Members.Any(m => m.SnowflakeId == member.SnowflakeId))
            {
                // If member isn't in db
                if (retreivedMember == null)
                {
                    if (await _memberRepository.CreateAsync(member)) // Create member
                        member = await _memberRepository.ReadFromSnowflakeAsync(member.SnowflakeId);
                    else
                        throw new Exception($"Failed to add member: {member}");
                }
                else // Will this ever be hit? Probably not. Don: "We can fix that logic later"
                {
                    member = retreivedMember;
                }

                project.Members.Add(member);
            }
            else
                return false;   // Else they are already in the project

            // Update the project with new member
            var result = await _projectRepository.AddMemberAsync(project, member);

            // Get the updated repository
            project = await _projectRepository.ReadAsync(projectName);

            // Compare if update was successful, and the project now contains the member
            return result && project.Members
                .Select(x => x.SnowflakeId)
                .Contains(member.SnowflakeId);
        }
    }
}
