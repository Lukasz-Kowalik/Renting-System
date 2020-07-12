using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace RentingSystemAPI.Validators
{
    public class ResponseValidator
    {
        private static readonly HttpResponseMessage _responseMessage = new HttpResponseMessage();

        public static HttpResponseMessage CheckResponse(IdentityResult result)
        {
            if (result.Succeeded)
            {
                _responseMessage.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                SelectStatusCode(result);
            }

            return _responseMessage;
        }

        private static void SelectStatusCode(IdentityResult result)
        {
            var error = result.Errors.FirstOrDefault().Code;
            _responseMessage.StatusCode = error.Contains("Duplicate") ? HttpStatusCode.Conflict : HttpStatusCode.NotFound;
            _responseMessage.Content = new StringContent(error);
            Debug.WriteLine(_responseMessage.Content);
        }
    }
}