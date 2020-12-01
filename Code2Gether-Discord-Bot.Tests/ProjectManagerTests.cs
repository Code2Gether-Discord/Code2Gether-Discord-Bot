using NUnit.Framework;

namespace Code2Gether_Discord_Bot.Tests
{
    internal class ProjectManagerTests
    {
        #region CreateProject Method
        [Test]
        public void CreateProject_SingleProject_ReturnsProject()
        {
            var stubUser = TestConfig.User();
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            const string PROJECT_NAME = "proj";

            var createdProject = projectManager.CreateProject(PROJECT_NAME, stubUser);

            Assert.IsNotNull(createdProject);
        }
        [Test]
        public void CreateProject_SingleProjectProperties_ChecksIfAuthorIsRight()
        {
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            var mockUser = TestConfig.User();
            const string PROJECT_NAME = "proj";

            var createdProject = projectManager.CreateProject(PROJECT_NAME, mockUser);

            Assert.AreEqual(mockUser, createdProject.Author);
        }
        [Test]
        [TestCase(0, "a", "a", "b", "c", "d", "e")]
        [TestCase(1, "b", "a", "b", "c", "d", "e")]
        [TestCase(2, "c", "a", "b", "c", "d", "e")]
        public void CreateProject_ManyProjectsProperties_ChecksIfIdIsRight(int expectedId, string selectedProjectName, params string[] projectsName)
        {
            var stubUser = TestConfig.User();
            var mockRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(mockRepository);

            foreach (var name in projectsName)
                projectManager.CreateProject(name, stubUser);
            var selectedProject = mockRepository.Read(selectedProjectName);

            Assert.AreEqual(expectedId, selectedProject.ID);
        }
        [Test]
        public void CreateProject_SingleProjectProperties_ChecksIfNameIsRight()
        {
            var stubUser = TestConfig.User();
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            const string PROJECT_NAME = "proj";

            var createdProject = projectManager.CreateProject(PROJECT_NAME, stubUser);

            Assert.AreEqual(PROJECT_NAME, createdProject.Name);
        }
        [Test]
        public void CreateProject_SingleProjectProperties_ChecksIfProjectMembersIsRight()
        {
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            var mockUser = TestConfig.User();
            const string PROJECT_NAME = "proj";

            var createdProject = projectManager.CreateProject(PROJECT_NAME, mockUser);

            Assert.IsTrue(createdProject.ProjectMembers.Count == 1 && createdProject.ProjectMembers.Contains(mockUser));
        }
        #endregion

        #region DoesProjectExist Method
        [Test]
        public void DoesProjectExist_CreateSingleProject_ReturnsProject()
        {
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            var project = TestConfig.Project(0);
            stubRepository.Create(project);

            projectManager.DoesProjectExist(project.Name, out var actualProject);

            Assert.IsNotNull(actualProject);
        }
        [Test]
        public void DoesProjectExist_CreateSingleProject_ChecksIfProjectExist()
        {
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            var project = TestConfig.Project(0);
            stubRepository.Create(project);

            var doesExist = projectManager.DoesProjectExist(project.Name);

            Assert.IsTrue(doesExist);
        }
        [Test]
        public void DoesProjectExist_CreateSingleProject_ChecksIfReturnedProjectIsTheCreated()
        {
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            var expectedProject = TestConfig.Project(0);
            stubRepository.Create(expectedProject);

            projectManager.DoesProjectExist(expectedProject.Name, out var actualProject);

            Assert.AreEqual(expectedProject, actualProject);
        }
        #endregion

        #region JoinProjectTests Method
        [Test]
        public static void JoinProject_UserJoinsProject_ChecksWhetherUserHasActuallyJoined()
        {
            var stubUser = TestConfig.User();
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            const string PROJECT_NAME = "proj";
            stubRepository.Create(TestConfig.Project(0, PROJECT_NAME, stubUser));

            projectManager.JoinProject(PROJECT_NAME, stubUser, out var actualProject);
            var isAmongMembers = actualProject.ProjectMembers.Contains(stubUser);

            Assert.IsTrue(isAmongMembers);
        }
        [Test]
        public static void JoinProject_UserJoinsProject_ReturnsProject()
        {
            var stubUser = TestConfig.User();
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            const string PROJECT_NAME = "proj";
            stubRepository.Create(TestConfig.Project(0, PROJECT_NAME, stubUser));

            projectManager.JoinProject(PROJECT_NAME, stubUser, out var actualProject);

            Assert.IsNotNull(actualProject);
        }
        [Test]
        public static void JoinProject_UserJoinsProject_ReturnsTrue()
        {
            var stubUser = TestConfig.User();
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            const string PROJECT_NAME = "proj";
            stubRepository.Create(TestConfig.Project(0, PROJECT_NAME, stubUser));

            var actual = projectManager.JoinProject(PROJECT_NAME, stubUser, out _);

            Assert.IsTrue(actual);
        }
        [Test]
        public static void JoinProject_UserAlreadyIsIn_ChecksProjectHasNotChanged()
        {
            var stubUser = TestConfig.User();
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            const string PROJECT_NAME = "proj";
            stubRepository.Create(TestConfig.Project(0, PROJECT_NAME, stubUser));
            var expectedProject = stubRepository.Read(0);
            expectedProject.ProjectMembers.Add(stubUser);

            projectManager.JoinProject(PROJECT_NAME, stubUser, out var actualProject);

            Assert.AreEqual(expectedProject, actualProject);
        }
        [Test]
        public static void JoinProject_UserAlreadyIsIn_ReturnsFalse()
        {
            var stubUser = TestConfig.User();
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            const string PROJECT_NAME = "proj";
            stubRepository.Create(TestConfig.Project(0, PROJECT_NAME, stubUser));
            stubRepository.Read(0).ProjectMembers.Add(stubUser);

            var actual = projectManager.JoinProject(PROJECT_NAME, stubUser, out _);

            Assert.IsFalse(actual);
        }
        [Test]
        public static void JoinProject_ProjectNotExist_ReturnsFalse()
        {
            var stubUser = TestConfig.User();
            var stubRepository = TestConfig.ProjectRepository();
            var projectManager = TestConfig.ProjectManager(stubRepository);
            const string PROJECT_NAME = "proj";

            var actual = projectManager.JoinProject(PROJECT_NAME, stubUser, out _);

            Assert.IsFalse(actual);
        }
        #endregion
    }
}
