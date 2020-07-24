using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Interfaces
{
    public interface IValuesService
    {
        /// <summary>
        /// Get all values
        /// </summary>
        /// <returns>array of strings</returns>
        IEnumerable<string> Get();
 
        /// <summary>
        /// Get all values async
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetAsync();
 
        /// <summary>
        /// Get value by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        string Get(int id);
 
        /// <summary>
        /// Get value by id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetAsync(int id);
 
        /// <summary>
        /// Add new value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Location</returns>
        Uri Post(string value);
 
        /// <summary>
        /// Add new value async
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Location</returns>
        Task<Uri> PostAsync(string value);
 
        /// <summary>
        /// Update value 
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="value">Value</param>
        HttpStatusCode Put(int id, string value);
 
        /// <summary>
        /// Update value async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<HttpStatusCode> PutAsync(int id, string value);
 
        /// <summary>
        /// Delete value
        /// </summary>
        /// <param name="id">Id</param>
        HttpStatusCode Delete(int id);
 
        /// <summary>
        /// Delete value async
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<HttpStatusCode> DeleteAsync(int id);

    }
}
