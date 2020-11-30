using System;

namespace Code2Gether_Discord_Bot.Library.Interfaces
{
    public interface IProject
    {
        string Name { get; }
        long AuthorId { get; }
    }
}
