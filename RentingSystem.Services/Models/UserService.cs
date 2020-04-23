using RentingSystem.Requests;
using RentingSystem.Services.Interfaces;
using RentingSystem.ViewModels.Models;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentingSystem.Services.Models
{
    public class UserService : IUserService
    {
        public async Task<HttpResponseMessage> RegisterAsync(UserVm userVm, HttpClient client)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            try
            {
                response = await client.SendPostAsync("CreateUser", userVm);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

            return response;
        }
    }
}