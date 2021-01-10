using System;

namespace GitHubApiWrapper.Models
{
    public class Organization
    {
        public string Name { get; }
        
        public Uri OrganizationEndpoint
        {
            get => new Uri($"https://api.github.com/orgs/{Name}");
        }

        public Uri InvitationsEndpoint
        {
            get => new Uri($"{OrganizationEndpoint}/invitations");
        }

        public Organization(string organizationName)
        {
            Name = organizationName;
        }
    }
}
