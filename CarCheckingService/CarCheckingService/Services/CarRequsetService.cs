using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CarCheckingService.Models;

namespace CarCheckingService.Services
{
    public class CarRequsetService : Service
    {
        public CarRequsetService(IConfiguration conf) : base(conf.GetSection("Addresses")["CarRequestService"]) { }

        public async Task<AccuListWithTime> GetAccuState(int IdCar)
        {
            var httpResponseMessage = await Get($"carservices/cars/{IdCar}/accus/");
            if (httpResponseMessage == null || httpResponseMessage.Content == null)
                return null;

            string response = await httpResponseMessage.Content.ReadAsStringAsync();
            try
            {
                var a = JsonConvert.DeserializeObject<AccuListWithTime>(response);
                return a;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AccuListWithTime> GetAccuState(int IdCar, int IdAccu)
        {
            var httpResponseMessage = await Get($"carservices/cars/{IdCar}/accus/{IdAccu}/");
            if (httpResponseMessage == null || httpResponseMessage.Content == null)
                return null;

            string response = await httpResponseMessage.Content.ReadAsStringAsync();
            try
            {
                var a = JsonConvert.DeserializeObject<AccuListWithTime>(response);
                return a;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> PutAccuInit(int id)
        {
            var httpResponseMessage = await PutForm($"carservices/cars/{id}/init", new Dictionary<string, string>());
            if (httpResponseMessage == null || httpResponseMessage.Content == null)
                return -1;

            string response = await httpResponseMessage.Content.ReadAsStringAsync();

            return 0;
        }
    }
}
