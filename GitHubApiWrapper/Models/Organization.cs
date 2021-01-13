using System;

namespace GitHubApiWrapper.Models
{
    public class Organization
    {
        public string Name { get; }
        
        private Uri _baseEndpoint
        {
            get => new Uri($"https://api.github.com/orgs/{Name}");
        }

        public Organization(string organizationName)
        {
            Name = organizationName;
        }

        public Uri GetEndpoint(string endpointName = "") => new Uri($"{_baseEndpoint}/{endpointName}");
    }
}
