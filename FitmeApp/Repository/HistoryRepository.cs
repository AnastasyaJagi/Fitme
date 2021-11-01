using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FitmeApp.Models;
using FitmeApp.Models.SubModel.Request;
using FitmeApp.Services;
using FitmeApp.Settings;
using FitmeApp.Utilities.Helper;
using FitmeApp.Utilities.Models.Response;

namespace FitmeApp.Repository
{
    public class HistoryRepository
    {
        // this is singleton implementation
        // implement lazy loading to make this class is used on-demand ( when we need )
        private static readonly Lazy<HistoryRepository> lazy = new Lazy<HistoryRepository>(() => new HistoryRepository());
        public static HistoryRepository Instance { get { return lazy.Value; } }

        protected readonly string baseUrl = AppSettingsManager.Settings["BaseUrl"];
        protected readonly string historyUrl = AppSettingsManager.Settings["HistoryRecommendationUrl"];


        private HistoryRepository() { }

        public async Task<ResponseUser> AddHistory(HistoryRequest history)
        {
            try
            {
                var url = $"{baseUrl}{historyUrl}";
                if (await HttpServiceHelper.ProcessHttpRequestAsync<HistoryRequest, ResponseUser>(
                        HttpMethod.Post, $"{url}",
                        new HistoryRequest { caseSimilarity = history.caseSimilarity, k= history.k, userId= history.userId, exercise_type = history.exercise_type })
                    is BaseHttpResponse<ResponseUser> response)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Successfully Add History!");
                        return response.Result;
                    }
                    else
                    {
                        Console.WriteLine("Unsuccessfully Add History!");
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Api - an error has occured : " + e.Message);
                return null;
            }
        }

        public async Task<List<HistoryRecommendation>> GetHistory(String userId)
        {
            try
            {
                var httpRequest = await HttpService.GetAsync<List<HistoryRecommendation>>($"{baseUrl}{historyUrl}/user/{userId}"); //{AppSettingsManager.Settings["PertanyaanUrl"]}
                var msg = $"{(httpRequest.Successful ? "" : "Not ")} Found History User";
                List<HistoryRecommendation> historyData = httpRequest.Result;
                Console.WriteLine(msg);
                return historyData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Api - an error has occured : " + e.Message);
                return null;
            }
        }

    }
}
