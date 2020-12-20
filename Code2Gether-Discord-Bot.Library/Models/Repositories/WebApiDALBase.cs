using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public abstract class WebApiDALBase<TDataModel> : IDataRepository<TDataModel>
        where TDataModel : class, IDataModel
    {
        protected string _connectionString;
        protected abstract string _tableRoute { get; }

        protected WebApiDALBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> CreateAsync(TDataModel newModel)
        {
            var client = GetClient();

            var request = new RestRequest(_tableRoute);

            var jsonBody = SerializeModel(newModel);

            request.AddJsonBody(jsonBody, "application/json");

            var result = await client.ExecutePostAsync<TDataModel>(request);
            
            return result.IsSuccessful;
        }

        protected virtual string SerializeModel(TDataModel modelToSerialize)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.SerializeObject(modelToSerialize, settings);
        }

        public async Task<IEnumerable<TDataModel>> ReadAllAsync()
        {
            var client = GetClient();

            var request = new RestRequest(_tableRoute);

            var result = await client.ExecuteGetAsync<IList<TDataModel>>(request);
            
            return result.IsSuccessful ? result.Data : null;
        }

        public async Task<TDataModel> ReadAsync(int id)
        {
            var client = GetClient();

            var request = new RestRequest($"{_tableRoute}/{id}");

            var result = await client.ExecuteGetAsync<TDataModel>(request);

            return result.IsSuccessful ? result.Data : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = GetClient();

            var request = new RestRequest($"{_tableRoute}/{id}", Method.DELETE);

            var result = await client.ExecuteAsync<TDataModel>(request);
            
            return result.IsSuccessful;
        }

        public async Task<bool> UpdateAsync(TDataModel existingModel)
        {
            var modelToUpdate = await ReadAsync(existingModel.ID);

            if (modelToUpdate == null)
            {
                return false;
            }

            var client = GetClient();

            var request = new RestRequest($"{_tableRoute}/{existingModel.ID}", Method.PUT);

            var jsonBody = SerializeModel(existingModel); // Had this set to the old model. Whoops!

            request.AddJsonBody(jsonBody);

            var result = await client.ExecuteAsync<TDataModel>(request);

            return result.IsSuccessful;
        }

        protected RestClient GetClient()
        {
            // Put any authentication protocols here?
            return new RestClient(_connectionString);
        }
    }
}
