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

        /// <summary>
        /// Creates a new record of the input model.
        /// </summary>
        /// <param name="newModel">Model to add to DB.</param>
        /// <returns>True if successful.</returns>
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

        /// <summary>
        /// Get all records in the DB.
        /// </summary>
        /// <returns>All records in the DB.</returns>
        public async Task<IEnumerable<TDataModel>> ReadAllAsync()
        {
            var client = GetClient();

            var request = new RestRequest(_tableRoute);

            var result = await client.ExecuteGetAsync<IList<TDataModel>>(request);
            
            return result.IsSuccessful ? result.Data : null;
        }

        /// <summary>
        /// Gets the record for the input primary key.
        /// </summary>
        /// <param name="id">Primary key of record to retrieve.</param>
        /// <returns>Record data if retrieved, null if not.</returns>
        public async Task<TDataModel> ReadAsync(int id)
        {
            var client = GetClient();

            var request = new RestRequest($"{_tableRoute}/{id}");

            var result = await client.ExecuteGetAsync<TDataModel>(request);

            return result.IsSuccessful ? result.Data : null;
        }

        /// <summary>
        /// Deletes the record with the input primary key.
        /// </summary>
        /// <param name="id">Primary key of record to delete.</param>
        /// <returns>True if delete is successful.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var client = GetClient();

            var request = new RestRequest($"{_tableRoute}/{id}", Method.DELETE);

            var result = await client.ExecuteAsync<TDataModel>(request);
            
            return result.IsSuccessful;
        }

        /// <summary>
        /// Updates the selected record.
        /// </summary>
        /// <param name="modelToReplace">Record to update.</param>
        /// <returns>False if the record doesn't exist, or if the update failed.</returns>
        public async Task<bool> UpdateAsync(TDataModel modelToReplace)
        {
            var modelToUpdate = await ReadAsync(modelToReplace.ID);

            if (modelToUpdate == null)
                return false;

            var client = GetClient();

            var request = new RestRequest($"{_tableRoute}/{modelToReplace.ID}", Method.PUT);

            var jsonBody = SerializeModel(modelToReplace); // Had this set to the old model. Whoops!

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
