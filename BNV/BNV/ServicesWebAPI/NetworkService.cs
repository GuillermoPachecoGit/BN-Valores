using System;
using BNV.ServicesWebAPI.Interfaces;
using BNV.Settings;
using Refit;

namespace BNV.ServicesWebAPI
{
    public static class NetworkService
    {
        public static IAPIService apiService;

        public static IAPIService GetApiService()
        {
            apiService = RestService.For<IAPIService>(Config.BaseUrl);
            return apiService;
        }
    }
}
