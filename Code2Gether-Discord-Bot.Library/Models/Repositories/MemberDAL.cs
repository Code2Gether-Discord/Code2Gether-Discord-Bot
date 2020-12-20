using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

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
            var request = new RestRequest($"{_tableRoute}/snowflakeID={snowflakeId}");

            var result = await GetClient().ExecuteGetAsync<Member>(request);

            return result.IsSuccessful ? result.Data : null;
        }
    }
}
