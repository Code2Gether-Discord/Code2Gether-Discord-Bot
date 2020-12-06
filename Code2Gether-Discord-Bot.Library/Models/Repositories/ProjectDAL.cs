using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public class ProjectDAL : IProjectRepository
    {
        public string _connectionString;
        
        public ProjectDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<bool> CreateAsync(Project newProject)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Project>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Project> ReadAsync(string projectName)
        {
            throw new NotImplementedException();
        }

        public Task<Project> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Project newProject)
        {
            throw new NotImplementedException();
        }
    }
}
