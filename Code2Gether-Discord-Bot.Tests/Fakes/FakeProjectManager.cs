using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;

namespace Code2Gether_Discord_Bot.Tests.Fakes
{
    /// <summary>
    /// Represents a fake <see cref="ProjectManager"/>. This class cannot be inherited.
    /// </summary>
    internal sealed class FakeProjectManager : ProjectManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeProjectManager"/> class using the especified project repository.
        /// </summary>
        /// <param name="projectRepository">This intance's project repository.</param>
        public FakeProjectManager(IProjectRepository projectRepository) : base(projectRepository) { }
    }
}