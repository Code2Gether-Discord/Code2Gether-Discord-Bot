using System;
using System.Collections.Generic;

namespace GitHubApiWrapper.Models
{
    public class Organization
    {
        public string Name { get; }
        
        public List<Team> Teams { get; } = new List<Team>();

        public Uri BaseEndpoint
        {
            get => new Uri($"https://api.github.com/orgs/{Name}");
        }

        public Uri InvitationsEndpoint
        {
            get => new Uri($"{BaseEndpoint}/invitations");
        }

        public Organization(string organizationName)
        {
            Name = organizationName;
        }
    }
}
