using Code2Gether_Discord_Bot.Library.Models;
using Discord;

namespace Code2Gether_Discord_Bot.Tests.Fakes
{
    /// <summary>
    /// Represents a fake <see cref="Project"/>.This class cannot be inherited.
    /// </summary>
    internal sealed class FakeProject : Project
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeProject"/> class with the specified Id, Name and Author.
        /// </summary>
        /// <param name="id">This instance's Id.</param>
        /// <param name="name">This intance's name.</param>
        /// <param name="author">This instance's Author.</param>
        public FakeProject(int id, string name, IUser author) : base(id, name, author) { }
    }
}
