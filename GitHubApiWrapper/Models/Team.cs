using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApiWrapper.Models
{
    public class Team
    {
        private Uri _organizationEndpoint;

        public string Slug { get; }

        public Uri BaseEndpoint
        {
            get => new Uri($"{_organizationEndpoint}/teams/{Slug}");
        }

        public Uri MembershipsEndpoint
        {
            get => new Uri($"{BaseEndpoint}/memberships");
        }

        public Team(Uri organizationEndpoint, string teamSlug)
        {
            _organizationEndpoint = organizationEndpoint;
            Slug = teamSlug;
        }

        public enum Role
        {
            member,
            maintainer
        }
    }
}
