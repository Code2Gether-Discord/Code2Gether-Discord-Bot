using System;
using System.Linq;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public class MemberDAL : WebApiDALBase<Member>, IMemberRepository
    {
        protected override string tableRoute => "Members";

        public MemberDAL(string connectionString) : base(connectionString) { }

        public async Task<Member> ReadFromSnowflakeAsync(ulong snowflakeId)
        {
            var users = await ReadAllAsync();
            var queriedUser = users.FirstOrDefault(u => u.SnowflakeId.Equals(snowflakeId));
            return queriedUser;
        }
    }
}
