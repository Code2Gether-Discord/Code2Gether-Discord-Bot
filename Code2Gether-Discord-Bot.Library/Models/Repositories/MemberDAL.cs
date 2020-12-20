using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public class MemberDAL : WebApiDALBase<Member>, IMemberRepository
    {
        protected override string _tableRoute => "Members";

        public MemberDAL(string connectionString) : base(connectionString) { }
        
        protected override string SerializeModel(Member memberToSerialize)
        {
            var newMember = new Member { SnowflakeId = memberToSerialize.SnowflakeId };

            var json = JsonConvert.SerializeObject(newMember);

            return json;
        }

        public async Task<Member> ReadFromSnowflakeAsync(ulong snowflakeId)
        {
            var users = await ReadAllAsync();
            var queriedUser = users.FirstOrDefault(u => u.SnowflakeId.Equals(snowflakeId));
            return queriedUser;
        }
    }
}
