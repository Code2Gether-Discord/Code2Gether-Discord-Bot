using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public abstract class WebApiDALBase<TDataModel> : IDataRepository<TDataModel>
        where TDataModel : class, IDataModel
    {
        protected string _connectionString;
        protected abstract string tableRoute { get; }

        protected WebApiDALBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> CreateAsync(TDataModel newModel)
        {
            var client = getClient();

            var request = new RestRequest(tableRoute);

            request.AddJsonBody(newModel);

            var result = await client.ExecutePostAsync<TDataModel>(request);

            // Log result information?

            return result.IsSuccessful;
        }

        public async Task<IEnumerable<TDataModel>> ReadAllAsync()
        {
            var client = getClient();

            var request = new RestRequest(tableRoute);

            var result = await client.ExecuteGetAsync<IList<TDataModel>>(request);

            // Log result information?

            return result.IsSuccessful ? result.Data : null;
        }

        public async Task<TDataModel> ReadAsync(int id)
        {
            var client = getClient();

            var request = new RestRequest($"{tableRoute}/{id}");

            var result = await client.ExecuteGetAsync<TDataModel>(request);

            // Log result information?

            return result.IsSuccessful ? result.Data : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = getClient();

            var request = new RestRequest($"{tableRoute}/{id}", Method.DELETE);

            var result = await client.ExecuteAsync<TDataModel>(request);

            // Log result information?

            return result.IsSuccessful;
        }

        public async Task<bool> UpdateAsync(TDataModel existingModel)
        {
            var modelToUpdate = await ReadAsync(existingModel.ID);

            if (modelToUpdate == null)
            {
                // Log error?
                return false;
            }

            existingModel.ID = existingModel.ID;

            var client = getClient();

            var request = new RestRequest($"{tableRoute}/{existingModel.ID}", Method.PUT);

            request.AddJsonBody(existingModel);

            var result = await client.ExecuteAsync<TDataModel>(request);

            // Log result information?

            return result.IsSuccessful;
        }

        protected RestClient getClient()
        {
            // Put any authentication protocols here?
            return new RestClient(_connectionString);
        }
    }
}
