using System;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public class MemberDAL : WebApiDALBase<Member>, IMemberRepository
    {
        protected override string tableRoute => "Members";

        public MemberDAL(string connectionString) : base(connectionString) { }

        public Task<Member> ReadFromSnowflakeAsync(ulong snowflakeId)
        {
            throw new NotImplementedException();
        }
    }
}
